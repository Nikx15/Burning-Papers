using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperManager : MonoBehaviour
{
    [Header("Paper Prefab")]
    public GameObject paperPrefab;

    [Header("Spawn Position")]
    public Transform paperSpawnPoint; //a point in front of the camera, paper is spawned in

    public BossSuspicionController bossSuspicion;

    private GameObject activePaper;

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
}
