using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float shootTimerMax;

    private float shootTimer;
    private Enemy targetEnemy;
    private float lookForTargetTimer;
    private float lookForTargerTimerMax = .2f;
    private Vector3 projectileSpawnPosition;
    private void Update(){
        HandleTargetting();
        HandleShooting();
    }

    private void Awake()
    {
        projectileSpawnPosition = transform.Find("projectileSpawnPosition").position;
    }
    private void HandleTargetting(){
        lookForTargetTimer -= Time.deltaTime;
        if(lookForTargetTimer < 0f){
            lookForTargetTimer += lookForTargerTimerMax;
            LookForTargets();
        }
    }

    private void HandleShooting(){
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f){
            shootTimer += shootTimerMax;
            if (targetEnemy != null){
                ArrowProjectile.Create(projectileSpawnPosition, targetEnemy);
            }
        }
    }
   private void LookForTargets(){
        float targetMaxRadius = 20f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach(Collider2D collider2D in collider2DArray){
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null){
                if (targetEnemy == null){
                    targetEnemy = enemy;
                } else {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, targetEnemy.transform.position)){
                        targetEnemy = enemy;
                    }
                }
            }
        }
    }
}
