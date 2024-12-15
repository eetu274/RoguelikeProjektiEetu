using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{

    Animator deathAnimation;
    public int maxHealth = 300;
    public int currentHealth;
    public GameObject fireBall;
    PlayerMovement playerMovement;
    Rigidbody2D rb;
    FireShoot fireShoot;
    Shoot shoot;

    public HealthBar healthBar;
    public GameObject miniBossExplode;

    // Start is called before the first frame update
    void Start()
    {
        fireShoot = GetComponent<FireShoot>();
        shoot = GetComponent<Shoot>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        deathAnimation = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnCollisionStay2D(Collision2D collision) // Kun osutaan vihollisiin suoritetaan damage funktio
    {
        if (collision.gameObject.tag == "Slime")
        {
            TakeDamage(1);
        }

        if (collision.gameObject.tag == "Demon")
        {
            TakeDamage(2);
        }

        if (collision.gameObject.tag == "Boss")
        {
            TakeDamage(2);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // Kun osuu minibossiin, se r‰j‰ht‰‰
    {
        if (collision.gameObject.tag == "MiniBoss")
        {
            Vector2 explosionPos = new Vector2(collision.transform.position.x - 0.4f, collision.transform.position.y - 1);

            TakeDamage(50);
            GameObject ExplosionInstance = Instantiate(miniBossExplode, explosionPos, Quaternion.identity);
            Destroy(ExplosionInstance, 1);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage) // Damagen ottamis script sek‰ koodi joka muuttaa healthbaria sen mukaan
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {

            rb.mass = 10000;

            // Kuoleman j‰lkeen pelaaja ei voi liikkua tai ampua
            playerMovement.canMove = false;
            shoot.canShoot = false;
            fireShoot.canShootFire = false;

            StartCoroutine(PlayerDeath());

        }
    }

    private IEnumerator PlayerDeath()
    {
        deathAnimation.SetBool("IsDead", true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync(3);
    }

}
