using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private PlayerMirrored playerMirrored;
    private PlayerDash playerDash;
    private PlayerAbilities playerAbilities;
    private Transform playerTransform;

    private bool mirrored;
    private const float speed = 5.6f;
    private float direction;
    private bool facingRight = true;
    private bool isDashing = false;
    private bool dashUnlocked;


    // Awake is called when the script instance is being loaded. 
    private void Awake()
    {
        // get the Player's rigidbody, PlayerMirrored, PlayerDash and PlayerAbilities components
        body = GetComponent<Rigidbody2D>();
        playerMirrored = GetComponent<PlayerMirrored>();
        playerDash = GetComponent<PlayerDash>();
        playerAbilities = GetComponent<PlayerAbilities>();

        // cache player transform
        playerTransform = transform;
    }

    // Start is called before the first frame update
    private void Start()
    {
        mirrored = playerMirrored.GetMirrored();
        isDashing = playerDash.GetDashing();
        dashUnlocked = playerAbilities.GetDashUnlocked();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(body.velocity.x);  // testing
        // Debug.Log(direction);  // testing
        // Debug.Log(facingRight);  // testing

        // mirrored changed, then update value
        if (mirrored != playerMirrored.GetMirrored()) {
            mirrored = !mirrored;
        }

        // isDashing changed, then update value
        if (isDashing != playerDash.GetDashing()) {
            isDashing = !isDashing;
        }

        // if dash unlocked, then player dash, else check if unlocked
        if (dashUnlocked) {
            playerDash.Dash();
        }else{
            dashUnlocked = playerAbilities.GetDashUnlocked();
        }
    }

    // FixedUpdate is called once per 0.02 seconds
    private void FixedUpdate()
    {
        // if movement button is pressed, then get direction, inverse if mirrored
        direction = mirrored ? -Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("Horizontal");

        // if not dashing, then move player postition
        if (!isDashing) {
            body.velocity = new Vector2(direction * speed, body.velocity.y);
        }

        // if moving right, facing left, not mirrored, then flip player
        if (direction > 0 && !facingRight && !mirrored) {
            flipPlayer();      
        }
        // if moving left, facing right, not mirrored, then flip player
        if (direction < 0 && facingRight && !mirrored) {
            flipPlayer();
        }
        // if moving right, facing left, mirrored, then flip player
        if (direction < 0 && !facingRight && mirrored) {
            flipPlayer();      
        }
        // if moving left, facing right, mirrored, then flip player
        if (direction > 0 && facingRight && mirrored) {
            flipPlayer();
        }
    }

    // flips the direction the player is facing and update the facingRight bool
    private void flipPlayer()
    {
        playerTransform.localScale = new Vector3(playerTransform.localScale.x*-1,1,1);
        facingRight = !facingRight;
    }
}
