using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Player player;
    public MovingPlat mov;

    public Vector3 movep;
    // Start is called before the first frame update
    void Start()
    {
        mov = GameObject.FindGameObjectWithTag("Movingplat").GetComponent<MovingPlat>();
        player = gameObject.GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == false || collision.CompareTag("water"))
            player.grounded = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger == false || collision.CompareTag("water"))
            player.grounded = true;
        if (collision.isTrigger == false && collision.CompareTag("Movingplat"))
        {
            movep = player.transform.position;
            movep.x += mov.speed * 1.3f;
            player.transform.position = movep;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger == false || collision.CompareTag("water"))
            player.grounded = false;
    }
}
