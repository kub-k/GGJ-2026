using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    
    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    
    BoxCollider2D _myFeetCollider;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _myFeetCollider = GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
       _moveInput = value.Get<Vector2>(); 
    }
    
    void OnJump(InputValue value)
    {
        if (!_myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return;}
        
        if(value.isPressed)
        {
            _rb.linearVelocity += new Vector2 (0f, jumpSpeed);
        }
    }


    void Run()
    {
        Vector2 playerVelocity = new Vector2 (_moveInput.x * runSpeed, _rb.linearVelocity.y);
        _rb.linearVelocity = playerVelocity;
        //bool playerHasHorizontalSpeed = Mathf.Abs(_rb.linearVelocity.x) > Mathf.Epsilon;
        //for animation 

    }
    
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(_rb.linearVelocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(_rb.linearVelocity.x), 1f);
        }
    }

}
