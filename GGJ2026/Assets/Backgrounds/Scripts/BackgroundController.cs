using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float _startPos, _lenght;
    public GameObject cam;
    public float parallaxEffect;
    
    private void Start()
    {
        if (cam == null) 
        {
            cam = Camera.main?.gameObject;
        }
        _startPos = transform.position.x;
        _lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    
    private void FixedUpdate()
    {
        // 0 move with cam, 1 won't move, 0.5 half speed 
        var distance = cam.transform.position.x * parallaxEffect;
        var movement = cam.transform.position.x * (1 - parallaxEffect);
        
        transform.position = new Vector3(_startPos + distance, transform.position.y, transform.position.z);

        // if background has reached the end of its lenght adjust its position for infinite scrolling 
        if (movement > _startPos + _lenght)
        {
            _startPos += _lenght;
        }
        else if (movement < _startPos - _lenght)
        {
            _startPos -= _lenght;
        }
    }
}
