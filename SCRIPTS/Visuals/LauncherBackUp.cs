using Cinemachine;
using System.Collections; // Grants access to collections and data structures like ArrayList, Hashtable, etc.
using System.Collections.Generic; // grants access to collections and data structures like List, Dictionary, etc.
using UnityEngine; // Grants access to Unity's core classes and functions, such as GameObject, MonoBehaviour, etc.

public class LauncherBackUp : MonoBehaviour
{
    /*
    public Bird birdPrefab;
    public Transform launchPosition;
    private Bird currentBird;
    private bool isDragging = false;
    public float launchPower = 10f;
    private bool hasLaunched = false;
    public float maxDragDistance = 3f; // Adjust in the Inspector
    public CinemachineVirtualCamera virtualCamera;

    [Header("Slingshot Line Settings")]
    public LineRenderer slingshotLine; // LineRenderer for the slingshot cord
    public float slingshotLineWidth = 1f;  // The width of the slingshot line
    public float slingshotLineMaxLength = 5f;


    [Header("Trajectory Settings")]
    public int trajectoryPoints = 30;  // Number of trajectory points
    public float timeStep = 0.1f;      // Time step for each trajectory point
    public LineRenderer trajectoryRenderer;  // LineRenderer to show trajectory
    public GameObject camera_initPos;

    void Start()
    {
        SpawnNextBird();
        slingshotLine.positionCount = 0;
    }

    void Update()
    {
        if (currentBird == null || hasLaunched) return;

        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            slingshotLine.positionCount = 2;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = 0;

            Vector3 dragVector = mouseWorld - launchPosition.position;

            // Clamp the drag distance
            if (dragVector.magnitude > maxDragDistance)
            {
                dragVector = dragVector.normalized * maxDragDistance;
            }

            currentBird.transform.position = launchPosition.position + dragVector;

            // Update trajectory visualization
            Vector2 dir = (Vector2)(launchPosition.position - currentBird.transform.position);
            Vector2 launchVelocity = dir * launchPower;
            ShowTrajectory(currentBird.transform.position, launchVelocity);
            UpdateSlingshotLine(currentBird.transform.position);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            hasLaunched = true; // Prevent further dragging

            virtualCamera.Follow = currentBird.transform;

            HideTrajectory();
            Vector2 dir = (Vector2)(launchPosition.position - currentBird.transform.position);
            currentBird.Launch(dir * launchPower);

            slingshotLine.positionCount = 0;
        }
    }

    void UpdateSlingshotLine(Vector2 birdPosition)
    {
        // The first point is the launch position, second is the bird's current position (scaled)
        Vector2 direction = birdPosition - (Vector2)launchPosition.position;

        // Calculate the distance between launch position and the bird's position
        float distance = direction.magnitude;

        // Clamp the distance to prevent the line from becoming too long
        distance = Mathf.Min(distance, slingshotLineMaxLength);

        // Normalize the direction to scale it and apply the clamped distance
        Vector2 stretchedPosition = (Vector2)launchPosition.position + direction.normalized * distance;

        slingshotLine.SetPosition(0, launchPosition.position);  // Set start point (launch position)
        slingshotLine.SetPosition(1, stretchedPosition);  // Set end point (stretched position)
    }
    void ShowTrajectory(Vector2 startPos, Vector2 startVelocity)
    {
        Vector3[] points = new Vector3[trajectoryPoints];

        // Calculate trajectory path points
        for (int i = 0; i < trajectoryPoints; i++)
        {
            float t = i * timeStep;
            Vector2 pos = startPos + startVelocity * t + 0.5f * Physics2D.gravity * t * t;
            points[i] = pos;
        }

        // Set the trajectory points to the LineRenderer
        trajectoryRenderer.positionCount = trajectoryPoints;
        trajectoryRenderer.SetPositions(points);
    }

    void HideTrajectory()
    {
        // Reset the LineRenderer when trajectory is no longer needed
        trajectoryRenderer.positionCount = 0;
    }

    void SpawnNextBird()
    {
        currentBird = Instantiate(birdPrefab, launchPosition.position, Quaternion.identity);
        currentBird.GetComponent<Rigidbody2D>().isKinematic = true;
        currentBird.OnBirdStopped += HandleBirdStopped;
        StartCoroutine(AnimateCameraMove(camera_initPos.transform.position, 1f));
    }

    void HandleBirdStopped(Bird bird)
    {
        bird.OnBirdStopped -= HandleBirdStopped;
        Destroy(bird.gameObject);
        hasLaunched = false; // Allow dragging the new bird
        Invoke(nameof(SpawnNextBird), 1.5f);
    }

    private IEnumerator AnimateCameraMove(Vector3 targetPosition, float duration)
    {
        Vector3 startPos = virtualCamera.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            virtualCamera.transform.position = Vector3.Lerp(startPos, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        virtualCamera.transform.position = targetPosition; // Ensure it ends exactly at target
    }
    */
}
