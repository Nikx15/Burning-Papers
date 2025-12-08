using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for RectTransform

public class PaperManager : MonoBehaviour
{
    [Header("Paper Prefab")]
    public GameObject paperPrefab;

    [Header("Spawn Position")]
    public Transform paperSpawnPoint; //a point in front of the camera, paper is spawned in

    public BossSuspicionController bossSuspicion;

    [Header("Paper Routing")]
    public RectTransform turnButtonRect;   // The TURN AROUND button (for dropping)
    public RedPaperStack redPaperStack;    // The red paper stack on the other side

    [Header("Paper Parent")]
    public Transform paperParent;          // Optional: set this to FrontView so paper hides when you turn

    private GameObject activePaper;

    private void Awake()
    {
        // Auto-find the red paper stack if not assigned
        if (redPaperStack == null)
        {
            redPaperStack = FindObjectOfType<RedPaperStack>();
        }

        // Auto-find the turn button if not assigned
        if (turnButtonRect == null)
        {
            // Make sure your Turn Button GameObject has the tag "TurnButton"
            GameObject btn = GameObject.FindWithTag("TurnButton");
            if (btn != null)
            {
                turnButtonRect = btn.GetComponent<RectTransform>();
            }
        }
    }

    private void Update()
    {
        if (activePaper == null) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            ProcessPaper();//When you click 1, a paper is processed

        if (Input.GetKeyDown(KeyCode.Alpha2))
            BurnPaper();//When you click 2, a paper us burned
    }

    public void ShowPaper()
    {
        if (activePaper != null) return;

        activePaper = Instantiate(paperPrefab, paperSpawnPoint.position, Quaternion.identity);
        //Prototyped right now, it just shows a rectangle

        // Parent under the front view group so it hides when you face the furnace
        if (paperParent != null)
        {
            activePaper.transform.SetParent(paperParent, true);
        }

        // Inject runtime references into the spawned paper
        PaperDrag drag = activePaper.GetComponent<PaperDrag>();
        if (drag != null)
        {
            drag.paperManager = this;
            drag.redPaperStack = redPaperStack;
            drag.dropButton = turnButtonRect;
        }
    }

    private void ProcessPaper()
    {
        Debug.Log("Paper processed!");
        Destroy(activePaper);
        activePaper = null;
        //Check that a paper is processed
    }

    private void BurnPaper()
    {
        Debug.Log("Paper burned!");
        Destroy(activePaper);
        activePaper = null;
        //check that a paper is burned

        if (bossSuspicion != null)
            bossSuspicion.RegisterBurn();
        //Calls the Boss script to speed up the multiplier
    }

    // Called when the paper is moved somewhere else (like into the red stack),
    // so this manager stops thinking there is an active paper.
    public void ClearActivePaper()
    {
        activePaper = null;
    }
}
