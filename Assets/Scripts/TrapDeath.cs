﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDeath : MonoBehaviour
{
    public Animator myAnim;
    Rigidbody2D m_Rigidbody;

    bool moveIntoTrap;
    bool alreadyMovedBack;

    public float trapCenterDistance;

    private TrapSelect trapSelector;
    private PlayerPush pushControl;

    public AudioClip trapDeathTrigger;
    AudioSource audio;
    public bool havePlayedSound;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

        moveIntoTrap = false;
        alreadyMovedBack = true;

        m_Rigidbody = GetComponent<Rigidbody2D>();

        trapSelector = gameObject.GetComponent<TrapSelect>();

        pushControl = gameObject.GetComponent<PlayerPush>();
            havePlayedSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(trapSelector.getTrapped && !trapSelector.dead){
            if (moveIntoTrap == false){   //haven't moved
                if (pushControl.faceRight){
                    transform.position = new Vector2(transform.position.x+trapCenterDistance, transform.position.y);
                    moveIntoTrap = true;    //already moved
                    m_Rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                    alreadyMovedBack = false;
                }
                else{
                    transform.position = new Vector2(transform.position.x-trapCenterDistance, transform.position.y);
                    moveIntoTrap = true;    //already moved
                    m_Rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                    alreadyMovedBack = false;
                }
            }
            else{
                myAnim.SetBool("trapDead", true);   //dead
            
            if(havePlayedSound == false & trapDeathTrigger != null){
            audio.PlayOneShot(trapDeathTrigger, 0.3f);
            havePlayedSound=true;
        }
                
            }
        }
        else{
            myAnim.SetBool("trapDead", false);   //alive
            moveIntoTrap = false;
            m_Rigidbody.constraints = RigidbodyConstraints2D.None;
            m_Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (alreadyMovedBack == false){
                if (pushControl.faceRight){
                    transform.position = new Vector2(transform.position.x-trapCenterDistance, transform.position.y);
                    alreadyMovedBack = true;
                }
                else{
                    transform.position = new Vector2(transform.position.x+trapCenterDistance, transform.position.y);
                    alreadyMovedBack = true;
                }
            }
            else{
                return;
            }
        }
    }
}
