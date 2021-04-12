using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatMove : MonoBehaviour
{
    private bool moving;

    Vector3 velocity = Vector3.right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "land"){
            collision.collider.transform.SetParent(null);
            moving = false;
        }
        else if (collision.gameObject.tag == "Player"){
            moving = true;
            collision.collider.transform.SetParent(transform, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.tag == "Player"){
            collision.collider.transform.SetParent(null);
            moving = false;
        }
    }

    private void FixedUpdate(){
        if (moving){
            transform.position += (velocity * Time.deltaTime);
        }
    }

}
