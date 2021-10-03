using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Rigidbody2D _rigidbody;
    [SerializeField]
    float pspeed;
    private bool canexplode = false;
    GameObject explode;
    public Explodable _explodable;
    public GameObject PaddleBreak;
    GameObject ParticlePosLeft;
    GameObject ParticlePosRight;
    ParticleSystem psLeft;
    ParticleSystem psRight;
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
        if(Input.GetKeyDown(KeyCode.E))
        {
            canexplode = !canexplode;
        }
        //movement
        move = Input.GetAxisRaw(input) * Time.deltaTime * pspeed;
        
        if (transform.position.y < GameManager.bottomLeft.y + height/2 && move < 0)
        {
            move = 0;
        }
        if (transform.position.y > GameManager.topRight.y - height / 2 && move > 0)
        {
            move = 0;
        }
        transform.Translate(move * Vector2.up);
        if(Input.GetKey(KeyCode.LeftAlt))
        {
            if(!isRight)
            {
                transform.localScale = new Vector2(transform.localScale.x, 20);
                height = transform.localScale.y;
                return;
            }
        }
        if(Input.GetKeyUp(KeyCode.LeftAlt))
        {
            if(!isRight)
            {
                transform.localScale = new Vector2(transform.localScale.x, 3.5f);
                height = transform.localScale.y;
                return;
            }
            
        }
        // if(Input.GetKey(KeyCode.RightShift))
        // {
        //     if(isRight)
        //     {
        //         transform.localScale = new Vector2(transform.localScale.x, 20);
        //         height = transform.localScale.y;
        //         return;
        //     }
        // }
        // if(Input.GetKeyUp(KeyCode.RightShift))
        // {
        //     if(isRight)
        //     {
        //         transform.localScale = new Vector2(transform.localScale.x, 3.5f);
        //         height = transform.localScale.y;
        //         return;
        //     }
        // }
    }

    public void init(bool IsRightPaddle)
    {
        isRight = IsRightPaddle;
        //Vector2 pos = Vector2.zero;
        if (IsRightPaddle)
        {
            //pos = new Vector2(GameManager.topRight.x - 1, 0);

            input = "PaddleRight";
        }
        else
        {
            //pos = new Vector2(GameManager.bottomLeft.x + 1, 0);
            input = "PaddleLeft";
        }

       // transform.position = pos;
        transform.name = input;
    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        psRight = GameObject.Find("HitParticleRight").GetComponent<ParticleSystem>();
        psLeft = GameObject.Find("HitParticleLeft").GetComponent<ParticleSystem>();
        ParticlePosLeft = GameObject.Find("HitParticleLeft");
        ParticlePosRight = GameObject.Find("HitParticleRight");
        if(collider.gameObject.tag == "Ball")
        {
            
            if(isRight)
            {
                ParticlePosRight.transform.position = new Vector3(ParticlePosRight.transform.position.x, GameObject.Find("Ball").transform.position.y, ParticlePosRight.transform.position.z);
                psRight.Play();
            }
            
            if(!isRight)
            {
                ParticlePosLeft.transform.position = new Vector3(ParticlePosLeft.transform.position.x, GameObject.Find("Ball").transform.position.y, ParticlePosLeft.transform.position.z);
                psLeft.Play();
            }
            if(canexplode == true)
            {
                ContactPoint2D contact = collider.GetContact(0);
                if(contact.point.y < transform.position.y)
                {
                    transform.localScale -= new Vector3(0,1,0) * Mathf.Abs(transform.position.y - height/2 - contact.point.y); 
                    transform.position += new Vector3(0,1,0) * Mathf.Abs(transform.position.y - height/2 - contact.point.y)/2; 
                    
                    explode = Instantiate(PaddleBreak, new Vector3(-10,contact.point.y,0), Quaternion.identity);
                    explode.transform.localScale = new Vector3(1,1* Mathf.Abs(transform.position.y - height/2 - contact.point.y),0);
                    explode.transform.position = new Vector3(this.transform.position.x,contact.point.y,0);
                    
                    //Debug.Log(this.transform.position.y);
                    //Debug.Log(contact.point.y);
                }
                if(contact.point.y > transform.position.y)
                {
                    transform.localScale -= new Vector3(0,1,0) * Mathf.Abs(transform.position.y + height/2 - contact.point.y); 
                    transform.position -= new Vector3(0,1,0) * Mathf.Abs(transform.position.y + height/2 - contact.point.y)/2; 
                    
                    explode = Instantiate(PaddleBreak, new Vector3(-10,contact.point.y,0), Quaternion.identity);
                    explode.transform.localScale = new Vector3(1,1* Mathf.Abs(transform.position.y + height/2 - contact.point.y),0);
                    explode.transform.position = new Vector3(this.transform.position.x,contact.point.y,0);
                    
                    //Debug.Log(this.transform.position.y);
                    //Debug.Log(contact.point.y);
                }
                    
                height = transform.localScale.y;
                Explodable _explodable = explode.GetComponent<Explodable>();
                _explodable.explode();
                ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
                ef.doExplosion(explode.transform.position);
            }
            
        }

    }
}
