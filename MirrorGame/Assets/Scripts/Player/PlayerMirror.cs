using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMirror : MonoBehaviour
{
    private PlayerMirrored playerMirrored;
    private MirrorCollision mirrorCollision;
    private Transform playerTransform;
    [SerializeField] private CinemachineVirtualCamera defaultCamera;
    [SerializeField] private CinemachineVirtualCamera mirroredCamera;
    private const float positionShift = 16f;
    private bool mirrored;

    private bool triggerPressed = false;

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // get the Player's PlayerMirrored and the Mirror's MirrorCollision components
        playerMirrored = GetComponent<PlayerMirrored>();
        mirrorCollision = GameObject.Find("Mirror").GetComponent<MirrorCollision>();

        // cache player transform
        playerTransform = transform;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // get mirrored
        mirrored = playerMirrored.GetMirrored();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(mirrored);  // testing
        // Debug.Log(triggerPressed);  // testing

        // if trigger is not pressed, set triggerPressed to false
        if (Input.GetAxisRaw("Mirror") == 0 && triggerPressed)
        {
            triggerPressed = false;
        }

        // if Mirror button is pressed
        if ((Input.GetButtonDown("Mirror") || (Input.GetAxisRaw("Mirror") > 0 && !triggerPressed)) && mirrorCollision.GetCanMirror()) {
            playerMirrored.MirrorPlayer();
            mirrored = playerMirrored.GetMirrored();
            MovePlayer();
            ChangeCamera();
            triggerPressed = true;
        }
    }

    // Moves the player to and from the Default and Mirrored position
    public void MovePlayer()
    {
        // if mirrored move to mirrored postion and flip player, else move to default postition and flip player
        if (playerMirrored.GetMirrored()) {
            playerTransform.Translate(Vector3.right * positionShift);
            flipPlayer();
        }else{
            playerTransform.Translate(Vector3.left * positionShift);
            flipPlayer();
        }
    }

    // Swaps between the Default and Mirrored Camera
    public void ChangeCamera() {
        // if mirrored change to MirroredCamera, else change to DefaultCamera
        if (playerMirrored.GetMirrored()) {
            defaultCamera.Priority = 0;
            mirroredCamera.Priority = 1;
        }else{
            defaultCamera.Priority = 1;
            mirroredCamera.Priority = 0;
        }
    }

    // flips the direction the player is facing
    private void flipPlayer(){
        playerTransform.localScale = new Vector3(playerTransform.localScale.x*-1,1,1);
    }
}
