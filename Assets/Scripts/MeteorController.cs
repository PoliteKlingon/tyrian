using UnityEngine;

public class MeteorController : MonoBehaviour
{
    private float _speed = 20.0f;
    private float _radius = 1.0f;
    
    [SerializeField]
    public Health health;
    [SerializeField]
    private float collisionDamage = 40.0f;

    // Update is called once per frame
    void Update()
    {
        // move meteor down
        transform.position += new Vector3(0, 0, -_speed * Time.deltaTime);
    }
    
    public void Set(float speed, float radius)
    {
        _speed = speed;
        _radius = radius;
        transform.localScale = new Vector3(_radius, _radius, _radius);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Health>()?.DealDamage(collisionDamage);
    }
}
