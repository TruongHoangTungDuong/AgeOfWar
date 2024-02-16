using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public float spawnRate;
    [SerializeField] public List<GameObject> enemyPrefabs;
    [SerializeField] public List<GameObject> enemySoldier = new List<GameObject>();
    public List<GameObject> enemyWeapon = new List<GameObject>();
    public GameObject last_enemy_spawned = null;
    public GameObject weapon_present;
    public EnemyBase enemybase;
    public int enemyNumber;
    public int epoch;
    public float frame;
    public int id;
    public int x;

    void Start()
    {
        enemyNumber = 0;
        spawnRate = 2.5f;
        epoch = 1;
        enemybase = FindObjectOfType<EnemyBase>();
        StartCoroutine(Spawn());
        frame = 0;
        x = 2;
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds (spawnRate);
        Vector2 SpawnPos = new Vector2(15f, -4f);
        yield return wait;
        int id = Random.Range (this.epoch*3-3, this.epoch*3-x);
        GameObject enemytoSpawn = enemyPrefabs [id];
        if (enemySoldier.Count < 8)
        {
            GameObject m_enemy = Instantiate(enemytoSpawn, SpawnPos, Quaternion.identity);
            Soldier enemy = m_enemy.GetComponent<Soldier>();
            enemy.isPlayer = false;
            enemy.transform.localScale = new Vector3(-3,3,0);
            enemy.tag = "Enemy";
            enemySoldier.Add(m_enemy);
            enemy.next_soldier = last_enemy_spawned;
            if (last_enemy_spawned != null)
            {
                Soldier enemy_tr = last_enemy_spawned.GetComponent<Soldier>();
                enemy_tr.prev_soldier = m_enemy;
            }
            
            last_enemy_spawned = m_enemy;
        }
        StartCoroutine(Spawn());
    }
    void BuyWeapon()
    {
        Vector2  Pos = new Vector2(15.4f, -1.25f);
        GameObject m_weapon=Instantiate(enemyWeapon[id], Pos, Quaternion.identity);
        Weapon weapon = m_weapon.GetComponent<Weapon>();
        weapon.isPlayer = false;
        weapon.transform.localScale = new Vector3(-3, 3, 0);
        weapon_present = m_weapon;
    }
    void SellWeapon()
    {   
        Destroy(weapon_present);
    }
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        frame += 1;
        if (frame > 8000)
        {
            epoch = 2;
            enemybase.max_health += 1000;
            enemybase.cur_health += 1000;
        }
        if (frame > 16000)
        {
            epoch = 3;
            enemybase.max_health += 1000;
            enemybase.cur_health += 1000;
        }
        if (frame > 24000)
        {
            epoch = 4;
            enemybase.max_health += 1000;
            enemybase.cur_health += 1000;
        }
        if (frame > 32000)
        {
            epoch = 5;
            enemybase.max_health += 1000;
            enemybase.cur_health += 1000;
        }
        if(frame == 0 || frame==8000 || frame == 16000 || frame == 24000 | frame == 32000)
        {
            x = 2;
        }
        if(frame == 1500 || frame == 9500 || frame == 17500 || frame == 25500 | frame == 33500)
        {
            x = 1;
            if(weapon_present != null)
            {
                SellWeapon();
            }
            id = this.epoch * 2 - 2;
            BuyWeapon();
        }
        if(frame == 5000 || frame == 13000 || frame == 21000 || frame == 29000 | frame == 37000)
        {
            x = 0;
            SellWeapon();
            id=this.epoch * 2 - 1;
            BuyWeapon();
        }
        if (enemySoldier.Count > 0)
        {
            enemySoldier[0].tag = "firstenemy";
        }

    }
}
