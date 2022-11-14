using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float _speed = 5.0f;

    private bool _goingRight = true;
    
    [SerializeField]
    public Health health;
    [SerializeField]
    private float collisionDamage = 40.0f;

    // Update is called once per frame
    void Update()
    {
        // move ship
        if ((transform.position.x >= EnvironmentProps.Instance.maxX() && !_goingRight) ||
            (transform.position.x <= EnvironmentProps.Instance.minX() && _goingRight))
        {
            transform.position += new Vector3(0, 0, -1);
            _goingRight = !_goingRight;
        }

        transform.position += new Vector3((_goingRight ? -1 : 1) * _speed * Time.deltaTime, 0, 0);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        collision.other.gameObject.GetComponent<Health>()?.DealDamage(collisionDamage);
    }

    private void OnDestroy()
    {
        if (EnemyFactory.Instance is not null) EnemyFactory.Instance.VesselNum--;
    }

    public void SetSpeed(float speed)
    {
        this._speed = speed;
    }
}
