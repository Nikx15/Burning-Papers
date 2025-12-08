using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperGrabButton : MonoBehaviour
{
    public PaperDrag paper; // Reference to the parent paper script

    private void OnMouseDown()
    {
        if (paper != null)
        {
            paper.BeginDrag();
        }
        else
        {
            Debug.LogWarning("PaperGrabButton: No PaperDragToRedStack assigned.");
        }
    }
}
