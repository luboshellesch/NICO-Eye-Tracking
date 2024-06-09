using UnityEngine;
using UnityEngine.Events;
using Tobii.G2OM;

public class PositionReporter : MonoBehaviour, IGazeFocusable
{
    [SerializeField]
    private PositionEvent onPositionReport;

    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
    [SerializeField]
    private Color highlightColor = Color.yellow;
    [SerializeField]
    private float animationTime = 0.1f;

    private Renderer _renderer;
    private Color _originalColor;
    private Color _targetColor;

    // Called when the script instance is being loaded
    private void Awake()
    {
        InitializeRenderer(); 
        InitializePositionEvent(); 
    }

    // Initializes the renderer and sets up color variables
    private void InitializeRenderer()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer == null)
        {
            Debug.LogError($"Renderer component missing on {gameObject.name}");
            this.enabled = false; 
        }

        _originalColor = _renderer.material.color; 
        _targetColor = _originalColor; 
    }

    // Initializes the position report event if not already set
    private void InitializePositionEvent()
    {
        if (onPositionReport == null)
        {
            onPositionReport = new PositionEvent(); 
        }
    }

    // Called when the gaze focus changes
    public void GazeFocusChanged(bool hasFocus)
    {
        _targetColor = hasFocus ? highlightColor : _originalColor;
        if (hasFocus)
        {
            onPositionReport.Invoke(gameObject.transform); 
        }
    }

    // Called once per frame
    private void Update()
    {
        UpdateColorTransition(); 
    }

    // Handles the transition of the material color towards the target color
    private void UpdateColorTransition()
    {
        if (_renderer.material.HasProperty(BaseColorID))
        {
            _renderer.material.SetColor(BaseColorID, Color.Lerp(_renderer.material.GetColor(BaseColorID), _targetColor, Time.deltaTime / animationTime));
        }
        else
        {
            _renderer.material.color = Color.Lerp(_renderer.material.color, _targetColor, Time.deltaTime / animationTime);
        }
    }
}
