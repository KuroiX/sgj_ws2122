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
        //Debug.Log(newIndex);
        //Debug.Log(SceneManager.sceneCount);

        if (newIndex >= SceneManager.sceneCountInBuildSettings)
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
