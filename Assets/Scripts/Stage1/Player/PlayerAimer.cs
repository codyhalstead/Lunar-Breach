using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimer : MonoBehaviour
{
    [SerializeField] private Transform reticle; // Assign the reticle in the inspector
    [SerializeField] private float orbitRadius = 1.5f;

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {

        if (mainCam == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }
        if (Mouse.current == null)
        {
            Debug.LogError("Mouse.current is null! Are you using the Input System?");
            return;
        }
        if (reticle == null)
        {
            Debug.LogError("Reticle not assigned!");
            return;
        }
        // Get mouse position in world space
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0f;

        // Get direction from player to mouse
        Vector3 direction = (mouseWorldPos - transform.position).normalized;

        // Position the reticle at a fixed radius in that direction
        reticle.position = transform.position + direction * orbitRadius;

        // (Optional) Rotate the reticle if needed
        reticle.up = direction; // If your reticle has orientation (like an arrow)
    }
}
