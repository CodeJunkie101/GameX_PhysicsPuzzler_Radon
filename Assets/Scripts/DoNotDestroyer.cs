using UnityEngine;

public class DontDestroyUniversal : MonoBehaviour
{
    [Tooltip("Unique ID to prevent duplicates (e.g., 'GameManager', 'UI', 'Audio')")]
    public string uniqueID = "Default";

    private static System.Collections.Generic.Dictionary<string, DontDestroyUniversal> instances
        = new System.Collections.Generic.Dictionary<string, DontDestroyUniversal>();

    void Awake()
    {
        // If ID is empty, fallback to object name
        if (string.IsNullOrEmpty(uniqueID))
        {
            uniqueID = gameObject.name;
        }

        // Check if an instance with same ID already exists
        if (instances.ContainsKey(uniqueID))
        {
            if (instances[uniqueID] != this)
            {
                Destroy(gameObject); // kill duplicate
                return;
            }
        }
        else
        {
            instances.Add(uniqueID, this);
        }

        DontDestroyOnLoad(gameObject);
    }
}