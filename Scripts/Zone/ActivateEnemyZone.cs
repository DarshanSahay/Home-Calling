using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemyZone : MonoBehaviour
{
    [SerializeField] private AudioSource mainSoundTrack;
    [SerializeField] private AudioSource enterZoneMusic;
    [SerializeField] private Animator shieldAnim;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject holoShield;
    [SerializeField] private PlayerHealth pHealth;
    [SerializeField] private MeshCollider col;

    public GameObject checkPoint;
    public int enemyCount;

    private bool canAdvance = false;

    private void Start()
    {
        col = GetComponent<MeshCollider>();
        pHealth = PlayerHealth.Instance;
        pHealth.onPlayerDeath += OnPlayerDeath;
    }
    private void Update()
    {
        OnZoneCompletion();
    }
    private void CloseBarrier()
    {
        canAdvance = false;
        shieldAnim.ResetTrigger("closeShield");
        Destroy(gameObject);
        Destroy(holoShield);
    }
    private void OnZoneCompletion()                           //when there is no enemy inside the zone , function will be called
    {                                                         //so that player can move outside the zone
        if (enemyCount == 0)
        {
            mainSoundTrack.Play();
            enterZoneMusic.Stop();
            canAdvance = true;
            enemyCount = -1;
            if (canAdvance)
            {
                shieldAnim.SetTrigger("closeShield");
                Invoke("CloseBarrier", 2f);
            }
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<PlayerHealth>() != null)
        {
            OnPlayerEnter();
        }
    }
    private void OnPlayerEnter()                                      //when player enters the zone , plays different sound  
    {                                                                 //and activates all the enemy inside the zone
        mainSoundTrack.Stop();
        enterZoneMusic.Play();
        holoShield.gameObject.SetActive(true);
        Invoke("EnableCollider", 2f);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.SetActive(true);
        }
    }
    private void OnPlayerDeath()
    {
        Invoke("SetRespawnPosition", 5f);
    }
    private Vector3 GetActiveCheckPointPosition()                   //get checkpoint position inside the Enemyzone
    {
        Vector3 result = checkPoint.transform.position;
        return result;
    }
    private void SetRespawnPosition()                               //setting player position after re-spawning
    {
        pHealth.transform.position = GetActiveCheckPointPosition();
    }
    private void EnableCollider()                                  //enables the zone mesh to be in escapable
    {
        col.convex = false;
        col.isTrigger = false;
    }
}
