﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElement : MonoBehaviour
{   
    // Player
    public PlayerMovement Player;
    private bool PlayerFaceDirection; // Right = true, Left = false

    // Whether the player can use elements
    public bool ElementOn = false;

    // Key to use elements and switch 
    public KeyCode ElementShiftKey;
    public KeyCode ElementAttackKey;
    private int ElementNumber = 0;

    // ElementAir
    public float AirSpeedChange;

    // ElementGrass
    public GameObject Grass;
    public Vector2 GrassVelocity;
    public bool boolGrassAcquire;

    // ElementIce
    public GameObject Ice;
    public Vector2 IceVelocity;
    public bool boolIceAcquire;

    // ElementFire
    public GameObject Fire;
    public Vector2 FireVelocity;
    public bool boolFireAcquire;

    void FixedUpdate()
    {
        // Which direction the player is facing to
        if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)))
        {
            PlayerFaceDirection = true;
        }
        else if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)))
        {
            PlayerFaceDirection = false;
        }

        // Shift between elements
        if (Input.GetKeyDown(ElementShiftKey) && ElementOn)
        {
            // Element Air: 0
            if (ElementNumber == 0)
            {
                ElementAir(true);
                ElementNumber = 1;
            }
            // Element Grass: 1
            else if (ElementNumber == 1)
            {
                ElementAir(false);
                ElementNumber = 2;
            }
            // Element Ice: 2
            else if (ElementNumber == 2)
            {
                ElementNumber = 3;
            }
            // Element Fire: 3
            else 
            {
                ElementNumber = 0;
            }

        }

        // Attack with elements
        if (Input.GetKeyDown(ElementAttackKey) && ElementOn)
        {
            StartCoroutine("ElementAttack");
        }
    }

    // Element Air
    void ElementAir(bool AirOn)
    {
        if (AirOn)
        {
            Player.MaxSpeed = Player.MaxSpeed * AirSpeedChange;
            Player.JumpForce = Player.JumpForce * AirSpeedChange;
            Player.MaxJump = 2;
        }
        else
        {
            Player.MaxSpeed = Player.MaxSpeed / AirSpeedChange;
            Player.JumpForce = Player.JumpForce / AirSpeedChange;
            Player.MaxJump = 1;
        }
    }

    // Elements
    void Element(GameObject Ele, float t,Vector2 vel)
    {

        if (!PlayerFaceDirection)
        {
            vel.x = -vel.x;
        }

        GameObject New = GameObject.Instantiate(Ele, transform.position, Quaternion.identity);
        GameObject.Destroy(New, t);
        New.GetComponent<Rigidbody2D>().velocity = vel;
    }

    IEnumerator ElementAttack()
    {
        // Element Grass: 1
        if (ElementNumber == 2)
        {
            Element(Grass, 5f, GrassVelocity);
        }
        // Element Ice: 2
        if (ElementNumber == 3)
        {
            Element(Ice, 8f, IceVelocity);
        }
        // Element Fire: 3
        if (ElementNumber == 0)
        {
            Element(Fire, 8f, FireVelocity);
        }
        // suspend execution for 2 seconds
        yield return new WaitForSeconds(2f);
    }

}   

