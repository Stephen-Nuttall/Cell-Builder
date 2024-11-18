using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 10;
    [SerializeField] float minZoom = 1f;
    [SerializeField] float maxZoom = 10f;

    void Update()
    {
        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        
        if (Camera.main.orthographicSize < minZoom)
        {
            Camera.main.orthographicSize = minZoom;
        }
        else if (Camera.main.orthographicSize > maxZoom)
        {
            Camera.main.orthographicSize = maxZoom;
        }
    }
}
