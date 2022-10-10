using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory Instance {
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
    
    // reference to prefab
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    private float _shootDelayMin;
    [SerializeField]
    private float _shootDelayMax;
    
    [SerializeField]
    private int vesselNumMax = 10;
    
    public int VesselNum {get; set; }
    
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
        float x = EnvironmentProps.Instance.minX();
//vertical
        float z = EnvironmentProps.Instance.maxZ();
		
//set speed and size of meteor:
        var shootDelay = Random.Range(_shootDelayMin, _shootDelayMax);

// set new delay for next spawn
        _delay = spawnDelay;

        if (VesselNum == vesselNumMax)
        {
            return;
        }
        
        // create new instance of prefab at given position
        var enemyGO = Instantiate(enemyPrefab, new Vector3(x, 0, z),
            Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 1.0f, 0.0f)));
        VesselNum++;
        //Debug.Log("New enemy spawned at: " + enemyGO.transform.position);
        var enemyGun = enemyGO.GetComponent<EnemyGun>();
        if (enemyGun != null)
        {
            enemyGun.Set(shootDelay);
        }
        else
        {
            Debug.LogError("Missing EnemyController component");
        }
    }
}

