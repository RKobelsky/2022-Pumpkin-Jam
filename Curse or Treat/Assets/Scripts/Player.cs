using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class Player : MonoBehaviour
{

    public AudioSource neighborMusic;

    public bool levelSong = true;

    public void LevelMusic() {
        levelSong = true;
        neighborMusic.Play();
    }

    public GameOverScreen GameOverScreen;

    Rigidbody2D rb;

    float speed = 200f;
    public bool gameOver = false;

    public Text hungerText;
    public float hungerValue = 100f;
    public float hungerDecreasePerSec = 10f;

    //candy meter
    public Slider candySlider;
    public float sugarVal;

    public float candyValue = 0f;
    public float timeSinceCandy = 10f;

    //score
    public int candiesEaten = 0;
    public float distance = 0f;
    public float distanceIncreasePerSecond = 1f;
    public float time = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        candySlider.maxValue = sugarVal;
        candySlider.value = sugarVal; 
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (gameOver) return;
        hungerText.text = ((int)hungerValue).ToString();
        hungerValue -= hungerDecreasePerSec * Time.fixedDeltaTime;

        candySlider.value -= hungerDecreasePerSec * Time.fixedDeltaTime;

        if (candyValue != 0f) candyValue -= timeSinceCandy * Time.fixedDeltaTime;
        if (candyValue < 0f) candyValue = 0f;

        distance += distanceIncreasePerSecond * Time.fixedDeltaTime;
        time = Time.fixedDeltaTime;
    }
    void Update()
    {
        if (gameOver)
        {
            GameOverScreen.Setup((int)distance);
            return;
        }
        if (candyValue == 0f)
        {
            speed = 200f;
        }
        
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.AddForce(new Vector3(0, speed, 0), ForceMode2D.Force);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                rb.velocity *= 0f;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                rb.AddForce(new Vector3(0, speed * -1, 0), ForceMode2D.Force);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                rb.velocity *= 0f;
            }
        
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    { 
        if (collider.gameObject.name == "Obstacle(Clone)")
        {
            gameOver = true;
            rb.isKinematic = true;
        }

        if (collider.gameObject.name == "butterfingerCandy(Clone)")
        {
            if (candySlider.value < 70f) candySlider.value += 30f;
            else candySlider.value = 100f;
            hungerValue += 30f;
            candyValue = 40f;
            speed = 100f;

            ++candiesEaten;
            


        }
        if (collider.gameObject.name == "tricksyCandy(Clone)")
        {
            if (candySlider.value < 70f) candySlider.value += 30f;
            else candySlider.value = 100f; hungerValue += 30f;
            candyValue = 40f;
            speed = 400f;

            ++candiesEaten;
            

        }
        if (collider.gameObject.name == "twistyCandy(Clone)")
        {
            if (candySlider.value < 70f) candySlider.value += 30f;
            else candySlider.value = 100f;
            hungerValue += 30f;
            candyValue = 40f;

            ++candiesEaten;
            


        }

    }
}