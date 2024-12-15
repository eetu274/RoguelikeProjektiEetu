using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DemonBulletScript : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float force;
    PlayerHP playerHpScript;
    public GameObject fireBallAnimation;

    private Vector3 fireBallAnimationPos;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Ammus lent‰‰ pelaajan suuntaan
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        // Rotation pelaajaa p‰in
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);   
    }

    private void Update()
    {
        fireBallAnimationPos = transform.position;
        fireBallAnimationPos.y = fireBallAnimationPos.y - 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "InvisibleWalls")
        {
            GameObject fireBallAnimInstance = Instantiate(fireBallAnimation, fireBallAnimationPos, Quaternion.identity);
            Destroy(gameObject);
            Destroy(fireBallAnimInstance, 2);
        }

        if (collision.gameObject.tag == "Player")
        {
            playerHpScript = player.GetComponent<PlayerHP>();
            playerHpScript.TakeDamage(30);
            GameObject fireBallInstance = Instantiate(fireBallAnimation, fireBallAnimationPos, Quaternion.identity);
            Destroy(fireBallInstance, 1);
            Destroy(gameObject);

        }
    }

}
