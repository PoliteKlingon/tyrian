using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossShip : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject burstProjectilePrefab;
    
    [SerializeField] private float collisionDamage = 50.0f;
    
    enum State
    {
        ENTER_GAME_ZONE,
        NEAR,
        FAR
    }
    private State _activeState = State.ENTER_GAME_ZONE;
    
    [SerializeField] private float minReactionDelay = 0.1f;
    [SerializeField] private float maxReactionDelay = 0.2f;
    private float _reactionDelay = 0.0f;
    
    private bool _gameZoneEntered = false;
    [SerializeField] private int numLongShotsToCooldown = 7;
    private int _numLongShots = 0;

    private void SelectState()
    {
        if (!_gameZoneEntered)
            _activeState = State.ENTER_GAME_ZONE;
        else if (_numLongShots < numLongShotsToCooldown)
            _activeState = State.FAR;
        else
            _activeState = State.NEAR;
    }
    
    public float maxAccel = 20.0f;
    public float maxSpeed = 5.0f;
    private Vector3 velocity = Vector3.zero;
    
    private void Process_ENTER_GAME_ZONE()
    {
        EnvironmentProps env = EnvironmentProps.Instance;
        Vector3 target = new Vector3(
            0.5f * (env.minX() + env.maxX()),
            0.0f,
            env.minZ() + 0.75f * (env.maxZ() - env.minZ())
        );
        velocity = GameUtils.ComputeSeekVelocity(
            transform.position, velocity,
            maxSpeed, maxAccel,
            target, Time.deltaTime
        );
        transform.position = GameUtils.ComputeEulerStep(
            transform.position, velocity, Time.deltaTime
        );
        if ((target - transform.position).sqrMagnitude < 1.0f)
            _gameZoneEntered = true;
    }
    
    [SerializeField] private float shortFirePointShiftZ = 8.0f;
    private Vector3 _enemyPosition = Vector3.zero;
    
    private void Process_SHORT()
    {
        velocity = GameUtils.ComputeSeekVelocity(
            transform.position, velocity,
            maxSpeed, maxAccel,
            _enemyPosition + new Vector3(0, 0, shortFirePointShiftZ),
            Time.deltaTime
        );
        transform.position = GameUtils.ComputeEulerStep(
            transform.position, velocity, Time.deltaTime
        );
        transform.position = EnvironmentProps.Instance.IntoArea(
            transform.position
        );
        if ((transform.position - _enemyPosition).magnitude < shortFirePointShiftZ + 1)
        {
            shoot();
        }
    }
    
    private void ScanEnvironment()
    {
        var ship = GameObject.Find("Ship");
        if (ship != null)
            _enemyPosition = ship.transform.position;
        else
            Debug.Log("ship is null!");
    }
    
    private void Process_LONG()
    {
        velocity = GameUtils.ComputeSeekVelocity(
            transform.position, velocity,
            maxSpeed, maxAccel,
            new Vector3(
                Random.Range(EnvironmentProps.Instance.minX(), EnvironmentProps.Instance.maxX()), 
                0, 
                EnvironmentProps.Instance.maxZ() - 2.0f),
            Time.deltaTime
        );
        transform.position = GameUtils.ComputeEulerStep(
            transform.position, velocity, Time.deltaTime
        );
        transform.position = EnvironmentProps.Instance.IntoArea(
            transform.position
        );
        
        shoot();
    }
    
    [SerializeField] private float shortReloadSeconds = 0.75f;
    private float _shortReload = 0.0f;
    [SerializeField] private float longReloadSeconds = 0.5f;
    private float _longReload = 0.0f;
    
    [SerializeField] private float burstCooldown = 10.0f;
    private float _burstCooldown;
    
    [SerializeField] private float longCooldown = 6.0f;
    private float _longCooldown;
    //private Transform _gun;
    
    private void ProcessGunTimers()
    {
        if (_numLongShots == numLongShotsToCooldown)
        {
            _longCooldown -= Time.deltaTime;
            if (_longCooldown <= 0.0f)
            {
                _longCooldown = longCooldown;
                _longReload = 1 + longReloadSeconds;
                _numLongShots = 0;
            }
        }
        else if (_longReload > 0.0f)
            _longReload -= Time.deltaTime;
        
        if (_shortReload > 0.0f)
            _shortReload -= Time.deltaTime;

        _burstCooldown -= Time.deltaTime;
    }
    
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip longShotSound;
    [SerializeField] private AudioClip shortShotSound;
    [SerializeField] private AudioClip burstShotSound;
    
    private void BurstShot()
    {
        source.volume = Random.Range(0.8f, 1.1f);
        source.pitch = Random.Range(1.0f, 1.2f);

        for (float i = 0; i < 360; i += 12)
        {
            Instantiate(burstProjectilePrefab, transform.position, Quaternion.Euler(0, i, 0));                
        }
        if (source != null && burstShotSound != null)
            source.PlayOneShot(burstShotSound);
    }

    private void LongRangeShot()
    {
        source.volume = Random.Range(0.8f, 1.1f);
        source.pitch = Random.Range(1.0f, 1.2f);
        
        Instantiate(bombPrefab, transform.position, Quaternion.Euler(0, 180, 0));
        if (source != null && longShotSound != null)
            source.PlayOneShot(longShotSound);
    }
    
    private void ShortRangeShot()
    {
        source.volume = Random.Range(0.8f, 1.1f);
        source.pitch = Random.Range(1.0f, 1.2f);
        Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 180, 0));
        if (source != null && shortShotSound != null)
            source.PlayOneShot(shortShotSound);
    }

    private void shoot()
    {
        if (_activeState == State.FAR)
        {
            if (_longReload <= 0.0f)
            {
                LongRangeShot();
                ++_numLongShots;
                _longReload = longReloadSeconds;
            }
        }
        else if (_activeState == State.NEAR)
        {
            if (_shortReload <= 0.0f)
            {
                ShortRangeShot();
                _shortReload = shortReloadSeconds;
            }
        }
        else
        {
            Debug.Log("Trying to shoot in state " + _activeState);
        }
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _longCooldown = longCooldown;
        _burstCooldown = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_activeState != State.ENTER_GAME_ZONE && _burstCooldown <= 0.0f)
        {
            BurstShot();
            _burstCooldown = burstCooldown;
        }

        _reactionDelay -= Time.deltaTime;
        if (_reactionDelay <= 0.0f)
        {
            _reactionDelay = Random.Range(minReactionDelay, maxReactionDelay);
            ScanEnvironment();
            SelectState();
        }

        ProcessGunTimers();
        switch (_activeState)
        {
            case State.ENTER_GAME_ZONE:
                Process_ENTER_GAME_ZONE();
                break;
            case State.NEAR:
                Process_SHORT();
                break;
            case State.FAR:
                Process_LONG();
                break;
            default:
                Debug.Assert(false);
                break;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Health>()?.DealDamage(collisionDamage);
    }
}
