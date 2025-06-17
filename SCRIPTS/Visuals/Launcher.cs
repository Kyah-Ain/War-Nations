using Cinemachine; // Grants access to Cinemachine features for camera control
using System.Collections; // Grants access to collections and data structures like ArrayList
using UnityEngine; // Grants access to Unity's core features 

public class Launcher : MonoBehaviour
{
    public StarManager starManager; // Reference to the StarManager.cs script
    public Enemy enemy; // Reference to the Enemy.cs script
    public GameObject[] birdPrefabs; // Array of bird prefabs to spawn

    public GameObject slingSFX; // Reference to the slingshot sound effect prefab
    public GameObject nextBirdAnim; // Reference to the next bird animation prefab

    [Header("Slingshot Positions")]
    public Transform slingLeftLaunchPosition; // Position of the left slingshot cord
    public Transform birdLaunchPosition; // Position where the bird is launched from
    public Transform slingRightLaunchPosition; // Position of the right slingshot cord

    private Bird currentBird; // Reference to the currently launched bird
    private bool isDragging = false; // Flag to check if the player is dragging the bird
    public float launchPower = 10f; // Power of the launch force applied to the bird
    private bool hasLaunched = false; // Flag to check if the bird has been launched
    public float maxDragDistance = 3f; // Adjust in the Inspector
    public CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine virtual camera

    [Header("Slingshot Line Settings")]
    public LineRenderer slingshotLineLeft; // LineRenderer for the slingshot cord
    public float slingLineLeftWidth = 1f;  // The width of the slingshot line
    public float slingLineLeftMaxLength = 5f; // The maximum length of the Left slingshot line

    public LineRenderer slingshotLineRight; // LineRenderer for the slingshot cord
    public float slingLineRightWidth = 1f;  // The width of the slingshot line
    public float slingLineRightMaxLength = 5f; // The maximum length of the Right slingshot line

    [Header("Trajectory Settings")]
    public int trajectoryPoints = 30; // Number of trajectory points
    public float timeStep = 0.1f; // Time step for each trajectory point
    public LineRenderer trajectoryRenderer; // LineRenderer to show trajectory
    public GameObject camera_initPos; // Reference to the initial camera position

    [Header("Regulator")]
    public int birdCount = 3; // Number of birds left to spawn
    public int birdIndex = -1; // Default to the first bird prefab
    private bool hasPlayedSlingSFX = false; // Flag to check if the slingshot sound effect has been played

    void Start()
    {
        starManager.numberOfBirdsLeft = birdCount; // Update the number of birds left
        PlayNextBirdAnim(); // Play the next bird animation at the start
        Invoke(nameof(SpawnNextBird), 0.8f); // Spawn the first bird after a short delay
        slingshotLineLeft.positionCount = 0; // Initialize the Left slingshot line
        slingshotLineRight.positionCount = 0; // Initialize the Right slingshot line
    }

    void Update()
    {
        starManager.numberOfBirdsLeft = birdCount; // Update the number of birds left in the StarManager

        if (currentBird == null || hasLaunched) return; // If no bird is selected or has been launched, do nothing

        if (Input.GetMouseButtonDown(0) && starManager.isGameComplete == false) // Check if the left mouse button is pressed and the game is not complete
        {
            isDragging = true; // Set dragging to true
            slingshotLineLeft.positionCount = 2; // Set the position count for the Left slingshot line
            slingshotLineRight.positionCount = 2; // Set the position count for the Right slingshot line

            // Launch Rubber SFX 
            if (slingSFX != null && hasPlayedSlingSFX == false) // Check if the slingshot sound effect is set and has not been played yet
            {
                Instantiate(slingSFX, transform.position, Quaternion.identity); // Instantiate the slingshot sound effect at the launcher position
                hasPlayedSlingSFX = true; // Prevent multiple instantiations
            }
        }

        if (Input.GetMouseButton(0) && isDragging) // Check if the left mouse button is held down and dragging is true
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Convert mouse position to world coordinates
            mouseWorld.z = 0; // Set the z-coordinate to 0 to keep it in 2D space

            Vector3 dragVector = mouseWorld - birdLaunchPosition.position; // Calculate the drag vector from the launch position to the mouse position

            // Clamp the drag distance
            if (dragVector.magnitude > maxDragDistance) 
            {
                dragVector = dragVector.normalized * maxDragDistance; // Normalize the vector and scale it to the maximum drag distance
            }

            currentBird.transform.position = birdLaunchPosition.position + dragVector; // Set the bird's position to the launch position plus the drag vector

            // Update trajectory visualization
            Vector2 dir = (Vector2)(birdLaunchPosition.position - currentBird.transform.position); // Calculate the direction from the bird's current position to the launch position
            Vector2 launchVelocity = dir * launchPower; // Calculate the launch velocity based on the direction and launch power
            ShowTrajectory(currentBird.transform.position, launchVelocity); // Show the trajectory based on the bird's current position and calculated launch velocity
            UpdateSlingshotLine(currentBird.transform.position); // Update the slingshot line based on the bird's current position
        }

