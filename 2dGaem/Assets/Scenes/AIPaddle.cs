using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    public Rigidbody2D Ballrb;
    protected Rigidbody2D _rigidbody;
    [SerializeField]
    float pspeed;
    
    float height;
    public float move;

    public bool isRight;
    string input;
    // Start is called before the first frame update
    void Start()
    {
      height = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(Ballrb.velocity.x > 0)
        {
            if(Ballrb.position.y > transform.position.y)
            {
                move = 1 * pspeed * Time.deltaTime;
            }
            if(Ballrb.position.y < transform.position.y)
            {
                move = -1 * pspeed * Time.deltaTime;
            }  

            if (transform.position.y < GameManager.bottomLeft.y + height/2 && move < 0)
            {
                move = 0;
            }
            if (transform.position.y > GameManager.topRight.y - height / 2 && move > 0)
            {
                move = 0;
            }

            //if(Ballrb.position.y > transform.position.y)
            //{
            transform.Translate(move * Vector2.up);
           // }
            //if(Ballrb.position.y < transform.position.y)
            //{
                //transform.Translate(move * Vector2.up);
            //}  
        }
        if(Ballrb.velocity.x < 0)
        {
            if(transform.position.y < 0)
            {
                move = 1 * pspeed * Time.deltaTime;
            }
            if(transform.position.y > 0)
            {
                move = -1 * pspeed * Time.deltaTime;
            }  

            if (transform.position.y < GameManager.bottomLeft.y + height/2 && move < 0)
            {
                move = 0;
            }
            if (transform.position.y > GameManager.topRight.y - height / 2 && move > 0)
            {
                move = 0;
            }
            transform.Translate(move * Vector2.up);

        }
    }

    public void init(bool IsRightPaddle)
    {
        isRight = IsRightPaddle;
        Vector2 pos = Vector2.zero;
        if (IsRightPaddle)
        {
            pos = new Vector2(GameManager.topRight.x - 1, 0);

            input = "PaddleRight";
        }
        else
        {
            pos = new Vector2(GameManager.bottomLeft.x + 1, 0);
            input = "PaddleLeft";
        }

        transform.position = pos;
        transform.name = input;
    }
}
