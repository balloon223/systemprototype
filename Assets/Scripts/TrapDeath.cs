using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDeath : MonoBehaviour
{
    public Animator myAnim;

    bool moveIntoTrap;

    private TrapSelect trapSelector;

    // Start is called before the first frame update
    void Start()
    {
        moveIntoTrap = false;

        trapSelector = gameObject.GetComponent<TrapSelect>();
    }

    // Update is called once per frame
    void Update()
    {
        if(trapSelector.getTrapped && !trapSelector.dead){
            if (moveIntoTrap == false){
                transform.position = new Vector2(transform.position.x+1, transform.position.y);
                moveIntoTrap = true;
            }
            else{
                myAnim.SetBool("trapDead", true);
                
            }
        }
        else{
            myAnim.SetBool("trapDead", false);
            moveIntoTrap = false;
        }
    }
}
