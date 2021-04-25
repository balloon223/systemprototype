using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushAnimation : MonoBehaviour
{
    public Animator myAnim;
    public bool detectPushableObj;
    public bool collideWithObject;

    // Start is called before the first frame update
    void Start()
    {
        collideWithObject = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
        //    if(collideWithObject = true){
 //               myAnim.SetBool("push", true);
        //    }
        //    else{
        //        myAnim.SetBool("push", false);
        //    }
        }
        else if(Input.GetKey("left") || Input.GetKey("right")){
        //    if (collideWithObject = true){
    //            myAnim.SetBool("push", true);
        //    }
        //    else{
        //        myAnim.SetBool("push", false);
        //    }
        }
        

        if (collideWithObject){
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
                myAnim.SetBool("push", true);
            }
            else if(Input.GetKey("left") || Input.GetKey("right")){
                myAnim.SetBool("push", true);
            }
            else {
                myAnim.SetBool("push", false);
            }
        }
        else {
            myAnim.SetBool("push", false);
        }

    }


    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Pushable")){
            detectPushableObj = true;
            myAnim.SetBool("detectPushable", true);
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Pushable")){
            detectPushableObj = false;
            myAnim.SetBool("detectPushable", false);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Pushable")
        {
            collideWithObject = true;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Pushable")
        {
           collideWithObject = false;
        }
    }
}
