using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    private bool dashUnlocked = false;
    private bool doubleJumpUnlocked = false;

    // returns dashUnlocked
    public bool GetDashUnlocked()
    {
        return dashUnlocked;
    }

    // returns doubleJumpUnlocked
    public bool GetDoubleJumpUnlocked()
    {
        return doubleJumpUnlocked;
    }

    // sets dashUnlocked to true
    public void unlockDash()
    {
        dashUnlocked = true;
    }

    // sets doubleJumpUnlocked to true
    public void unlockDoubleJump()
    {
        doubleJumpUnlocked = true;
    }
}
