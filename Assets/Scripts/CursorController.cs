using UnityEngine;

public class CursorController : MonoBehaviour
{
    [Header("Cursor Settings For This Scene")]
    public bool lockCursor = false;
    public bool hideCursor = false;

    void Start()
    {
        ApplyCursorState();
    }

    void OnEnable()
    {
        ApplyCursorState();
    }

    void ApplyCursorState()
    {
        Cursor.lockState = lockCursor 
            ? CursorLockMode.Locked 
            : CursorLockMode.None;

        Cursor.visible = !hideCursor;
    }
}
