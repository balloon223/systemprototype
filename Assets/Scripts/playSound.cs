using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSound : MonoBehaviour
{
    public AudioClip triggerSound;
    AudioSource audio;

    void Start(){
        audio = GetComponent<AudioSource>();
    }

    void Update(){

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(triggerSound != null && other.gameObject.tag == "Player"){
            audio.PlayOneShot(triggerSound, 1);
        }

    }
}
