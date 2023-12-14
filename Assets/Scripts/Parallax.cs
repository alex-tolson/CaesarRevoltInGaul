using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _length, _startingPos;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float _temp;
    private float _dist;

    void Start()
    {
        _startingPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x; 
    }

    void FixedUpdate()
    {
        _temp = (cam.transform.position.x * (1 - parallaxEffect));
        _dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(_startingPos + _dist, transform.position.y, transform.position.z);

        if (_temp > _startingPos + _length)
        {
            _startingPos += _length;
        }
        else if (_temp < _startingPos - _length)
        {
            _startingPos -= _length;
        }       
    }
}
