using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    
    [SerializeField] private CameraFollow cameraScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Villa"))
        {
            StartCoroutine(cameraScript.CenterCamera());
        }
        else if (other.CompareTag("NoVilla"))
        {
            StartCoroutine(cameraScript.UncenterCamera());
        }
    }

}
