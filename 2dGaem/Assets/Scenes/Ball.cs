
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Ball : MonoBehaviour
{
    //public GameObject RightWall;
    //public GameObject LeftWall;
    bool CanClick;
    bool AutoLoad;
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
    //float jitter = 10f;
    int[] jitterDirection = {1, 2};
    int[] takeoffdirection = {-1, 1};
    public int restart;
    public GameObject blackhole;
    
    float xmove;
    float ymove;
    //float speed_increase;
    Vector2 movement;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    float opposite;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(1%5);
        CanClick = false;
        AutoLoad = true;
        pLeftWin.SetActive(false);
        pRightWin.SetActive(false);
        //speed = 6;
        //direction = new Vector2();
        radius = transform.localScale.y/2;
        pLeftcount = 0;
        pRightcount = 0;
        restart = 0;
        Starttakeoff();
        //speed_increase = 0;
        
    }

    IEnumerator SetCountText()
    {
         Score.text = pLeftcount.ToString() + " - " + pRightcount.ToString();
        if (pLeftcount >= 7)
        {
            CanClick = true;
            Time.timeScale = 0;
            foreach(GameObject Blackhole in GameObject.FindGameObjectsWithTag("Blackhole"))
            {
                Blackhole.SetActive(false);
            }
            //enabled = false;
            pLeftWin.SetActive(true);
            restart = 1;
            Debug.Log(CanClick);
            yield return new WaitForSecondsRealtime(5);
            if(AutoLoad) 
            {
                FindObjectOfType<AudioManager>().Stop("GameTheme");
                FindObjectOfType<AudioManager>().Play("MenuTheme");
                SceneManager.LoadScene("StartMenu");
                Time.timeScale = 1;
            }
            
            
        }

        if (pRightcount >= 7)
        {
            CanClick = true;
            Time.timeScale = 0;
            FindObjectsOfType<Gravity>();
            foreach(Gravity a in FindObjectsOfType<Gravity>())
            {
                GameObject.FindGameObjectWithTag("Blackhole").SetActive(false);
            }
            //enabled = false;
            pRightWin.SetActive(true);
            restart = 1;
            Debug.Log(CanClick);
            yield return new WaitForSecondsRealtime(5);
            if(AutoLoad) 
            {
                FindObjectOfType<AudioManager>().Stop("GameTheme");
                FindObjectOfType<AudioManager>().Play("MenuTheme");
                SceneManager.LoadScene("StartMenu");
                Time.timeScale = 1;
            } 
        }
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_rigidbody.velocity.x);
        if(_rigidbody.velocity.x > -8 - ((hitcount - hitcount%5)/5) && _rigidbody.velocity.x < 8 + ((hitcount - hitcount%5)/5))
        {
            if (_rigidbody.velocity.x > 0) 
            {

                _rigidbody.velocity = new Vector2(8 + ((hitcount - hitcount%5)/5), _rigidbody.velocity.y);
                //_rigidbody.AddForce(new Vector2(1*speed, 0));
            }
            if (_rigidbody.velocity.x < 0) 
            {

                _rigidbody.velocity = new Vector2(-8 - ((hitcount - hitcount%5)/5), _rigidbody.velocity.y);
                //_rigidbody.AddForce(new Vector2(-1*speed, 0));
            }
           
        }
        if(_rigidbody.velocity.x < -8 - ((hitcount - hitcount%5)/5) || _rigidbody.velocity.x > 8 + ((hitcount - hitcount%5)/5))
        {
            if (_rigidbody.velocity.x > 0) 
            {

                _rigidbody.velocity = new Vector2(8 + ((hitcount - hitcount%5)/5), _rigidbody.velocity.y);
                //_rigidbody.AddForce(new Vector2(1*speed, 0));
            }
            if (_rigidbody.velocity.x < 0) 
            {

                _rigidbody.velocity = new Vector2(-8 - ((hitcount - hitcount%5)/5), _rigidbody.velocity.y);
                //_rigidbody.AddForce(new Vector2(-1*speed, 0));
            }
           
        }
        if (CanClick == true)
        {
            if (Input.GetKeyDown(KeyCode.Return) == true)
            {
                AutoLoad = false;
                FindObjectOfType<AudioManager>().Stop("GameTheme");
                FindObjectOfType<AudioManager>().Play("MenuTheme");
                SceneManager.LoadScene("StartMenu");
                Time.timeScale = 1;
            }
        }
       // Debug.Log(_rigidbody.velocity.x);
        
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
        

        if (collision.gameObject.tag == "Paddle"|| collision.gameObject.tag == "AIPaddle")
        {
            FindObjectOfType<AudioManager>().Play("PongPaddleAudio");
            Vector2 normal = collision.GetContact(0).normal;
            Debug.Log(_rigidbody.velocity.x);
        
            int jitterDir = jitterDirection[Random.Range(0, 2)];
            bool isRight = true;
            
            if(collision.gameObject.tag == "Paddle")
            {
                isRight = collision.gameObject.GetComponent<Paddle>().isRight;
                return;
            }
            

            if (isRight == true || collision.gameObject.tag == "AIPaddle")
            {
                //_rigidbody.AddForce(new Vector2(-2*_rigidbody.velocity.x*-2 , 100));
                hitcount += 1;
                if (jitterDir == 1)
                {
                    _rigidbody.AddForce(new Vector2 (0, -10));
                }
                if (jitterDir == 2)
                {
                    _rigidbody.AddForce(new Vector2 (0, 10));
                }
                
            }
            if (isRight == false)
            {
                //_rigidbody.AddForce(new Vector2(-2*_rigidbody.velocity.x , 100));
                hitcount += 1;
                if (jitterDir == 1)
                {
                    _rigidbody.AddForce(new Vector2 (0, -10));
                }
                if (jitterDir == 2)
                {
                    _rigidbody.AddForce(new Vector2 (0, 10));
                }
                
            }
        

            float move = collision.gameObject.GetComponent<Paddle>().move;
            float AImove = collision.gameObject.GetComponent<AIPaddle>().move;
            if (move != 0)
            {
                _rigidbody.AddForce(new Vector2(0, move*50));
            }
            if(AImove != 0)
            {
                _rigidbody.AddForce(new Vector2(0, move*50));
            }
            //Debug.Log(move);

            //Debug.Log(_rigidbody.velocity.y);
            
            if (hitcount % 5 == 0)
            {
        
                speed += 25;
               _rigidbody.AddForce(normal*25);
                
            }
            //Debug.Log(hitcount);
        }

        if (collision.gameObject.tag == "Wall")
        {
            FindObjectOfType<AudioManager>().Play("Score");
            
            bool rightwall = collision.gameObject.GetComponent<Wall>().rightwall;
           
            takeoff();
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
            
        }
        if (collision.gameObject.tag == "TopWall")
        {
            FindObjectOfType<AudioManager>().Play("WallBounce");
        }
    }

    void takeoff()
    {
        float Blackhole = Random.Range(1,5);
        if(Blackhole == 4)
        {
            GameObject clone = Instantiate(blackhole, new Vector2(Random.Range(-6.2f, 6.2f), Random.Range(-6.2f, 6.2f)), Quaternion.identity);
            clone.SetActive(true);
        }
        // int random1 = takeoffdirection[Random.Range(0, 2)];
        // int random2 = takeoffdirection[Random.Range(0, 2)];
        _rigidbody.velocity = Vector2.zero;
        transform.position = new Vector2(0, 0);
        // direction = new Vector2(random1, random2).normalized;
        
        xmove = Random.value < 0.5f ? -1.0f : 1.0f;
        ymove = Random.value < 0.5f ? Random.Range( -1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
        //direction = new Vector2(xmove, ymove);
        Vector2 movement = new Vector2(xmove * speed, ymove * speed);
        _rigidbody.AddForce(movement);
        //Debug.Log(movement.x);
    }
    void Starttakeoff()
    {
        // float Blackhole = 4 /*Random.Range(1,5)*/;
        // if(Blackhole == 4)
        // {
        //     Instantiate(GameObject.FindGameObjectWithTag("Blackhole"), new Vector2(0, Random.Range(-4f, 4f)), Quaternion.identity);
        // }
        // int random1 = takeoffdirection[Random.Range(0, 2)];
        // int random2 = takeoffdirection[Random.Range(0, 2)];
        _rigidbody.velocity = Vector2.zero;
        transform.position = new Vector2(0, 0);
        // direction = new Vector2(random1, random2).normalized;
        
        xmove = Random.value < 0.5f ? -1.0f : 1.0f;
        ymove = Random.value < 0.5f ? Random.Range( -1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
        //direction = new Vector2(xmove, ymove);
        Vector2 movement = new Vector2(xmove * speed, ymove * speed);
        _rigidbody.AddForce(movement);
        //Debug.Log(movement.x);
    }
}
