﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public int Levelload = 2;
    public gamemaster gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("gamemaster").GetComponent<gamemaster>();
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            savescore();
            gm.Inputtext.text = ("Press E to enter");
        }
    }


    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                savescore();
                SceneManager.LoadScene(2);
            }
        }
    }



    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gm.Inputtext.text = ("");
        }
    }



    void savescore()
    {
        PlayerPrefs.SetInt("points", gm.points);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
