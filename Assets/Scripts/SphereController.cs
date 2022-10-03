using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public float speed = 5;

    private CapsuleCollider collider;

    void Awake()
    {
        collider = GetComponent<CapsuleCollider>();
        if (collider == null)
        {
            throw new Exception("No collider found!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Save the current position of the gameObject
        Vector3 pos = transform.position;

        // Check, if the 'A' key is pressed
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // if so, change position to the left
            pos.x -= speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += speed * Time.deltaTime; 
        }
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            pos.z += speed * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            pos.z -= speed * Time.deltaTime;
        }
        
        pos = EnvironmentProps.Instance.IntoArea(pos, collider.radius, collider.height);

        //set position of gameOjbect to calculated pos
        transform.position = pos;
    }
}
