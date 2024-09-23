using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    InputActionReference movement;
    [SerializeField]
    private Rigidbody2D body;
    [SerializeField]
    private float moveSpeed, jumpHeight;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [Range(0f, 1f)]
    public float groundDecay;

    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    private float newSpeed;
    Vector2 movementInput;
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {
        ApplyFriction();
        Gravity();
    }

    private void Update()
    {

        body.velocity = new Vector2(movementInput.x * moveSpeed, body.velocity.y);
    }

    private void Gravity()
    {
        if(body.velocity.y < 0)
        {
            body.gravityScale = baseGravity * fallSpeedMultiplier;
            body.velocity = new Vector2(body.velocity.x, Mathf.Max(body.velocity.y, -maxFallSpeed));
        }
        else
        {
            body.gravityScale = baseGravity;
        }
    }
    private bool isGrounded()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, ground))
        {
            return true;
        }
        return false;
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(isGrounded())
        {
            if(context.performed)
            {
                body.velocity = new Vector2(body.velocity.x, jumpHeight);
            }
            else if( context.canceled)
            {
                body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
            }
        }
        
    }

    public void ApplyFriction() 
    { 
        if(isGrounded() && movementInput.x == 0 && movementInput.y == 0)
        {
            body.velocity *= groundDecay;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
