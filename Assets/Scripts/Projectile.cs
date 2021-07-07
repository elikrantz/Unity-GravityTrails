using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Throwable direction;
    public float speed;

    private Vector3 directionVector;

    void Start()
    {
        direction = GameObject.FindGameObjectWithTag("Player").GetComponent<Throwable>();
        transform.position = direction.transform.position + direction.offset;
        directionVector = (direction.mousePos - transform.position).normalized;
        Invoke("DestroyThrowable", 1);
    }
    
    void Update()
    {
        //transform.position += direction.offset * Time.deltaTime * speed;
        //transform.position = Vector3.MoveTowards(transform.position, direction.throwablePosition, Time.deltaTime * speed);
        transform.Translate(directionVector * speed * Time.deltaTime);
    }

    private void DestroyThrowable()
    {
        Destroy(gameObject);
    }
}
