using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Collider2D myCollider;
    public LayerMask whatIsGround;
    
    public float footWidth; //how wide is our "foot" ray?
    public float footDepth; //how far down is our "foot" ray?

    public float speed; //speed multiplier
    public float speedCap; //max speed
    public float floorDrag; //how fast we should stop moving when touching the floor

    public float jumpPower; //how much velocity we should apply to our player PER JUMP FRAME
    public float jumpLength; //max jump length in frames

    private float moveInput; //no idea yet

    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;

    SpriteRenderer myRenderer;
    public static bool faceRight = true;


    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        groundFinder();
        leftRightMotionHandler();
    }

    void Update()
    {
        jumpHandler();
        InputFinder();
    }
//----------------------------------------------------------------------------------------------------------------------

    void groundFinder()
    {
        Physics2D.queriesStartInColliders = true;
        Vector2 Xoffset = new Vector2(transform.position.x + (footWidth/2), transform.position.y - footDepth);  //this tells us what offset we use
        RaycastHit2D floorDet = Physics2D.Raycast(Xoffset, new Vector2(-1, 0), footWidth, whatIsGround);
        if(floorDet.collider != null)
        {
            Debug.Log(floorDet.collider.tag);
            isGrounded = true;
        } else{
            isGrounded = false;
        }

        Debug.DrawRay(Xoffset, new Vector2(-footWidth, 0), Color.green);
    }

    void leftRightMotionHandler()
    {
        if(isGrounded)
        {
            myBody.velocity += new Vector2(moveInput * speed, 0);
        }
        if(Mathf.Abs(myBody.velocity.x) > speedCap)
        {
            myBody.velocity = new Vector2(moveInput * speedCap, myBody.velocity.y);
        }
        if(moveInput == 0 && isGrounded)
        {
            myBody.velocity = new Vector2(myBody.velocity.x * floorDrag, myBody.velocity.y);
        }
    }

    void jumpHandler()
    {
        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))//fix this so it's more dynamic
        {
            isGrounded = false;
            myBody.velocity = new Vector2(myBody.velocity.x, jumpPower); //temp
        }
    }

    void InputFinder()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); //essentially positive or negative, pretty clever
    }
}
