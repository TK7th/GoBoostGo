using UnityEngine;

public class RocketSpin : MonoBehaviour
{
    float rotateY;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Rotate(0, rotateY, 0);
            rotateY += .09f;
        }
        else
        {
            rotateY = 0;
        }
    }
}
