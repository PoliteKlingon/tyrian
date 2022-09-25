using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public float speed = 10;

    private SphereCollider collider;

    void Awake()
    {
        collider = GetComponent<SphereCollider>();
        //here should be some null check...
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
        if (Input.GetKey(KeyCode.A))
        {
            // if so, change position to the left
            pos.x -= speed * Time.deltaTime;
        }

        // Check, if the 'D' key is pressed
        if (Input.GetKey(KeyCode.D))
        {
            // if so, change position to the right
            pos.x += speed * Time.deltaTime; //toto je cas mezi dvema snimky
        }
        
        // Check, if the 'W' key is pressed
        if (Input.GetKey(KeyCode.W))
        {
            // if so, change position up
            pos.z += speed * Time.deltaTime; //toto je cas mezi dvema snimky
        }
        
        // Check, if the 'S' key is pressed
        if (Input.GetKey(KeyCode.S))
        {
            // if so, change position down
            pos.z -= speed * Time.deltaTime; //toto je cas mezi dvema snimky
        }
        
        pos = EnvironmentProps.Instance.IntoArea(pos, collider.radius);

        //set position of gameOjbect to calculated pos
        transform.position = pos;
    }
}
