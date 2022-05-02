using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    private BoxCollider2D playerCollider;
    [SerializeField]private LayerMask groundLayerMask;
    private bool grounded;
    
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // get the Player's collider component
        playerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Debug.Log(grounded);  // testing

        // set grounded variable
        grounded = IsGrounded();
    }

    // returns true if the player is touching the ground
    private bool IsGrounded()
    {
        // Debug.Log(playerCollider.bounds.center + ", " + playerCollider.bounds.size); // testing
        // RaycastHit2D checkGround = Physics2D.CapsuleCast(playerCollider.bounds.center, playerCollider.bounds.size, CapsuleDirection2D.Horizontal, 0f, Vector2.down, 0.1f, groundLayerMask); // old code removed as was sometimes inaccurate

        RaycastHit2D checkGround = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0f, groundLayerMask);
        return checkGround.collider != null;
    }

    // returns the value of the bool grounded
    public bool GetGrounded()
    {
        return grounded;
    }
}
