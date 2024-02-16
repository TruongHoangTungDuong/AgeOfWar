using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    public int max_health;
    public float cur_health;
    public HealthBar healthbar;
    public Spawner spawner;
    [SerializeField] List<SpriteRenderer> renderers = new List<SpriteRenderer>();   
    public List<Sprite> Image = new List<Sprite>();
    void Start()
    {
        max_health = 500;
        this.cur_health = max_health;
        healthbar = GetComponentInChildren<HealthBar>();
        spawner = FindObjectOfType<Spawner>();
    }
    void Update()
    {
        healthbar.UpdateHealthBar(cur_health, max_health);
        renderers[0].sprite = Image[spawner.epoch * 2 - 2];
        for(int i = 1; i < 4; i++)
        {
            renderers[i].sprite = Image[spawner.epoch * 2 - 1];
        }
        if (cur_health <= 0)
        {
            SceneManager.LoadSceneAsync(2);
        }
    }
}
