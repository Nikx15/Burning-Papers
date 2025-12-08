using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RedPaperStack : MonoBehaviour
{
    [Header("Visuals")]
    public SpriteRenderer stackSprite;      // The red stack sprite (can be on this same object)
    public TMP_Text countText;             // UI text showing x1, x2, x3...

    private int storedPaperCount = 0;

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

    private void UpdateCountText()
    {
        if (countText != null)
        {
            countText.text = "x" + storedPaperCount;
        }
    }
}
