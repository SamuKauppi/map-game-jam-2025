using UnityEngine;

public class DragZoomBackground : MonoBehaviour
{
    public Camera cam; // Reference to the main camera
    public float minZoom = 2f;
    public float maxZoom = 10f;
    public float zoomSpeed = 2f;
    public float dragSpeed = 0.5f;

    private Vector3 dragOrigin;

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main; // Automatically assign the main camera
        }
    }

    void Update()
    {
        HandleDrag();
        HandleZoom();
    }

    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button or touch
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) // While dragging
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += difference * dragSpeed;
        }
    }

    void HandleZoom()
    {
        // Mouse Scroll Zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            cam.orthographicSize -= scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }

        // Touch Pinch Zoom
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0Prev = touch0.position - touch0.deltaPosition;
            Vector2 touch1Prev = touch1.position - touch1.deltaPosition;

            float prevMagnitude = (touch0Prev - touch1Prev).magnitude;
            float currentMagnitude = (touch0.position - touch1.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            cam.orthographicSize -= difference * zoomSpeed * 0.01f;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }
    }
}
