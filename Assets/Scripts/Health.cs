using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100.0f;
    public HealthBar healthBar;
    [SerializeField]
    
    private bool isPlayerShip = false;
    [SerializeField]
    private bool isEnemyShip = false;
    [SerializeField]
    private bool isEnemyBossShip = false;
    [SerializeField]
    private bool isMeteor = false;
    [SerializeField]
    
    private float killPoints = 0;
    
    [SerializeField]
    private GameObject deathAnimation;
    [SerializeField]
    private GameObject hitAnimation;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip hitClip;
    
    private float _currentHealth;
    private float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            if (healthBar is not null) healthBar.SetFillLevel(_currentHealth / maxHealth);
            if(_currentHealth <= 0) // to suppress the float errors  
            {
                if (deathAnimation != null)
                {
                    var deathAnim = Instantiate(deathAnimation, transform.position, transform.rotation);
                    Destroy(deathAnim, 2f);
                }

                if (source is not null && explosionClip is not null)
                    source.PlayOneShot(explosionClip);

                gameObject.GetComponent<Collider>().enabled = false;
                foreach (var rndr in gameObject.GetComponentsInChildren<MeshRenderer>())
                {
                    rndr.enabled = false;
                }
                foreach (var partSystem in gameObject.GetComponentsInChildren<ParticleSystem>())
                {
                    partSystem.Stop();
                }

                var enemyGun = gameObject.GetComponent<EnemyGun>();
                if (enemyGun is not null)
                    enemyGun.enabled = false;
                Destroy(gameObject, 3.0f);
                ScoreManager.Instance.AddScore(killPoints);
                
                if (isPlayerShip)
                {
                    if (healthBar is not null) healthBar.SetFillLevel(100);
                    PauseManager.PauseGame(showPauseMenu:false);
                    UIManager.Show<DeathMenuView>(remember:false);
                    UIManager.GetView<DeathMenuView>().ShowScore();
                }

                if (isEnemyShip)
                {
                    if (EnemyFactory.Instance is not null) EnemyFactory.Instance.EnemyKilled();
                    if (BossFightFactory.Instance is not null) BossFightFactory.Instance.EnemyKilled();
                    Debug.Log("enemy killed");
                    if (EnemyFactory.Instance is not null && EnemyFactory.Instance.AllEnemiesKilled())
                    {
                        PauseManager.PauseGame(showPauseMenu:false);
                        UIManager.Show<WinMenuView>(remember:false);
                        UIManager.GetView<WinMenuView>().ShowScore();
                    }
                }

                if (isMeteor)
                {
                    MeteorFactory.Instance.MeteorDestroyed();
                    Debug.Log("meteor destroyed");
                    if (MeteorFactory.Instance is not null && MeteorFactory.Instance.ALlMeteorsDestroyed())
                    {
                        PauseManager.PauseGame(showPauseMenu:false);
                        UIManager.Show<WinMenuView>(remember:false);
                        UIManager.GetView<WinMenuView>().ShowScore();
                    }
                }

                if (isEnemyBossShip)
                {
                    PauseManager.PauseGame(showPauseMenu:false);
                    UIManager.Show<WinMenuView>(remember:false);
                    UIManager.GetView<WinMenuView>().ShowScore();
                }
            }
        }
    }
    
    private void Start()
    {
        CurrentHealth = maxHealth;
    }
    public void DealDamage(float damage)
    {
        if (hitAnimation != null)
        {
            var hitAnim = Instantiate(hitAnimation, transform.position, transform.rotation);
            Destroy(hitAnim, 2f);
        }
        if (source is not null && hitClip is not null)
            source.PlayOneShot(hitClip);
        
        CurrentHealth -= damage;
    }
    /*public void Heal(float heal)
    {
        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + heal);
    }*/
    /*public void Kill()
    {
        CurrentHealth = 0;
        Debug.Log("You died!");
    }*/
}