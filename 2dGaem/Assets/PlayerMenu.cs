using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
{
   //public GameObject yourChildObj;
    public GameObject PaddleDecider;
    private GameObject AIPaddle;
    public GameObject PlayerPaddle;
    //public AsyncOperation asyncOperation;
    
    void Start()
    {   
        //asyncOperation = SceneManager.LoadSceneAsync("PongPlay");
        
        DontDestroyOnLoad(PaddleDecider);
        
        //SceneManager.LoadScene("PongPlay", LoadSceneMode.Additive);
        //asyncOperation.allowSceneActivation = false;
        //yourChildObj.transform.parent = null;
       // DontDestroyOnLoad(yourChildObj);
    }
        
    public void PlayGame1Player()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("PongPlay");
        SceneManager.sceneLoaded += PlayGameSingle;
    }
    public void PlayGameSingle(Scene scene, LoadSceneMode mode)
    {
        
        //SceneManager.LoadScene("PongPlay");
        Scene scene2 = SceneManager.GetActiveScene();
        if(scene2.name == "PongPlay")
        {
            PaddleDecider.SetActive(false);
            AIPaddle = GameObject.Find("AIpaddle");
            PlayerPaddle = GameObject.Find("PaddleRight");
            AIPaddle.SetActive(true);
            PlayerPaddle.SetActive(false);
            //SceneManager.sceneLoaded -= PlayGameSingle;
        }
        if(scene2.name == "PongPlay")
        {
            SceneManager.sceneLoaded -= PlayGameSingle;
        }
        
    }
    public void PlayGame2Player()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("PongPlay");
        SceneManager.sceneLoaded += PlayGameDouble;
    }
    public void PlayGameDouble(Scene scene, LoadSceneMode mode)
    {
        
        //SceneManager.LoadScene("PongPlay");
        //scene = SceneManager.GetSceneByName("PongPlay");
        Scene scene2 = SceneManager.GetActiveScene();
        if(scene2.name == "PongPlay")
        {
            PaddleDecider.SetActive(false);
            AIPaddle = GameObject.Find("AIpaddle");
            PlayerPaddle = GameObject.Find("PaddleRight");
            AIPaddle.SetActive(false);
            PlayerPaddle.SetActive(true);
            //SceneManager.sceneLoaded -= PlayGameSingle;
        }
        if(scene2.name == "PongPlay")
        {
            SceneManager.sceneLoaded -= PlayGameDouble;
        }
    }
    
}
