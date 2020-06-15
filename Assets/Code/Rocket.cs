using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    private int scene = 0;

    // game states
    enum State { Alive, Dying, Transcending }
    State playerState = State.Alive;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == State.Alive) // todo somewhere stop sound on death
        {
            Thrust();
            Rotate();
        }
        else
        {
            PauseAudio();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (playerState != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing
                break;
            case "Finish":
                playerState = State.Transcending;
                Invoke("LoadNextLevel", 1f); // parameterize time
                break;
            default:
                playerState = State.Dying; // parameterize time
                Invoke("LoadFirstLevel", 2f);
                break;
        }
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        scene++;
        SceneManager.LoadScene(scene);
    }

    void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; // resume physics control of rotation
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {

            rigidBody.AddRelativeForce(Vector3.up * mainThrust);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            PauseAudio();
        }
    }

    private void PauseAudio()
    {
        audioSource.Pause();
    }
}
