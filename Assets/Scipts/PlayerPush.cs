using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask boxMask;
    public Transform boxCheckTransform;
    public float boxCheckRadius;

    GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast (boxCheckTransform.position, Vector2.right, distance, boxMask);
        
        if (hit.collider != null && Input.GetKeyDown(KeyCode.E)){
            Debug.Log(hit.collider.gameObject.name);
            //Debug.Log("press E");
            box = hit.collider.gameObject;

            box.GetComponent<FixedJoint2D> ().enabled = true;
            box.GetComponent<FixedJoint2D> ().connectedBody = this.GetComponent<Rigidbody2D> ();
            Debug.Log("drag");
        }
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine (boxCheckTransform.position, new Vector2(boxCheckTransform.position.x+distance, boxCheckTransform.position.y));
    }
}
