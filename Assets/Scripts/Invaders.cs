using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;

    public int rows = 5;

    public int colums = 11;

    private Vector3 direction = Vector3.right;

    public AnimationCurve speed;    

    public int amountKilled { get; private set; }
    public float missileAttackRate = 1f;
    public Bullet missilePrefab;
    public int amountAlive => totalInvaders - amountKilled;
    public int totalInvaders => rows * colums;

    public float percentKilled => (float)amountKilled / (float)totalInvaders;

    private void Awake()
    {
        for ( int row = 0; row < rows; row++ )
        {
            Vector3 rowPosition =  new Vector3(0f ,row * 2f , 0f);

            for ( int col = 0;  col < colums; col++)
            {
               Invader invader =  Instantiate(prefabs[row],transform);
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += col * 2f;
                invader.transform.localPosition = position; 
            }
        }
    }
    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), missileAttackRate, missileAttackRate);
    }
    private void Update()
    {
        transform.position += direction * speed.Evaluate(percentKilled) * Time.deltaTime;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach ( Transform invader in transform)
        {
            if(!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if(direction == Vector3.right && invader.position.x  >= (rightEdge.x -1f))
            {
                AdvanceRow();
            }
            else if (direction == Vector3.left && invader.position.x <= (leftEdge.x +1f))
            {
                AdvanceRow();
            }
        }
    }
    private void MissileAttack()
    {
        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if(Random.value < (1f / (float)amountAlive))
            {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }
    private void AdvanceRow()
    {
        direction.x *= -1f;
        Vector3 position = transform.position;
        position.y -= 1f;
        transform.position = position;
    }
    private void InvaderKilled()
    {
        amountKilled++;

        if(amountKilled >= totalInvaders)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
 
}
