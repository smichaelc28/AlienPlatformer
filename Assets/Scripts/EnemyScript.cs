using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 1;
    private Vector2 curVelocity;
    private Vector3 curScale;

    void Start()
    {
        //Set initial direction and speed
        GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * speed, 0);
    }

    void Update()
    {
        //get the current velocity 
        curVelocity = GetComponent<Rigidbody2D> ().velocity;

        //resume walking if the enemy stops
        if(curVelocity.x <= 0)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y);
            GetComponent<Rigidbody2D>().velocity = new Vector2(curScale.x > 0 ? -1 : 1 * speed, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "GroundCheck")
        {
            Debug.Log("Killed By Jump!");
            Destroy(gameObject);
        }

        if(collision.tag == "Obstacle")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * curVelocity.x, 0);
            curScale = transform.localScale;
            curScale.x *= 1;
            transform.localScale = curScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.transform.GetComponent<PlayerManager>().SubtractHealth();
            Destroy(gameObject);
        }

        //shown here again. as some obstacles will have trigger colliders and some regular ones
        if (collision.collider.tag == "Obstacle")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * curVelocity.x, 0);
            curScale = transform.localScale;
            curScale.x *= -1;
            transform.localScale = curScale;
        }
    }
}
