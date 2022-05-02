using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D body;
    private PlayerGrounded playerGrounded;
    private PlayerDash playerDash;
    private PlayerAbilities playerAbilities;

    private const float jumpForce = 20f;
    private const float jumpCutMultiplier = 0.8f;
    private const float doubleJumpMultiplier = 0.713f;
    private const float coyoteTime = 0.2f;
    private const float jumpBuffer = 0.1f;
    private float coyoteTimer;
    private float jumpBufferTimer;
    private bool canDoubleJump;
    private bool doubleJumpUnlocked;
    private bool isDashing;
    private bool doubleJump;
    private bool dashDoubleJump;

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // get the Player's rigidbody, PlayerGrounded, PlayerDash and PlayerAbilities components
        body = GetComponent<Rigidbody2D>();
        playerGrounded = GetComponent<PlayerGrounded>();
        playerDash = GetComponent<PlayerDash>();
        playerAbilities = GetComponent<PlayerAbilities>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // get isDashing & doubleJumpUnlocked
        isDashing = playerDash.GetDashing();
        doubleJumpUnlocked = playerAbilities.GetDoubleJumpUnlocked();
    }

    // Update is called once per frame
    private void Update()
    {
        // Debug.Log(body.velocity.y);  // testing
        // Debug.Log(coyoteTimer);  // testing
        // Debug.Log(jumpBufferTimer);  // testing
        
        // if doubleJumpUnlocked has changed, then update value
        if (!doubleJumpUnlocked) {
            doubleJumpUnlocked = playerAbilities.GetDoubleJumpUnlocked();
        }

        // if player is grounded then reset coyete timer, else let coyete timer count down
        if (playerGrounded.GetGrounded()) {
            canDoubleJump = true;
            coyoteTimer = coyoteTime;
        }else{
            coyoteTimer -= Time.deltaTime;
        }

        // if jump button is pressed then reset jump buffer timer, else let jump buffer timer count down
        if (Input.GetButtonDown("Jump")) {
            jumpBufferTimer = jumpBuffer;
        }else{
            jumpBufferTimer -= Time.deltaTime;
        }

        // if the jump buffer and coyote timers are positive then the player jumps
        if (jumpBufferTimer > 0 && coyoteTimer > 0) {
            // stop player's downward velocity
            body.velocity = new Vector2(body.velocity.x, 0);

            // apply an upward force to the player
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // set the jump buffer timer to 0
            jumpBufferTimer = 0;
        }

        // if during the 1st jump the jump button is let go, while the player is still rising, then cut the jump short
        if (Input.GetButtonUp("Jump") && canDoubleJump && body.velocity.y > 0) {
            // apply a downward force to the player
            body.AddForce(Vector2.down * body.velocity.y * jumpCutMultiplier, ForceMode2D.Impulse); 
            // set the coyote timer to 0
            coyoteTimer = 0;
        }
        
        // if player is dashing, then set isDashing to true
        if (playerDash.GetDashing()) {
             isDashing = true;
        }

        // if jump is pressed again, have not double jump yet, double jump unlocked and they have not touched the ground yet and player is not dashing, then set doubleJump to true
        if (Input.GetButtonDown("Jump") && canDoubleJump && doubleJumpUnlocked && coyoteTimer <= 0 && !playerDash.GetDashing()) {
            Debug.Log("double jump");
            doubleJump = true;
        }
        // else if jump is pressed again, have not double jump yet, double jump unlocked and they have not touched the ground yet and player is dashing, then set dashDoubleJump to true
        else if (Input.GetButtonDown("Jump") && canDoubleJump && doubleJumpUnlocked && coyoteTimer <= 0 && playerDash.GetDashing()) {
            dashDoubleJump = true;
            doubleJump = false;
            
            // Debug.Log("dash double jump pressed "); // testing
        }
        // else set doubleJump to false
        else{
            doubleJump = false;
        }

        // if dashDoubleJump is true and is no longer dashing, then set doubleJump to true and dashDoubleJump and isDashing = false;
        if (dashDoubleJump && isDashing && !playerDash.GetDashing()) {

            // Debug.Log("dash double jump executed "); // testing

            doubleJump = true;
            dashDoubleJump = false;
            isDashing = false;
        }

        // doubleJump is true, then the player double jumps
        if (doubleJump) {
            // stop player's downward velocity
            body.velocity = new Vector2(body.velocity.x, 0);

            // apply an upward force to the player
            body.AddForce(Vector2.up * doubleJumpMultiplier * jumpForce, ForceMode2D.Impulse);
            canDoubleJump = false;

            doubleJump = false;
        }
    }
}
