using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{

    public Rigidbody2D rb2d;
    public float ballspeed = 5f;
    public Vector3 vel;
    public bool isPlaying = false;
    public ScoreManager scoreManager;
    public float spinForce;
    public int multiplier = 0;
    public int mnoznikmocy = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        multiplier = 0;

    }

    private void FixedUpdate()
    {
      Vector2 curveForce = vel;
        
       curveForce.x *= Vector2.up.x * (multiplier * spinForce * Random.Range(0.8f, 0.3f));
       curveForce.y *= Vector2.up.y * (multiplier * spinForce * Random.Range(0.8f, 0.3f));
        
       rb2d.AddForce(curveForce, ForceMode2D.Force);
        
    } 

    private Vector3 GenerateRandomVelocity(bool ShouldReturnNormalized)
    {
        Vector3 velocity = new Vector3();
        bool shouldGoRight = Random.Range(1, 100) > 50;
        //velocity.x = shouldGoRight ? Random.Range(0.8f, 0.3f) : Random.Range(-0.8f, -0.3f);
        //velocity.y = shouldGoRight ? Random.Range(0.8f, 0.3f) : Random.Range(-0.8f, -0.3f);
        velocity.x = shouldGoRight ? Random.Range(0.6f, 0.4f) : Random.Range(-0.6f, -0.4f);
        velocity.y = shouldGoRight ? Random.Range(0.6f, 0.4f) : Random.Range(-0.6f, -0.4f);

        return ShouldReturnNormalized ? velocity.normalized : velocity;
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2d.velocity = Vector3.Reflect(vel, collision.contacts[0].normal);
        Vector3 NewVelocity = rb2d.velocity;
        NewVelocity += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        rb2d.velocity = NewVelocity.normalized * ballspeed;
        vel = rb2d.velocity;

        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        Vector2 curveForce = vel;

        curveForce.x *= Vector2.up.x * (multiplier * spinForce);
        curveForce.y *= Vector2.up.y * (multiplier * spinForce);
        if (playerController == null)
            return;

        if (playerController.IsDownKeyPressed())
        {
            multiplier = 1;
            rb2d.AddForce(curveForce * 10, ForceMode2D.Force);
            rb2d.AddTorque(10);
        }
            
        else
        if (playerController.IsUpKeyPressed())
        {
            multiplier = -1;
            rb2d.AddForce(curveForce * 10, ForceMode2D.Force);
            rb2d.AddTorque(-10);
        }

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
          //  ResetBallInRandomDirection();
        }
    }
}
