using UnityEngine;
using Tobii.G2OM;

public class ArmWave : MonoBehaviour, IGazeFocusable
{
    [SerializeField] private ArticulationBody collarBone;
    [SerializeField] private ArticulationBody upperArm;
    [SerializeField] private ArticulationBody lowerArm;

    [SerializeField] private float maximumWaveAngle = 30f; // Maximum angle for the wave motion
    private const float WavePeriod = Mathf.PI * 2; // Period of the sine wave for a complete wave cycle

    private float _elapsedWaveTime = 0f;  // Timer to control wave position
    private int _totalWaves = 0;          // Counter for number of complete waves
    private bool _isCurrentlyWaving = false; // Indicates if the waving is actively occurring

    // Called when the script instance is being loaded
    private void Awake()
    {
        ValidateArticulationBodies(); 
    }

    // Validates that the articulation bodies are assigned
    private void ValidateArticulationBodies()
    {
        if (!collarBone || !upperArm || !lowerArm)
        {
            Debug.LogError("Articulation bodies for the arm are not assigned.");
            enabled = false;
        }
    }

    // Called when the gaze focus changes
    public void GazeFocusChanged(bool hasFocus)
    {
        if (hasFocus && !_isCurrentlyWaving)
        {
            InitializeArmPosition(); 
            _totalWaves = 0; // Reset wave counter
            _elapsedWaveTime = 0f; // Reset wave timer
            _isCurrentlyWaving = true; 
        }
    }

    // Called once per frame
    private void Update()
    {
        if (_isCurrentlyWaving && _totalWaves < 2)
        {
            PerformWave(); 
        }
        else if (_totalWaves >= 2)
        {
            ReturnToInitialPosition(); 
            _isCurrentlyWaving = false; 
        }
    }

    // Performs the waving motion by updating the lower arm's angle
    private void PerformWave()
    {
        _elapsedWaveTime += Time.deltaTime; 
        float targetWaveAngle = Mathf.Sin(_elapsedWaveTime) * maximumWaveAngle; 

        if (_elapsedWaveTime >= WavePeriod)
        {
            _elapsedWaveTime -= WavePeriod; 
            _totalWaves++; 
        }

        lowerArm.SetDriveTarget(ArticulationDriveAxis.X, targetWaveAngle); 
    }

    // Initializes the arm position before starting the wave
    private void InitializeArmPosition()
    {
        collarBone.SetDriveTarget(ArticulationDriveAxis.X, -90); 
        upperArm.SetDriveTarget(ArticulationDriveAxis.X, -90); 
    }

    // Returns the arm to its initial position after waving
    private void ReturnToInitialPosition()
    {
        lowerArm.SetDriveTarget(ArticulationDriveAxis.X, 0);
        collarBone.SetDriveTarget(ArticulationDriveAxis.X, 0);
        upperArm.SetDriveTarget(ArticulationDriveAxis.X, 0); 
    }
}
