using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Variables
    [SerializeField] float thrustValue;
    [SerializeField] float rotationValue;
    [SerializeField] AudioClip thrustAudioClip;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] ParticleSystem mainEngineThrustParticles;

    //References to Components
    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

//Thrusting the Rocket
    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Pressing Space - Thrusting");

            if(audioSource.isPlaying != true)
            {
                Debug.Log("Playing AudioClip");
                audioSource.PlayOneShot(thrustAudioClip);
            }

              if(mainEngineThrustParticles.isEmitting!=true)
              {
                mainEngineThrustParticles.Play();
              }

        rb.AddRelativeForce(Vector3.up * thrustValue * Time.deltaTime );
        }
        else
        {
            audioSource.Stop();
            mainEngineThrustParticles.Stop();
        }
    }

//Rotating the Rocket
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            Debug.Log("Pressing A - Rotating Left");
            
            if(leftThrustParticles.isEmitting!=true)
              {
                leftThrustParticles.Play();
              }

            ApplyRotation(rotationValue);
        }

        else if(Input.GetKey(KeyCode.D))
        {
            Debug.Log("Pressing D - Rotating Right");

            if(rightThrustParticles.isEmitting!=true)
              {
                rightThrustParticles.Play();
              }

            ApplyRotation(-rotationValue);
        }

        else if(Input.GetKeyUp(KeyCode.D))
        {
            rightThrustParticles.Stop();
        }
        else
        {
                leftThrustParticles.Stop();     
        }
    }

    // Applying the Rotation to the Rocket
     void ApplyRotation(float rotatingValue)
    {
        rb.freezeRotation = true; //Freezing Rotation from the Physics system 
        transform.Rotate(Vector3.forward * rotatingValue * Time.deltaTime);
        rb.freezeRotation = false; //Resuming Rotation by the Physics system
    }
}