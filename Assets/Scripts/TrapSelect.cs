using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapSelect : MonoBehaviour
{

    public GameObject[] trapList;
    List<Animator> animatorList = new List<Animator>();

    public bool dead;
    public bool getTrapped;
    

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        getTrapped = false;

        if (trapList.Length >=1){
            for (int i = 0; i < trapList.Length; i++){
                animatorList.Add(trapList[i].GetComponent<Animator>());
                animatorList[i].enabled = false;
                //Debug.Log("no problem until here");
            }
        }
        else{
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindTrap(string trapName){
        if (trapList.Length >=1){
            for (int i = 0; i < trapList.Length; i++){
                Debug.Log("search");
                if (trapList[i].name == trapName){
                    if (!dead){
                        Debug.Log("same name");        
                        animatorList[i].enabled = true;
                        animatorList[i].SetBool("beingTrapped", true);
                        getTrapped = true;
                    }
                    else{
                        animatorList[i].SetBool("beingTrapped", false);
                        getTrapped = false;
                    }
                }
            }
        }
        else{
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D trig){
        if(trig.gameObject.tag == "Trap"){
            string trapGameobjectName = trig.gameObject.name;
            Debug.Log(trapGameobjectName);
            FindTrap(trapGameobjectName);
            //myAnim.SetBool("trapDead", true);
            StartCoroutine(waitForTrapDeath());
            //SceneManager.LoadScene(0);
        }
    }

    IEnumerator waitForTrapDeath(){
        yield return new WaitForSeconds(1);        
        dead = true;   
        SceneManager.LoadScene(0);
        dead = false;
    }
}
