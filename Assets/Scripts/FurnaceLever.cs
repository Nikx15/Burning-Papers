using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceLever : MonoBehaviour
{
    public FurnaceController furnace;

    private bool isHolding = false;

    private void OnMouseDown()
    {
        isHolding = true;
        if (furnace != null)
        {
            furnace.SetLeverHeld(true);
        }
    }

    private void OnMouseUp()
    {
        isHolding = false;
        if (furnace != null)
        {
            furnace.SetLeverHeld(false);
        }
    }

    private void OnMouseExit()
    {
        // If the mouse leaves while held, treat like release
        if (isHolding)
        {
            isHolding = false;
            if (furnace != null)
            {
                furnace.SetLeverHeld(false);
            }
        }
    }
}
