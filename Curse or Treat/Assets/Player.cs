using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    float speed = 200f;
    public bool gameOver = false;

    public Text hungerText;
    public float hungerValue = 100f;
    public float hungerDecreasePerSec = 10f;

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
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (gameOver) return;
        hungerText.text = ((int)hungerValue).ToString();
        hungerValue -= hungerDecreasePerSec * Time.fixedDeltaTime;


        if (candyValue != 0f) candyValue -= timeSinceCandy * Time.fixedDeltaTime;
        if (candyValue < 0f) candyValue = 0f;

        distance += distanceIncreasePerSecond * Time.fixedDeltaTime;
        time = Time.fixedDeltaTime;
    }
    void Update()
    {
        if (gameOver)
        {

            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
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
            hungerValue += 30f;
            candyValue = 40f;
            speed = 100f;

            ++candiesEaten;
            collider.gameObject.SetActive(false);
        }
        if (collider.gameObject.name == "tricksyCandy(Clone)")
        {
            hungerValue += 30f;
            candyValue = 40f;
            speed = 400f;

            ++candiesEaten;
            collider.gameObject.SetActive(false);
        }
        if (collider.gameObject.name == "twisty(Clone)")
        {
            hungerValue += 30f;
            candyValue = 40f;

            ++candiesEaten;
            collider.gameObject.SetActive(false);

        }

    }
}
