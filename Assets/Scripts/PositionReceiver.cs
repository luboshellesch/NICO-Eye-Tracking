using UnityEngine;

public class PositionReceiver : MonoBehaviour
{
    [SerializeField]
    private ArticulationBody headArticulation;
    [SerializeField]
    private ArticulationBody neckArticulation;
    [SerializeField]
    private Transform head;

    private const float MaxVerticalAngle = 40f;
    private const float MinVerticalAngle = -40f;
    private const float MaxHorizontalAngle = 30f;
    private const float MinHorizontalAngle = -30f;
    private Vector3 lastReportedPosition = Vector3.positiveInfinity;

    // Called when the script instance is being loaded
    private void Awake()
    {
        ValidateComponents(); 
        InitializeArticulation(); 
    }

    // Validates that the required components are assigned
    private void ValidateComponents()
    {
        if (!headArticulation|| !neckArticulation)
        {
            Debug.LogError($"Articulation bodies are not assigned on {gameObject.name}");
            this.enabled = false; 
        }
    }

    // Initializes the articulation bodies with default drive targets
    private void InitializeArticulation()
    {
        headArticulation.SetDriveTarget(ArticulationDriveAxis.X, 0);
        neckArticulation.SetDriveTarget(ArticulationDriveAxis.X, 0);
    }

    // Called when a position report is received
    public void OnPositionReport(Transform targetObject)
    {
        if (!targetObject)
        {
            Debug.LogError($"Target object is not assigned when reported to {gameObject.name}");
            this.enabled = false; 
        }

        if (lastReportedPosition == targetObject.position)
        {
            this.enabled = false;
        }

        lastReportedPosition = targetObject.position; 
        var angles = CalculateMovement(targetObject);

        // Update the drive targets for head and neck articulation
        headArticulation.SetDriveTarget(ArticulationDriveAxis.X, angles.VerticalAngle);
        neckArticulation.SetDriveTarget(ArticulationDriveAxis.X, angles.HorizontalAngle);
    }

    // Calculates the movement angles based on the target object's position
    private (float HorizontalAngle, float VerticalAngle) CalculateMovement(Transform targetObject)
    {
        Vector3 direction = targetObject.position - head.position; 
        if (direction.sqrMagnitude < 0.001f)
        {
            return (0, 0); // No significant movement
        }

        float horizontalAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float distance = new Vector2(direction.x, direction.z).magnitude;
        float verticalAngle = -Mathf.Atan2(direction.y, distance) * Mathf.Rad2Deg;
        horizontalAngle *= -1f;

        // Clamp the angles to their respective limits
        return (
            ClampAngle(horizontalAngle, MinHorizontalAngle, MaxHorizontalAngle),
            ClampAngle(verticalAngle, MinVerticalAngle, MaxVerticalAngle)
        );
    }

    // Clamps an angle between a minimum and maximum value
    private float ClampAngle(float angle, float min, float max)
    {
        return Mathf.Clamp(angle, min, max);
    }
}
