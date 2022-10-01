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
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // if so, change position to the left
            pos.x -= speed * Time.deltaTime;
        }

        // Check, if the 'D' key is pressed
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // if so, change position to the right
            pos.x += speed * Time.deltaTime; //toto je cas mezi dvema snimky
        }
        
        // Check, if the 'W' key is pressed
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // if so, change position up
            pos.z += speed * Time.deltaTime; //toto je cas mezi dvema snimky
        }
        
        // Check, if the 'S' key is pressed
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            // if so, change position down
            pos.z -= speed * Time.deltaTime; //toto je cas mezi dvema snimky
        }
        
        pos = EnvironmentProps.Instance.IntoArea(pos, collider.radius, collider.height);

        //set position of gameOjbect to calculated pos
        transform.position = pos;
    }
}
