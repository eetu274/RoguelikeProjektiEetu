using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    BossScript dashCheck;
    public GameObject bullet;
    public Transform bulletPos;

    private float timer;

    private void Start()
    {
        dashCheck = GetComponent<BossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 3)
        {
            if (dashCheck != null && !dashCheck.isDashing) // Tarkistetaan dashaako boss sillä hetkellä ettei
            {                                 // liikkeet tehdä samaan aikaan
                timer = 0;
                shoot();
            }
        }
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
