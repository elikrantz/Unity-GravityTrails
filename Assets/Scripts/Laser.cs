using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour
{

    private LineRenderer lr;
    public Transform LaserHit;
    public GameObject childObject;
    //public float maxLength = 3.0f;
    private int offset = 2;
    private Vector3 maxLength;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        maxLength = new Vector3(childObject.transform.position.x, childObject.transform.position.y - offset, childObject.transform.position.z);
        lr.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Abs(childObject.transform.position.y - offset)))
        {
            if (hit.collider)
            {
                Debug.Log("hit");
                lr.SetPosition(1, hit.point);
                //lr.positionCount = (int)maxLength;
                if (hit.collider.tag == "Player")
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
        else lr.SetPosition(1, maxLength);
    }


}