using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHealth : GenericSingleton<PlayerHealth>,IDamageable
{
    [SerializeField] private PlayerStateHandler stateHandler;      
    [SerializeField] private Image healthBar;
    [SerializeField] private Animator anim;
    public event Action onPlayerDeath;

    private float maxHealth;
    private float currentHealth;
    
    private void Start()
    {
        SetHealth();
        stateHandler = PlayerStateHandler.Instance;
    }
    public void TakeDamage(int Damage)                            //interface TakeDamage to be applied on enemy hit
    {
        currentHealth -= Damage;

        if (currentHealth <= 0)                                  
        {
            stateHandler.currentState.ChangeState(stateHandler.deathState);
            anim.SetTrigger("isDead");                            
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            onPlayerDeath?.Invoke();
        }
        else
        {
            anim.SetTrigger("isHurt");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EnemyWeapon")
        {
            IDamageable damageable = GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(UnityEngine.Random.Range(10, 15));             //giving a random damage to player using the Interface method
                UpdateHealth();
            }
        }
    }
    public void SetHealth()                                             //function to be called at start and when player dies to set stats again
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth;
    }
    private void UpdateHealth()                                        
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
