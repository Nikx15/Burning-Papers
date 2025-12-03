using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewSwitcher : MonoBehaviour
{
    [Header("View Groups")]
    public GameObject frontViewGroup;   // Facing the boss and your desk
    public GameObject backViewGroup;    // Facing the Furnace

    [Header("Boss Visual Only")]
    public SpriteRenderer bossSprite;  // The boss' sprite

    [Header("State")]
    public bool facingBoss = true; // Looking @ the boss

    [Header("Input")]
    //Key to turn around
    public KeyCode toggleKey = KeyCode.F;

    private void Start()
    {
        // Making sure the state is consistent
        SetFacingBoss(facingBoss);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleView();
        }
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

        Debug.Log(facingBoss ? "Facing BOSS side." : "Facing FURNACE side.");
    }
}
