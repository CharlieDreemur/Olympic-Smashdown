using UnityEngine;

public class ObstacleGeneration : MonoBehaviour
{
    [SerializeField] Vector2 mapMinBounds;
    [SerializeField] Vector2 mapMaxBounds;
    float mapWidth;
    float mapHeight;

    [SerializeField] float biomeWidth;
    [SerializeField] float biomeHeight;

    [SerializeField] Vector2 safeMinBounds;
    [SerializeField] Vector2 safeMaxBounds;
    
    [SerializeField] float obstacleProbabilityPerBiome = 1f;
    [SerializeField] float roundPositionToNearest = 0.5f;
    [SerializeField] bool shouldRotateObstacles = true;
    [SerializeField] bool shouldReflectObstacles = false;

    [SerializeField] GameObject obstaclePrefab;
    float obstacleWidth;
    float obstacleHeight;

    [SerializeField] GameObject wallPrefab;

    GameObject obstacleParent;

    void Awake()
    {
        // Calculate the map dimensions
        mapWidth = mapMaxBounds.x - mapMinBounds.x;
        mapHeight = mapMaxBounds.y - mapMinBounds.y;

        // Calculate the obstacle dimensions
        obstacleWidth = obstaclePrefab.transform.localScale.x;
        obstacleWidth = obstaclePrefab.transform.localScale.y;

        // Generate the obstacles
        GenerateWalls();
        GenerateObstacles();
    }

    private void GenerateWalls()
    {
        // Create a parent object for the walls
        GameObject wallParent = new GameObject("Walls");

        // Create the walls
        GameObject topWall = Instantiate(wallPrefab, wallParent.transform);
        topWall.transform.position = new Vector3(0f, mapMaxBounds.y + 1f, 0f);
        topWall.transform.localScale = new Vector3(mapWidth + 3f, 1f, 1f);

        GameObject bottomWall = Instantiate(wallPrefab, wallParent.transform);
        bottomWall.transform.position = new Vector3(0f, mapMinBounds.y - 1f, 0f);
        bottomWall.transform.localScale = new Vector3(mapWidth + 3f, 1f, 1f);

        GameObject leftWall = Instantiate(wallPrefab, wallParent.transform);
        leftWall.transform.position = new Vector3(mapMinBounds.x - 1f, 0f, 0f);
        leftWall.transform.localScale = new Vector3(1f, mapHeight + 3f, 1f);

        GameObject rightWall = Instantiate(wallPrefab, wallParent.transform);
        rightWall.transform.position = new Vector3(mapMaxBounds.x + 1f, 0f, 0f);
        rightWall.transform.localScale = new Vector3(1f, mapHeight + 3f, 1f);
    }

    private void GenerateObstacles()
    {
        obstacleParent = new GameObject("Obstacles");

        // Calculate the number of biomes on the map
        int numBiomesX = Mathf.FloorToInt(mapWidth / biomeWidth);
        int numBiomesY = Mathf.FloorToInt(mapHeight / biomeHeight);

        // Loop through each biome and generate obstacles with the specified probability
        for (float x = mapMinBounds.x; x < mapMaxBounds.x; x += biomeWidth)
        {
            for (float y = mapMinBounds.y; y < mapMaxBounds.y; y += biomeHeight)
            {
                // Generate a random number between 0 and 1 for this biome
                // If the random value is less than the probability, generate an obstacle
                if (Random.value >= obstacleProbabilityPerBiome)
                {
                    continue;
                }

                // Calculate the bounds of the current biome
                float minX = x;
                float minY = y;
                float maxX = minX + biomeWidth;
                float maxY = minY + biomeHeight;

                float obstacleZRot = 0f;
                if (shouldRotateObstacles)
                {
                    obstacleZRot = Random.Range(0, 4) * 90f;
                }
                float obstacleWidthNew = obstacleWidth;
                float obstacleHeightNew = obstacleHeight;
                if (obstacleZRot == 90f || obstacleZRot == 270f)
                {
                    obstacleWidthNew = obstacleHeight;
                    obstacleHeightNew = obstacleWidth;
                }

                Debug.Log(minX + obstacleWidthNew / 2f);
                Debug.Log(maxX - obstacleWidthNew / 2f);
                // Generate a random position for the obstacle within the bounds of the biome
                float obstacleX = Random.Range(minX + obstacleWidthNew / 2f, maxX - obstacleWidthNew / 2f);
                float obstacleY = Random.Range(minY + obstacleHeightNew / 2f, maxY - obstacleHeightNew / 2f);

                // Make sure the obstacle is entirely placed in the biome
                obstacleX = Mathf.Floor(obstacleX / 0.5f) * 0.5f + obstacleWidthNew / 2f;
                obstacleY = Mathf.Floor(obstacleY / 0.5f) * 0.5f + obstacleHeightNew / 2f;

                obstacleX = RoundToNearest(obstacleX, roundPositionToNearest);
                obstacleY = RoundToNearest(obstacleY, roundPositionToNearest);

                // Check if the obstacle overlaps with the safe region
                if (obstacleX - obstacleWidthNew / 2f < safeMaxBounds.x && obstacleX + obstacleWidthNew / 2f > safeMinBounds.x &&
                    obstacleY - obstacleHeightNew / 2f < safeMaxBounds.y && obstacleY + obstacleHeightNew / 2f > safeMinBounds.y)
                {
                    // If it does, skip this obstacle and move on to the next one
                    continue;
                }

                // Randomly choose one of the obstacle prefabs
                /// GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

                // Instantiate the obstacle and rotate it randomly
                GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(obstacleX, obstacleY, 0), Quaternion.identity);
                if (shouldReflectObstacles && Random.value < 0.5f)
                {
                    obstacle.transform.localScale = new Vector3(obstacle.transform.localScale.x * -1f, obstacle.transform.localScale.y, obstacle.transform.localScale.z);
                }
                obstacle.transform.Rotate(Vector3.forward, obstacleZRot);
                obstacle.transform.SetParent(obstacleParent.transform);

                // Set the obstacle's scale based on its dimensions
                /// obstacle.transform.localScale = new Vector3(obstacleWidthNew, obstacleHeightNew, 1);
            }
        }
    }

    public float RoundToNearest(float val, float nearest)
    {
        return Mathf.Round(val / nearest) * nearest;
    }
}
