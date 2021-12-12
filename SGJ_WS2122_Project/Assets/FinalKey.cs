using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalKey : MonoBehaviour
{
    [SerializeField] private GameObject gO;

    [SerializeField] private AudioClip clip;
    
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<PostProcess>().enabled = false;
        gO.SetActive(true);

        FindObjectOfType<AudioSource>().PlayOneShot(clip);
        
        RenderSettings.ambientLight = new Color(0.9649928f, 0.9649928f,0.9649928f, 1);
        
        Destroy(transform.parent.gameObject);
    }
}
