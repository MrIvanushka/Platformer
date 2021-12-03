using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _minGroundNormalY = .65f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _speedModifier;
    [SerializeField] private float _jumpForce;

    private Vector2 _velocity;
    private Vector2 _targetVelocity;
    private bool _isGrounded;
    private Vector2 _groundNormal;
    private Rigidbody2D _rigidbody;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    private const float _minMoveDistance = 0.001f;
    private const float _shellRadius = 0.01f;

    public Vector2 Velocity => _velocity;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_groundLayer);
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        if (_isGrounded == true)
        {
            _targetVelocity = new Vector2(Input.GetAxis("Horizontal") * _speedModifier, 0);

            if (Input.GetKey(KeyCode.Space))
                _velocity.y = _jumpForce;
        }
    }

    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        
        if(_isGrounded == true)
            _velocity.x = _targetVelocity.x;

        _isGrounded = false;

        Vector2 deltaPosition = Velocity * Time.deltaTime;
        Vector2 motionAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 motion = motionAlongGround * deltaPosition.x;

        Move(motion, false);

        motion = Vector2.up * deltaPosition.y;

        Move(motion, true);
    }

    public void TakePunch(float xDirection, float punchForce)
    {
        _velocity += new Vector2(xDirection, Mathf.Abs(xDirection)) * punchForce;
        _isGrounded = false;
    }

    private void Move(Vector2 motion, bool yMovement)
    {
        float distance = motion.magnitude;

        if (distance > _minMoveDistance)
        {
            int count = _rigidbody.Cast(motion, _contactFilter, _hitBuffer, distance + _shellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;
                if (currentNormal.y > _minGroundNormalY)
                {
                    _isGrounded = true;
                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(Velocity, currentNormal);
                if (projection < 0)
                {
                    _velocity = Velocity - projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - _shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rigidbody.position = _rigidbody.position + motion.normalized * distance;
    }
}
