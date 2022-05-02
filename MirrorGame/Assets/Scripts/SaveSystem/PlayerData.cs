using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool dashUnlocked;
    public bool doubleJumpUnlocked;

    public bool mirrored;

    public float[] position;

    public PlayerData (PlayerAbilities playerAbilities, PlayerMirrored playerMirrored)
    {
        dashUnlocked = playerAbilities.GetDashUnlocked();
        doubleJumpUnlocked = playerAbilities.GetDoubleJumpUnlocked();

        mirrored = playerMirrored.GetMirrored();

        position = new float[3];
        position[0] = playerAbilities.transform.position.x;
        position[1] = playerAbilities.transform.position.y;
        position[2] = playerAbilities.transform.position.z;

        
        Debug.Log("Saved: position = (" + playerAbilities.transform.position.x + ", " + playerAbilities.transform.position.y + ", " + playerAbilities.transform.position.z + "), dashUnlocked = " + dashUnlocked + ", doubleJumpUnlocked = " + doubleJumpUnlocked); //testing
    }
}
