using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraController : MonoBehaviour
{
    public Transform target; // The center of the map you want to look at
    public float distance = 8.0f; // Distance from the target
    public float height = 5.0f; // Height above the target
    public float rotationSpeed = 30.0f; // Speed of rotation
    public float verticalRotationLimit = 30.0f; // Limit of vertical rotation in degrees
    public bool reset;

    public float angleAroundTarget = 0; // Current horizontal angle around the target
    public float angleAboveTarget = 30; // Initial vertical angle above the target
    public GameData gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
    }

    void Start()
    {
        // Initialize camera position
        UpdateCameraPosition();
    }

    void Update()
    {
        if (gameManager.gameState == GameData.GameState.PlayerTurn || gameManager.gameState == GameData.GameState.EnemyTurn)
        {
            float horizontalInput = Input.GetAxis("Horizontal"); // A and D keys for horizontal rotation
            float verticalInput = Input.GetAxis("Vertical"); // W and S keys for vertical rotation

            // Update the angle around the target based on horizontal input
            angleAroundTarget -= horizontalInput * rotationSpeed * Time.deltaTime;

            // Update the angle above the target based on vertical input, clamping to prevent flipping
            angleAboveTarget += verticalInput * rotationSpeed * Time.deltaTime;
            angleAboveTarget = Mathf.Clamp(angleAboveTarget, 0, verticalRotationLimit); // Clamping between 0 and the limit

            UpdateCameraPosition();
        } 
    }

    void UpdateCameraPosition()
    {
        // Calculate rotation
        Quaternion rotation = Quaternion.Euler(angleAboveTarget, angleAroundTarget, 0);

        // Calculate position based on rotation and offsets
        Vector3 positionOffset = rotation * new Vector3(0, height, -distance);
        transform.position = target.position + positionOffset;

        // Always look at the target
        transform.LookAt(target);
    }
}
