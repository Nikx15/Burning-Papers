using UnityEngine;

public class ClickTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("GlobalMouseDebug: Left mouse clicked.");
        }
    }
}
