using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefabA;
    public float spawnInterval = 5.0f;
    public float spawnDistance = 7.0f;
    [SerializeField] private GameObject player;
    public bool canSpawnSlime = true;

    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (canSpawnSlime == true)
        {
            if (timer >= spawnInterval) // Otetaan aikaa ja resetataan se ainakun funktio suoritetaan
            {
                SpawnEnemyNearPlayer();
                timer = 0.0f;
            }
        }
    }

    void SpawnEnemyNearPlayer()
    {
        if (player != null)
        {
            Vector2 playerPosition = player.transform.position;

            float randomAngle = Random.Range(0f, Mathf.PI * 2); // Otetaan satunnainen luku 360 asteen alueelta

            float offsetX = Mathf.Cos(randomAngle) * spawnDistance; // Otetaan satunnaiset kulmat ja lis‰t‰‰n niihin kuinka kauas viholliset spawnaa
            float offsetY = Mathf.Sin(randomAngle) * spawnDistance;

            Vector2 spawnPosition = new Vector2(playerPosition.x + offsetX, playerPosition.y + offsetY);

            if (canSpawnSlime == true)
            {
                Instantiate(enemyPrefabA, spawnPosition, Quaternion.identity); // Spawnataan demon
            }
        }
        else
        {
            Debug.LogError("Pelaajaa ei ole asetettu scriptiin!");
        }
    }

}
