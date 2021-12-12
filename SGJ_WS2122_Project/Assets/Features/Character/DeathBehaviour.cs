using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{
    private Vector3 _initialPosition;
    private CharacterController _controller;

    [SerializeField] private LayerMask deathMask;
    
    [SerializeField] private AudioClip clip;

    private void Awake()
    {
        _initialPosition = transform.position;
        _controller = GetComponent<CharacterController>();
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (1 << col.gameObject.layer != deathMask) return;

        FindObjectOfType<AudioSource>().PlayOneShot(clip);
        
        transform.position = _initialPosition;
        _controller.ResetLayer();
        StartCoroutine(DisableInputForASec());
    }


    public void SetPoint(Vector3 position)
    {
        _initialPosition = position;
    }

    private IEnumerator DisableInputForASec()
    {
        CharacterController.Input.Character.Disable();
        
        yield return new WaitForSeconds(.5f);
        
        CharacterController.Input.Character.Enable();
    }
}
