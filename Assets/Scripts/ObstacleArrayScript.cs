using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleArrayScript : MonoBehaviour {

    List<GameObject> obstacles = new List<GameObject>();
    public GameObject obstacle = null;
    float[] obstacleHeights = new float[] {-2f, -3f, -4f, -5f, -6f};
    const float leftSideOfScreenX = -10.5f;
    const float distanceBetweenObjects = 5.9f;
    const float initialObjectX = 14f;
    const float rightmostObjectX = 13f;
    float x = initialObjectX;
    public static float obstaclesSpeed = 3f;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 5; i++) {
            createObstacle(x);
            x += distanceBetweenObjects;
        }

    }
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(-obstaclesSpeed * Time.deltaTime, 0, 0);

        foreach (GameObject obstacle in obstacles) {
            if(obstacle.transform.position.x < leftSideOfScreenX) {
                obstacles.Remove(obstacle);
                Destroy(obstacle);
                createObstacle(rightmostObjectX+distanceBetweenObjects);
                break;
            }
        }
	}

    void createObstacle(float obstacleXPosition) {
        int randomIndex = Random.Range(0, 5);
        float randomY = obstacleHeights[randomIndex];
        Vector3 obstaclePosition = new Vector3(obstacleXPosition, randomY, 0);

        GameObject newObstacle = Instantiate(obstacle, obstaclePosition, Quaternion.identity);
        obstacles.Add(newObstacle);
        newObstacle.transform.parent = this.gameObject.transform;
    }
}
