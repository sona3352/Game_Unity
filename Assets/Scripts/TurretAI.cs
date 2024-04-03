using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public int curHealth = 100;

    public float distance;
    public float wakerange;
    public float shootinterval =3;
    public float bulletspeed = 2;
    public float bullettimer;

    public bool awake = false;
    public bool lookingRight = true;

    public GameObject bullet;
    public Transform target;
    public Animator anim;
    public Transform shootpointL, shootpointR;
    public SoundManager sound;
    public AudioClip box;
    public AudioSource audiosrc;
    public gamemaster gm;

    private void Awake() {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
        audiosrc = gameObject.GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("gamemaster").GetComponent<gamemaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
        anim.SetBool("Awake", awake);
        anim.SetBool("LookRight", lookingRight);

        RangeCheck();

        if (target.transform.position.x > transform.position.x) {
            lookingRight = true;
        }
        if (target.transform.position.x < transform.position.x)
        {
            lookingRight = false;
        }
        if (curHealth <= 0) {
            sound.Playsound("destroy");
            Destroy(gameObject);
            gm.points += 10;
        }
    }
    void RangeCheck() {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance < wakerange)
            awake = true;
        if (distance > wakerange)
            awake = false;
    }
    public void Attack(bool attackright) {
        bullettimer += Time.deltaTime;
        if (bullettimer >= shootinterval) {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();

            if (attackright) {
                GameObject bulletclone;
                bulletclone = Instantiate(bullet, shootpointR.transform.position, shootpointR.transform.rotation) as GameObject;
                bulletclone.GetComponent<Rigidbody2D>().velocity = direction * bulletspeed;

                bullettimer = 0;
            }

            if (!attackright)
            {
                GameObject bulletclone;
                bulletclone = Instantiate(bullet, shootpointL.transform.position, shootpointL.transform.rotation) as GameObject;
                bulletclone.GetComponent<Rigidbody2D>().velocity = direction * bulletspeed;

                bullettimer = 0;
            }
        }
    }
    public void Damage(int dmg)
    {
        audiosrc.PlayOneShot(box, 0.8f);
        curHealth -= dmg;
        gameObject.GetComponent<Animation>().Play("redflash");
    }
}
