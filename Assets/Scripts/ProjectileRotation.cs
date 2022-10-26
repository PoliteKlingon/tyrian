using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotation : MonoBehaviour
{
    [SerializeField] 
    private float rotationX = 250;
    [SerializeField] 
    private float rotationY = 0;
    [SerializeField] 
    private float rotationZ = 0;
    private Vector3 _rotation;
    
    private void Start()
    {
        _rotation = new Vector3(rotationX, rotationY,rotationZ);
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_rotation * Time.deltaTime);
    }
}
