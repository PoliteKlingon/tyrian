using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossShip : MonoBehaviour
{
    [SerializeField] private GameObject BombPrefab;
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private GameObject BurstProjectilePrefab;

    private float _delay = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_delay > 0)
        {
            _delay -= Time.deltaTime;
            return;
        }

        Instantiate(BombPrefab, transform.position, Quaternion.Euler(0, 180, 0));
        Instantiate(BurstProjectilePrefab, transform.position, Quaternion.Euler(0, 180, 0));
        Instantiate(ProjectilePrefab, transform.position, Quaternion.Euler(0, 180, 0));
        _delay += 1;
    }
}
