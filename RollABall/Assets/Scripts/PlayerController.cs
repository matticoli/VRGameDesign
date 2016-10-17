﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    public float speed;

    private Vector3 rinit;
    private bool jump;

    private int score;
    private Text scoreLabel;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rinit = transform.position;
        jump = false;
    }

    void Update () {
	
	}

    void FixedUpdate()
    {
        float my = Input.GetAxis("Vertical");
        float mx = Input.GetAxis("Horizontal");

        rb.AddForce(new Vector3(mx * speed, 0.0f, my * speed));
        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {
            rb.AddForce(new Vector3(0f, 20f, 0f), ForceMode.VelocityChange);
            // rb.AddExplosionForce(10f, transform.position + new Vector3(0f, -1f, 0f), 10f);
            jump = true;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
            transform.position = rinit;
        }
    }


    void OnTriggerEnter(Collider target)
    {
        Debug.Log(target.gameObject.GetType());
        if(target.gameObject.CompareTag("Pickup"))
        {
            target.gameObject.GetComponent<PickupController>().Die();
            BumpScore();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        jump = false;
    }

    public void BumpScore()
    {
        this.score += 100;
        if (scoreLabel == null)
        {
            scoreLabel = GameObject.Find("ScoreLabel").GetComponent<Text>();
        }
        scoreLabel.text = this.score.ToString();

    }
}

