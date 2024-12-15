using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileScript : MonoBehaviour
{

    public GameObject fireExploEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(fireExploEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f); // Kun ammus osuu johonkin missä on collider, se luo hitEffect animaation
        Destroy(gameObject);
    }

}
