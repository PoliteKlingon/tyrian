using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100.0f;
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private bool reviveOnDestroy = false;
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
            healthBar.SetFillLevel(_currentHealth / maxHealth);
            if(_currentHealth <= 0) // to suppress the float errors  
            {
                if (deathAnimation != null)
                {
                    var deathAnim = Instantiate(deathAnimation, transform.position, transform.rotation);
                    Destroy(deathAnim, 2f);
                }
                if (reviveOnDestroy)
                {
                    _currentHealth = maxHealth;
                    healthBar.SetFillLevel(_currentHealth / maxHealth);
                    Debug.Log("You died!");
                }
                else
                {
                    Destroy(gameObject);
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