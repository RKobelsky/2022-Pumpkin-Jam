using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using TMPro;

public class Player : MonoBehaviour
{

    public GameObject player;

    public GameObject obstacle;
    
    public GameObject orangecandy;

    public GameObject bluecandy;

    public GameObject redcandy;

    public AudioSource neighborMusic;

    public bool levelSong = true;


    public GameOverScreen GameOverScreen;

    Rigidbody2D rb;

    float speed = 200f;
    public bool gameOver = false;

    
    public TextMeshProUGUI distanceText;
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
    public float distanceIncreasePerSecond = 4f;
    public float time = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        candySlider.maxValue = sugarVal;
        candySlider.value = sugarVal; 

        player.SetActive(true);
        redcandy.SetActive(true);
        bluecandy.SetActive(true);
        orangecandy.SetActive(true);
        obstacle.SetActive(true);
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (gameOver) return;
        distanceText.text = ((int)distance).ToString();
        hungerValue -= hungerDecreasePerSec * Time.fixedDeltaTime;

        candySlider.value -= hungerDecreasePerSec * Time.fixedDeltaTime;

        if (candyValue != 0f) candyValue -= timeSinceCandy * Time.fixedDeltaTime;
        if (candyValue < 0f) candyValue = 0f;

        distance += (distanceIncreasePerSecond * Time.fixedDeltaTime) * 10;
        time = Time.fixedDeltaTime;

        if (candySlider.value <= 0)
        {
            gameOver = true;
        }
    }

    void Update()
    {
        if (gameOver)
        {
            GameOverScreen.Setup((int)distance);

            player.SetActive(false);

            //destroy all game object clones once you die
            foreach(GameObject o in GameObject.FindObjectsOfType<GameObject>())
            {
                if(o.name == "Obstacle(Clone)")
                {
                o.SetActive(false);
                }
                if (o.name == "mediumObstacle(Clone)")
                {
                    o.SetActive(false);
                }
                if (o.name == "largeObstacle(Clone)")
                {
                    o.SetActive(false);
                }

                if (o.name == "tricksyCandy(Clone)")
                {
                o.SetActive(false);
                }

                if(o.name == "butterfingerCandy(Clone)")
                {
                o.SetActive(false);
                }

                if(o.name == "twistyCandy(Clone)")
                {
                o.SetActive(false);
                }
            }

            neighborMusic.Pause();
            levelSong = false;
            
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
        if (collider.gameObject.name == "Obstacle(Clone)" || 
            collider.gameObject.name == "mediumObstacle(Clone)" || collider.gameObject.name == "largeObstacle(Clone)")
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


            collider.gameObject.transform.position = new Vector2(collider.gameObject.transform.position.x, collider.gameObject.transform.position.y + 100);

        }
        if (collider.gameObject.name == "tricksyCandy(Clone)")
        {
            if (candySlider.value < 70f) candySlider.value += 30f;
            else candySlider.value = 100f; hungerValue += 30f;
            candyValue = 40f;
            speed = 400f;

            ++candiesEaten;
            collider.gameObject.transform.position = new Vector2(collider.gameObject.transform.position.x, collider.gameObject.transform.position.y + 100);

        }
        if (collider.gameObject.name == "twistyCandy(Clone)")
        {
            if (candySlider.value < 70f) candySlider.value += 30f;
            else candySlider.value = 100f;
            hungerValue += 30f;
            candyValue = 40f;

            ++candiesEaten;


            collider.gameObject.transform.position = new Vector2(collider.gameObject.transform.position.x, collider.gameObject.transform.position.y + 100);
        }

    }
}