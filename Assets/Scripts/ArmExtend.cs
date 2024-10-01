using System.Collections;
using System.Collections.Generic;
using Tobii.G2OM;
using UnityEngine;

public class ArmExtend : MonoBehaviour, IGazeFocusable
{
    [SerializeField] private ArticulationBody shoulder;
    [SerializeField] private ArticulationBody collarBone;
    [SerializeField] private ArticulationBody upperArm;
    [SerializeField] private ArticulationBody lowerArm;
    [SerializeField] private ArticulationBody foreArm;
    public bool exrendArm;
    public bool manualControl;

    private bool _isEnabled = true;
    private void Awake()
    {
        ValidateComponents();
    }

    private void ValidateComponents()
    {
        if (!shoulder || !collarBone || !upperArm || !lowerArm || !foreArm)
        {
            _isEnabled = false;
            Debug.LogError($"Articulation bodies are not assigned!");
        }
    }
    private void extendArm()
    {
        shoulder.SetDriveTarget(ArticulationDriveAxis.X, 20);
        collarBone.SetDriveTarget(ArticulationDriveAxis.X, -50);
        upperArm.SetDriveTarget(ArticulationDriveAxis.X, 0);
        lowerArm.SetDriveTarget(ArticulationDriveAxis.X, -40);
        foreArm.SetDriveTarget(ArticulationDriveAxis.X, -90);
    }

    public void GazeFocusChanged(bool hasFocus)
    {
        if (hasFocus && _isEnabled)
        {
            extendArm();
        }
    }

    public void onEyeContact()
    {
        shoulder.SetDriveTarget(ArticulationDriveAxis.X, 0);
        collarBone.SetDriveTarget(ArticulationDriveAxis.X, 0);
        upperArm.SetDriveTarget(ArticulationDriveAxis.X, 0);
        lowerArm.SetDriveTarget(ArticulationDriveAxis.X, 0);
        foreArm.SetDriveTarget(ArticulationDriveAxis.X, 0);
    }
    void Update()
    {
        if (manualControl)
        {
            if (_isEnabled && exrendArm) { extendArm(); }
            else { onEyeContact(); }
        }
    }
}