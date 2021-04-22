using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask boxMask;
    public Transform boxCheckTransform;
    public float boxCheckRadius;

    public GameObject boxCheck;

    private PlayerController control;
    GameObject dragging;
    SpriteRenderer myRenderer;
    public bool faceRight;
    public float horizontalInput;

    Vector2 direction = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        control = gameObject.GetComponent<PlayerController>();
        faceRight = true;
    }

    void FixedUpdate(){  
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeys();  
        horizontalInput = Input.GetAxis("Horizontal");
        PushPushable();
        

    }

    void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine (transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x *distance);
    }
//----------------------------------------------------------------------------------------------------------------------

    void PushPushable()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast (boxCheckTransform.position, direction * boxCheck.transform.localScale.x, distance, boxMask);

        if (hit.collider != null && hit.collider.gameObject.tag=="Pushable")
        {
            dragging = hit.collider.gameObject;
            Debug.Log("collide");
            if(Input.GetKey(KeyCode.E) && control.isGrounded)   //for now
            {
                Debug.Log("drag");
                dragging.GetComponent<FixedJoint2D>().enabled = true;
                dragging.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D> ();
            }
        }
        if (!Input.GetKey(KeyCode.E) && dragging.GetComponent<FixedJoint2D>().enabled)   //catch-all for terror joints
        {
            dragging.GetComponent<FixedJoint2D>().enabled = false;
        }

    }

    void CheckKeys()
    {
        if(Input.GetKey(KeyCode.D) || Input.GetKey("right")){
            faceRight = true;
            transform.localScale = new Vector2(1, transform.localScale.y);
            direction = Vector2.right;
        }
        else if(Input.GetKey(KeyCode.A) || Input.GetKey("left")){
            faceRight = false;
            transform.localScale = new Vector2(-1, transform.localScale.y);
            direction = Vector2.left;
        }
    }




}
