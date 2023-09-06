using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet laserPrefab;
    public float speed = 5f;
    private bool laserActive;
    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }
    private void Shoot()
    {
        if (!laserActive)
        { 
           Bullet bullet =  Instantiate(laserPrefab, transform.position, Quaternion.identity);
            bullet.destroyed += LaserDestroyed;
            laserActive = true;
        }
    }
    private void LaserDestroyed()
    {
        laserActive = false;
    }
}
