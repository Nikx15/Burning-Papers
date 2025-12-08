using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for RectTransform

public class PaperDrag : MonoBehaviour
{
    [Header("Runtime References (Assigned After Spawn)")]
    public PaperManager paperManager;       // So we can ClearActivePaper when stored
    public RedPaperStack redPaperStack;     // The red stack this paper will be queued into
    public RectTransform dropButton;        // The TURN AROUND button RectTransform

    [Header("Thumbnail Settings")]
    public Sprite normalSprite;            // Full-size paper sprite
    public Sprite thumbnailSprite;         // Small icon sprite for dragging
    public float thumbnailScale = 0.5f;    // How small the paper becomes while dragging

    private SpriteRenderer spriteRenderer;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private bool isDragging = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Save where the paper started and its original scale
        originalPosition = transform.position;
        originalScale = transform.localScale;

        // Ensure we start in normal mode
        if (spriteRenderer != null && normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    private void Update()
    {
        if (!isDragging) return;

        // Follow the mouse while dragging
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;
        transform.position = mouseWorldPos;

        // When the mouse is released, try to drop
        if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }

    /// <summary>
    /// Called by the grab button on the paper (top-left corner)
    /// to start dragging this paper.
    /// </summary>
    public void BeginDrag()
    {
        if (isDragging) return;

        isDragging = true;

        // Swap to thumbnail sprite and scale
        if (spriteRenderer != null && thumbnailSprite != null)
        {
            spriteRenderer.sprite = thumbnailSprite;
        }

        transform.localScale = originalScale * thumbnailScale;
    }

    private void EndDrag()
    {
        isDragging = false;

        bool droppedOnTurnButton = false;

        if (dropButton != null)
        {
            // For Screen Space - Overlay canvas, camera can be null
            if (RectTransformUtility.RectangleContainsScreenPoint(dropButton, Input.mousePosition, null))
            {
                droppedOnTurnButton = true;
            }
        }

        if (droppedOnTurnButton && redPaperStack != null)
        {
            // Successfully queued to the other side (red stack)
            redPaperStack.AddPaper();

            if (paperManager != null)
            {
                paperManager.ClearActivePaper();
            }

            Destroy(gameObject);
            return;
        }

        // If not dropped over the button, snap back and return to normal appearance
        transform.position = originalPosition;
        transform.localScale = originalScale;

        if (spriteRenderer != null && normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }
}
