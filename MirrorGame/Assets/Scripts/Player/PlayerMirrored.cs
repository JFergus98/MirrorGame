using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMirrored : MonoBehaviour
{
    private bool mirrored = false;

    // swaps the value of mirrored between true and false
    public void MirrorPlayer()
    {
        mirrored = !mirrored;
    }

    // returns the value of the bool mirrored
    public bool GetMirrored()
    {
        return mirrored;
    }
}
