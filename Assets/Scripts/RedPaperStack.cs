using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RedPaperStack : MonoBehaviour
{
    [Header("Visuals")]
    public SpriteRenderer stackSprite;      // The red stack sprite
    public TMP_Text countText;              // UI text showing x1, x2, x3...

    private int storedPaperCount = 0;

    public int StoredPaperCount => storedPaperCount;

    private void Start()
    {
        UpdateCountText();
    }

    public void AddPaper()
    {
        storedPaperCount++;
        UpdateCountText();
        Debug.Log("Red stack now holds " + storedPaperCount + " paper(s).");
    }

    // Called by the furnace when it takes all queued papers
    public int TakeAllPapers()
    {
        int taken = storedPaperCount;
        storedPaperCount = 0;
        UpdateCountText();
        Debug.Log("Red stack gave " + taken + " paper(s) to the furnace.");
        return taken;
    }

    private void UpdateCountText()
    {
        if (countText != null)
        {
            countText.text = "x" + storedPaperCount;
        }
    }
}
