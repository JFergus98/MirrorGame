using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    private bool GameIsPaused = false;

    private GameObject player;
    private PlayerMirrored playerMirrored;
    private PlayerAbilities playerAbilities;
    private Transform playerTransform;

    private void Awake()
    {
        // get the Player game object and its playerMirrored and playerAbilities components
        player = GameObject.Find("Player");
        playerMirrored = player.GetComponent<PlayerMirrored>();
        playerAbilities = player.GetComponent<PlayerAbilities>();

        // cache game objects tranforms
        playerTransform = player.transform;

        mainMenu = GameObject.Find("MainMenu");
        optionsMenu = GameObject.Find("OptionsMenu");
    }

    // Update is called once per frame
    private void Update()
    {   
        if (Input.GetButtonDown("Pause") && !mainMenu.activeInHierarchy && !optionsMenu.activeInHierarchy) {
            if (GameIsPaused)
            {
                ResumeGame();
            }else{
                PauseGame();
            }
        } 
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }
    
    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void SaveGame()
    {
        SaveSystem.Save(playerAbilities, playerMirrored);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        GameIsPaused = false;
        SceneManager.LoadScene(0,  LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");  // testing

        Application.Quit();
    }
}
