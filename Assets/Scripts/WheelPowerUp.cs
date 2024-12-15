using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WheelPowerUp : MonoBehaviour
{
    private float RotatePower;
    public float StopPower;

    private Rigidbody2D rbody;

    PlayerMovement playerSpeed;
    Shoot playerBulletSpeed;
    PlayerHP playerHeal;
    Shoot playerFirerate;
    Slider HpBarValue;
    public Text powerUpText;

    int inRotate;

    private float message;

    [SerializeField] private Animator wheelAnimationController;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        // Otetaan toisesta scriptist‰ muuttujat ett‰ voidaan antaa lis‰‰ voimia
        playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerBulletSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoot>();
        playerHeal = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
        playerFirerate = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoot>();
        HpBarValue = GameObject.FindGameObjectWithTag("healthBar").GetComponent<Slider>();

    }

    float t;
    private void Update()
    {

        RotatePower = Random.Range(300f, 1000f);

        // Pyˆr‰ nopeutuu
        if (rbody.angularVelocity > 0)
        {
            rbody.angularVelocity -= StopPower * Time.deltaTime;

            rbody.angularVelocity = Mathf.Clamp(rbody.angularVelocity, 0, 1440);
        }

        // Otetaan selv‰‰ milloin pyˆr‰ on pys‰htynyt
        if (rbody.angularVelocity == 0 && inRotate == 1)
        {
            t += 1 * Time.deltaTime;
            if (t >= 0.5f)
            {
                GetReward();
                wheelAnimationController.SetInteger("PlayWheel", 2);

                inRotate = 0;
                t = 0;
            }
        }
    }


    // Pyˆritet‰‰n pyˆr‰‰
    public void Rotate()
    {
        if (inRotate == 0)
        {
            rbody.AddTorque(RotatePower);
            inRotate = 1;
        }
    }


    // Saadaan PowerUp
    public void GetReward()
    {
        float rot = transform.eulerAngles.z;

        if (rot > 0 && rot <= 90)
        {
            Debug.Log("movement");

            playerSpeed.moveSpeed += 0.35f;

            powerUpText.text = "movement speed";
            
        }

        else if (rot > 90 && rot <= 180)
        {
            Debug.Log("bullet speed");

            playerBulletSpeed.bulletForce += 5f;

            powerUpText.text = "bullet speed";
        }

        else if (rot > 180 && rot <= 270)
        {
            Debug.Log("heal");

            playerHeal.currentHealth = 300;
            HpBarValue.value = 300;

            powerUpText.text = "heal";
        }

        else if (rot > 270 && rot <= 360)
        {
            Debug.Log("firerate");

            playerFirerate.bulletCooldown -= 0.3f;

            powerUpText.text = "firerate";
        }

    }

}