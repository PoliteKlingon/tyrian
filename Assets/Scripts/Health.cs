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
    private bool isMeteor = false;
    [SerializeField]
    private float killPoints = 0;
    [SerializeField]
    private GameObject deathAnimation;
    [SerializeField]
    private GameObject hitAnimation;
    
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
                Destroy(gameObject);
                ScoreManager.Instance.AddScore(killPoints);
                
                if (isPlayerShip)
                {
                    if (healthBar is not null) healthBar.SetFillLevel(100);
                    UIManager.Show<DeathMenuView>();
                    UIManager.GetView<DeathMenuView>().ShowScore();
                }

                if (isEnemyShip)
                {
                    EnemyFactory.Instance.EnemyKilled();
                    Debug.Log("enemy killed");
                }

                if (isMeteor)
                {
                    MeteorFactory.Instance.MeteorDestroyed();
                    Debug.Log("meteor destroyed");
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