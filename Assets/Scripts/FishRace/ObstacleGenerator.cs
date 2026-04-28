using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform player;
    public Vector2 spawnPosition;
    public float distanceBetweenObstacle = 40f;

    public GameObject warningPrefab;

    private Vector2 warningPosition;

    public float horizonDistance = 400f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // void Start()
    // {
    //     for (int i = 0; i < 10; i++)
    //     {
    //         int x = Random.Range(-3, 4);
    //         spawnPosition = new Vector2(x, spawnPosition.y + distanceBetweenObstacle);
    //         Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    //     }
    // }


    // Update is called once per frame
    void Update()
    {
        var totObstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        int numOfObstacles = totObstacles.Length;
        
        if (numOfObstacles < 1)
        {
            int x = Random.Range(-3, 4);
            spawnPosition = new Vector2(x, spawnPosition.y);
            warningPosition = new Vector2(x, spawnPosition.y-26f);

            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

            Instantiate(warningPrefab, warningPosition, Quaternion.identity);
        }

    }
}
