using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followTransform;
    
    private void LateUpdate()
    {
        transform.position = new Vector3(followTransform.position.x + 2f, transform.position.y, transform.position.z);
    }
}
