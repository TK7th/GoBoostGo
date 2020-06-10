using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    //Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        //transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            print("Thrusting!!!");
            rigidBody.AddRelativeForce(Vector3.up);

        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            print("Rotating left");
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            print("Rotating right");
            transform.Rotate(-Vector3.forward);
        }
    }
}
