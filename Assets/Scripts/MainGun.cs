using Unity.VisualScripting;
using UnityEngine;

public class MainGun : MonoBehaviour
{
    // reference to prefab
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float delay = 0.25f;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip gunSound;
    
    [SerializeField]
    private float _projectileRadius = 0.25f;
    [SerializeField]
    private float _projectileSpeed = 20.0f;

    private float _delay;

    private CapsuleCollider _collider;

    void Awake()
    {
        _collider = GetComponent<CapsuleCollider>();
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
        if (Input.GetKey(KeyCode.Space) 
#if UNITY_ANDROID
            || true
#endif
            )
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
                : this.gameObject.transform.position.z + _collider.height / 2 + _collider.center.z;

            // set new delay for next spawn
            _delay = delay;
            source.pitch = Random.Range(1.00f, 1.15f);
            source.volume = Random.Range(0.7f, 1.1f);
            source.PlayOneShot(gunSound);
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
