using UnityEngine;

public class PanCamera : MonoBehaviour
{
    Vector2 mouseStartPos;

    void Awake()
    {
        mouseStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Mouse2))
        {
            transform.position = new Vector3(
                transform.position.x + mouseStartPos.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                transform.position.y + mouseStartPos.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                transform.position.z
            );
        }
        else
        {
            mouseStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
