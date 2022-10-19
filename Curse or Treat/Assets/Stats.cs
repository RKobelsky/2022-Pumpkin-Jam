using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    public Player player;

    public Text distanceText;
    public Text timeText;
    public Text candyText;
    // Start is called before the first frame update
    void Start()
    {
        distanceText.text = ((int)player.distance).ToString();
        timeText.text = ((int)player.time).ToString();
        candyText.text = ((int)player.candiesEaten).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
