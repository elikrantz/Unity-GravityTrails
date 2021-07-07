using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public int enemyCount;

    void Start()
    {
        
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && enemyCount == 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}
