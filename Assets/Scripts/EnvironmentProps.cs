using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentProps : MonoBehaviour
{
    public static EnvironmentProps Instance {
        get; private set;

    }

    void Awake()
    {
        // Check, if we do not have any instance yet.
        if (Instance == null)
        {
            // 'this' is the first instance created => save it.
            Instance = this;
            // Initialize references to other scripts.
            //InitializeReferences();
        } else if (Instance != this)
        {
            // Destroy 'this' object as there exist another instance
            Destroy(this.gameObject);
        }
    }

    public float minX;
    public float maxX;

    
    public Vector3 IntoArea(Vector3 pos, float dx) //dx = radius
    {
        Vector3 result = pos;
        result.x = result.x - dx < minX ? minX + dx : result.x;
        result.x = result.x + dx > maxX ? maxX - dx : result.x;
        return result;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
