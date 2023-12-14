using UnityEngine;

public class Cam : MonoBehaviour
{

    private Vector3 _startingPos;
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothing;

    // Start is called before the first frame update
    void Start()
    {
        _startingPos = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPos = new Vector3(_target.position.x, 0, -10);

        if (transform.position != _target.transform.position)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, _smoothing);
        }
    }
}
