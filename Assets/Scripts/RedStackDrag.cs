using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedStackDrag : MonoBehaviour
{
    [Header("References")]
    public RedPaperStack redPaperStack;     // The stack we are dragging
    public FurnaceController furnace;       // The furnace controller
    public Collider2D furnaceCollider;      // Collider on the furnace for drop detection

    private Vector3 originalPosition;
    private bool isDragging = false;

    private void Start()
    {
        originalPosition = transform.position;

        if (redPaperStack == null)
        {
            redPaperStack = GetComponent<RedPaperStack>();
        }
    }

    private void Update()
    {
        if (!isDragging) return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;
        transform.position = mouseWorldPos;

        if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }

    private void OnMouseDown()
    {
        // Start dragging the red stack itself
        isDragging = true;
    }

    private void EndDrag()
    {
        isDragging = false;

        bool droppedOnFurnace = false;

        if (furnaceCollider != null)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 point2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            if (furnaceCollider.OverlapPoint(point2D))
            {
                droppedOnFurnace = true;
            }
        }

        if (droppedOnFurnace && furnace != null && redPaperStack != null)
        {
            int count = redPaperStack.TakeAllPapers();

            if (count > 0)
            {
                furnace.AddPapers(count);
            }
        }

        // Always snap the red stack back to its original spot
        transform.position = originalPosition;
    }
}
