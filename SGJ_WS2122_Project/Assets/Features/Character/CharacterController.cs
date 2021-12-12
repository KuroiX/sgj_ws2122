using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CharacterController : MonoBehaviour
{
    private static CharacterInput _input;
    public static CharacterInput Input => _input;

    private Rigidbody _rb;
    private Collider _collider;

    [SerializeField] private float gravityScale = 10;
    [SerializeField] private float maxFallSpeed = 20;

    [SerializeField]
    private float moveSpeed;
    private float _move;

    [SerializeField] private float[] layerValues;
    [SerializeField] private int initialLayer;
    private int _currentLayerIndex;

    [SerializeField] private float jumpHeight;

    [SerializeField] private LayerMask groundedMask;

    private PostProcess _postProcess;

    [SerializeField] private AudioClip clip;
    private AudioSource _source;
    
    private void Awake()
    {
        _input = new CharacterInput();
        _input.Character.Enable();
        _input.UITutorial.Enable();

        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _currentLayerIndex = initialLayer;

        _postProcess = FindObjectOfType<PostProcess>();
        _source = FindObjectOfType<AudioSource>();
    }

    private void OnEnable()
    {
        _input.Character.Move.performed += Move;
        _input.Character.Move.canceled += Move;

        _input.Character.LayerSwitch.performed += SwitchLayer;

        _input.Character.Jump.performed += Jump;

        _input.Character.MirrorSwitch.performed += SwitchMirror;
    }

    private void OnDisable()
    {
        _input.Character.Move.performed -= Move;
        _input.Character.Move.canceled -= Move;
        
        _input.Character.LayerSwitch.performed -= SwitchLayer;
        
        _input.Character.Jump.performed -= Jump;
        
        _input.Character.MirrorSwitch.performed -= SwitchMirror;
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        _move = moveSpeed * ctx.ReadValue<float>();
    }
    
    private void SwitchLayer(InputAction.CallbackContext ctx)
    {
        int temp = _currentLayerIndex + (int) ctx.ReadValue<float>();
        
        if (temp < 0 || temp > layerValues.Length - 1) return;

        Collider[] colliders = Physics.OverlapBox(new Vector3(_collider.bounds.center.x, _collider.bounds.center.y,layerValues[temp]), 
            _collider.bounds.extents * 0.95f, 
            Quaternion.identity, 
            groundedMask);

        bool isCollidingWithWall = false;

        foreach (var col in colliders)
        {
            if (!col.isTrigger)
            {
                isCollidingWithWall = true;
                break;
            }
        }
        
        if (isCollidingWithWall) return;

        _source.PlayOneShot(clip);
        
        _currentLayerIndex = temp;
        
        transform.position = new Vector3(transform.position.x, transform.position.y, layerValues[_currentLayerIndex]);
    }

    private void FixedUpdate()
    {
        if (!(_rb.velocity.y < -maxFallSpeed))
        {
            _rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
        }

        _rb.velocity = new Vector3(_move, _rb.velocity.y, _rb.velocity.z);
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (IsGrounded())
        {
            _rb.velocity = new Vector3(_rb.velocity.x, jumpHeight, _rb.velocity.z);
        }
    }
    
    private void SwitchMirror(InputAction.CallbackContext ctx)
    {
        _postProcess.SwitchTexture();
    }

    private bool IsGrounded()
    {
        Collider[] colliders = Physics.OverlapBox(
            _collider.bounds.center + Vector3.down * _collider.bounds.extents.y, 
            new Vector3(_collider.bounds.extents.x, 0.2f, _collider.bounds.extents.z),
            Quaternion.identity,
            groundedMask);
        
        return colliders.Length != 0;
    }

    public void ResetLayer()
    {
        _currentLayerIndex = initialLayer;
    }
}
