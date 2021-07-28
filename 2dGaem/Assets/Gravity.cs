using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public GameObject ball;
    public Rigidbody2D ballrb;
    public float strength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Ball")
        {
            Vector2 difference = this.transform.position - ball.transform.position;
            float dist = difference.magnitude;
            Vector2 diretion = difference.normalized;
            float gravity = 6.7f * (this.transform.localScale.x * ballrb.mass * strength)/(3);
            Vector2 gravitymove = diretion * gravity;
            ballrb.AddForce(gravitymove, ForceMode2D.Force);

        }
        
    }
}
