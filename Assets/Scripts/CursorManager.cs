using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        Cursor.visible = false;
    }
    
    void Update()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPosition;
    }
    
    void OnMouseExit()
    {
        Cursor.visible = true;
    }
}