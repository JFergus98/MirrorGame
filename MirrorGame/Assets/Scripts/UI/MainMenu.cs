using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private GameObject player;
    private PlayerAbilities playerAbilities;
    private PlayerMirrored playerMirrored;

    // private PlayerMirror playerMirror;

    private Transform playerTransfrom;

    private Button loadGameButton;

    public GameObject optionsMenu;

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerAbilities = player.GetComponent<PlayerAbilities>();
        playerMirrored = player.GetComponent<PlayerMirrored>();

        // playerMirror = player.GetComponent<PlayerMirror>();

        playerTransfrom = player.transform;

        loadGameButton = GameObject.Find("LoadGameButton").GetComponent<Button>();
        optionsMenu = GameObject.Find("OptionsMenu");
    }

    private void Start()
    {
        optionsMenu.SetActive(false);
        Time.timeScale = 0;
        if (SaveSystem.Load() == null) {
            loadGameButton.interactable = false;
        }
    }

    public void NewGame()
    {
        // Debug.Log(SceneManager.GetActiveScene().buildIndex); // testing

        // load scene manager scene
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        
        Time.timeScale = 1;
    }

    public void LoadGame()
    {
        Debug.Log("menu test");

        // load player data
        PlayerData data = SaveSystem.Load();

        //Debug.Log(data.position[0] + ", " + data.position[1] + ", " + data.position[2]); // testing

        // if data is not null, else data is null
        if (data != null) {
            // if saved game data's dash is unlocked then unlock dash
            if (data.dashUnlocked) {
                playerAbilities.unlockDash();
                
                // Debug.Log("dash unlocked on load"); // testing
            }

            // if saved game data's double jump is unlocked then unlock double jump
            if (data.doubleJumpUnlocked) {
                playerAbilities.unlockDoubleJump();
                
                // Debug.Log("double jump unlocked on load"); // testing
            }

            // if (data.mirrored != playerMirrored.GetMirrored()){ // currently not working as intended
            //     playerMirrored.MirrorPlayer();
            //     playerMirror.MovePlayer();
            //     playerMirror.ChangeCamera();
            // }

            playerTransfrom.position = new Vector3(data.position[0], data.position[1], data.position[2]);
            
            // load scene manager scene
            SceneManager.LoadScene(1, LoadSceneMode.Additive);

            // unfreeze game state
            Time.timeScale = 1;

        }else{
            Debug.Log("No Save Game found");
        }
    }

    public void Quit()
    {
        Debug.Log("Quit");  // testing

        Application.Quit();
    }
}
