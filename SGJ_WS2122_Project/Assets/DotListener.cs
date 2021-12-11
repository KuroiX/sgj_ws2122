using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotListener : MonoBehaviour
{
    [SerializeField] private GameObject dotObject;

    private bool _isActive = false;
    
    private void Start()
    {
        CharacterController.Input.UITutorial.Enter.performed += ctx => dotObject.SetActive(_isActive = !_isActive);
    }
}
