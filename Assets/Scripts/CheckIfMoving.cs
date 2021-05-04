using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfMoving : MonoBehaviour
{
    public PlayerController isWalking;
    AudioSource walkAudio;
    public float distance = 30f;
    public bool isMoving;
    public Transform playerCheckTransform;
    Vector2 direction = Vector2.right;
    public float rayLength = 1.65f;
    public LayerMask playerLayerMask;
    BoxCollider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        walkAudio = GetComponent<AudioSource>();
        myCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D playerHit = Physics2D.Raycast(playerCheckTransform.position, Vector2.left, rayLength, playerLayerMask);

        if (playerHit.collider != null)
        {
            isMoving=true;
            if(!walkAudio.isPlaying){
                walkAudio.Play();
                }
        }
        else
        {
            isMoving=false;
            walkAudio.Stop();
        }


    }

    void OnDrawGizmos(){
            Gizmos.color = Color.red;
            Gizmos.DrawLine(playerCheckTransform.position, new Vector2(playerCheckTransform.position.x - rayLength, playerCheckTransform.position.y));
        }       

/*    void CheckPlayerInRange()    
    {
        Physics2D.queriesStartInColliders = false;

    }



/*    void OnTriggerEnter2D(Collider2D other)
    {
        if(other != null && other.gameObject.tag == "Player"){
            isMoving=true;
        }
        else{
            isMoving=false;
        }
    }   */
}