using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushAnimation : MonoBehaviour
{
    public Animator myAnim;
    public bool detectPushableObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
