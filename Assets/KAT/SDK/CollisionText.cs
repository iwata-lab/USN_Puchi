using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionText : MonoBehaviour
{

    public Text collisionText;
    private float displayTime = 2.0f;
    private float timer = 0.0f;
    private bool showText = false;

    void Start()
    {
        collisionText.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            collisionText.gameObject.SetActive(true);
            showText = true;
            timer = displayTime;
        }
    }

    void Update()
    {
        if (showText)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                collisionText.gameObject.SetActive(false);
                showText = false;
            }
        }
    }

    public void DisplayCollisionText()
    {
        collisionText.gameObject.SetActive(true);
        showText = true;
        timer = displayTime;

    }

}

