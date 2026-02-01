using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;

    private Vector3 _nextPos;
    private Vector3 _lastPosition;
    private Rigidbody2D _playerRb;
    void Start()
    {
        _nextPos = pointB.position;
        _lastPosition = transform.position;
    }
    
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _nextPos,
            moveSpeed * Time.fixedDeltaTime);


        Vector3 deltaMovement = transform.position - _lastPosition;
        _lastPosition = transform.position;
        
        if (_playerRb != null)
        {
            _playerRb.position += (Vector2)deltaMovement;
        }
        
        if (Vector3.Distance(transform.position, _nextPos) < 0.01f)
        {
            _nextPos = (_nextPos == pointA.position) ? pointB.position : pointA.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _playerRb == null)
        {
            _playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerRb = null;
        }
    }
}
