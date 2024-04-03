using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 50f, maxspeed = 3, maxjump = 4, jumpPow = 220f;
    public bool grounded = true, faceright = true, doublejump = false;

    public int ourHealth;
    public int maxhealth = 5;

    public Rigidbody2D r2;
    public Animator anim;
    public gamemaster gm;
    public SoundManager sound;
    public GameObject GameOverPanel;
    // Start is called before the first frame update  
    void Start()
    {
        GameOverPanel.SetActive(false);
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        ourHealth = maxhealth;
        gm = GameObject.FindGameObjectWithTag("gamemaster").GetComponent<gamemaster>();
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                grounded = false;
                doublejump = true;
                r2.AddForce(Vector2.up * jumpPow);
            }
            else
            {
                if (doublejump)
                {
                    doublejump = false;
                    r2.velocity = new Vector2(r2.velocity.x, 0);
                    r2.AddForce(Vector2.up * jumpPow * 0.7f);
                }
            }
        }
       
    }


    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        r2.AddForce((Vector2.right) * speed * h);

        if (r2.velocity.x > maxspeed)
            r2.velocity = new Vector2(maxspeed, r2.velocity.y);
        if (r2.velocity.x < -maxspeed)
            r2.velocity = new Vector2(-maxspeed, r2.velocity.y);

        if (r2.velocity.y > maxjump)
            r2.velocity = new Vector2(r2.velocity.x, maxjump);
        if (r2.velocity.y < -maxjump)
            r2.velocity = new Vector2(r2.velocity.x, -maxjump);
        if (h > 0 && !faceright)
        {
            Flip();
        }

        if (h < 0 && faceright)
        {
            Flip();
        }

        if (grounded)
        {
            r2.velocity = new Vector2(r2.velocity.x * 0.7f, r2.velocity.y);
        }
        if (ourHealth <= 0)
        {
            Death();
            gm.points = 0;
            Time.timeScale = 0;
        }
    }


    public void Flip()
    {
        faceright = !faceright;
        Vector3 Scale;
        Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    public void Death()
    {

        if (PlayerPrefs.GetInt("highscore") < gm.points)
        gm.points = 0;
        Destroy(gameObject);
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
        
    }


    public void Damage(int damage)
    {
        ourHealth -= damage;
        gameObject.GetComponent<Animation>().Play("redflash");
    }
    public void restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void mainmenu() {
        SceneManager.LoadScene(0);
    }
    public void quit() {
        Debug.Log("Exit");
        Application.Quit();
    }
    public void Knockback(float Knockpow, Vector2 Knockdir)
    {
        r2.velocity = new Vector2(0, 0);
        r2.AddForce(new Vector2(Knockdir.x * -100, Knockdir.y + Knockpow));
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Coins"))
        {
            sound.Playsound("coins");
            Destroy(col.gameObject);
            gm.points += 1;
        }
        if (col.CompareTag("shoes"))
        {
            
            Destroy(col.gameObject);
            maxspeed = 6;
            speed = 120f;
            StartCoroutine(timecount(5));
            gm.points += 2;
        }
        if (col.CompareTag("hearts"))
        {
            
            Destroy(col.gameObject);
            ourHealth += 1;
            gm.points += 5;
        }
    }
    IEnumerator timecount(float time)
    {
        yield return new WaitForSeconds(time);
        maxspeed = 3f;
        speed = 50f;
        yield return 0;
    }
}
