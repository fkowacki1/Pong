using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public Rigidbody2D rb2d;
    public float ballspeed = 5f;
    public Vector3 vel;
    public bool isPlaying = false;
    public ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
       
        
    }

    private Vector3 GenerateRandomVelocity(bool ShouldReturnNormalized)
    {
        Vector3 velocity = new Vector3();
        bool shouldGoRight = Random.Range(1, 100) > 50;
        velocity.x = shouldGoRight ? Random.Range(0.8f, 0.3f) : Random.Range(-0.8f, -0.3f);
        velocity.y = shouldGoRight ? Random.Range(0.8f, 0.3f) : Random.Range(-0.8f, -0.3f);

        return ShouldReturnNormalized ? velocity.normalized : velocity;
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2d.velocity = Vector3.Reflect(vel, collision.contacts[0].normal);
        Vector3 NewVelocity = rb2d.velocity;
        NewVelocity += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        rb2d.velocity = NewVelocity.normalized * ballspeed;
        vel = rb2d.velocity;
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
        {
            scoreManager.IncrementLeftPlayerScore();
            print("Gracz lewy punkt");
        }
        if (transform.position.x < 0)
        {
            scoreManager.IncrementRightPlayerScore();
            print("Gracz prawy punkt");
        }
        ResetBallInRandomDirection();
        
    }
    
    private void SendBallInRandomDirection()
    { 
        rb2d.velocity = GenerateRandomVelocity(true) * ballspeed;
        vel = rb2d.velocity;
        isPlaying = true;
    }
    
    private void ResetBallInRandomDirection()
    {

        rb2d.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        rb2d.angularVelocity = 0f;
        vel = rb2d.velocity;
        isPlaying =false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isPlaying == false)
        {
            SendBallInRandomDirection();
        }

        if (rb2d.velocity.magnitude < ballspeed * 0.5f)
        {
            ResetBallInRandomDirection();
        }
    }
}
