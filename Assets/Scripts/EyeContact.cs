using UnityEngine;
using Tobii.G2OM;

public class EyeContact : MonoBehaviour, IGazeFocusable
{
    // Serialized field for position reporting event
    [SerializeField] private PositionEvent onPositionReport;

    // Called when the script instance is being loaded
    private void Awake()
    {
        ValidateCamera(); 
        InitializeEvents(); 
    }

    // Checks if the main camera is assigned
    private void ValidateCamera()
    {
        if (!gameObject)
        {
            Debug.LogError("Main Camera not found! Please assign a Main Camera in the inspector.");
            enabled = false; 
        }
    }

    // Initializes the position report event if not already set
    private void InitializeEvents()
    {
        if (onPositionReport == null)
        {
            onPositionReport = new PositionEvent(); 
        }
    }

    // Called when the gaze focus changes
    public void GazeFocusChanged(bool hasFocus)
    {
        ReportPosition(); 
    }

    // Reports the position of the camera by invoking the position report event
    private void ReportPosition()
    {
        Transform cameraPosition = gameObject.transform; 
        onPositionReport.Invoke(cameraPosition); 
    }
}
