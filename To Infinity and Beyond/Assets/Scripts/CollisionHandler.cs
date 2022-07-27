using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //Variables
    [SerializeField]float delayValue;
    [SerializeField] AudioClip crashAudioClip;
    [SerializeField] AudioClip successAudioClip;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem explosionParticles;

    //References to Components
    Movement movement;
    AudioSource audioSource;


    //States
    bool isColliding = false;

    // Start is called before the first frame update
     void Start()
     {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
     }

    //Checking for Collision
    void OnCollisionEnter(Collision other) 
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
            Debug.Log("This thing is Friendly, you are safe");
            break;

            case "Finish":
            Debug.Log("Congratulations!!! You have finished");
            FinishSequence();
            break;

            default:
            Debug.Log("You Have Crashed :(");
            CrashSequence();
            break;
        }
    }

    //Reloading the Scene
    void ReloadLevel()
    {
        int currentSceneIndex;

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    //Loading the Next Level
    void LoadNextLevel()
    {
       int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       int maxSceneIndex = SceneManager.sceneCountInBuildSettings; 

       //Debug.Log(currentSceneIndex);
       //Debug.Log(maxSceneIndex);
       
       if(currentSceneIndex < maxSceneIndex-1)
       {
        SceneManager.LoadScene(currentSceneIndex+1);
       }

       else if(currentSceneIndex == maxSceneIndex-1)
       {
        currentSceneIndex = 0;
        SceneManager.LoadScene(currentSceneIndex);
        }
    }

    void CrashSequence()
    {
        if(!isColliding)
        {
            audioSource.Stop();
            isColliding = true;
            movement.enabled = false;
            explosionParticles.Play();
            audioSource.PlayOneShot(crashAudioClip);
            Invoke("ReloadLevel",delayValue);
        }
    }

    void FinishSequence()
    {
        if(!isColliding)
        {
        audioSource.Stop();
        isColliding = true;
        movement.enabled = false;
        successParticles.Play();
        audioSource.PlayOneShot(successAudioClip);
        Invoke("LoadNextLevel",delayValue);
        }
    }
}