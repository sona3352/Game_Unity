using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Box : MonoBehaviour
{
    public int Health = 100;
    public AudioSource audiosrc;
    public AudioClip box;
    public gamemaster gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("gamemaster").GetComponent<gamemaster>();
        audiosrc = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            gm.points += 10;
        }
    }
    void Damage(int damage) {
        audiosrc.PlayOneShot(box, 0.8f);
        Health -= damage;
        gameObject.GetComponent<Animation>().Play("redflash");
        
    }
}
