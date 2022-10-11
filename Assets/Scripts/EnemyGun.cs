using UnityEngine;

public class EnemyGun : MonoBehaviour
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

    private BoxCollider collider;

    void Awake()
    {
        collider = GetComponent<BoxCollider>();
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
            : this.gameObject.transform.position.z - collider.size.z / 2 - collider.center.z;

        // set new delay for next spawn
        _delay = delay;

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
