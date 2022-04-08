using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float distGroundToJump = .01f;

    private Rigidbody2D _rigidbodyRef;
    private Collider2D _collider2DRef;
    private Animator _animatorRef;
    private SpriteRenderer _spriteRendererRef;

    private int _jumpCount = 0;

    void Start()
    {
        _rigidbodyRef = GetComponent<Rigidbody2D>();
        _collider2DRef = GetComponent<Collider2D>();
        _animatorRef = GetComponent<Animator>();
        _spriteRendererRef = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _rigidbodyRef.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), _rigidbodyRef.velocity.y);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var canJump = false;
            if (IsPlayerGrounded())
            {
                _jumpCount = 1;
                canJump = true;
            }
            else if (_jumpCount == 1)
            {
                _jumpCount++;
                canJump = true;
            }
            
            if (canJump)
            {
                _animatorRef.SetBool("IsGrounded", false);
                _rigidbodyRef.velocity = new Vector2(_rigidbodyRef.velocity.x, jumpForce);
            }
        }

        _animatorRef.SetBool("IsGrounded", IsPlayerGrounded());
        _animatorRef.SetFloat("MoveSpeed", Math.Abs(_rigidbodyRef.velocity.x));

        if (_rigidbodyRef.velocity.x < 0)
        {
            _spriteRendererRef.flipX = true;
        }else if (_rigidbodyRef.velocity.x > 0)
        {
            _spriteRendererRef.flipX = false;
        }
    }

    private bool IsPlayerGrounded()
    {
        var bounds = _collider2DRef.bounds;

        var dist = Physics2D.BoxCast(transform.position, bounds.size, 0f, Vector2.down,
            bounds.extents.y + distGroundToJump, LayerMask.GetMask("Ground"));

        return dist;
    }
}