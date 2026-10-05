using UnityEngine;

public class ProceduralArena : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject medkitPrefab;
    public int width = 40;
    public int depth = 40;
    public int obstacleCount = 12;
    public int enemyCount = 8;

    void Start()
    {
        BuildArena();
        SpawnEnemies();
        SpawnMedkits();
    }

    private void BuildArena()
    {
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = "Floor";
        floor.transform.localScale = new Vector3(width, 1f, depth);
        floor.transform.position = new Vector3(0f, -0.5f, 0f);

        CreateWall(new Vector3(0f, 2f, -depth / 2f), new Vector3(width, 4f, 1f));
        CreateWall(new Vector3(0f, 2f, depth / 2f), new Vector3(width, 4f, 1f));
        CreateWall(new Vector3(-width / 2f, 2f, 0f), new Vector3(1f, 4f, depth));
        CreateWall(new Vector3(width / 2f, 2f, 0f), new Vector3(1f, 4f, depth));

        for (int i = 0; i < obstacleCount; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-width / 2 + 4, width / 2 - 4),
                0.8f,
                Random.Range(-depth / 2 + 4, depth / 2 - 4));

            GameObject obstacle = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obstacle.name = "Obstacle_" + i;
            obstacle.transform.position = pos;
            obstacle.transform.localScale = new Vector3(Random.Range(2f, 6f), Random.Range(2f, 5f), Random.Range(2f, 6f));
            obstacle.GetComponent<Renderer>().material.color = Color.gray;
        }
    }

    private void CreateWall(Vector3 position, Vector3 scale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.name = "Wall";
        wall.transform.position = position;
        wall.transform.localScale = scale;
        wall.GetComponent<Renderer>().material.color = Color.green;
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            if (enemyPrefab == null)
            {
                enemyPrefab = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                enemyPrefab.name = "Enemy";
                enemyPrefab.AddComponent<EnemyAI>();
                enemyPrefab.tag = "Enemy";
            }

            Vector3 spawnPoint = new Vector3(
                Random.Range(-width / 2 + 2, width / 2 - 2),
                1f,
                Random.Range(-depth / 2 + 2, depth / 2 - 2));

            GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
            enemyObj.name = "Enemy_" + i;
            enemyObj.tag = "Enemy";
        }
    }

    private void SpawnMedkits()
    {
        for (int i = 0; i < 5; i++)
        {
            if (medkitPrefab == null)
            {
                medkitPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
                medkitPrefab.name = "MedkitPickup";
                medkitPrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                medkitPrefab.AddComponent<PickupItem>();
            }

            Vector3 position = new Vector3(
                Random.Range(-width / 2 + 2, width / 2 - 2),
                1f,
                Random.Range(-depth / 2 + 2, depth / 2 - 2));

            GameObject item = Instantiate(medkitPrefab, position, Quaternion.identity);
            item.name = "Medkit_" + i;
            item.GetComponent<PickupItem>().itemId = "medkit";
            item.GetComponent<PickupItem>().itemName = "Medkit";
            item.GetComponent<PickupItem>().type = ItemType.Medkit;
        }
    }
}
