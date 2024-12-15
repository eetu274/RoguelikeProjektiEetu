using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiScript : MonoBehaviour
{

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        // Lis‰‰ pelaaja automaattisesti
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;

        if (Player != null)
        {

            if (Player.transform.position.x < transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1; // Jos pelaaja on x akselissa pidemm‰ll‰ niin vihollinen vaihtaa puolta
            }
            else
            {
                scale.x = Mathf.Abs(scale.x);
            }

            transform.localScale = scale;
        }
    }
}
