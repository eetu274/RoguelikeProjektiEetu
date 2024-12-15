using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShoot : MonoBehaviour
{
    private bool ajastin = true;

    public bool canShootFire;

    public Transform firingPoint;
    public GameObject fireProjectilePrefab;

    public float fireProjectileCooldown = 5f;
    public float fireProjectileForce = 3f;

    private void Start()
    {
        canShootFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShootFire == true)
        {

            if (Input.GetButtonDown("Fire2")) // Kun Mouse2 painetaan niin Shoott funktio suoritetaan
            {
                StartCoroutine(ShootFire());
            }

        }
    }

    IEnumerator ShootFire()
    {
        if (ajastin == true)
        {
            Quaternion newRotation = firingPoint.rotation * Quaternion.Euler(0, 0, 90);
            GameObject bullet = Instantiate(fireProjectilePrefab, firingPoint.position, newRotation); // Luodaan kopio luoti prefabista
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firingPoint.up * fireProjectileForce, ForceMode2D.Impulse); // Luotiin annetaan AddForce joka työntää luotia eteenpäin joka myös kerrotaan bulletForcella 
                                                                            // joka antaa siihen lisää nopeutta
            ajastin = false;
            yield return new WaitForSeconds(fireProjectileCooldown);
            ajastin = true;
        }
    }
}