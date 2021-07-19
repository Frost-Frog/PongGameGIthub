using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOB : MonoBehaviour
{
    public float frequency;
    public float intensity;
    // Start is called before the first frame update
    void Start()
    {
        //Time.time * frequency
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.up * Mathf.Cos(Time.time * frequency) * intensity);
        Debug.Log(Time.time);
    }
}
