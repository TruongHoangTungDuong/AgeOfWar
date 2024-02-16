using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Soldier : MonoBehaviour
{
    public float damage;
    public float current_health;
    public float max_health;
    public int move_speed;
    public int cost;
    public bool isMoving;
    public bool Stopbycollision;
    public bool isAttacking;
    public float width;
    public bool collisionEnemyBase;
    public bool collisionPlayerBase;
    public bool isRangeAttack;
    public bool isMeleeAttack;
    public GameObject next_soldier;
    public GameObject prev_soldier;
    public float distance;
    public float dis;
    public int exp;
    public MoneyExpManager me_manager;
    public Spawner playerspawn;
    public EnemySpawner enemyspawn;
    public EnemyBase enemybase;
    public Base playerbase;
    public Rigidbody2D rb;
    public HealthBar healthbar;
    public Animator anim;
    public bool isPlayer;
    public AudioClip soldierAudioClip;
    public virtual void Move()
    {
        if (isPlayer == true)
        {
            if (isMoving == true)
            {
                transform.position = transform.position + new Vector3(move_speed * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            if (isMoving == true)
            {
                transform.position = transform.position - new Vector3(move_speed * Time.deltaTime, 0, 0);
            }
        }
        
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        current_health = max_health;
        enemyspawn = FindObjectOfType<EnemySpawner>();
        enemybase = FindObjectOfType<EnemyBase>();
        playerbase = FindObjectOfType<Base>();
        playerspawn = FindObjectOfType<Spawner>();
        healthbar = GetComponentInChildren<HealthBar>();
        me_manager = FindObjectOfType<MoneyExpManager>();
        isMoving = true;
        healthbar.UpdateHealthBar(current_health, max_health);
    }

    void Update()
    {
        checkmoving();
        Move();
        healthbar.UpdateHealthBar(current_health, max_health);
        if (isMoving == false)
        {
            anim.SetBool("isMoving", false);
        }
        if(isMoving== true)
        {
            anim.SetBool("isMoving", true);
        }
        if(isAttacking == false)
        {
            anim.SetBool("isAttacking", false);
        }
        if(isAttacking== true)
        {
            anim.SetBool("isAttacking", true);
        }
        if (current_health < 0)
        {
            Die();
        }
        
    }
    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (isPlayer == true)
        {
            if (isMeleeAttack == true)
            {
                if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "firstenemy")
                {
                    isMoving = false;
                    Stopbycollision = true;
                    isAttacking = true;
                    StartCoroutine(attack());
                }
                if (col.gameObject.tag == "EnemyBase")
                {
                    isMoving = false;
                    Stopbycollision = true;
                    isAttacking = true;
                    collisionEnemyBase = true;
                    StartCoroutine(attack());
                }
            }
            else
        if (isRangeAttack == true)
            {
                if (col.gameObject.tag == "firstenemy")
                {
                    isAttacking = true;
                    StartCoroutine(attack());
                }
                if (col.gameObject.tag == "EnemyBase")
                {
                    isMoving = false;
                    isAttacking = true;
                    collisionEnemyBase = true;
                    StartCoroutine(attack());
                }
            }

        }
        else
        {
            if (isMeleeAttack == true)
            {
                if (col.gameObject.tag == "Player" || col.gameObject.tag == "firstplayer")
                {
                    isMoving = false;
                    Stopbycollision = true;
                    isAttacking = true;
                    StartCoroutine(attack());

                }
                if (col.gameObject.tag == "Base")
                {
                    isMoving = false;
                    Stopbycollision = true;
                    isAttacking = true;
                    collisionPlayerBase = true;
                    StartCoroutine(attack());
                }
            }
            else
        if (isRangeAttack == true)
            {
                if (col.gameObject.tag == "firstplayer")
                {
                    isAttacking = true;
                    StartCoroutine(attack());

                }
                if (col.gameObject.tag == "Base")
                {
                    isMoving = false;
                    isAttacking = true;
                    collisionPlayerBase = true;
                    StartCoroutine(attack());
                }
            }
            if (col.gameObject.tag == "Ability")
            {
                Die();
            }
        }

    }
    public virtual void OnTriggerExit2D(Collider2D col)
    {
        if (isPlayer == true)
        {
            if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "firstenemy")
            {
                isMoving = true;
                Stopbycollision = false;
                isAttacking = false;
                StopAllCoroutines();
            }
        }
        else
        {
            if (col.gameObject.tag == "Player" || col.gameObject.tag == "firstplayer")
            {
                isMoving = true;
                Stopbycollision = false;
                isAttacking = false;
                StopAllCoroutines();
            }
        }
    }
    public virtual void checkmoving()
    {
        if (isPlayer == true)
        {
            isMoving = true;
            if(next_soldier == null  && Stopbycollision == true)
            {
                isMoving = false;
            }
            if (next_soldier != null)
            {
                distance = next_soldier.transform.position.x - gameObject.transform.position.x;
                if (distance < width || Stopbycollision == true)
                {
                    isMoving = false;
                }
            }

            if (enemyspawn.enemySoldier.Count > 0)
            {
                Soldier m_enemy = enemyspawn.enemySoldier[0].GetComponent<Soldier>();
                dis = m_enemy.transform.position.x - gameObject.transform.position.x;
                if (isRangeAttack == true && dis < (m_enemy.width + this.width))
                {
                    isMoving = false;
                    isAttacking = true;
                }
            }
            if (isRangeAttack == true && transform.position.x >= 14)
            {
                isMoving = false;
            }
        }
        else
        {
            isMoving = true;
            if (collisionPlayerBase == true)
            {
                isMoving = false;
                isAttacking = true;
            }
            if (next_soldier == null && Stopbycollision == true)
            {
                isMoving = false;
            }
            if (next_soldier != null)
            {
                distance = -next_soldier.transform.position.x + gameObject.transform.position.x;

                if (distance < width || Stopbycollision == true)
                {
                    isMoving = false;
                }
            }
            if (playerspawn.playerSoldier.Count > 0)
            {
                Soldier m_player = playerspawn.playerSoldier[0].GetComponent<Soldier>();
                dis = -m_player.transform.position.x + gameObject.transform.position.x;
                if (isRangeAttack == true && dis < (m_player.width + this.width))
                {
                    isMoving = false;
                    isAttacking = true;

                }
            }
            if (isRangeAttack == true && transform.position.x <= -14)
            {
                isMoving = false;
            }
        }
    }
    public virtual void Die()
    {
        if (isPlayer == true)
        {
            isAttacking = false;
            playerspawn.playerSoldier.Remove(gameObject);
            if (prev_soldier != null)
            {
                Soldier prev_tr = prev_soldier.GetComponent<Soldier>();
                prev_tr.next_soldier = next_soldier;
            }

            Destroy(gameObject);
        }
        else
        {
            isAttacking = false;
            me_manager.exp += this.exp;
            me_manager.money += this.cost;
            if (prev_soldier != null)
            {
                Soldier prev_tr = prev_soldier.GetComponent<Soldier>();
                prev_tr.next_soldier = next_soldier;
            }
            if (next_soldier != null && prev_soldier != null)
            {
                Soldier next_tr = next_soldier.GetComponent<Soldier>();
                next_tr.prev_soldier = prev_soldier;
            }
            Destroy(gameObject);
            enemyspawn.enemySoldier.Remove(gameObject);
        }
        
    }
    IEnumerator attack()
    {
        if (isPlayer == true)
        {
            yield return new WaitForSeconds(1);
            if (enemyspawn.enemySoldier.Count > 0)
            {
                Soldier m_enemy = enemyspawn.enemySoldier[0].GetComponent<Soldier>();
                if (isAttacking == true)
                {
                    float damage_attack = Random.Range(damage * 0.9f, damage * 1.1f);
                    m_enemy.current_health -= damage_attack;
                }
            }
            if (collisionEnemyBase == true)
            {
                enemybase.cur_health -= damage;
            }
            StartCoroutine(attack());
        }
        else
        {
            yield return new WaitForSeconds(1);
            if (playerspawn.playerSoldier.Count > 0)
            {
                Soldier m_player = playerspawn.playerSoldier[0].GetComponent<Soldier>();
                if (isAttacking == true)
                {
                    m_player.current_health -= damage;
                }
            }
            if (collisionPlayerBase == true)
            {
                playerbase.cur_health -= damage;
            }
            StartCoroutine(attack());
        }
        AudioManager.Instance.PlaySound(soldierAudioClip);
    }  
}

