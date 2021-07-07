using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    //public GameObject player;
    public Vector3 blockPos;
    public Transform playerPos;
    
    // Start is called before the first frame update
    void Start()
    {
        //playerPos = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        blockPos = new Vector3(playerPos.position.x, playerPos.position.y, 0.0f);
        transform.position = blockPos;
    }
}
