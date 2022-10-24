using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovment : MonoBehaviour
{
    public float xBounds;
    public float yEdge;
    private Vector2 velocity;
    private Rigidbody2D rb2d;
    public AudioClip CollisionSound;
    public AudioClip WallBounce;
    public AudioClip DeathSound;
    public AudioSource audio;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //CollisionSound = GetComponent<AudioSource>();
        audio = GetComponent<AudioSource>();
    }

   
    void Start()
    {
        Reset();
    }


    private void FixedUpdate()
    {
        if (GameManager.Instance.State == "Play")
        {
            rb2d.MovePosition(rb2d.position + velocity * Time.fixedDeltaTime);

            if (Mathf.Abs(rb2d.position.y) >= yEdge)
            {
                WallCollision();
                audio.PlayOneShot(WallBounce);
            }
            if (Mathf.Abs(rb2d.position.x)>= xBounds)
            {
                Death();
                audio.PlayOneShot(DeathSound);
            }


            if (Mathf.Abs(transform.position.x) >= xBounds)
            {
                GameManager.Instance.UpdateScore(transform.position.x > 0 ? 1 : 2);
                Reset();
            }
        }
    }

    private void Reset()
    {
        GameManager.Instance.State = "Serve";
        GameManager.Instance.messagesGUI.text = "Press Enter";
        GameManager.Instance.messagesGUI.enabled = true;
        transform.position = new Vector3(
            0, 0, 0);

        velocity = new Vector2(
            GameManager.Instance.InitialBallSpeed * (Random.Range(0.0f, 1.0f) > 0.5f ? 1 : -1),
            GameManager.Instance.InitialBallSpeed * (Random.Range(0.0f, 1.0f) > 0.5f ? 1: -1)
            );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            velocity.x *= -1;

            velocity.x = IncrimentSpeed(velocity.x);
            velocity.y = IncrimentSpeed(velocity.y);
            audio.PlayOneShot(CollisionSound);
        }
    }


    private float IncrimentSpeed(float axis)
    {
        axis += axis > 0 ? +GameManager.Instance.BallSpeedIncriment : -GameManager.Instance.BallSpeedIncriment;
        return axis;
    }





    private void WallCollision()
    {
        velocity.y *= -1;

        rb2d.MovePosition(new Vector2(
            rb2d.position.x,
            rb2d.position.y > 0 ? yEdge - 0.01f : -yEdge + 0.01f
            ));
    }
    private void Death()
    {
        GameManager.Instance.UpdateScore(rb2d.position.x > 0 ? 1 : 2);
        Reset();
    }
}
