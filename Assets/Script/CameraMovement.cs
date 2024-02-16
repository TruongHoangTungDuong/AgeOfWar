using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float boundary;
    private int moveSpeed;
    private float leftLimit;
    private float rightLimit;

    private void Start()
    {
        this.boundary = 5f;
        this.moveSpeed = 5;
        this.leftLimit = -10.5f;
        this.rightLimit = 10;
    }

    private void LateUpdate()
    {

        // move right
        if (transform.position.x < rightLimit && Input.mousePosition.x > Screen.width - boundary)
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        // move left
        if (transform.position.x > leftLimit && Input.mousePosition.x < boundary)
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }

    }
}
