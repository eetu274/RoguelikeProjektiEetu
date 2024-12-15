using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutScript : MonoBehaviour
{
    public GameObject player;
    PlayerHP playerHpScript;
    private Rigidbody2D rb;
    public float force = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        // Ammus lentää pelaajaa päin
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        // Ammuksen pyöriminen
        transform.Rotate(0, 0, 3);
    }

    // Mitä tapahtuu kun ammus osuu mihinkin
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "InvisibleWalls")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            playerHpScript = player.GetComponent<PlayerHP>();
            playerHpScript.TakeDamage(30);
            Destroy(gameObject);

        }
    }
}
