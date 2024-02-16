using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public GameObject bullet;
    public Spawner spawner;
    public int cooldown;
    public bool isCooldown;
    [SerializeField] public Image imagecooldown;
    [SerializeField] public List<Sprite> images;

    // Object pooling variables
    private List<GameObject> bulletPool;
    private int poolSize = 30;

    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        isCooldown = false;
        imagecooldown.fillAmount = 0;

        // Initialize object pool
        InitializeObjectPool();
    }

    void Update()
    {
        if (isCooldown == true && cooldown > 0)
        {
            cooldown -= 1;
        }
        if (cooldown == 0)
        {
            isCooldown = false;
        }
        if (isCooldown == true)
        {
            imagecooldown.fillAmount = (float)cooldown / 3600;
        }
    }

    void InitializeObjectPool()
    {
        bulletPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(bullet);
            obj.SetActive(false);
            bulletPool.Add(obj);
        }
    }

    public void use_ability()
    {
        if (isCooldown == false)
        {
            StartCoroutine(ability(0));
            isCooldown = true;
            cooldown = 3600;
        }
    }

    IEnumerator ability(int bullet_launch)
    {
        if (bullet_launch != 30)
        {
            // Get bullet from the pool
            GameObject bulletObj = GetBulletFromPool();

            Vector2 spawnpos = new Vector2(Random.Range(-9, 9), 5);
            yield return new WaitForSeconds(0.25f);

            // Set the position and activate the bullet
            bulletObj.transform.position = spawnpos;
            bulletObj.SetActive(true);

            StartCoroutine(ability(bullet_launch + 1));
        }
    }

    GameObject GetBulletFromPool()
    {
        // Find the first inactive bullet in the pool
        foreach (GameObject obj in bulletPool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        // If no inactive bullet is found, create a new one
        GameObject newObj = Instantiate(bullet);
        newObj.SetActive(false);
        bulletPool.Add(newObj);

        return newObj;
    }
    public void ModifyAllBullets(Sprite image)
    {
        foreach (GameObject obj in bulletPool)
        {

            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();

            spriteRenderer.sprite = image;
            
        }
    }
}
