using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperStack : MonoBehaviour
{
    public PaperManager paperManager;

    private void OnMouseDown()
    {
        if (paperManager != null)
        {
            Debug.Log("PaperStack clicked, calling ShowPaper().");
            paperManager.ShowPaper();
        }
        else
        {
            Debug.LogWarning("PaperStack: paperManager is NOT assigned in the Inspector.");
        }
    }
}
