using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR.Input;


[RequireComponent(typeof(LineRenderer))]
public class LineConnector : MonoBehaviour
{
    public Transform pointA; // Assign the first point in the Unity Inspector
    public Transform pointB; // Assign the second point in the Unity Inspector
    public float extensionLength = 0.3f; // Length to extend the line beyond pointB
    public bool crossedSmth = false; // This will be set to true if the line crosses an object
    public bool isRight;

    [SerializeField] private string selectableTag = "selectable";
    [SerializeField] private Material highlightMaterial;
    
    private Material _defaultMaterial;
    private Transform _selection;

    private LineRenderer _lineRenderer;
    private Vector3 _pointC; // The extended point
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (pointA != null && pointB != null)
        {
            bool triggerPressed = CheckTriggerPressed();

            if (triggerPressed)
            {
                _lineRenderer.enabled = true;
                CalculateExtendedPoint();
                CheckLineIntersection();
                DrawLine();
            }
            else
            {
                _lineRenderer.enabled = false;
            }
        }
    }
    bool CheckTriggerPressed()
    {
        bool leftTriggerPressed = false;
        bool rightTriggerPressed = false;

        //// Check left hand trigger
        var leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out leftTriggerPressed);

        // Check right hand trigger
        var rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out rightTriggerPressed);

        if (isRight)
        {
            return rightTriggerPressed;

        }
        return leftTriggerPressed;
    }
    void CalculateExtendedPoint()
    {
        Vector3 direction = (pointB.position - pointA.position).normalized;
        _pointC = pointB.position + direction * extensionLength;
    }

    void CheckLineIntersection()
    {
        ResetSelection();

        Vector3 direction = pointB.position - pointA.position;
        float distance = Vector3.Distance(pointA.position, _pointC);
        RaycastHit[] hits = Physics.RaycastAll(pointA.position, direction, distance);

        if (hits.Length > 0)
        {
            crossedSmth = true;
            ProcessHits(hits);
        }
    }

    void ResetSelection()
    {
        crossedSmth = false;

        if (_selection != null && _defaultMaterial != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = _defaultMaterial;
            _selection = null;
        }
    }

    void ProcessHits(RaycastHit[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            var selection = hit.transform;

            if (selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    _defaultMaterial = selectionRenderer.material;
                    selectionRenderer.material = highlightMaterial;
                }

                _selection = selection;
                break;
            }
        }
    }


    void DrawLine()
    {
        _lineRenderer.SetPosition(0, pointB.position);
        _lineRenderer.SetPosition(1, _pointC);
        _lineRenderer.startColor = Color.red;
        _lineRenderer.endColor = Color.red;
        _lineRenderer.startWidth = 0.0025f;
        _lineRenderer.endWidth = 0.0025f;
    }
}
