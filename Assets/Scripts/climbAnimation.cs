using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbAnimation : MonoBehaviour
{
    public Animator myAnim;
    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!controller.isGrounded && controller.grabbing){
            myAnim.SetBool("climb", true);
        }
        else{
            myAnim.SetBool("climb", false);
        }
    }
}
