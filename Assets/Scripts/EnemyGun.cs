using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    // reference to prefab
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float delay = 0.25f;
    
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip gunSound;
    
    [SerializeField]
    private float _projectileRadius = 1.00f;
    [SerializeField]
    private float _projectileSpeed = 20.0f;

    private float _delay;

    private BoxCollider _collider;

    void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        if (_collider == null)
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
        // time elapsed from previous frame
        _delay -= Time.deltaTime;
        if (_delay > 0.0f)
            return;
        //choose position for new spawn
        //horizontal
        float x = this.gameObject.transform.position.x;
        //vertical
        float z = _collider == null
            ? this.gameObject.transform.position.z
            : this.gameObject.transform.position.z - _collider.size.z / 2 - _collider.center.z;

        // set new delay for next spawn
        _delay = delay;

        source.volume = Random.Range(0.8f, 1.1f);
        source.pitch = Random.Range(0.6f, 1.0f);
        
        if (source != null && gunSound != null)
            source.PlayOneShot(gunSound);
        
        // create new instance of prefab at given position
        var projectileGO = Instantiate(projectilePrefab, new Vector3(x, 0, z), Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 1.0f, 0.0f)));
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

    public void Set(float shootDelay)
    {
        this.delay = shootDelay;
    }
}
