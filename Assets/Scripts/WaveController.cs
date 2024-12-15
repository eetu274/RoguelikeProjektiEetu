using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour
{
    public float waveLength = 55;

    [SerializeField] private Animator animator;
    public GameObject samuraiObject;

    EnemySpawner spawnInterval;
    public GameObject waveText;
    Text waveText2;
    EnemySpawner canSpawnSlime;
    DemonSpawner canSpawnDemon;

    public string WaveTextAnimation = "WaveTextAnimation";

    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>();
        waveText2 = GameObject.FindGameObjectWithTag("WaveText").GetComponent<Text>();
        canSpawnSlime = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>();
        canSpawnDemon = GameObject.FindGameObjectWithTag("Spawner").GetComponent<DemonSpawner>();

        StartCoroutine(SpawnIncreaser());
    }


    // Spawnaus nopenee pelin edetessä
    IEnumerator SpawnIncreaser()
    {
        animator.SetBool("PlayAnimation", true);
        yield return new WaitForSeconds(5);
        animator.SetBool("PlayAnimation", false);

        yield return new WaitForSeconds(waveLength);

        waveText2.text = "Wave 2";
        spawnInterval.spawnInterval = 3.5f;

        animator.SetBool("PlayAnimation", true);
        yield return new WaitForSeconds(5);
        animator.SetBool("PlayAnimation", false);

        yield return new WaitForSeconds(waveLength);

        waveText2.text = "Wave 3";
        spawnInterval.spawnInterval = 2f;

        animator.SetBool("PlayAnimation", true);
        yield return new WaitForSeconds(5);
        animator.SetBool("PlayAnimation", false);

        yield return new WaitForSeconds(waveLength);


        canSpawnSlime.canSpawnSlime = false;
        canSpawnDemon.canSpawnDemon = false;


        // Poistetaan kaikki pelissä olevat viholliset
        GameObject[] slimes = GameObject.FindGameObjectsWithTag("Slime");
        foreach (GameObject go in slimes)
            Destroy(go);

        GameObject[] demons = GameObject.FindGameObjectsWithTag("Demon");
        foreach (GameObject go in demons)
            Destroy(go);

        samuraiObject.SetActive(true);
    }
}
