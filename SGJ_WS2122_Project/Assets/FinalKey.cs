using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class FinalKey : MonoBehaviour
{
    [SerializeField] private GameObject gO;

    [SerializeField] private AudioClip clip;
    [SerializeField] private BackgroundSound backgroundSound;
    
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    
        FindObjectOfType<PostProcess>().enabled = false;
        gO.SetActive(true);

        FindObjectOfType<AudioSource>().PlayOneShot(clip);
        
        RenderSettings.ambientLight = new Color(0.9649928f, 0.9649928f,0.9649928f, 1);
        
        backgroundSound.EndMusic();
        
        Destroy(transform.parent.gameObject);
    }
}
