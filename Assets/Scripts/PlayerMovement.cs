using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // Tehd‰‰n liikkumisnopeus muuttuja josta tehd‰‰n public ett‰ sit‰ on helpompi muutta
    public Rigidbody2D rb;
    public Camera cam;
    public bool canMove;

    Vector2 movement;
    Vector2 mousePos;

    private void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // A ja D antaa -1 ja 1 luvun
        movement.y = Input.GetAxisRaw("Vertical"); // W ja S antaa -1 ja 1 luvun

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // Muutetaan hiiren lokaatio pelin kordinaatteihin n‰ytˆn kordinaateista
    }

    void FixedUpdate()
    {
        // Tarkistetaan voidaanko liikkua
        if (canMove == true)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); // movement kertoutuu moveSpeedill‰ joka antaa liikkumisnopeuden

            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; // Atan2 palauttaa kulman hiiren lokaatioon. Rad2Deg k‰‰nt‰‰ radiaanit asteiksi
            rb.rotation = angle;
        }
        else
        {
            Debug.Log("Cant move, canMove variable is set to false");
        }
    }
}