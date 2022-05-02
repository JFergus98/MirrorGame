using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoubleJump : MonoBehaviour
{
    private PlayerAbilities playerAbilities;
    private GameObject player;

    [SerializeField] private GameObject doubleJumpAbility;

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // get the Player's PlayerAbilitie component
        player = GameObject.Find("Player");
        playerAbilities = player.GetComponent<PlayerAbilities>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // if player has alreafy unlocked double jump, despawn ability
        if (playerAbilities.GetDoubleJumpUnlocked()) {
            doubleJumpAbility.SetActive(false);

            // Debug.Log("double jump pickup disabled"); // testing
        }
    }

    // OnTriggerEnter2D is called when player enter objects 2D collider
    private void OnTriggerEnter2D()
    {
        // Debug.Log("Player unlocks double jump");  // testing

        // unlock double jump ability
        playerAbilities.unlockDoubleJump();
        doubleJumpAbility.SetActive(false);
    }
}
