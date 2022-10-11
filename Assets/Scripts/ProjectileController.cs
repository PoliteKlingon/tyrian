using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float _speed = 25.0f;
    private float _radius = 0.25f;
    
    [SerializeField]
    public Health health;
    [SerializeField]
    private float collisionDamage = 40.0f;
    
    // Update is called once per frame
    void Update()
    {
        // move projectile forward
        //transform.position += new Vector3(0, 0, _speed * Time.deltaTime);
        transform.localPosition += transform.forward * _speed * Time.deltaTime;
    }
    
    public void Set(float speed, float radius)
    {
        _speed = speed;
        _radius = radius;
        transform.localScale = new Vector3(_radius, _radius, _radius);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        collision.other.gameObject.GetComponent<Health>()?.DealDamage(collisionDamage);
    }

}