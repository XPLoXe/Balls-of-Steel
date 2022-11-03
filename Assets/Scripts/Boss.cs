using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    //AUDIO\\
    private AudioSource bossAudioSource;
    public AudioClip bossHitClip;
    public GameObject projectilePrefab;
    private bool isProjectileActive = false;
    private bool shotsDifference;

    // Start is called before the first frame update
    void Start()
    {
        bossAudioSource = GetComponent<AudioSource>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        
        switch (MainManager.Instance.difficulty)
        {
            case 1:
                setSpeed(20.0f);    //Easy
                setWallForceMultiplier(1.5f);
                break;
            case 2:
                setSpeed(5.0f);    //God
                setWallForceMultiplier(3f);
                break;
            default:
                setSpeed(5.0f);     //Testing
                setWallForceMultiplier(1.5f);
                break;
        }
        
        StartCoroutine(SpawnCooldown());
        
    }
 
    // Update is called once per frame
    void Update()
    {
        if (isProjectileActive)
        {
            StartCoroutine(BurstShots());
            StartCoroutine(Cooldown());
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsPlayer(collision.gameObject))
        {
            bossAudioSource.PlayOneShot(bossHitClip);
        }
    }

    IEnumerator Cooldown()
    {
        isProjectileActive = false;
        yield return new WaitForSeconds(3);
        isProjectileActive = true;
    }

    IEnumerator BurstShots() //CD inbetween shots
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
            ShootProjectile();
        }
    }

    IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(2);
        isProjectileActive = true;
    }

    private void ShootProjectile()
    {
        Instantiate(projectilePrefab, ProjectileSpawnPosition() + new Vector3(0,3,0), projectilePrefab.transform.rotation);
    }

    private Vector3 ProjectileSpawnPosition()
    {
        return transform.position;
    }

}
