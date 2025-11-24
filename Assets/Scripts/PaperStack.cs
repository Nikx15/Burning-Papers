using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperStack : MonoBehaviour
{
    public PaperManager manager;

    private void OnMouseDown()
    {
        manager.ShowPaper();
    }
    //When you click, a new paper is shown
}
