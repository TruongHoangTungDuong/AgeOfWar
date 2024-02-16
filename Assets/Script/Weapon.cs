using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Weapon : MonoBehaviour
{
    public int id;
    public bool canBuy;
    public int cost;
    public GameObject bullet;
    public float cooldown;
    public EnemySpawner enemyspawn;
    public Spawner spawner;
    public bool isAttacking;
    public bool routine;
    public bool isPlayer;
    void Start()
    {
        enemyspawn = FindObjectOfType<EnemySpawner>();
        spawner = FindObjectOfType<Spawner>();
    }
    IEnumerator attack(float cooldown)
    {
        if (isPlayer == true)
        {
            routine = true;
            yield return new WaitForSeconds(cooldown);
            if (isAttacking == true)
            {
                GameObject m_bullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
                Bullet bullet1 = m_bullet.GetComponent<Bullet>();
                bullet1.isPlayer = true;
                if (enemyspawn.enemySoldier.Count > 0)
                {
                    bullet1.vector = enemyspawn.enemySoldier[0].transform.position - transform.position;
                }
                StartCoroutine(attack(cooldown));
            }
            else
            {
                routine = false;
            }
        }
        else
        {
            routine = true;
            yield return new WaitForSeconds(cooldown);
            if (isAttacking == true)
            {
                GameObject m_bullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
                Bullet bullet1 = m_bullet.GetComponent<Bullet>();
                bullet1.isPlayer = false;
                if (spawner.playerSoldier.Count > 0)
                {
                    bullet1.vector = spawner.playerSoldier[0].transform.position - transform.position;
                }
                    StartCoroutine(attack(cooldown));
            }
            else
            {
                routine = false;
            }
        }
        
    }
    
    void check_attacking()
    {
        if (isPlayer == true)
        {
            isAttacking = false;
            if (enemyspawn.enemySoldier.Count > 0)
            {
                if (enemyspawn.enemySoldier[0].transform.position.x < -5f)
                {
                    isAttacking = true;
                    Vector3 directionToEnemy = enemyspawn.enemySoldier[0].transform.position - transform.position;
                    float angleToEnemy = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, angleToEnemy);
                }
            }
        }
        else
        {
            isAttacking = false;
            if (spawner.playerSoldier.Count > 0)
            {
                if (spawner.playerSoldier[0].transform.position.x > 5f)
                {
                    isAttacking = true;
                    Vector3 directionToEnemy = spawner.playerSoldier[0].transform.position - transform.position;
                    float angleToEnemy = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, -angleToEnemy);
                }
            }
        }
        
    }
    void Update()
    {
        check_attacking();
        if (isAttacking == true)
        {
            if(routine == false)
            {
                StartCoroutine(attack(cooldown));
            }
           
        }
    }
}
