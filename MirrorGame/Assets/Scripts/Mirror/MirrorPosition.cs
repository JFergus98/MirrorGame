using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPosition : MonoBehaviour
{
    private GameObject player;
    private PlayerMirrored playerMirrored;
    private Transform mirrorTransform;
    private Transform playerTransform;
    private bool mirrored;
    private const float offset = 16f;

    // Awake is called when the script instance is being loaded. 
    private void Awake()
    {
        // get the Player game object and its playerMirrored component
        player = GameObject.Find("Player");
        playerMirrored = player.GetComponent<PlayerMirrored>();

        // cache game objects tranforms
        mirrorTransform = transform;
        playerTransform = player.transform;
    }

    // Start is called before the first frame update
    private void Start()
    {
        UpdatePosition(offset);
        mirrored = playerMirrored.GetMirrored();
    }

    // Update is called once per frame
    private void Update()
    {
        if (mirrored != playerMirrored.GetMirrored()) {
            mirrored = !mirrored;
        }
        if (!mirrored){
            UpdatePosition(offset);
        }else{
            UpdatePosition(-offset);
        }
    }

    // updates the mirrors x position with the passed in offset value
    private void UpdatePosition(float offset){
        Vector3 position = playerTransform.position;
        mirrorTransform.position = new Vector3(position.x + offset, position.y - 0.35f, position.z);
    }
}
