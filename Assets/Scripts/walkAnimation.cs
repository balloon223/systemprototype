using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkAnimation : MonoBehaviour
{
    public Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
            myAnim.SetBool("walk", true);
        }
        else if(Input.GetKey("left") || Input.GetKey("right")){
            myAnim.SetBool("walk", true);
        }
        
        else {
            myAnim.SetBool("walk", false);
        }
    }
}
