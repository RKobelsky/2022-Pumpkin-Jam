using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject prevCeiling;
    public GameObject prevFloor;
    public GameObject ceiling;
    public GameObject floor;

    public GameObject player;

    //Obstacles
    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3;
    public GameObject obstacle4;

    public GameObject obstaclePrefab;
    public GameObject medObstaclePrefab;
    public GameObject largeObstaclePrefab;

    public float minObstacleY;
    public float maxObstacleY;
    public float minObstacleSpacing;
    public float maxObstacleSpacing;
    public float minObstacleScaleY;
    public float maxObstacleScaleY;

    //Candies
    public GameObject candy1;
    public GameObject candy2;
    public GameObject candy3;
    public GameObject candy4;

    public GameObject butterCandyPrefab;
    public GameObject tricksyCandyPrefab;
    public GameObject twistyCandyPrefab;

    public float minCandyY;
    public float maxCandyY;
    public float minCandySpacing;
    public float maxCandySpacing;

   

    // Start is called before the first frame update
    void Start()
    {
        obstacle1 = GenerateObstacle(player.transform.position.x + 10);
        obstacle2 = GenerateObstacle(obstacle1.transform.position.x);
        obstacle3 = GenerateObstacle(obstacle2.transform.position.x);
        obstacle4 = GenerateObstacle(obstacle3.transform.position.x); 

        candy1 = GenerateCandy(player.transform.position.x + 15);
        candy2 = GenerateCandy(candy1.transform.position.x);
        candy3 = GenerateCandy(candy2.transform.position.x);
        candy4 = GenerateCandy(candy3.transform.position.x);


    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > floor.transform.position.x - 10)
        {
            var tempCeiling = prevCeiling;
            var tempFloor = prevFloor;
            prevCeiling = ceiling;
            prevFloor = floor;
            tempCeiling.transform.position += new Vector3(80, 0, 0);
            tempFloor.transform.position += new Vector3(80, 0, 0);
            ceiling = tempCeiling;
            floor = tempFloor;
        }

        if (player.transform.position.x > obstacle2.transform.position.x)
        {
            var tempObstacle = obstacle1;
            obstacle1 = obstacle2;
            obstacle2 = obstacle3;
            obstacle3 = obstacle4;

            SetTransform(tempObstacle, obstacle3.transform.position.x);
            obstacle4 = tempObstacle;

        }

        if (player.transform.position.x > candy2.transform.position.x)
        {
            var tempCandy = candy1;
            candy1 = candy2;
            candy2 = candy3;
            candy3 = candy4;

            SetTransformCandy(tempCandy, candy3.transform.position.x);
            candy4 = tempCandy;
        }
    }

    GameObject GenerateCandy(float referenceX)
    {
        int rand = Random.Range(1, 4);
        GameObject candy;
        candy = GameObject.Instantiate(twistyCandyPrefab);

        if (rand == 1)
        {
            candy = GameObject.Instantiate(butterCandyPrefab);
            SetTransformCandy(candy, referenceX);
            return candy;
        }
        else if (rand == 2)
        {
            candy = GameObject.Instantiate(tricksyCandyPrefab);
            SetTransformCandy(candy, referenceX);
            return candy;
        }
        else if (rand == 3)
        {
            candy = GameObject.Instantiate(twistyCandyPrefab);
            SetTransformCandy(candy, referenceX);
        }
        
        
        return candy;

    }
    GameObject GenerateObstacle(float referenceX)
    {
        int rand = Random.Range(1, 4);
        GameObject obstacle;
        obstacle = GameObject.Instantiate(obstaclePrefab);

        if (rand == 1)
        {
            obstacle = GameObject.Instantiate(obstaclePrefab);
            SetTransform(obstacle, referenceX);
        }
        else if (rand == 2)
        {
            obstacle = GameObject.Instantiate(medObstaclePrefab);
            SetTransform(obstacle, referenceX);
        }
        else
        {
            obstacle = GameObject.Instantiate(largeObstaclePrefab);
            SetTransform(obstacle, referenceX);
        }

        return obstacle;
    }

    void SetTransform(GameObject obstacle, float referenceX)
    {
        obstacle.transform.position = new Vector3(referenceX + Random.Range(
            minObstacleSpacing, maxObstacleSpacing), Random.Range(minObstacleY, maxObstacleY), 0);
    }

    void SetTransformCandy(GameObject candy, float referenceX)
    {
        candy.transform.position = new Vector3(referenceX + Random.Range(
            minCandySpacing, maxCandySpacing), Random.Range(minCandyY, maxCandyY), 0);
    }
}
