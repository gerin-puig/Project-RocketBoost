using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] float rotateVar = 250f;
    [SerializeField] float thrustForce = 25f;

    new Rigidbody rigidbody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateShip();
        Thruster();
    }

    private void OnCollisionEnter(Collision collision)
    {
        string colName = collision.gameObject.tag;
        if(colName == "Friendly")
        {
            
        }
        else
        {

        }
    }

    private void RotateShip()
    {
        //take manual control of rotation
        rigidbody.freezeRotation = true;
        float rotationThisFrame = rotateVar * Time.deltaTime;

        //rotate left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        //rotate right
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidbody.freezeRotation = false;
    }

    private void Thruster()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * thrustForce);
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
