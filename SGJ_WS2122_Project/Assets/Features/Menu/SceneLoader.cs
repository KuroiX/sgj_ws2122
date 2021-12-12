using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        int newIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (newIndex >= SceneManager.sceneCount)
        {
            Application.Quit();
            Debug.Log("Quit");
        }
        else
        {
            SceneManager.LoadScene(newIndex);
        }
        
    }
}
