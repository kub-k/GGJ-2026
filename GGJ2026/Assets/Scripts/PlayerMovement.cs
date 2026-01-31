using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private Vector2 deathKick = new Vector2(10f, 10f);
    
    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    
    private CapsuleCollider2D CurrentFeetCollider => GetActiveFeetCollider();
    private BoxCollider2D CurrentBodyCollider => GetActiveBodyCollider();
    Animator _animator;

    private bool _isAlive;
    
    void Start()
    {
        _isAlive = true;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (!_isAlive) return;
        Run();
        FlipSprite();
        Die();
    }
    
    
    //these methods find the current colliders (hell or heaven, depending on the world)
    private BoxCollider2D GetActiveBodyCollider()
    {
        // Alt objelerdeki tüm BoxCollider2D'leri tara
        BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>();
        foreach (var col in colliders)
        {
            // Hangi obje aktifse (DimensionManager tarafından açılmışsa) onu döndür
            if (col.gameObject.activeInHierarchy) return col;
        }
        return null;
    }
    
    private CapsuleCollider2D GetActiveFeetCollider()
    {
        // Alt objelerdeki tüm BoxCollider2D'leri tara
        CapsuleCollider2D[] colliders = GetComponentsInChildren<CapsuleCollider2D>();
        foreach (var col in colliders)
        {
            // Hangi obje aktifse (DimensionManager tarafından açılmışsa) onu döndür
            if (col.gameObject.activeInHierarchy) return col;
        }
        return null;
    }

    void OnMove(InputValue value)
    {
        if (!_isAlive) return;
       _moveInput = value.Get<Vector2>(); 
    }
    
    void OnJump(InputValue value)
    {
        if (!CurrentFeetCollider.IsTouchingLayers(LayerMask.GetMask("HellGround", "HeavenGround")) 
            || !value.isPressed || !_isAlive) return; 
        _rb.linearVelocity += new Vector2 (0f, jumpSpeed);
    }


    void Run()
    {
        Vector2 playerVelocity = new Vector2 (_moveInput.x * runSpeed, _rb.linearVelocity.y);
        _rb.linearVelocity = playerVelocity;
        
        bool playerHasHorizontalSpeed = Mathf.Abs(_rb.linearVelocity.x) > Mathf.Epsilon;
        _animator.SetBool("isRunning", playerHasHorizontalSpeed);

    }
    
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(_rb.linearVelocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(_rb.linearVelocity.x), 1f);
        }
    }
    
    void Die()
    {
        if (!CurrentFeetCollider.IsTouchingLayers(LayerMask.GetMask("HellObstacles")) &&
            (!CurrentBodyCollider.IsTouchingLayers(LayerMask.GetMask("HellObstacles")))) return;
        _isAlive = false;
        _rb.linearVelocity = new Vector2 (0, 0);
        _rb.linearVelocity = deathKick;
        StartCoroutine(WaitForDeath());
        Debug.Log("you died");
    }
    
    IEnumerator WaitForDeath()
    {
        //we need to wait a little bit before death, so our animation can play
        yield return new WaitForSeconds(2f);
        FindFirstObjectByType<GameSession>().ProcessPlayerDeath();
    }

}
