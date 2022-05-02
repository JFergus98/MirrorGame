using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private BoxCollider2D sceneTrigger;

    private bool isLoaded = false;
    [SerializeField] private byte sceneToLoadIndex;

    // Awake is called when the script instance is being loaded. 
    private void Awake()
    {
        sceneTrigger = GetComponent<BoxCollider2D>();

        // if scene is loaded update loaded variable
        if(!isLoaded && SceneManager.GetSceneByBuildIndex(sceneToLoadIndex).isLoaded) {
            isLoaded = true;
        }
    }

    // OnTriggerEnter2D is called when Player enters this objects 2D collider
    private void OnTriggerEnter2D()
    { 
        // Debug.Log("Player entered trigger");  // testing

        // is scene not loaded, then load scene
        if (!isLoaded) {
            isLoaded = true;
            SceneManager.LoadSceneAsync(sceneToLoadIndex, LoadSceneMode.Additive);

            // Debug.Log("scene " + sceneToLoadIndex + " loaded");  // testing
        }   
    }

    // OnTriggerEnter2D is called when Player exits this objects 2D collider
    private void OnTriggerExit2D() 
    {
        // Debug.Log("Player exited trigger"); // testing

        // is scene loaded, then unload scene
        if (isLoaded) {
            isLoaded = false;
            SceneManager.UnloadSceneAsync(sceneToLoadIndex);

            // Debug.Log("scene " + sceneToLoadIndex + " unloaded");  // testing
        }
    }
}
