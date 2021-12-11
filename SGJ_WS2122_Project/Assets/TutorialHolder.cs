using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialHolder : MonoBehaviour
{
    [SerializeField] private GameObject exclamationMark;

    [SerializeField] private GameObject tutorialText;


    private void OnTriggerEnter(Collider other)
    {
        tutorialText.SetActive(true);
        //CharacterController.Input.UITutorial.Enter.performed += OnEnter;
    }

    private void OnTriggerExit(Collider other)
    {
        tutorialText.SetActive(false);
        //CharacterController.Input.UITutorial.Enter.performed -= OnEnter;
    }

    private void OnEnter(InputAction.CallbackContext ctx)
    {
        tutorialText.SetActive(true);
        CharacterController.Input.Character.Disable();
        
        CharacterController.Input.UITutorial.Exit.performed += OnExit;
        CharacterController.Input.UITutorial.Enter.performed -= OnEnter;
    }

    private void OnExit(InputAction.CallbackContext ctx)
    {
        tutorialText.SetActive(false);
        CharacterController.Input.Character.Enable();
        
        CharacterController.Input.UITutorial.Exit.performed -= OnExit;
        CharacterController.Input.UITutorial.Enter.performed += OnEnter;
    }
}
