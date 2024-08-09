using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SceneControl : MonoBehaviour
{
    // Function to quit the application
    public void QuitGame()
    {

        // If the game is running in the editor, stop playing the scene
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        // If the game is running as a build, quit the application
        Application.Quit();
        #endif
    }

    private Dictionary<GameObject, TransformData> initialPositions = new Dictionary<GameObject, TransformData>();

    void Start()
    {
        // Store initial positions, rotations, and Rigidbody states of all children objects
        foreach (Transform child in transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            initialPositions[child.gameObject] = new TransformData(child.position, child.rotation, rb);
        }
    }

    // This method resets all stored objects to their original positions, rotations, and stops their motion
    public void ResetPositions()
    {
        foreach (var item in initialPositions)
        {
            item.Key.transform.position = item.Value.Position;
            item.Key.transform.rotation = item.Value.Rotation;
            if (item.Value.Rigidbody != null)
            {
                item.Value.Rigidbody.velocity = Vector3.zero;
                item.Value.Rigidbody.angularVelocity = Vector3.zero;
            }
        }
    }

    // A class to store position, rotation data and Rigidbody reference
    private class TransformData
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Rigidbody Rigidbody;

        public TransformData(Vector3 position, Quaternion rotation, Rigidbody rb)
        {
            Position = position;
            Rotation = rotation;
            Rigidbody = rb;
        }
    }
}