        if (Input.GetMouseButtonUp(0) && isDragging) // Check if the left mouse button is released and dragging is true
        {
            isDragging = false; // Set dragging to false
            hasLaunched = true; // Prevent further dragging

            virtualCamera.Follow = currentBird.transform; // Set the camera to follow the current bird

            HideTrajectory(); // Hide the trajectory visualization
            Vector2 dir = (Vector2)(birdLaunchPosition.position - currentBird.transform.position); // Calculate the direction from the bird's current position to the launch position
            currentBird.Launch(dir * launchPower); // Launch the bird with the calculated direction and launch power

            slingshotLineLeft.positionCount = 0; // Reset the Left slingshot line
            slingshotLineRight.positionCount = 0; // Reset the Right slingshot line
        }
    }

    void UpdateSlingshotLine(Vector2 birdPosition)
    {
        // The first point is the launch position, second is the bird's current position (scaled)
        Vector2 leftDirection = birdPosition - (Vector2)slingLeftLaunchPosition.position;

        // Calculate the distance between launch position and the bird's position
        float leftDistance = Mathf.Min(leftDirection.magnitude, slingLineLeftMaxLength);

        // Clamp the distance to prevent the line from becoming too long
        Vector2 leftStretchedPos = (Vector2)slingLeftLaunchPosition.position + leftDirection.normalized * leftDistance;

        // Normalize the direction to scale it and apply the clamped distance
        slingshotLineLeft.startWidth = slingLineLeftWidth; // Set the start width of the slingshot line
        slingshotLineLeft.endWidth = slingLineLeftWidth; // Set the end width of the slingshot line
        slingshotLineLeft.SetPosition(0, slingLeftLaunchPosition.position); // base of slingshot
        slingshotLineLeft.SetPosition(1, leftStretchedPos); // clamped stretched end

        Vector2 rightDirection = birdPosition - (Vector2)slingRightLaunchPosition.position; // Calculate the direction from the right launch position to the bird's current position
        float rightDistance = Mathf.Min(rightDirection.magnitude, slingLineRightMaxLength); // Calculate the distance between the right launch position and the bird's position, clamping it to the maximum length
        Vector2 rightStretchedPos = (Vector2)slingRightLaunchPosition.position + rightDirection.normalized * rightDistance; // Calculate the clamped stretched position for the right slingshot line

        slingshotLineRight.startWidth = slingLineRightWidth; // Set the start width of the right slingshot line
        slingshotLineRight.endWidth = slingLineRightWidth; // Set the end width of the right slingshot line
        slingshotLineRight.SetPosition(0, slingRightLaunchPosition.position); // base of slingshot
        slingshotLineRight.SetPosition(1, rightStretchedPos); // clamped stretched end
    }

    void ShowTrajectory(Vector2 startPos, Vector2 startVelocity) // Calculates and displays the trajectory of the bird
    {
        Vector3[] points = new Vector3[trajectoryPoints]; // Create an array to hold the trajectory points

        // Calculate trajectory path points
        for (int i = 0; i < trajectoryPoints; i++)
        {
            float t = i * timeStep; // Calculate the time for each point based on the index and time step
            Vector2 pos = startPos + startVelocity * t + 0.5f * Physics2D.gravity * t * t; // Calculate the position of the point using the kinematic equation for projectile motion
            points[i] = pos; // Store the calculated position in the points array
        }

        // Set the trajectory points to the LineRenderer
        trajectoryRenderer.positionCount = trajectoryPoints; // Set the number of points in the LineRenderer
        trajectoryRenderer.SetPositions(points); // Set the positions of the trajectory points in the LineRenderer
    }

    void HideTrajectory() // Hides the trajectory visualization by clearing the LineRenderer
    {
        // Reset the LineRenderer when trajectory is no longer needed
        trajectoryRenderer.positionCount = 0; // Clear the trajectory points
    }

    void PlayNextBirdAnim() // Plays the next bird animation when a new bird is spawned
    {
        if (starManager.isGameComplete == false && birdCount > 0)
        {
            GameObject anim = Instantiate(nextBirdAnim, transform.position, Quaternion.identity); // Instantiate the next bird animation prefab
        }
    }

    void SpawnNextBird() // Spawns another bird 
    {
        if (starManager.isGameComplete == false)  // Check if the game is not complete
        {
            if (birdCount > 0) // Check if there are birds left to spawn
            {           
                if (birdIndex < birdPrefabs.Length - 1) // Check if we have not reached the last bird prefab
                {
                    birdIndex++; // Increment to get the next bird prefab
                }
                else
                {
                    // Reset to the first bird prefab if we reach the end
                    birdIndex = 0;
                }

                GameObject birdObj = Instantiate(birdPrefabs[birdIndex], birdLaunchPosition.position, Quaternion.identity); // Instantiate the next bird prefab at the launch position
                currentBird = birdObj.GetComponent<Bird>(); // This will work for Bird or any subclass like SpainNormal

                if (currentBird == null) // Check if the spawned bird prefab has a Bird component
                {
                    Debug.LogError("Spawned bird prefab does not have a Bird (or child) component!"); 
                    return;
                }
               
                currentBird.GetComponent<Rigidbody2D>().isKinematic = true; // Set the Rigidbody2D to kinematic to allow dragging
                currentBird.OnBirdStopped += HandleBirdStopped; // Subscribe to the OnBirdStopped event to handle when the bird stops moving
                StartCoroutine(AnimateCameraMove(camera_initPos.transform.position, 1f)); // Animate the camera to the initial position
                hasPlayedSlingSFX = false; // Reset for the next bird's turn
                birdCount--; // Decrease the bird count
            }
            else
            {
                starManager.ShowLosePanel(); // Show the lose panel if no birds are left
                starManager.isGameComplete = true; // Set the game as complete
                Enemy.enemyCount = 0; // Reset enemy count
            }
        }
    }

    void HandleBirdStopped(Bird bird) // This method is called when the bird stops moving
    {
        bird.OnBirdStopped -= HandleBirdStopped; // Unsubscribe from the OnBirdStopped event to prevent multiple calls
        Destroy(bird.gameObject); // Destroy the bird GameObject when it stops moving

        hasLaunched = false; // Allow dragging the new bird

        PlayNextBirdAnim(); // Play the next bird animation
        Invoke(nameof(SpawnNextBird), 0.8f); // Spawn the next bird after a short delay
    }

    private IEnumerator AnimateCameraMove(Vector3 targetPosition, float duration) // Coroutine to animate the camera movement to a target position
    {
        Vector3 startPos = virtualCamera.transform.position; // Store the current camera position
        float elapsed = 0f; // Initialize elapsed time

        while (elapsed < duration) // Loop until the duration is reached
        {
            virtualCamera.transform.position = Vector3.Lerp(startPos, targetPosition, elapsed / duration); // Interpolate the camera position from start to target position
            elapsed += Time.deltaTime; // Increment elapsed time by the time since the last frame
            yield return null; // Wait for the next frame
        }

        virtualCamera.transform.position = targetPosition; // Ensure it ends exactly at target
    }
}
