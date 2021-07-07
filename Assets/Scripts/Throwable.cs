using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Throwable : MonoBehaviour
{
    public GameObject objectThrown;
    public Vector3 offset;
    public int throwableCounter;
    public Text collectableCounter;

    public Vector3 mousePos;
    public Vector3 throwablePosition;

    void Update()
    {
        if (throwableCounter >= 1)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15));

                throwableCounter -= 1;
                offset = transform.localScale.x * new Vector3(1, 0, 0);
                //Vector3 throwablePosition = transform.position + offset;
                //throwablePosition = Vector3.MoveTowards(transform.position, mousePos, Mathf.Infinity);
                throwablePosition = mousePos;
                Instantiate(objectThrown, throwablePosition, transform.rotation);
            }
        }
        collectableCounter.text = throwableCounter.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectable")
        {
            throwableCounter++;
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Collectable")
        {
            throwableCounter++;
            Destroy(collision.gameObject);
        }
    }
}
