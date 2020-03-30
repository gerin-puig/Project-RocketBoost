using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketController : MonoBehaviour
{
    [SerializeField] float rotateVar = 250f;
    [SerializeField] float thrustForce = 25f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip shipExplosion;
    [SerializeField] AudioClip success;

    new Rigidbody rigidbody;
    AudioSource audioSource;

    enum State
    {
        Alive,
        Dying,
        Transcending
    }

    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToRotateInput();
            RespondToThrustInput();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive)
            return;

        string colName = collision.gameObject.tag;
        if(colName == "Finish")
        {
            state = State.Transcending;
            audioSource.Stop();
            audioSource.PlayOneShot(success);
            Invoke("LoadNextScene", 2f);
        }
        else if(colName == "Friendly")
        {

        }
        else
        {
            state = State.Dying;
            audioSource.Stop();
            audioSource.PlayOneShot(shipExplosion);
            Invoke("PlayerDeath", 2f);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void PlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        state = State.Alive;
    }

    private void RespondToRotateInput()
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

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * thrustForce);
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(mainEngine);
    }

}
