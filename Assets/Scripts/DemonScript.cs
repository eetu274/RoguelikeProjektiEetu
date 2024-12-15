using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DemonScript : MonoBehaviour
{
    public GameObject Player;
    public float demonSpeed = 1.5f;
    public float demonHealth = 10;

    public ParticleSystem enemyDeathParticle;
    public GameObject pwrUpBox;

    // Start is called before the first frame update
    void Start()
    {
        // Lisää pelaaja automaattisesti
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;

        if (Player != null)
        {

            if (Player.transform.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1; // Jos pelaaja on x akselissa pidemmällä niin vihollinen vaihtaa puolta
            }
            else
            {
                scale.x = Mathf.Abs(scale.x);
            }

            transform.localScale = scale;


            transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, demonSpeed * Time.deltaTime);
            // Vihollinen liikkuu aina paikasta missä se sijaitsee pelaajan kordinaatteihin
        }
        else
        {
            Debug.LogWarning("Player not assigned!"); // Jos pelaajaa ei ole assignattu scriptiin (jonka tämän scriptin pitäisi tehdä automaattisesti) tulee consoleen varoitus
        }

        if (demonHealth <= 0)
        {
            ParticleSystem deathparticle = Instantiate(enemyDeathParticle, transform.position, Quaternion.identity); // Kun vihollisen HP on 0 se luo partikkelieffektin ja se tuhoutuu
            Destroy(deathparticle.gameObject, 1f);
            Instantiate(pwrUpBox, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // Kun vihollinen collidaa ja sillä objectilla on tag "Bullet", sen healthista lähtee 1.
    {
        if (collision.gameObject.tag == "Bullet")
        {
            EnemyDamage(1);
        }
    }

    void EnemyDamage(int damage)
    {
        demonHealth -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EnemyDeathArea")
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
