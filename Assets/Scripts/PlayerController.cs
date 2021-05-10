using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//testing
public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myBody;
    Collider2D myCollider;
    private LayerMask whatIsGround;
    
    [Header("Floor Detection")]
    [SerializeField]
    float footWidth;
    [SerializeField]
    float footDepth;

    [Space]
    [Header("Movement Variables")]
    [SerializeField]
    float speed;
    [SerializeField]
    float speedCap, airSpeedMul, floorDrag;

    [Space]
    [Header("Jump Variables")]
    [SerializeField]
    float jumpPower; //how much velocity we should apply to our player PER JUMP FRAME
    [SerializeField]
    int jumpLength; //max jump length in frames
    int jumpRemaining;  //we don't need to know this

    [Space]
    [Header("Mantle Variables")]
    [SerializeField]
    float mantleDetLength; //length of our mantle raycast (not too big)
    [SerializeField]
    float mantlePower, mantleX, mantleY;
    bool mantling; //if we're mantling over something (housekeeping variable)

    float moveInput; //no idea yet
    [Space]
    [Header("Public Variables")]
    public bool isGrounded;
    public bool grabbing;

    bool isWalking;

    public bool drown;

    SpriteRenderer myRenderer;
    public static bool faceRight = true;

    public Animator myAnim;

    AudioSource walkAudio;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        walkAudio = GetComponent<AudioSource>();
        whatIsGround = LayerMask.GetMask("Ground");

        drown = false;
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

        if(drown == true){
            myAnim.SetBool("drown", true);
        }
        else{
            myAnim.SetBool("drown", false);
        }
     
        rotationJustifier();

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
        {   //need to make this change based on what direction we're actually facing
            float _cRot = Mathf.Deg2Rad * myBody.rotation;  //current rotation in radian
            float _sSpeed = moveInput * speed * airSpeedMul*3; //swingspeed
            myBody.velocity += new Vector2(Mathf.Cos(_cRot) * _sSpeed, Mathf.Sin(_cRot) * _sSpeed);

            Debug.DrawRay(gameObject.transform.position, new Vector2(Mathf.Cos(_cRot), Mathf.Sin(_cRot)), Color.blue);

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
        if(grabbing && Mathf.Abs(myBody.rotation) > 60)
        {
            if(myBody.rotation > 60){
                myBody.rotation = Mathf.LerpAngle(gameObject.GetComponent<Rigidbody2D>().rotation, 60, 0.005f);
            }
            if(myBody.rotation < -60){
                myBody.rotation = Mathf.LerpAngle(gameObject.GetComponent<Rigidbody2D>().rotation, -60, 0.005f);
            }
        } else if(!grabbing && Mathf.Abs(myBody.rotation) > 1)
        {
            myBody.rotation = Mathf.LerpAngle(gameObject.GetComponent<Rigidbody2D>().rotation, 0, 0.25f);
        } else if(!grabbing){
            myBody.rotation = 0;
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
        if(isGrounded == true && Input.GetKeyDown("up") || isGrounded == true && Input.GetKeyDown(KeyCode.W))
        {
            myAnim.SetTrigger("takeOff");
            isGrounded = false;
            jumpRemaining = jumpLength;
            myBody.velocity += new Vector2(0, jumpPower);
        }
        if(jumpRemaining != 0)
        {
            if(Input.GetKey("up") && jumpRemaining > 0 || Input.GetKey(KeyCode.W) && jumpRemaining > 0)
            {
                jumpRemaining--;
                myBody.velocity += new Vector2(0, jumpPower);
            } else if(!Input.GetKey("up") || Input.GetKey(KeyCode.W))
            {
                jumpRemaining = 0;
            }
        }
        if (isGrounded == true){
            myAnim.SetBool("jump", false);
        }
        else
        {
            myAnim.SetBool("jump", true);
        }
    }

    void InputFinder()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); //essentially positive or negative, pretty clever
    }


    void OnTriggerEnter2D(Collider2D trig){
        if(trig.gameObject.tag == "Water"){
            drown = true;   
            StartCoroutine(waitForDrowning());
        }
    }
    IEnumerator waitForDrowning(){
        yield return new WaitForSeconds(3);        
        SceneManager.LoadScene(0);
        drown = false;
    }

    //void OnTriggerEnter2D(Collider2D trig){
    //    if(trig.gameObject.tag == "Trap"){
 //           StartCoroutine(waitForTrapDeath());
    //        //SceneManager.LoadScene(0);
    //    }
    //}

   // IEnumerator waitForTrapDeath(){
    //    yield return new WaitForSeconds(1);
   //     SceneManager.LoadScene(0);
 //   }

}
