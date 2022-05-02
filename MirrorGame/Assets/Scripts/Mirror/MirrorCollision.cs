using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCollision : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool canMirror = true;

    // Awake is called when the script instance is being loaded. 
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.green;
    }

    // OnTriggerEnter2D is called when Mirror 2D collider enters another collider
    private void OnTriggerEnter2D()
    {
        // Debug.Log("Mirror entered collider");  // testing

        // change mirror colour to red and disable mirror ability
        spriteRenderer.color = Color.red;
        canMirror = false;
    }

    // OnTriggerStay2D is called when Mirror 2D collider is in another collider
    private void OnTriggerStay2D() 
    {
        // change mirror colour to red and disable mirror ability
        spriteRenderer.color = Color.red;
        canMirror = false;
    }

    // OnTriggerExit2D is called when Mirror 2D collider exits another collider
    private void OnTriggerExit2D() 
    {
        // Debug.Log("Mirror exited collider"); // testing

        // change mirror colour to green and enable mirror ability
        spriteRenderer.color = Color.green;
        canMirror = true;
    }

    // return canMirror
    public bool GetCanMirror()
        {
            return canMirror;
        }
}
