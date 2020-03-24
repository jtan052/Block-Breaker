using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //config params
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    //state
    Vector2 paddleToBallVector;

    bool start = false;

    AudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!start)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
        
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            start = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
            
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddleToBallVector + paddlePos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (start)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)]; 
            myAudioSource.PlayOneShot(clip);
        }
    }
}
