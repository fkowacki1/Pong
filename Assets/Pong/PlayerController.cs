 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public bool IsUpKeyPressed() => Input.GetKey(upKey);
    public bool IsDownKeyPressed() => Input.GetKey(downKey);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (IsUpKeyPressed() && transform.position.y < 3.5f)
        {
            transform.position += Vector3.up * Time.deltaTime * speed;
        }

        if (IsDownKeyPressed() && transform.position.y > -3.5f)
        {
            transform.position += Vector3.down * Time.deltaTime * speed;
        }
    }
}
