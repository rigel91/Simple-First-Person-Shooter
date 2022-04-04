using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Techability.Systems; 
public class BallBotBrain : MonoBehaviour
{
    [Header("Bot Controls")]
    public bool NoFire = true; 


    [Header("BotAI Firing Variables")]
    public GameObject Projectile;
    public Transform BarrelSpawn;
    bool RanOnce = false;
    public float ShootAniTime = .1f;
    Coroutine ShootCoRoutine = null;
    public bool ChasePlayer = true;
    public float MoveSpeed = 5;
    public FPSController player; 

    /*
    [Header("BotAI Firing Effect")]
    public bool hasFireEffect = false;
    public GameObject SpawnEffect;
    public Transform BarrelEffect;
    */

    [Header("BotAI Variables")]
    public bool SeesPlayer = false;
    public float PlayerAgroRange = 20;
    public float IgnorePlayerRange = 30;

    Rigidbody RB;
    Animator Ani; 
    
    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody>(); 
        Ani = gameObject.GetComponent<Animator>();
        player = GameObject.FindObjectOfType<FPSController>(); 
        if (!player)
        {
            Debug.LogError("No Player in the Scene!"); 
        }

    }

    // Update is called once per frame
    void Update()
    { 
        if (NoFire)
        {
            return;
        }
    
    }


    void Fire()
    {
        if (ShootCoRoutine == null)
        {
            Ani.SetBool("IsShooting", true);
            StartShootEndCoroutine();
        }
    }

    void SpawnProjectile()
    {
         
        GameObject bullet = Instantiate(Projectile, BarrelSpawn.position, BarrelSpawn.rotation);
        Projectile p = bullet.gameObject.GetComponent<Projectile>();
        p.ignoreOwner = true; 
        p.Owner = this.gameObject; 

        /*
        if (hasFireEffect)
        {
            Instantiate(SpawnEffect, BarrelEffect);
        }
        */
    }

    public void StartShootEndCoroutine()
    {
        RanOnce = false;
        ShootCoRoutine = StartCoroutine(TurnOffShooting()); 
    }

    IEnumerator TurnOffShooting()
    {
        if (!RanOnce)
        {
            RanOnce = true;
            yield return new WaitForSeconds(ShootAniTime);
        }
        Ani.SetBool("IsShooting", false);
        SpawnProjectile();
        StopCoroutine(ShootCoRoutine); 
        ShootCoRoutine = null; 
    }

    public void OnDeath()
    {
        Ani.SetBool("IsDead", true); 
    }
}
