using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoorScript : MonoBehaviour
{
    BossScript boss;

    private void Start()
    {

        StartCoroutine(FindBossWhenAvailable());
    }

    private IEnumerator FindBossWhenAvailable()
    {
        // Etsitään bossia niin kauan kunnes se löytyy
        while (boss == null)
        {
            GameObject bossObject = GameObject.FindGameObjectWithTag("Boss");
            if (bossObject != null)
            {
                boss = bossObject.GetComponent<BossScript>();
                Debug.Log("Boss found!");
            }
            yield return null;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Game ends when the boss is dead and the player walks through the final door
        if (collision.gameObject.name == "FinalDoor" && boss.canGoThroughDoor == true)
        {
            SceneManager.LoadSceneAsync(2);
        }
        else if (collision.gameObject.name == "FinalDoor" && boss == null)
        {
            Debug.Log("boss is null");
        }
    }
}