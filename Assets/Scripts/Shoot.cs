using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private bool ajastin = true;

    public Transform firingPoint;
    public GameObject bulletPrefab;

    public float bulletCooldown = 0.3f;
    public float bulletForce = 15f;
    public bool canShoot;

    private void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1")) // Kun Mouse1 painetaan niin Shoott funktio suoritetaan
        {
            StartCoroutine(Shoott());
        }
    }

    IEnumerator Shoott()
    {
        if (ajastin == true && canShoot == true)
        {
            GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation); // Luodaan kopio luoti prefabista
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firingPoint.up * bulletForce, ForceMode2D.Impulse); // Luotiin annetaan AddForce joka työntää luotia eteenpäin joka myös kerrotaan bulletForcella 
                                                                            // joka antaa siihen lisää nopeutta
            ajastin = false;
            yield return new WaitForSeconds(bulletCooldown);
            ajastin = true;
        }
    }
}
