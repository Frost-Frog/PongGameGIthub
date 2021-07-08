using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 

public class GameManager : MonoBehaviour
{
    //Gets A ball Component
    public GameObject BallGetter;
    public Ball ball;
    public Paddle paddleR; //Right Paddle object
    public Paddle paddleL; //Left Paddle Object

    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    // Start is called before the first frame update
    void Start()
    {
        // coverts pixel coordinate into game coordinate 
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        //create ball
        //Instantiate(ball);

        //create paddles
        Paddle paddle1 = Instantiate(paddleR) as Paddle;
        Paddle paddle2 = Instantiate(paddleL) as Paddle;
        paddle1.init(true); //rigth paddle
        paddle2.init(false); //left paddle
    }

    void Update()
    {
        // int restart = BallGetter.GetComponent<Ball>().restart;
        // if (restart == 1)
        // {
        //     if(Input.GetKeyDown(KeyCode.Return))
        //     {
        //     SceneManager.LoadScene("PongPlay");
            
        //     }
        //}
        
    }
}
   