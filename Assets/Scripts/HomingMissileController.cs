using UnityEngine;

public class HomingMissileController : MonoBehaviour
{
    private float _speed = 25.0f;
    private float _radius = 0.25f;
    
    [SerializeField]
    public Health health;
    [SerializeField]
    private float collisionDamage = 40.0f;

    [SerializeField] private float maxSpeed = 30;
    [SerializeField] private float maxAccel = 10;
    private Vector3 velocity;
    
    // Update is called once per frame
    void Update()
    {
        var ship = GameObject.Find("Ship");
        if (ship is null)
        {
            Debug.Log("Player not found!");
            return;
        }

        velocity = GameUtils.ComputeSeekVelocity(
            transform.position, velocity,
            maxSpeed, maxAccel,
            ship.transform.position,
            Time.deltaTime
        );
        transform.position = GameUtils.ComputeEulerStep(
            transform.position, velocity, Time.deltaTime
        );
    }
    
    public void Set(float speed, float radius)
    {
        _speed = speed;
        _radius = radius;
        transform.localScale = new Vector3(_radius, _radius, _radius);
        velocity = new Vector3(0, 0, -20);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Health>()?.DealDamage(collisionDamage);
    }
    
    

}