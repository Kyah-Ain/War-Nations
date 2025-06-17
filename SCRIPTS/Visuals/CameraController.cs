using UnityEngine;
using Cinemachine;

public class CameraZoomAndPan : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    [Header("Zoom Settings")]
    public float zoomSpeed = 2f;
    public float minZoom = 2f;
    public float maxZoom = 20f;

    [Header("Pan Settings")]
    public float panSpeed = 5f;
    public float edgeThickness = 10f; // pixels from screen edge to start panning
    public float minX = -10f;
    public float maxX = 10f;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        HandleZoom();
        HandleHorizontalPan();

        //Debug.Log($"{Input.mousePosition}"); for visual output of your mouse coordinates
    }

    private void HandleZoom()
    {
        if (virtualCamera == null || !virtualCamera.m_Lens.Orthographic)
            return;

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scrollInput) > 0.01f)
        {
            float newSize = virtualCamera.m_Lens.OrthographicSize - scrollInput * zoomSpeed;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
    }

    private void HandleHorizontalPan()
    {
        Vector3 camPos = virtualCamera.transform.position;

        if (Input.mousePosition.x <= edgeThickness)
        {
            camPos.x -= panSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.x >= Screen.width - edgeThickness)
        {
            camPos.x += panSpeed * Time.deltaTime;
        }

        // Clamp X position
        camPos.x = Mathf.Clamp(camPos.x, minX, maxX);
        virtualCamera.transform.position = camPos;
    }

    /* for visual debugging of edge zones
    private void OnGUI()
    {
        Color prevColor = GUI.color;

        // Left edge zone
        GUI.color = new Color(1, 0, 0, 0.3f); // semi-transparent red
        GUI.DrawTexture(new Rect(0, 0, edgeThickness, Screen.height), Texture2D.whiteTexture);

        // Right edge zone
        GUI.color = new Color(0, 0, 1, 0.3f); // semi-transparent blue
        GUI.DrawTexture(new Rect(Screen.width - edgeThickness, 0, edgeThickness, Screen.height), Texture2D.whiteTexture);

        GUI.color = prevColor;
    }
    */
}
