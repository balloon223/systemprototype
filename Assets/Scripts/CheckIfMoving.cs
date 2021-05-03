using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfMoving : MonoBehaviour
{
    AudioSource walkAudio;
    public float distance = 30f;
    public bool isMoving;
    public Transform playerCheckTransform;
    public GameObject playerCheck;
    Vector2 direction = Vector2.right;
    public float rayLength = 20f;
    public LayerMask playerLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        walkAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    void CheckPlayerInRange()    
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D playerHit = Physics2D.Raycast(playerCheckTransform.position, Vector2.left, rayLength, playerLayerMask);

        if (playerHit.collider != null)
        {
            isMoving=true;
        }
    }

    void OnDrawGizmos(){
            Gizmos.color = Color.red;
            Gizmos.DrawLine(playerCheckTransform.position, new Vector2(playerCheckTransform.position.x - rayLength, playerCheckTransform.position.y));
        }
}