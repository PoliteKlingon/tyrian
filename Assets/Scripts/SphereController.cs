using System;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public float speed = 5.0f;

    private CapsuleCollider _collider;
    
    [SerializeField]
    public Health health;
    [SerializeField]
    private float collisionDamage = 40.0f;
    
    [SerializeField]
    private GameObject[] leftThrusters;
    [SerializeField]
    private GameObject[] rightThrusters;
    [SerializeField]
    private GameObject[] frontThrusters;
    [SerializeField]
    private GameObject[] backThrusters;
    
    void Awake()
    {
        _collider = GetComponent<CapsuleCollider>();
        if (_collider == null)
        {
            throw new Exception("No collider found!");
        }
    }

    private void SetThrusters(GameObject[] thrusters, bool value)
    {
        foreach (GameObject thruster in thrusters)
        {
            thruster.SetActive(value);
        }
    }
    
    private void Start()
    {
        SetThrusters(frontThrusters, false);
        SetThrusters(backThrusters, false);
        SetThrusters(leftThrusters, false);
        SetThrusters(rightThrusters, false);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Health>().healthBar = UIManager.GetView<HUDView>().GetComponentInChildren<HealthBar>();
        
        SetThrusters(frontThrusters, false);
        SetThrusters(backThrusters, false);
        SetThrusters(leftThrusters, false);
        SetThrusters(rightThrusters, false);
        
        // Save the current position of the gameObject
        Vector3 pos = transform.position;

        // Check, if the 'A' key is pressed
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // if so, change position to the left
            pos.x -= speed * Time.deltaTime;
            SetThrusters(rightThrusters, true);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += speed * Time.deltaTime; 
            
            SetThrusters(leftThrusters, true);
        }
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            pos.z += speed * Time.deltaTime;
            SetThrusters(backThrusters, true);
        }
        
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            pos.z -= speed * Time.deltaTime;
            SetThrusters(frontThrusters, true);
        }
        
        pos = EnvironmentProps.Instance.IntoArea(pos, _collider.radius, _collider.height);

        //set position of gameObject to calculated pos
        transform.position = pos;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Health>()?.DealDamage(collisionDamage);
    }
}
