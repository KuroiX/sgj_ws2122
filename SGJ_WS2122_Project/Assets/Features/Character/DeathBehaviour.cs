using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{
    private Vector3 _initialPosition;
    private CharacterController _controller;

    [SerializeField] private LayerMask deathMask;
    
    [SerializeField] private AudioClip clip;

    [SerializeField] private Transform cameraPos;
    //private float _distance;
    private float _initialCamHeight;

    private void Awake()
    {
        _initialPosition = transform.position;
        _controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _initialCamHeight = cameraPos.position.y;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (1 << col.gameObject.layer != deathMask) return;

        cameraPos.position = new Vector3(cameraPos.position.x, _initialCamHeight, cameraPos.position.z);

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
