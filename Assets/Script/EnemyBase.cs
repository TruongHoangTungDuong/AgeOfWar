using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : MonoBehaviour
{
    public int max_health;
    public float cur_health;
    public HealthBar healthbar;
    Rigidbody2D rb;
    public EnemySpawner enemySpawner;
    [SerializeField] SpriteRenderer spriteRenderer;
    public List<Sprite> Image = new List<Sprite>();


    void Start()
    {
        max_health = 500;
        this.cur_health = max_health;
        rb= GetComponent<Rigidbody2D>();
        healthbar= GetComponentInChildren<HealthBar>();
        enemySpawner=FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {
        healthbar.UpdateHealthBar(this.cur_health, this.max_health);
        spriteRenderer.sprite = Image[enemySpawner.epoch - 1];
        if (cur_health <= 0)
        {
            SceneManager.LoadSceneAsync(2);
        }
    }
}
