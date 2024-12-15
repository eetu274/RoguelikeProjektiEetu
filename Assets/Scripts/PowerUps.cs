using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    public ParticleSystem smokeParticle;

    [SerializeField] private Animator wheelAnimationController;

    WheelPowerUp wheelPowerUpScript;

    private bool canOpenChest = true;

    private void Start()
    {
        wheelPowerUpScript = GameObject.FindGameObjectWithTag("Wheel").GetComponent<WheelPowerUp>();
    }


    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "powerUpBox" && canOpenChest == true)
        {

            StartCoroutine(AnimateAndSpinWheel());

            ParticleSystem smkParticle = Instantiate(smokeParticle, collision.transform.position, Quaternion.identity); // Kun Power UP laatikko on otettu syntyy particle effektit jonka j�lkeen ne tuhoutuu.
            Destroy(collision.gameObject);
            Destroy(smkParticle, 2f);

        }
    }

    // Laitetaan py�r� tekem��n animaatiot ja py�rim��n
    IEnumerator AnimateAndSpinWheel(float wheeltimer = 2f)
    {
        canOpenChest = false;

        wheelAnimationController.SetInteger("PlayWheel", 1);
        yield return new WaitForSeconds(wheeltimer);
        wheelPowerUpScript.Rotate();
        yield return new WaitForSeconds(10);
        wheelAnimationController.SetInteger("PlayWheel", 0);

        canOpenChest = true;
    }



}
