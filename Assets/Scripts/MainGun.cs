using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MainGun : MonoBehaviour
{
    // reference to prefab
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float delay = 0.25f;
    
    [SerializeField]
    private float _projectileRadius = 0.25f;
    [SerializeField]
    private float _projectileSpeed = 20.0f;

    private float _delay;

    private CapsuleCollider collider;

    void Awake()
    {
        collider = GetComponent<CapsuleCollider>();
        if (collider == null)
        {
            Debug.Log("No collider found!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _delay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // time elapsed from previous frame
            _delay -= Time.deltaTime;
            if (_delay > 0.0f)
                return;
            //choose position for new spawn
            //horizontal
            float x = this.gameObject.transform.position.x;
            //vertical
            float z = collider == null
                ? this.gameObject.transform.position.z
                : this.gameObject.transform.position.z + collider.height / 2 + collider.center.z;

            // set new delay for next spawn
            _delay = delay;

            // create new instance of prefab at given position
            var projectileGO = Instantiate(projectilePrefab, new Vector3(x, 0, z), Quaternion.identity);
            //Debug.Log("New projectile shot at: " + projectileGO.transform.position);
            var projectileContr = projectileGO.GetComponent<ProjectileController>();
            if (projectileContr != null)
            {
                projectileContr.Set(_projectileSpeed, _projectileRadius);
            }
            else
            {
                Debug.LogError("Missing ProjectileController component");
            }
        }
    }
}
