using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR.Input;

[RequireComponent(typeof(LineRenderer))]
public class LineConnector : MonoBehaviour
{
    public Transform pointA; // Assign the first point in the Unity Inspector
    public Transform pointB; // Assign the second point in the Unity Inspector
    public float extensionLength = 0.3f; // Length to extend the line beyond pointB
    public bool crossedSomething = false; // This will be set to true if the line crosses an object
    public bool rightController;

    [SerializeField] private Material highlightMaterial;
    [SerializeField] private GameObject markerPrefab;
    [SerializeField] private GameObject screen;
    private float _topSideOfTheScreen;
    private GameObject _marker = null;

    private Material _defaultMaterial;
    private Transform _selection;

    private LineRenderer _lineRenderer;
    private Vector3 _pointC; // The extended point
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _topSideOfTheScreen = screen.transform.lossyScale.y / 2;
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
                if (_marker)
                {
                    Destroy(_marker);
                }
            }
        }
    }
    bool CheckTriggerPressed()
    {
        bool leftTriggerPressed = false;
        bool rightTriggerPressed = false;

        if (rightController)
        {
            var rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out rightTriggerPressed);
            return rightTriggerPressed;

        }
        var leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out leftTriggerPressed);
        return leftTriggerPressed;
    }
    void CalculateExtendedPoint()
    {
        Vector3 direction = (pointB.position - pointA.position).normalized;
        _pointC = pointB.position + direction * extensionLength;
    }

    void CheckLineIntersection()
    {
        //ResetSelection();

        Vector3 direction = pointB.position - pointA.position;
        float distance = Vector3.Distance(pointA.position, _pointC);
        RaycastHit[] hits = Physics.RaycastAll(pointA.position, direction, distance);

        if (hits.Length > 0)
        {
            crossedSomething = true;
            ProcessHits(hits);
        }
    }

    //void ResetSelection()
    //{
    //    crossedSomething = false;

    //    if (_selection != null && _defaultMaterial != null)
    //    {
    //        var selectionRenderer = _selection.GetComponent<Renderer>();
    //        selectionRenderer.material = _defaultMaterial;
    //        _selection = null;
    //    }
    //}

    void ProcessHits(RaycastHit[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            var selection = hit.transform;
            // Lighting up gameObjects with tag selectable
            //if (selection.CompareTag("selectable"))
            //{
            //    var selectionRenderer = selection.GetComponent<Renderer>();
            //    if (selectionRenderer != null)
            //    {
            //        _defaultMaterial = selectionRenderer.material;
            //        selectionRenderer.material = highlightMaterial;
            //    }

            //    _selection = selection;
            //    break;
            //}

            // Showing marker at the point of cross between ray and screen.
            if (selection.CompareTag("screen")) // Assuming "Screen" is the tag of your screen object
            {
                Vector3 hitPosition = hit.point;
                Vector3 markerPosition = new Vector3(hitPosition.x, screen.transform.position.y + _topSideOfTheScreen, hitPosition.z);
                if (_marker)
                {
                    Destroy(_marker);
                }
                _marker = Instantiate(markerPrefab, markerPosition, Quaternion.identity);
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
