using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkAnimation : MonoBehaviour
{
    public Animator myAnim;
    AudioSource walkAudio;

    // Start is called before the first frame update
    void Start()
    {
        walkAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
            myAnim.SetBool("walk", true);
            if(!walkAudio.isPlaying){walkAudio.Play();}
        }
        else if(Input.GetKey("left") || Input.GetKey("right")){
            myAnim.SetBool("walk", true);
            if(!walkAudio.isPlaying){walkAudio.Play();}
        }
        
        else {
            myAnim.SetBool("walk", false);
            walkAudio.Stop();
        }
    }
}
