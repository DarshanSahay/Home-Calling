using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour,IDamageable
{
    [SerializeField] private ActivateEnemyZone zone;
    [SerializeField] private float Health;
    [SerializeField] private Animator enemyAnim;
    [SerializeField] private Material[] dissolveEffect;
    [SerializeField] private Image healthBar;

    private bool isDead = false;
    private bool canActivateEffect = false;
    private float onDeadEffectTime;
    private float onEnableEffectTime;
    private float maxEffectTime;

    public Transform playerTransform;

    private void OnEnable()
    {
        canActivateEffect = true;                                        //On enable show dissolve effect
    }
    private void Start()
    {
        playerTransform = PlayerHealth.Instance.transform;             //getting instance of player transform 
        onDeadEffectTime = 0f;
        onEnableEffectTime = 3f;
        maxEffectTime = 3f;
        Health = 100;
        SetDissolveEffect(1);                                            //setting dissolve effect to be completely invisible
    }
    private void Update()
    {
        OnZoneActivation();                                           
        OnDeath();
    }
    private void OnZoneActivation()                                      //when player enters the enemy zone , start applying 
    {                                                                    //dissolve effects to make enemies visible
        if (canActivateEffect == true)
        {
            onEnableEffectTime -= Time.deltaTime;
            float percentComplete2 = onEnableEffectTime / maxEffectTime;
            percentComplete2 = Mathf.Clamp01(percentComplete2);
            SetDissolveEffect(percentComplete2);
            if (onEnableEffectTime <= 0)
            {
                canActivateEffect = false;
                onEnableEffectTime = 3;
            }
        }
    }
    private void UpdateHealth()                                        
    {
        healthBar.fillAmount = Health / 100;
    }
    public void TakeDamage(int Damage)                                    //interface for take damage when the enemy takes hit
    {
        Health -= Damage;
        if (Health <= 0)
        {
            isDead = true;
            enemyAnim.SetTrigger("isDead");
            GetComponent<CapsuleCollider>().enabled = false;
            Destroy(gameObject, 4f);
        }
        else
        {
            enemyAnim.SetTrigger("isHurt");
            enemyAnim.SetBool("isChasing", true);
            UpdateHealth();                                               //Updating health on UI after enemy is hurt
        }
    }
    private void OnDeath()                                                //On Death function to apply dissolve effects 
    {
        if (isDead == true)
        {
            onDeadEffectTime += Time.deltaTime;
            float percentComplete1 = onDeadEffectTime / maxEffectTime;
            percentComplete1 = Mathf.Clamp01(percentComplete1);
            SetDissolveEffect(percentComplete1);
            if (onDeadEffectTime >= 3)
            {
                isDead = false;
                onDeadEffectTime = 0;
                zone.enemyCount--;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
        {
            IDamageable damageable = GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(Random.Range(15,20));               //take random damage when player attacks
            }
        }
    }
    private void SetDissolveEffect(float value)                          //Function to set the visibilty of enemy using Dissolve Effect
    {                                                                    //0 value is completely Visible and 1 is invisible
        for (int i = 0; i < dissolveEffect.Length; i++)
        {
            dissolveEffect[i].SetFloat("_Dissolve", value);
        }
    }
}
