using UnityEngine;
using System.Collections.Generic;

public class TouchDetection : MonoBehaviour
{
    [SerializeField] private GameObject markerPrefab;
    [SerializeField] private GameObject screen;
    [SerializeField] private List<GameObject> markers = new List<GameObject>();
    private float topSideOfTheScreen;
    private float markerRadiusSquared;

    private void Start()
    {
        topSideOfTheScreen = screen.transform.lossyScale.y / 2;
        markerRadiusSquared = Mathf.Pow(markerPrefab.transform.lossyScale.x, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("IndexFinger")) return;

        Vector3 handDirection = other.transform.position - transform.position;
        Vector3 upperSide = transform.up;

        // Check if the hand enters from the top
        if (Vector3.Dot(upperSide, handDirection) <= 0) return;

        Vector3 touchPosition = other.ClosestPoint(screen.transform.position);
        Vector3 markerPosition = new Vector3(touchPosition.x, screen.transform.position.y + topSideOfTheScreen, touchPosition.z);

        // Only create marker if there are none or it's far enough from the last one
        //if (markers.Count == 0 || (markers.Count > 0 && IsFarEnoughFromLastMarker(markerPosition)))
        //{
        //    AddMarker(markerPosition);
        //}

        // Always keep only one marker
        if (markers.Count > 0)
        {
            Destroy(markers[0]);
            markers.Clear(); 
        }
        AddMarker(markerPosition);
    }

    private bool IsFarEnoughFromLastMarker(Vector3 newMarkerPosition)
    {
        Vector3 lastMarkerPosition = markers[markers.Count - 1].transform.position;
        return (lastMarkerPosition - newMarkerPosition).sqrMagnitude > markerRadiusSquared;
    }

    private void AddMarker(Vector3 position)
    {
        GameObject newMarker = Instantiate(markerPrefab, position, Quaternion.identity);
        markers.Add(newMarker);
    }
}