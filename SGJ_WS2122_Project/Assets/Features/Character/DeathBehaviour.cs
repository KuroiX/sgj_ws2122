using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{
    private Vector3 _initialPosition;
    private CharacterController _controller;

    private void Awake()
    {
        _initialPosition = transform.position;
        _controller = GetComponent<CharacterController>();
    }
    
    private void OnTriggerEnter(Collider col)
    {
        transform.position = _initialPosition;
        _controller.ResetLayer();
    }
}
