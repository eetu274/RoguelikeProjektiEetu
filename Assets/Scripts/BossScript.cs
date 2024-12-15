using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public GameObject deathExplosion;
    public GameObject Player;
    public GameObject warningSign;
    public GameObject miniBoss;
    Rigidbody2D rb;

    // Text When game is done
    Text text;
    Animator animator;

    // Door
    public bool canGoThroughDoor = false;

    // HealthBar
    public float bossMaxHealth = 100;
    public float bossCurrentHealth;
    BossHealthBar bossHealthBar;

    // Movement
    public bool isDashing = false;
    public float bossSpeed = 0.5f;
    private float timer;
    public float dashSpeed = 10;
    public float dashCooldown = 15;
    public float summonTimer;

    // Start is called before the first frame update
    void Start()
    {
        // Asetetaan mit‰ text ja animator on
        text = GameObject.FindGameObjectWithTag("WaveText").GetComponent<Text>();
        animator = GameObject.FindGameObjectWithTag("WaveText").GetComponent<Animator>();

        bossHealthBar = GameObject.FindGameObjectWithTag("BossHealthBar").GetComponent<BossHealthBar>();

        // Asetetaan hp n‰kyviin healthbariin
        bossCurrentHealth = bossMaxHealth;
        bossHealthBar.SetMaxHealth(bossMaxHealth);

        rb = GetComponent<Rigidbody2D>();

        // Lis‰‰ pelaaja automaattisesti
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Bossin damagen ottaminen tavan ammuksesta
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            BossTakeDamage(1);
        }

    }

    public void BossTakeDamage(float damage)
    {
        bossCurrentHealth -= damage;

        bossHealthBar.SetHealth(bossCurrentHealth);
    }


    // Update is called once per frame
    void Update()
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

            transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, bossSpeed * Time.deltaTime);
        }

        // Ajastin dashaamiselle
        timer += Time.deltaTime;
        if (timer >= dashCooldown)
        {
            StartCoroutine(BossDash());
            timer = 0; // Uusitaan ajastin
        }

        // Kun boss kuolee
        if (bossCurrentHealth <= 0)
        {
            canGoThroughDoor = true;

            Vector2 ExploPos = new Vector2(transform.position.x, transform.position.y - 1);
            GameObject ExplosionInstance = Instantiate(deathExplosion, ExploPos, Quaternion.identity);
            Destroy(ExplosionInstance, 1f);

            Destroy(gameObject);
            bossHealthBar.SetBarActive();

            text.text = "Escape the castle";
            animator.SetBool("PlayAnimation", true);
        }

        Vector2 miniBossSpawnPos = new Vector2(transform.position.x, transform.position.y - 2);
        // Slimejen summonaamis voima bossille
        summonTimer += Time.deltaTime;
        if (summonTimer >= 15)
        {
            summonTimer = 0;
            Instantiate(miniBoss, miniBossSpawnPos, Quaternion.identity);
        }

    }

    // Dashaaminen
    private IEnumerator BossDash()
    {
        isDashing = true;

        Vector2 dashDirection = (Player.transform.position - transform.position).normalized;

        warningSign.SetActive(true);

        bossSpeed = 0;

        yield return new WaitForSeconds(1);

        rb.velocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(2f);

        rb.velocity = Vector2.zero;

        bossSpeed = 0.5f;

        warningSign.SetActive(false);

        isDashing = false;
    }
}
