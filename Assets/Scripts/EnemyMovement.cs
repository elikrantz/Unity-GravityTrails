using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{
    public GameObject maximumXObject;
    private float maximumXPosition;
    public GameObject minimumXObject;
    private float minimumXPosition;

    public float yForce;
    public float xForce;
    public float xDirection;
    private Rigidbody2D enemyRigidBody;
    private Teleport portal;

    public Tilemap tiles;
    public List<Tile> tileTypes;
    public Vector3Int location;
    private int change = 0;

    void Start()
    {
        portal = GameObject.FindGameObjectWithTag("Exit").GetComponent<Teleport>();
        enemyRigidBody = GetComponent<Rigidbody2D>();
        maximumXPosition = maximumXObject.transform.position.x;
        minimumXPosition = minimumXObject.transform.position.x;
    }

    void Update()
    {
        Vector3 mp = transform.position;
        location = tiles.WorldToCell(mp);

        for (int i = 0; i < 4;i++)
        {
            GetT(i);
        }

    }

    void GetT(int num)
    {
        Vector3 mp = transform.position;
        location = tiles.WorldToCell(mp);
        change = 0;
        switch (num)
        {
            case 1:
                location.y -= 1;
                change = 4;
                break;
            case 2:
                location.y += 1;
                change = 4;
                break;
            case 3:
                location.x -= 1;
                change = 5;
                break;
            case 4:
                location.x += 1;
                change = 5;
                break;
        }

        for (int i = 0; i < tileTypes.Count; i++)
        {
            if (num == 1 && i<=change)
            {
                if (tileTypes[i] == tiles.GetTile<Tile>(location))
                {
                    Vector2 jumpForce = new Vector2(xForce * xDirection, yForce);
                    enemyRigidBody.AddForce(jumpForce);
                    break;
                }
            } else if (num == 3 && i>=change) {
                if (tileTypes[i] == tiles.GetTile<Tile>(location))
                {
                    xDirection = 1;
                    enemyRigidBody.AddForce(Vector2.right * xForce);
                    break;
                }
            } else if (num == 4 && i>=change) {
                if (tileTypes[i] == tiles.GetTile<Tile>(location))
                {
                    xDirection = -1;
                    enemyRigidBody.AddForce(Vector2.left * xForce);
                    break;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Vector2 jumpForce = new Vector2(xForce * xDirection, yForce);
            enemyRigidBody.AddForce(jumpForce);
        }
        if (collision.gameObject.tag == "Wall")
        {
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                xDirection = -1;
                enemyRigidBody.AddForce(Vector2.left * xForce);
            }
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                xDirection = 1;
                enemyRigidBody.AddForce(Vector2.right * xForce);
            }
        }
        if (collision.gameObject.tag == "ThrowingObject")
        {
            portal.enemyCount -= 1;
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.x <= minimumXPosition)
        {
            xDirection = 1;
            enemyRigidBody.AddForce(Vector2.right * xForce);
        }
        if (transform.position.x >= maximumXPosition)
        {
            xDirection = -1;
            enemyRigidBody.AddForce(Vector2.left * xForce);
        }        
    }
}
