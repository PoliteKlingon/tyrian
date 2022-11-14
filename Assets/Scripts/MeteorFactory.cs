using UnityEngine;

public class MeteorFactory : MonoBehaviour
{
    public static MeteorFactory Instance {
        get; private set;

    }

    [SerializeField] private int meteorsToDestroy = 0;
    private int _meteorsLeftToDestroy;
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
        _meteorsLeftToDestroy = meteorsToDestroy;
    }

    public void MeteorDestroyed()
    {
        _meteorsLeftToDestroy--;
    }

    public bool ALlMeteorsDestroyed()
    {
        return _meteorsLeftToDestroy == 0;
    }

    public void ResetMeteorsToDestroy()
    {
        _meteorsLeftToDestroy = meteorsToDestroy;
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
