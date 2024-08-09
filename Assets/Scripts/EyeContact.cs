using UnityEngine;
using Tobii.G2OM;

public class EyeContact : MonoBehaviour, IGazeFocusable
{
    // Serialized field for position reporting event
    [SerializeField] private PositionEvent onPositionReport;
    [SerializeField] private EyeContactEvent onEyeContact;
    [SerializeField] private GameObject mainCamera; 

    private bool _isEnabled = true; 

    // Called when the script instance is being loaded
    private void Awake()
    {
        ValidateCamera(); 
        InitializeEvents(); 
    }

    // Checks if the main camera is assigned
    private void ValidateCamera()
    {
        if (!mainCamera)
        {
            Debug.LogError("Main Camera not found! Please assign a Main Camera in the inspector.");
            _isEnabled = false; 
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

    bool hasFocus = false;
    // Called when the gaze focus changes
    public void GazeFocusChanged(bool hasFocus)
    {
        this.hasFocus = hasFocus;
    }

    // Continuously reports the position while the object has focus
    private void Update()
    {
        if (hasFocus && _isEnabled)
        {
            ReportEyeContact();
        }
    }

    // Reports the position of the camera by invoking the position report event
    private void ReportEyeContact()
    {
        Transform cameraPosition = mainCamera.transform;
        onPositionReport.Invoke(cameraPosition);
        onEyeContact.Invoke();
    }
}
