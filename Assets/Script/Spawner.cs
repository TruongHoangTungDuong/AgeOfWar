using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Spawner : MonoBehaviour
{
    public int spawnID;
    public int id;
    public int epoch;
    public int i;
    public int[,] Cost = new int[5, 3] { { 15, 25, 100 }, { 50, 75, 500 }, { 200, 400, 1000 }, { 1500, 2000, 7000 }, { 5000, 6000, 20000 } };
    public int[,] Costweapon= new int[5, 2] { {100, 200}, {500,750 }, { 3000,6000}, {9000,14000 }, {50000,100000} };
    public List<GameObject> weaponPrefab;
    public List<GameObject> spawnPrefab;
    public List<GameObject> playerSoldier=new List<GameObject>();
    public List<GameObject> playerWeapon=new List<GameObject>();
    public List<Sprite> ImageIcon=new List<Sprite>();
    public List<Button> button = new List<Button>();
    public GameObject last_friendly_spawned=null;
    public MoneyExpManager me_manager;
    public Base playerbase;
    public Ability ability;
    public Vector2 Pos;
    public GameObject emulator;
    public bool canSpawn;
    public bool canBuy;
    public int[] exptoupgrade = new int[4] { 4000,14000,45000,200000 };
    void Start()
    {
        this.epoch = 1;
        this.i = 0;
        me_manager = FindObjectOfType<MoneyExpManager>();
        playerbase = FindObjectOfType<Base>();
        ability = FindObjectOfType<Ability>();
        canSpawn=true;
        canBuy=true;
    }

    void Update()
    {
        if (playerSoldier.Count > 0)
        {
            playerSoldier[0].tag = "firstplayer";
        }
    }
    public void SelectSoldier1(int spawnID)
    {
        this.spawnID = this.epoch * 3 - 3;
        if ((me_manager.money - Cost[epoch-1, spawnID])<0)
        {
            canSpawn=false;
        }
        else
        {
            canSpawn = true;
        }
    }
    public void SelectSoldier2(int spawnID)
    {
        this.spawnID = this.epoch * 3 - 2;
        if ((me_manager.money - Cost[epoch-1, spawnID]) < 0)
        {
            canSpawn = false;
        }
        else
        {
            canSpawn = true;
        }
    }
    public void SelectSoldier3(int spawnID)
    {
        this.spawnID = this.epoch * 3 - 1;
        if ((me_manager.money - Cost[epoch-1, spawnID]) < 0)
        {
            canSpawn = false;
        }
        else
        {
            canSpawn = true;
        }
    }
    public void Upgrade()
    {
        if (epoch < 5)
        {
            if (me_manager.exp > exptoupgrade[i])
            {
                this.epoch += 1;
                i += 1;
                playerbase.max_health += 1000;
                playerbase.cur_health += 1000;
                for(int j = 0; j < 6; j++)
                {
                    button[j].GetComponent<Image>().sprite = ImageIcon[epoch * 6 - (6-j)];
                }
                ability.ModifyAllBullets(ability.images[epoch-1]);
            }
        }
        
    }
    public void Spawn()
    {
        Vector2 _SpawnPos = new Vector2(-15f, -4f);
        if (canSpawn == true)
        {
            GameObject m_soldier = Instantiate(spawnPrefab[spawnID], _SpawnPos, Quaternion.identity);
            Soldier soldier = m_soldier.GetComponent<Soldier>();
            soldier.isPlayer=true;
            soldier.transform.localScale = new Vector3(3, 3, 0);
            soldier.tag = "Player";
            me_manager.money -= soldier.cost;
            soldier.next_soldier = last_friendly_spawned;
            if (last_friendly_spawned != null)
            {
                Soldier enemy_tr = last_friendly_spawned.GetComponent<Soldier>();
                enemy_tr.prev_soldier = m_soldier;
            }
            if ((last_friendly_spawned) == null || (soldier.next_soldier == null))
            {
                soldier.next_soldier = emulator;
            }
            last_friendly_spawned = m_soldier;
            playerSoldier.Add(m_soldier);

        }
    }
    public void BuyWeapon1(int id)
    {
        this.id = this.epoch * 2 - 2;
        if ((me_manager.money -Costweapon[this.epoch - 1, id])<0)
        {
            canBuy = false;
        }
        else
        {
            canBuy = true;
        }
    }
    public void BuyWeapon2(int id)
    {
        this.id = this.epoch * 2 - 1;
        if ((me_manager.money -Costweapon[this.epoch - 1, id]) < 0)
        {
            canBuy = false;
        }
        else
        {
            canBuy = true;
        }
    }
    public void BuyWeapon()
    {
        if (playerWeapon.Count== 0)
        {
            Pos = new Vector2(-15.4f, -1.25f);
        }
        if (playerWeapon.Count == 1)
        {
            Pos = new Vector2(-15.4f, -0.25f);
        }
        if (playerWeapon.Count == 2)
        {
             Pos = new Vector2(-15.4f,1);
        }
        if (playerWeapon.Count == 3)
        {
             Pos = new Vector2(-15.4f,2.4f);
        }
        if (canBuy == true&&playerWeapon.Count<=4)
        {
            GameObject m_weapon = Instantiate(weaponPrefab[id], Pos, Quaternion.identity);
            Weapon weapon = m_weapon.GetComponent<Weapon>();
            weapon.isPlayer = true;
            weapon.transform.localScale = new Vector3(3, 3, 0);
            me_manager.money -= weapon.cost;
            playerWeapon.Add(m_weapon);
        }
    }
    public void SellWeapon()
    {
        GameObject m_weapon = playerWeapon[playerWeapon.Count-1];
        Weapon weapon=m_weapon.GetComponent<Weapon>();
        me_manager.money += weapon.cost * 1 / 2;
        playerWeapon.Remove(m_weapon);
        Destroy(m_weapon);
    }
}
