using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDash : MonoBehaviour
{
    private PlayerAbilities playerAbilities;
    private GameObject player;

    [SerializeField] private GameObject dashAbility;

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
        // if player has alreafy unlocked dash, despawn ability
        if (playerAbilities.GetDashUnlocked()) {
            dashAbility.SetActive(false);

            // Debug.Log("dash pickup disabled"); // testing
        }
    }

    // OnTriggerEnter2D is called when player enter objects 2D collider
    private void OnTriggerEnter2D()
    {
        // Debug.Log("Player unlocks dash");  // testing

        // unlcok dash ability
        playerAbilities.unlockDash();
        dashAbility.SetActive(false);
    }
}
