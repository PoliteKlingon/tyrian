using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float _speed = 25.0f;
    private float _radius = 0.25f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move projectile up
        transform.position += new Vector3(0, 0, _speed * Time.deltaTime);
// destroy it on border
        if (EnvironmentProps.Instance.escapedAbove(transform.position, _radius))
        {
            Destroy(this.gameObject);
        }
    }
    
    public void Set(float speed, float radius)
    {
        _speed = speed;
        _radius = radius;
        transform.localScale = new Vector3(_radius, _radius, _radius);
    }
}