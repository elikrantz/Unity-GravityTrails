using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlipBox : MonoBehaviour
{
    public Rigidbody2D avatarRigidbody;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("BoxFlip"))
        {
            avatarRigidbody.gravityScale *= -1;
            Vector3 newDirection = transform.localScale;
            newDirection.y *= -1;
        }
    }
}
