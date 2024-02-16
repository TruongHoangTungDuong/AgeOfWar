using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    public int damage;
    public EnemySpawner enemyspawn;
    public EnemyBase enemybase;
    public Spawner playerspawn;
    public Base playerbase;
    public Vector3 vector;
    public float move_speed;
    public bool isPlayer;
    void Start()
    {
        enemyspawn = FindObjectOfType<EnemySpawner>();
        enemybase = FindObjectOfType<EnemyBase>();
        playerspawn = FindObjectOfType<Spawner>();
        playerbase = FindObjectOfType<Base>();
    }
    void Update()
    {
            moving();
    }
    void moving()
    {
        transform.position = transform.position + move_speed * vector * Time.deltaTime;
    }
    void OnTriggerEnter2D (Collider2D col)
    {
        if (isPlayer == true)
        {
            if (col.gameObject.tag == "Enemy"|| col.gameObject.tag == "firstenemy")
            {
                if (enemyspawn.enemySoldier.Count > 0)
                {
                    Soldier m_enemy = enemyspawn.enemySoldier[0].GetComponent<Soldier>();
                    m_enemy.current_health -= damage;
                }
                Destroy(gameObject);
            }
            if (col.gameObject.tag == "EnemyBase")
            {
                enemybase.cur_health -= damage;
                Destroy(gameObject);
            }
            if (col.gameObject.tag == "Ground")
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (col.gameObject.tag == "Player"|| col.gameObject.tag == "firstplayer")
            {
                if (playerspawn.playerSoldier.Count > 0)
                {
                    Soldier m_player = playerspawn.playerSoldier[0].GetComponent<Soldier>();
                    m_player.current_health -= damage;
                }
                Destroy(gameObject);
            }
            if (col.gameObject.tag == "Base")
            {
                playerbase.cur_health -= damage;
                Destroy(gameObject);
            }
            if (col.gameObject.tag == "Ground")
            {
                Destroy(gameObject);
            }
        }
        
        
    }
}
