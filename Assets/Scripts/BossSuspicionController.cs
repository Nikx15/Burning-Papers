using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossSuspicionController : MonoBehaviour
{
    [Header("Boss Color")]
    public SpriteRenderer bossSprite;
    public Color normalColor = Color.white;
    public Color alertColor = Color.red;

    [Header("Suspicion Settings")]
    [Tooltip("How fast the suspicion meter fills")]
    public float baseFillSpeed = 10f;

    [Tooltip("How much the multiplier increases per paper burned")]
    public float multiplierPerBurn = 0.5f;

    [Tooltip("Maximum suspicion value")]
    public float maxSuspicion = 100f;

    [Tooltip("How long (in seconds) the boss stays turned")]
    public float bossCheckDuration = 3f;

    [Header("UI")]
    public TMP_Text suspicionText;  // Shows current percent and multiplier in the UI 

    private float currentSuspicion = 0f;
    private float currentMultiplier = 1f;
    private bool isChecking = false;

    private void Start()
    {
        if (bossSprite != null)
            bossSprite.color = normalColor; //base boss sprite

        UpdateUI();
    }

    private void Update()
    {
        if (isChecking) return;

        // Meter always goes up
        currentSuspicion += baseFillSpeed * currentMultiplier * Time.deltaTime;
        currentSuspicion = Mathf.Clamp(currentSuspicion, 0f, maxSuspicion);

        UpdateUI();

        if (currentSuspicion >= maxSuspicion && !isChecking)
        {
            StartCoroutine(BossCheckRoutine());
        }
    }

    private void UpdateUI()
    {
        if (suspicionText == null) return;

        float percent = (currentSuspicion / maxSuspicion) * 100f;
        suspicionText.text = $"Suspicion: {percent:0}%\nMultiplier: x{currentMultiplier:0.0}";
    }

    // Call this whenever a paper is burned.
    // Adds to the multiplier so the bar fills faster.
    public void RegisterBurn()
    {
        currentMultiplier += multiplierPerBurn;
        UpdateUI();
    }

    private IEnumerator BossCheckRoutine()
    {
        isChecking = true;

        // Boss turns alert
        if (bossSprite != null)
            bossSprite.color = alertColor; //Mad Color

        Debug.Log("Boss is turning around!");

        yield return new WaitForSeconds(bossCheckDuration); //How long he is turned around

        // Reset suspicion & multiplier
        currentSuspicion = 0f;
        currentMultiplier = 1f;

        // Boss back to normal
        if (bossSprite != null)
            bossSprite.color = normalColor;

        Debug.Log("Boss finished checking, suspicion reset!");

        isChecking = false;
        UpdateUI();
    }
}
