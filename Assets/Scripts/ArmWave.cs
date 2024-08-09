using UnityEngine;
using Tobii.G2OM;
using System.Collections.Generic;

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
    private bool _isInitialized = false;  // Flag to ensure initialization

    private void Awake()
    {
        ValidateArticulationBodies();
    }

    private void ValidateArticulationBodies()
    {
        if (!collarBone || !upperArm || !lowerArm)
        {
            Debug.LogError("Articulation bodies for the arm are not assigned.");
            enabled = false;
        }
    }

    public void GazeFocusChanged(bool hasFocus)
    {
        if (hasFocus && !_isCurrentlyWaving)
        {
            InitializeArmPosition();
            _totalWaves = 0; // Reset wave counter
            _elapsedWaveTime = 0f; // Reset wave timer
            _isInitialized = true; // Set initialization flag
        }
    }

    private void Update()
    {
        if (_isCurrentlyWaving && _totalWaves < 2)
        {
            if (_isInitialized) // Ensure initialization before waving
            {
                PerformWave();
            }
        }
        else if (_totalWaves >= 2)
        {
            ReturnToInitialPosition();
            _isCurrentlyWaving = false;
            _isInitialized = false; // Reset initialization flag
        }
    }

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

    private void InitializeArmPosition()
    {
        collarBone.SetDriveTarget(ArticulationDriveAxis.X, -90);
        upperArm.SetDriveTarget(ArticulationDriveAxis.X, -90);
        _isCurrentlyWaving = true;
    }

    private void ReturnToInitialPosition()
    {
        lowerArm.SetDriveTarget(ArticulationDriveAxis.X, 0);
        collarBone.SetDriveTarget(ArticulationDriveAxis.X, 0);
        upperArm.SetDriveTarget(ArticulationDriveAxis.X, 0);
    }
}
