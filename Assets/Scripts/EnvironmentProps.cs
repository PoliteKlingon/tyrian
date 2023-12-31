using System;
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

    public float sizeX;
    public float sizeZ;
    
    public float minX() { return -sizeX / 2.0f; }
    public float maxX() { return sizeX / 2.0f; }
    public float minZ() { return -sizeZ / 2.0f; }
    public float maxZ() { return sizeZ / 2.0f; }
    
    public Vector3 IntoArea(Vector3 pos, float dx, float dz)
    {
        Vector3 result = pos;
        result.x = result.x - dx < minX() ? minX() + dx : result.x;
        result.x = result.x + dx > maxX() ? maxX() - dx : result.x;
		result.z = result.z - dz < minZ() ? minZ() + dz : result.z;
		result.z = result.z + dz > maxZ() ? maxZ() - dz : result.z;
        return result;
    }

    public Vector3 IntoArea(Vector3 pos)
    {
        return new Vector3(
            Math.Clamp(pos.x, minX(), maxX()),
            0,
            Math.Clamp(pos.z, minZ(), maxZ())
            );
    }
}
