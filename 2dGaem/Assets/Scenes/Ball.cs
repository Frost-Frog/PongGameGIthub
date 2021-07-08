
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    //public GameObject RightWall;
    //public GameObject LeftWall;
    float hitcount = 0;
    private Rigidbody2D _rigidbody;
    public TextMeshProUGUI Score; 
    public float speed;
    float radius;
    Vector2 direction;
    public GameObject pLeftWin;
    public GameObject pRightWin;
    int pLeftcount;
    int pRightcount;
    float jitter = 10f;
    int[] jitterDirection = {1, 2};
    int[] takeoffdirection = {-1, 1};
    public int restart;
    
    float xmove;
    float ymove;
    
    Vector2 movement;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        pLeftWin.SetActive(false);
        pRightWin.SetActive(false);
        //speed = 6;
        //direction = new Vector2();
        radius = transform.localScale.y/2;
        pLeftcount = 0;
        pRightcount = 0;
        restart = 0;
        takeoff();
        
    }

    IEnumerator SetCountText()
    {
         Score.text = pLeftcount.ToString() + " - " + pRightcount.ToString();
        if (pLeftcount >= 7)
        {
            Time.timeScale = 0;
            enabled = false;
            pLeftWin.SetActive(true);
            restart = 1;
            yield return new WaitForSecondsRealtime(5);
            SceneManager.LoadScene("Menu");
            
        }

        if (pRightcount >= 7)
        {
            Time.timeScale = 0;
            enabled = false;
            pRightWin.SetActive(true);
            restart = 1;
            yield return new WaitForSecondsRealtime(5);
            SceneManager.LoadScene("Menu");   
        }
    }


    // Update is called once per frame
    void Update()
    {
        // transform.Translate(direction * speed * Time.deltaTime);
        // if (transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0)
        // {
        //     direction.y = -direction.y;
        // }
        // if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0)
        // {
        //     direction.y = -direction.y;
            
        // }
        

        //if (transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0)
        //{
        //    Debug.Log("Right Player Wins");

        //    Time.timeScale = 0;
        //    enabled = false;
        //}
        //if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0)
        //{
        //    Debug.Log("Left Player Wins");

        //    Time.timeScale = 0;
        //    enabled = false;
        //}
    }

    void OnCollisionEnter2D (Collision2D collision)
    {

        if (collision.gameObject.tag == "Paddle")
        {
            int jitterDir = jitterDirection[Random.Range(0, 2)];
            
            bool isRight = collision.gameObject.GetComponent<Paddle>().isRight;

            if (isRight == true)
            {
                hitcount += 1;
                if (jitterDir == 1)
                {
                    movement.y = movement.y - jitter;
                }
                if (jitterDir == 2)
                {
                    movement.y = movement.y + jitter;
                }
                //direction.x = -direction.x; 
            }
            if (isRight == false)
            {
                hitcount += 1;
                if (jitterDir == 1)
                {
                    movement.y = movement.y - jitter;
                }
                if (jitterDir == 2)
                {
                    movement.y = movement.y + jitter;
                }
                //direction.x = -direction.x;
                
            }

            if (hitcount % 10 == 0)
            {
                speed += 100;
                Debug.Log(hitcount);
            }

            float move = collision.gameObject.GetComponent<Paddle>().move;
            if (move != 0)
            {
                movement.y = movement.y + move*50;
            }
            Debug.Log(hitcount);
        }

        if (collision.gameObject.tag == "Wall")
        {
            bool rightwall = collision.gameObject.GetComponent<Wall>().rightwall;
           
           
            if (rightwall == true)
            {
                pLeftcount += 1;
                StartCoroutine(SetCountText());;
            }
            if (rightwall == false)
            {
                pRightcount += 1;
                StartCoroutine(SetCountText());;
            }
            takeoff();
        }
        
    }

    void takeoff()
    {
        // int random1 = takeoffdirection[Random.Range(0, 2)];
        // int random2 = takeoffdirection[Random.Range(0, 2)];
        _rigidbody.velocity = Vector2.zero;
        transform.position = new Vector2(0, 0);
        // direction = new Vector2(random1, random2).normalized;
        
        float xmove = Random.value < 0.5f ? -1.0f : 1.0f;
        float ymove = Random.value < 0.5f ? Random.Range( -1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
        //direction = new Vector2(xmove, ymove);
        Vector2 movement = new Vector2(xmove * speed, ymove * speed);
        _rigidbody.AddForce(movement);

    }
}
