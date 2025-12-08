using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerViewSwitcher : MonoBehaviour
{
    [Header("View Groups")]
    public GameObject frontViewGroup;   // Facing the boss and your desk
    public GameObject backViewGroup;    // Facing the Furnace

    [Header("Boss Visual Only")]
    public SpriteRenderer bossSprite;  // The boss' sprite

    [Header("Red Stack Visual Only")]
    public SpriteRenderer redStackSprite;   // The red paper stack sprite
    public TMP_Text redStackCountText;      // The "x1, x2, x3" UI text

    [Header("State")]
    public bool facingBoss = true; // Looking @ the boss

    private void Awake()
    {
        // Force a known-good starting state BEFORE anything else runs
        facingBoss = true;

        if (frontViewGroup != null)
        {
            frontViewGroup.SetActive(true);
        }

        if (backViewGroup != null)
        {
            backViewGroup.SetActive(false);
        }
    }

    private void Start()
    {
        // Making sure the state is consistent
        SetFacingBoss(facingBoss);
    }

    // Called from the UI Button
    public void OnTurnButtonPressed()
    {
        ToggleView();
    }

    public void ToggleView()
    {
        SetFacingBoss(!facingBoss);
    }

    public void SetFacingBoss(bool faceBoss)
    {
        facingBoss = faceBoss;

        // Enables Front objects 
        if (frontViewGroup != null)
        {
            frontViewGroup.SetActive(faceBoss);
        }

        // Enables Back objects 
        if (backViewGroup != null)
        {
            backViewGroup.SetActive(!faceBoss);
        }

        // Boss is only visually hidden, not disabled
        if (bossSprite != null)
        {
            bossSprite.enabled = faceBoss;
        }

        // Red stack is only visually hidden, not disabled
        if (redStackSprite != null)
        {
            redStackSprite.enabled = !faceBoss;
        }

        if (redStackCountText != null)
        {
            redStackCountText.enabled = !faceBoss;
        }

        Debug.Log(facingBoss ? "Facing BOSS side." : "Facing FURNACE side.");
    }
}
