using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private const float sideOffset = 3f;
    private const float thresholdMax = 3;
    private const float thresholdMin = 0;
    
    [SerializeField] private Transform followTransform;

    private float _startDistance;
    
    private void Start()
    {
        _startDistance = transform.position.y - followTransform.position.y;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(followTransform.position.x + sideOffset, transform.position.y, transform.position.z);
        
        float fixedCamPos = transform.position.y - _startDistance;
        
        if (followTransform.position.y - fixedCamPos > thresholdMax)
            transform.position = new Vector3(transform.position.x, followTransform.position.y + _startDistance - thresholdMax, transform.position.z);
        
        if (followTransform.position.y - fixedCamPos < thresholdMin)
            transform.position = new Vector3(transform.position.x, followTransform.position.y + _startDistance + thresholdMin, transform.position.z);

    }

}
