using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public GameObject hitEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f); // Kun ammus osuu johonkin missä on collider, se luo hitEffect animaation
        Destroy(gameObject);
    }

}
