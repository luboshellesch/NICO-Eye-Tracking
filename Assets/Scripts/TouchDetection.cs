using UnityEngine;

public class TouchDetection : MonoBehaviour
{
    [SerializeField] private GameObject markerPrefab;
    [SerializeField] private GameObject screen;
    private const float heightOfMarker = 0.00455f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("IndexFinger"))
        {
            Vector3 touchPosition = other.ClosestPoint(screen.transform.position);
            Vector3 markerPosition = new Vector3(touchPosition.x, screen.transform.position.y + heightOfMarker,touchPosition.z);
            Instantiate(markerPrefab, markerPosition, Quaternion.identity);
        }
    }
}