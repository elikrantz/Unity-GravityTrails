using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LineRenderer))]

public class LaserBeam : MonoBehaviour
{


    public float laserWidth = 1.0f;
    public float noise = 1.0f;
    public float maxLength = 50.0f;


    LineRenderer lineRenderer;
    int length;
    public Vector3[] position;
    //Cache any transforms here
    private Transform myTransform;
    Transform endEffectTransform;
    //The particle system, in this case sparks which will be created by the Laser
    private ParticleSystem endEffect;
    private Vector3 offset;


    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = laserWidth;
        myTransform = transform;
        offset = new Vector3(0, 0, 0);
        endEffect = GetComponentInChildren<ParticleSystem>();
        if (endEffect)
        {
            endEffectTransform = endEffect.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RenderLaser();
    }

    void RenderLaser()
    {

        //Shoot our laserbeam forwards!
        UpdateLength();
        //Move through the Array
        for (int i = 0; i < length; i++)
        {
            //Set the position here to the current location and project it in the forward direction of the object it is attached to
            offset.x = myTransform.position.x + i * myTransform.forward.x + Random.Range(-noise, noise);
            offset.y = -i * myTransform.up.y + Random.Range(-noise, noise) + myTransform.position.y;
            position[i] = offset;
            position[0] = myTransform.position;

            lineRenderer.SetPosition(i, position[i]);

        }



    }

    void UpdateLength()
    {

        //Raycast from the location of the cube forwards
        Vector3 lazerPos = transform.position;
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position, transform.forward, maxLength);
        Debug.DrawLine(transform.position, transform.forward, Color.green);
        int i = 0;
        while (i < hit.Length)
        {
            //Check to make sure we aren't hitting triggers but colliders
            if (!hit[i].collider.isTrigger)
            {
                length = (int)Mathf.Round(hit[i].distance) + 2;
                position = new Vector3[length];
                //Move our End Effect particle system to the hit point and start playing it
                if (endEffect)
                {
                    endEffectTransform.position = hit[i].point;
                    if (!endEffect.isPlaying)
                        endEffect.Play();
                }
                lineRenderer.positionCount = length;
                return;
            }
            i++;
        }
        
        //If we're not hitting anything, don't play the particle effects
        if (endEffect)
        {
            if (endEffect.isPlaying)
                endEffect.Stop();
        }
        length = (int)maxLength;
        position = new Vector3[length];
        lineRenderer.positionCount = length;


    }
}