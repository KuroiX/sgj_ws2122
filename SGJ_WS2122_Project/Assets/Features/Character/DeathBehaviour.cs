using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{
    private Vector3 _initialPosition;
    private CharacterController _controller;

    [SerializeField] private LayerMask deathMask;

    private void Awake()
    {
        _initialPosition = transform.position;
        _controller = GetComponent<CharacterController>();
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (1 << col.gameObject.layer != deathMask) return;
        
        transform.position = _initialPosition;
        _controller.ResetLayer();
    }
}
