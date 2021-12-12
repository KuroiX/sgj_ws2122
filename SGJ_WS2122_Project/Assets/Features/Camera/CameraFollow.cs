using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private const float maxOffset = 3f;
    private const float thresholdMax = 3;
    private const float thresholdMin = 0;
    
    [SerializeField] private Transform followTransform;

    private float _currentOffset;
    private float _startDistance;
    
    private void Start()
    {
        _currentOffset = maxOffset;
        _startDistance = transform.position.y - followTransform.position.y;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(followTransform.position.x + _currentOffset, transform.position.y, transform.position.z);
        
        float fixedCamPos = transform.position.y - _startDistance;
        
        if (followTransform.position.y - fixedCamPos > thresholdMax)
            transform.position = new Vector3(transform.position.x, followTransform.position.y + _startDistance - thresholdMax, transform.position.z);
        
        if (followTransform.position.y - fixedCamPos < thresholdMin)
            transform.position = new Vector3(transform.position.x, followTransform.position.y + _startDistance + thresholdMin, transform.position.z);

        if (transform.position.y < -0.7366674f)
            transform.position = new Vector3(transform.position.x, -0.7366674f, transform.position.z);
    }

    public IEnumerator CenterCamera()
    {
        float step = 0.1f;
        while (_currentOffset > 0)
        {
            _currentOffset -= step;
            yield return new WaitForSeconds(0.01f);
        }
        _currentOffset = 0f;
    }
    
    public IEnumerator UncenterCamera()
    {
        float step = 0.1f;
        while (_currentOffset < maxOffset)
        {
            _currentOffset += step;
            yield return new WaitForSeconds(0.01f);
        }
        _currentOffset = 3f;
    }

}
