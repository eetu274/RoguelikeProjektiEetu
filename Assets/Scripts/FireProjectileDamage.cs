using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileDamage : MonoBehaviour
{
    public float damage = 0.05f;

    Slime slimeHP;
    DemonScript demonHP;
    BossScript bossHP;


    // Kun viholliset osuu collideriin niiden HP miinustuu
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Slime")
        {
            slimeHP = GameObject.FindGameObjectWithTag("Slime").GetComponent<Slime>();
            slimeHP.slimeSpeed = 1f;
            slimeHP.slimeHealth -= damage;
        }

        if (collider.gameObject.tag == "Demon")
        {
            demonHP = GameObject.FindGameObjectWithTag("Demon").GetComponent<DemonScript>();
            demonHP.demonSpeed = 1f;
            demonHP.demonHealth -= damage;
        }

        if (collider.gameObject.tag == "Boss")
        {
            bossHP = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript>();
            bossHP.bossSpeed = 0.3f;
            bossHP.BossTakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            slimeHP.slimeSpeed = 2f;
        }

        if (collision.gameObject.tag == "Demon")
        {
            demonHP.demonSpeed = 1.5f;
        }
    }
}
