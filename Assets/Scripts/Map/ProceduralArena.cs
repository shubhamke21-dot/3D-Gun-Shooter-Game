using UnityEngine;

public class ProceduralArena : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject medkitPrefab;
    public GameObject ammoPrefab;
    public int arenaWidth = 40;
    public int arenaDepth = 40;
    public int obstacleCount = 14;
    public int enemyCount = 8;
    public int medkitCount = 5;
    public int ammoCount = 6;

    private void Start()
    {
        BuildArena();
        SpawnEnemies();
        SpawnPickups();
    }

    private void BuildArena()
    {
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = "Floor";
        floor.transform.localScale = new Vector3(arenaWidth, 1f, arenaDepth);
        floor.transform.position = new Vector3(0f, -0.5f, 0f);

        CreateWall(new Vector3(0f, 2f, -arenaDepth / 2f), new Vector3(arenaWidth, 4f, 1f));
        CreateWall(new Vector3(0f, 2f, arenaDepth / 2f), new Vector3(arenaWidth, 4f, 1f));
        CreateWall(new Vector3(-arenaWidth / 2f, 2f, 0f), new Vector3(1f, 4f, arenaDepth));
        CreateWall(new Vector3(arenaWidth / 2f, 2f, 0f), new Vector3(1f, 4f, arenaDepth));

        for (int i = 0; i < obstacleCount; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-arenaWidth / 2 + 4, arenaWidth / 2 - 4),
                0.8f,
                Random.Range(-arenaDepth / 2 + 4, arenaDepth / 2 - 4));

            GameObject obstacle = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obstacle.name = "Obstacle_" + i;
            obstacle.transform.position = pos;
            obstacle.transform.localScale = new Vector3(Random.Range(2f, 6f), Random.Range(2f, 5f), Random.Range(2f, 6f));
            obstacle.GetComponent<Renderer>().material.color = new Color(0.45f, 0.45f, 0.45f);
        }
    }

    private void CreateWall(Vector3 position, Vector3 scale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
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
                enemyPrefab.tag = "Enemy";
                enemyPrefab.AddComponent<EnemyAI>();
            }

            Vector3 spawnPoint = new Vector3(
                Random.Range(-arenaWidth / 2 + 2, arenaWidth / 2 - 2),
                1f,
                Random.Range(-arenaDepth / 2 + 2, arenaDepth / 2 - 2));

            GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
            enemy.name = "Enemy_" + i;
            enemy.tag = "Enemy";
        }
    }

    private void SpawnPickups()
    {
        for (int i = 0; i < medkitCount; i++)
        {
            if (medkitPrefab == null)
            {
                medkitPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
                medkitPrefab.name = "MedkitPickup";
                medkitPrefab.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                medkitPrefab.AddComponent<PickupItem>();
            }

            Vector3 position = new Vector3(Random.Range(-arenaWidth / 2 + 2, arenaWidth / 2 - 2), 1f, Random.Range(-arenaDepth / 2 + 2, arenaDepth / 2 - 2));
            GameObject pickup = Instantiate(medkitPrefab, position, Quaternion.identity);
            pickup.GetComponent<PickupItem>().itemId = "medkit";
            pickup.GetComponent<PickupItem>().itemName = "Medkit";
            pickup.GetComponent<PickupItem>().type = ItemType.Medkit;
            pickup.GetComponent<Renderer>().material.color = Color.red;
        }

        for (int i = 0; i < ammoCount; i++)
        {
            if (ammoPrefab == null)
            {
                ammoPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
                ammoPrefab.name = "AmmoPickup";
                ammoPrefab.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                ammoPrefab.AddComponent<PickupItem>();
            }

            Vector3 position = new Vector3(Random.Range(-arenaWidth / 2 + 2, arenaWidth / 2 - 2), 1f, Random.Range(-arenaDepth / 2 + 2, arenaDepth / 2 - 2));
            GameObject pickup = Instantiate(ammoPrefab, position, Quaternion.identity);
            pickup.GetComponent<PickupItem>().itemId = "ammo";
            pickup.GetComponent<PickupItem>().itemName = "Ammo";
            pickup.GetComponent<PickupItem>().type = ItemType.Ammo;
            pickup.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }
}
