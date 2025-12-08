using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // <-- needed for Slider

public class FurnaceController : MonoBehaviour
{
    [Header("Burn Settings")]
    [Tooltip("How many seconds it takes to burn ONE paper.")]
    public float secondsPerPaper = 1f;   // You can tweak this in Inspector

    [Header("State")]
    public int papersInside = 0;         // How many papers are currently in the furnace

    private float currentBurnTimeRequired = 0f; // = papersInside * secondsPerPaper
    private float currentBurnProgress = 0f;     // How long the lever has been held
    private bool leverHeld = false;

    [Header("UI")]
    public Slider burnProgressSlider;    // Progress bar for current burn

    [Header("Boss Suspicion")]
    public BossSuspicionController bossSuspicion; // If you want to add suspicion per paper burned

    private void Start()
    {
        // Initialize UI
        UpdateProgressUI();
    }

    private void Update()
    {
        if (papersInside <= 0) return;

        if (leverHeld)
        {
            currentBurnProgress += Time.deltaTime;

            if (currentBurnProgress >= currentBurnTimeRequired)
            {
                CompleteBurn();
            }

            UpdateProgressUI();
        }
    }

    /// <summary>
    /// Called when red stack is dropped into the furnace.
    /// Adds papers and recalculates burn time.
    /// </summary>
    public void AddPapers(int count)
    {
        if (count <= 0) return;

        papersInside += count;
        currentBurnTimeRequired = papersInside * secondsPerPaper;
        currentBurnProgress = 0f;

        Debug.Log("Furnace now holds " + papersInside + " paper(s). Burn time required: " + currentBurnTimeRequired + " seconds.");

        UpdateProgressUI();
    }

    /// <summary>
    /// Called by the lever when the player starts/ends holding it.
    /// </summary>
    public void SetLeverHeld(bool held)
    {
        leverHeld = held;
        Debug.Log("Lever held: " + held);
    }

    private void CompleteBurn()
    {
        int burned = papersInside;

        Debug.Log("Furnace burned " + burned + " paper(s)!");

        // Optional suspicion bump per paper actually burned
        if (bossSuspicion != null)
        {
            for (int i = 0; i < burned; i++)
            {
                bossSuspicion.RegisterBurn();
            }
        }

        // Reset furnace state
        papersInside = 0;
        currentBurnProgress = 0f;
        currentBurnTimeRequired = 0f;

        UpdateProgressUI();
    }

    private void UpdateProgressUI()
    {
        if (burnProgressSlider == null) return;

        if (papersInside <= 0 || currentBurnTimeRequired <= 0f)
        {
            burnProgressSlider.gameObject.SetActive(false);
            burnProgressSlider.value = 0f;
        }
        else
        {
            burnProgressSlider.gameObject.SetActive(true);
            burnProgressSlider.minValue = 0f;
            burnProgressSlider.maxValue = currentBurnTimeRequired;
            burnProgressSlider.value = currentBurnProgress;
        }
    }
}
