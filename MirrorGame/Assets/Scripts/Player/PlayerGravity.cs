using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    private Rigidbody2D body;
    private PlayerDash playerDash;
    
    private bool isDashing = false;
    private const float defaultGravityScale = 2.2f;
    private const float downwardGravityScale = 3.6f;
    private const float dashingGravityScale = 0f;

    private const float maxFallSpeed = 56f;

    // Awake is called when the script instance is being loaded. 
    private void Awake()
    {
        // get the Player's rigidbody and PlayerDash components
        body = GetComponent<Rigidbody2D>();
        playerDash = GetComponent<PlayerDash>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // get isDashing
        isDashing = playerDash.GetDashing();
    }

    // Update is called once per frame
    private void Update()
    {
        // Debug.Log(body.gravityScale);  // testing

        // if isDashing has changed, then update value
        if (isDashing != playerDash.GetDashing()) {
            isDashing = !isDashing;
        }
    }

    // FixedUpdate is called once per 0.02 seconds
    private void FixedUpdate()
    {
        // Debug.Log(body.velocity);  // testing

        // limit max velocity
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxFallSpeed);
        
        // if player is falling then apply greater gravity to player, else if player is dashing apply no gravity, else apply normal gravity
        if (body.velocity.y < 1 && !isDashing) {
            body.gravityScale = downwardGravityScale;
        }else if (isDashing){
            body.gravityScale = dashingGravityScale;
        }else{
            body.gravityScale = defaultGravityScale;
        }
    }
}
