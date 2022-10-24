using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovment : MonoBehaviour
{

    public KeyCode up;
    public KeyCode Down;
    public float movmentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State == "Play")
        {
            if (Input.GetKey(up) && transform.position.y <= 3.7f)
            {
                transform.position += new Vector3(0, movmentSpeed * Time.deltaTime, 0);
            }
            else if (Input.GetKey(Down) && transform.position.y >= -3.7f)
            {
                transform.position -= new Vector3(0, movmentSpeed * Time.deltaTime, 0);
            }
        }
    }
}
