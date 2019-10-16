using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player1Controller : MonoBehaviour {

    public float initSpeed = 10f;
    public float fixedSpeed;
    private float currentSpeed;
    private Rigidbody2D myRB;
    bool facingRight = true;
    bool dying = false;

    public float jumpSpeed = 5f;
    public bool grounded = false;
    public float groundRadius = .2f;
    public LayerMask whatIsGround;

    public float lowerTime = 5f;
    private float lowerTimer = 5f;

    private float score = 0;
    public float stageWin = 6;
    public int lastLevel = 2;
    private int currentLevel = 1;

    public GameObject lance;
    public Text winUI;

    public float angleBounds = 135f;
    public float maxFall = 13f;

    Animator myAnim;

    // Use this for initialization
    void Start () {
        fixedSpeed = initSpeed;
        currentSpeed = initSpeed;
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        lance.SetActive(false);
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(transform.position, groundRadius, whatIsGround);
    }

    // Update is called once per frame
    void Update () {
        if (!dying)
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                myAnim.SetBool("Lowered", true);
                lance.SetActive(true);
            }
            if (lowerTimer <= 0f)
            {
                myAnim.SetBool("Lowered", false);
                lance.SetActive(false);
                lowerTimer = lowerTime;
            }
            else
            {
                lowerTimer--;
            }
            float thisJump = myRB.velocity.y;
            if(Input.GetKeyDown(KeyCode.W) || !grounded)
            {
                myAnim.SetBool("Jumping", true);
            }
            else
            {
                myAnim.SetBool("Jumping", false);
            }
            if (Input.GetKeyDown(KeyCode.W) && grounded)
            {
                thisJump = jumpSpeed;
            }
            float move = Input.GetAxis("P1_Horizontal");
            /*
            if (myAnim.GetBool("Grinding") == false)
            {
                myRB.velocity = new Vector2(move * currentSpeed, thisJump);
            }
            */
            myRB.velocity = new Vector2(move * currentSpeed, thisJump);
            if (myRB.velocity.y <= -maxFall)
            {
                myRB.velocity = new Vector2(myRB.velocity.x, -maxFall);
            }
            if (grounded && transform.rotation.z > Mathf.Abs(angleBounds))
            {
                myRB.MoveRotation(0f);
            }
            myAnim.SetFloat("Speed", Mathf.Abs(move));
            if ((facingRight && move < 0) || (!facingRight && move > 0))
                Flip();
        }
        
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.CompareTag("Bounds") || collision.CompareTag("Gravity Floor")) && (myRB.rotation > Mathf.Abs(90f)))
        {
            myRB.MoveRotation(0f);
        }
    }
    
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        float move = Input.GetAxis("P1_Horizontal");
        if (collision.gameObject.CompareTag("GrindRail") && Input.GetKeyDown(KeyCode.LeftShift) && move != 0f)
        {
            myRB.velocity = new Vector2((move / Mathf.Abs(move)) * currentSpeed, 0f);
            myRB.MoveRotation(0f);
            myAnim.SetBool("Grinding", true);
            if(currentSpeed <= 8f)
            {
                currentSpeed += .01f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(myAnim.GetBool("Grinding") == true)
        {
            myAnim.SetBool("Grinding", false);
        }
    }
    */
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        myAnim.SetBool("Jumping", false);
        if (other.collider.CompareTag("Lance"))
        {
            Die();
        }
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("Gravity Floor"))
        {
            myRB.velocity = Vector2.zero;
            transform.Translate(0f, .5f, 0f, Space.World);
        }
        else if (other.collider.CompareTag("Wall"))
        {
            myRB.MoveRotation(0f);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;
    }

    public void Die()
    {
        if (!dying)
        {
            dying = true;
            myAnim.SetTrigger("Dead");
            myRB.velocity = Vector2.zero;
            winUI.color = Color.cyan;
            winUI.text = "You got SLAMMED by Player 2!!!";
            Invoke("ResetLevel", 3.5f);
        }
    }

    void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("GrindRail"))
        {
            transform.position = new Vector3(transform.position.x, collision.transform.position.y, transform.position.z);
            currentSpeed++;
        }
    }


    
    void SetScoreUI()
    {
        scoreUI.text = "Coins: " + score.ToString() + " / " + stageWin.ToString();
        if (score >= stageWin)
        {
            if (currentLevel >= lastLevel)
            {
                winUI.text = "YOU WIN !!!";
            }
            else
            {
                Invoke("NextLevel", 3.5f);
            }
            source.PlayOneShot(levelDoneSound, 3.0f);
        }
    }

    bool IsInMyLayerMask(LayerMask mask, GameObject obj)
    {
        return ((mask.value & (1 << obj.layer)) > 0);
    }

    void NextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene("lvl02");
    }
    */
}
