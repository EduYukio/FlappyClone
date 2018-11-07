using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleArrayScript : MonoBehaviour {

    List<GameObject> obstacles = new List<GameObject>();
    public GameObject obstacle = null;
    float[] obstacleHeights = new float[] {-2f, -3f, -4f, -5f, -6f};
    const float leftSideOfScreenX = -10.5f;
    const float initialObjectX = 14f;
    float x = initialObjectX;
    int lastRandom = 0;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 5; i++) {
            createObstacle(x);
            x += GameManager.distanceBetweenObjects;
        }

    }
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(-GameManager.pipeMovementSpeed * Time.deltaTime, 0, 0);

        foreach (GameObject obstacle in obstacles) {
            if(obstacle.transform.position.x < leftSideOfScreenX) {
                obstacles.Remove(obstacle);
                Destroy(obstacle);
                createObstacle(leftSideOfScreenX+GameManager.distanceBetweenObjects*5);
                break;
            }
        }
	}

    void createObstacle(float obstacleXPosition) {
        int randomIndex = Random.Range(0, 5);
        while(randomIndex == lastRandom) {
            randomIndex = Random.Range(0, 5);
        }
        lastRandom = randomIndex;
        float randomY = obstacleHeights[randomIndex];
        Vector3 obstaclePosition = new Vector3(obstacleXPosition, randomY, 0);

        GameObject newObstacle = Instantiate(obstacle, obstaclePosition, Quaternion.identity);
        obstacles.Add(newObstacle);
        newObstacle.transform.parent = this.gameObject.transform;
    }
}
