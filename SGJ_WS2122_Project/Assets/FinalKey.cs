using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalKey : MonoBehaviour
{
    [SerializeField] private GameObject gO;
    
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<PostProcess>().enabled = false;
        gO.SetActive(true);
        Destroy(gameObject);
    }
}
