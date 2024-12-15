using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public GameObject Player;
    public float slimeSpeed = 2f;
    public float slimeHealth = 3;

    public ParticleSystem enemyDeathParticle;

    private void Start()
    {
        // Lis‰‰ pelaaja automaattisesti
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

        private void Update()
    {
        Vector3 scale = transform.localScale;

        if (Player != null)
        {

            if (Player.transform.position.x < transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1; // Jos pelaaja on x akselissa pidemm‰ll‰ niin vihollinen vaihtaa puolta
            }
            else
            {
                scale.x = Mathf.Abs(scale.x);
            }

            transform.localScale = scale;


            transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, slimeSpeed * Time.deltaTime);
            // Vihollinen liikkuu aina paikasta miss‰ se sijaitsee pelaajan kordinaatteihin
        }
        else
        {
            Debug.LogWarning("Player not assigned!"); // Jos pelaajaa ei ole assignattu scriptiin (jonka t‰m‰n scriptin pit‰isi tehd‰ automaattisesti) tulee consoleen varoitus
        }


        if (slimeHealth <= 0)
        {
            ParticleSystem deathparticle = Instantiate(enemyDeathParticle, transform.position, Quaternion.identity); // Kun vihollisen HP on 0 se luo partikkelieffektin ja se tuhoutuu
            Destroy(deathparticle.gameObject, 1f);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // Kun vihollinen collidaa ja sill‰ objectilla on tag "Bullet", sen healthista l‰htee 1.
    {
        if (collision.gameObject.tag == "Bullet")
        {
            EnemyDamage(1);
        }
    }

    void EnemyDamage(int damage)
    {
        slimeHealth -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EnemyDeathArea")
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }



}
