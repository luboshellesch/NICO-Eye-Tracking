using UnityEngine;

public class TouchDetection : MonoBehaviour
{
    [SerializeField] private GameObject markerPrefab;
    [SerializeField] private GameObject screen;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hello!");
        if (other.CompareTag("IndexFinger"))
        {
            Vector3 touchPosition = other.ClosestPoint(screen.transform.position);
            Instantiate(markerPrefab, touchPosition, Quaternion.identity);
        }
    }
}