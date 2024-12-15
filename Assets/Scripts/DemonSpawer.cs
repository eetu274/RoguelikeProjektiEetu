using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSpawner : MonoBehaviour
{
    public GameObject demonPrefabA;
    public float spawnInterval = 30f;
    public float spawnDistance = 8.0f;
    public bool canSpawnDemon = true;
    [SerializeField] private GameObject player;

    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (canSpawnDemon == true)
        {
            if (timer >= spawnInterval) // Otetaan aikaa ja resetataan se ainakun funktio suoritetaan
            {
                SpawnDemonNearPlayer();
                timer = 0.0f;
            }
        }
    }

    void SpawnDemonNearPlayer()
    {
        if (player != null)
        {
            Vector2 playerPosition = player.transform.position;

            float randomAngle = Random.Range(0f, Mathf.PI * 2); // Otetaan satunnainen luku 360 asteen alueelta

            float offsetX = Mathf.Cos(randomAngle) * spawnDistance; // Otetaan satunnaiset kulmat ja lis‰t‰‰n niihin kuinka kauas viholliset spawnaa
            float offsetY = Mathf.Sin(randomAngle) * spawnDistance;

            Vector2 spawnPosition = new Vector2(playerPosition.x + offsetX, playerPosition.y + offsetY);

            Instantiate(demonPrefabA, spawnPosition, Quaternion.identity); // Spawnataan demon
        }
        else
        {
            Debug.LogError("Pelaajaa ei ole asetettu scriptiin!");
        }
    }
}
