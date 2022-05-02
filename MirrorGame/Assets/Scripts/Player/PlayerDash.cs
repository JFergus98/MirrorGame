using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D body;
    private PlayerGrounded playerGrounded;
    private Transform playerTransform;

    private bool isDashing = false;
    private bool touchedGround;
    private const float dashVelocity = 20f;
    private const float dashTime = 0.2f;
    private float dashTimer;
    private const float dashCooldown = 0.5f;
    private float dashCooldownTimer;

    private bool triggerPressed = false;

    // Awake is called when the script instance is being loaded. 
    private void Awake()
    {
        // get the Player's rigidbody and PlayerGrounded components
        body = GetComponent<Rigidbody2D>();
        playerGrounded = GetComponent<PlayerGrounded>();

        // cache player transform
        playerTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        // reset dash timer
        dashTimer = dashTime;
        dashCooldownTimer = 0;
    }

    // functions that controls the player dash movement 
    public void Dash()
    {
        // Debug.Log(dashTimer);  // testing
        // Debug.Log(dashCooldownTimer);  // testing

        // if player has touched the ground since dashing, then set touchedGround to true
        if (!touchedGround && playerGrounded.GetGrounded()) {
            touchedGround = true;
        }

        // if the player has touched the ground and dash is on cooldown, then reduce cooldown timer
        if (touchedGround && dashCooldownTimer > 0) {
            dashCooldownTimer -=Time.deltaTime;
        }

        // if trigger is no longer pressed
        if (Input.GetAxisRaw("Dash") == 0 && triggerPressed)
        {
            triggerPressed = false;
        }
        
        // dash button is pressed and dash is not on cooldown, then set isDashing to true
        if ((Input.GetButtonDown("Dash") || (Input.GetAxisRaw("Dash") > 0 && !triggerPressed))  && dashCooldownTimer <= 0) {
            isDashing =true;
            triggerPressed = true;
        }

        // if player is dashing
        if (isDashing) {
            // move player positon
            body.velocity = new Vector2(transform.localScale.x * dashVelocity, 0f);
            
            // if dash timer is over, then stop dashing, reset dash timer and start dash cooldown timer, else reduce dash timer
            if (dashTimer <= 0 ) {
                isDashing = false;
                dashTimer = dashTime;
                dashCooldownTimer = dashCooldown;
                touchedGround = false;
            }else{
                dashTimer -= Time.deltaTime;
            }
        }
    }

    // returns true if player is dashing, false if player is not dashing
    public bool GetDashing()
    {
        return isDashing;
    }
}
