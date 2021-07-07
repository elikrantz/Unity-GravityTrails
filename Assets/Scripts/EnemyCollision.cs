using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            if (sceneName == "level1")
            {
                SceneManager.LoadScene("level1");
            }
            if (sceneName == "level2")
            {
                SceneManager.LoadScene("level2");
            }
        }
    }
}
