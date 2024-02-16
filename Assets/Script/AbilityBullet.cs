using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AbilityBullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "firstenemy")
        {
            DisableBullet();
        }
    }
    void DisableBullet()
    {
        gameObject.SetActive(false);
    }
}
