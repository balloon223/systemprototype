using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//testing
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Collider2D myCollider;
    public LayerMask whatIsGround;
    
    public bool grabbing;

    public float footWidth; //how wide is our "foot" ray?
    public float footDepth; //how far down is our "foot" ray?

    public float speed; //speed multiplier
    public float airSpeedMul; //airspeed multiplier so we move slower while in midair — less air control
    public float speedCap; //max speed
    public float floorDrag; //how fast we should stop moving when touching the floor

    public float jumpPower; //how much velocity we should apply to our player PER JUMP FRAME
    public int jumpLength; //max jump length in frames
    private int jumpRemaining;

    public float mantleDetLength; //length of our mantle raycast (not too big)
    public float mantleX; //location of our mantle raycast's start (should be near our feet, looking down)
    public float mantleY;
    public bool mantling; //if we're mantling over something
    public float mantlePower; //how much juice mantling should have

    private float moveInput; //no idea yet

    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public bool isWalking;


    SpriteRenderer myRenderer;
    public static bool faceRight = true;

    AudioSource walkAudio;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        walkAudio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        groundFinder();
        leftRightMotionHandler();

    }

    void Update()
    {
        if(isWalking){
            if(!walkAudio.isPlaying){walkAudio.Play();}
        }
        else{
            walkAudio.Stop();
        }

        if(!grabbing){
            rotationJustifier();
        }
        mantler();
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
            //Debug.Log(floorDet.collider.tag);
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
            if(moveInput != 0){
            isWalking = true;}
            else{isWalking = false;}
        } else if(!isGrounded && !grabbing) //if we're in midair, don't remove player agency totally!!
        {
            myBody.velocity += new Vector2(moveInput * speed * airSpeedMul, 0 );
            isWalking = false;
        } else if(!isGrounded && grabbing)
        {
            myBody.velocity += new Vector2(moveInput * speed * airSpeedMul*2, 0 );
            isWalking = true;
        }

        if(Mathf.Abs(myBody.velocity.x) > speedCap)
        {
            myBody.velocity = new Vector2(moveInput * speedCap, myBody.velocity.y);
        }
        if(moveInput == 0 && isGrounded) //slow us down with a static drag
        {
            myBody.velocity = new Vector2(myBody.velocity.x * floorDrag, myBody.velocity.y);
        }
    }

    void rotationJustifier()
    {
        if(Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().rotation) > 1)
        {
            gameObject.GetComponent<Rigidbody2D>().rotation = Mathf.LerpAngle(gameObject.GetComponent<Rigidbody2D>().rotation, 0, 0.25f);
        } else{
            gameObject.GetComponent<Rigidbody2D>().rotation = 0;
        }
    }

    void mantler()
    {
        Physics2D.queriesStartInColliders = false;
        Vector2 mantleOffset = new Vector2(transform.position.x + (mantleX * transform.localScale.x), transform.position.y + mantleY);  //this tells us what offset we use
        RaycastHit2D mantleDet = Physics2D.Raycast(mantleOffset, new Vector2(0, -1), mantleDetLength, whatIsGround);
        if(mantleDet.collider != null && Input.GetKey(KeyCode.Space))
        {
            mantling = true;
        } else{
            mantling = false;
        }
        if(mantling)
        {
            myBody.velocity = new Vector2(0, mantlePower);
        }

        Debug.DrawRay(mantleOffset, new Vector2(0, -mantleDetLength), Color.red);
    }

    void jumpHandler()
    {
        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            jumpRemaining = jumpLength;
            myBody.velocity += new Vector2(0, jumpPower);
        }
        if(jumpRemaining != 0)
        {
            if(Input.GetKey(KeyCode.Space) && jumpRemaining > 0)
            {
                jumpRemaining--;
                myBody.velocity += new Vector2(0, jumpPower);
            } else if(!Input.GetKey(KeyCode.Space))
            {
                jumpRemaining = 0;
            }
        }
    }

    void InputFinder()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); //essentially positive or negative, pretty clever
    }


}
