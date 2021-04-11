using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask boxMask;
    public Transform boxCheckTransform;
    public float boxCheckRadius;
    public float dir = 1f;

    public GameObject boxCheck;

    GameObject box;
    SpriteRenderer myRenderer;
    public static bool faceRight = true;
    public float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast (boxCheckTransform.position, Vector2.right * boxCheck.transform.localScale.x, distance, boxMask);

        if (hit.collider != null && hit.collider.gameObject.tag=="Pushable" && Input.GetKeyDown(KeyCode.E)){
            box = hit.collider.gameObject;

            box.GetComponent<FixedJoint2D> ().enabled = true;
            box.GetComponent<FixedJoint2D> ().connectedBody = this.GetComponent<Rigidbody2D> ();
            Debug.Log("drag");
        }
        else if (Input.GetKeyUp (KeyCode.E)){
            box.GetComponent<FixedJoint2D> ().enabled = false;
        }
        
        
    }
    void CheckKeys(){
        if(Input.GetKeyDown(KeyCode.D)){
            faceRight = true;
            dir = 1f;
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            faceRight = false;
            dir = -1f;
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine (transform.position, transform.position + Vector3.right *distance);
    }


}
