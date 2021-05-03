using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushAnimation : MonoBehaviour
{
    public Animator myAnim;
    public bool detectPushableObj;
    public bool collideWithObject;

    private PlayerController control;

    // Start is called before the first frame update
    void Start()
    {
        control = gameObject.GetComponent<PlayerController>();
        collideWithObject = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if (control.grabbing && control.isGrounded){ //pulling
            myAnim.SetBool("pull", true);
            myAnim.SetBool("push", false);
            //Debug.Log("pull");
        }
        else {
            myAnim.SetBool("pull", false);
            if (collideWithObject){ 
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){ //pushing
                    myAnim.SetBool("push", true);
                    //Debug.Log("Pushing");
                }
                else if (Input.GetKey("right") || Input.GetKey("left")){ //pushing
                    myAnim.SetBool("push", true);
                    //Debug.Log("Pushing");
                }
                else { //waiting to pull/push
                    myAnim.SetBool("pull", false);
                    myAnim.SetBool("push", false);
                }
            }
            else {
                myAnim.SetBool("push", false);
            }
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
