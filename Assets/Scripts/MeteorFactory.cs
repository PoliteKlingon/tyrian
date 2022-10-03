using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorFactory : MonoBehaviour
{
    // reference to prefab
    [SerializeField]
    private GameObject meteorPrefab;
    [SerializeField]
    private float delayMin;
    [SerializeField]
    private float delayMax;
    [SerializeField]
    private float _meteorRadius;
    [SerializeField]
    private float _meteorSpeed;
    [SerializeField]
    private bool randomize;
// delay from last spawn
    private float _delay;
    
    // Start is called before the first frame update
    void Start()
    {
        _delay = 0;
    }

    // Update is called once per frame
    void Update()
    {
    
// time elapsed from previous frame
        _delay -= Time.deltaTime;
        if (_delay > 0.0f)
            return;
//choose position for new spawn
//horizontal
        float x = Random.Range(
            EnvironmentProps.Instance.minX() + _meteorRadius,
            EnvironmentProps.Instance.maxX() - _meteorRadius
        );
//vertical
        float z = EnvironmentProps.Instance.maxZ() + _meteorRadius;
		
//set speed and size of meteor:
        var randomness = Random.Range(0.5f, 1.5f);
		var meteorSpeed = randomize ? _meteorSpeed / randomness : _meteorSpeed;
		var meteorRadius = randomize ? _meteorRadius * randomness : _meteorRadius;

// set new delay for next spawn
        _delay = Random.Range(delayMin, delayMax);

        // create new instance of prefab at given position
        var meteorGO = Instantiate(meteorPrefab, new Vector3(x, 0, z + 2),
            Quaternion.identity);
        //Debug.Log("New meteor spawned at: " + meteorGO.transform.position);
        var meteorContr = meteorGO.GetComponent<MeteorController>();
        if (meteorContr != null)
        {
            meteorContr.Set(meteorSpeed, meteorRadius);
        }
        else
        {
            Debug.LogError("Missing MeteorController component");
        }
    }
}
