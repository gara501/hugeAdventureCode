using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player obj; // Singleton

    public int lives = 3;
    public bool isGrounded = true;
    public bool isMoving = false;
    public bool isImmune = false;
    public bool isFalling = false;

    public float speed = 5f;
    public float jumpForce = 3f;
    public float movHor;

    public float immuneTimeCnt = 0.5f;
    public float immuneTime = 0.5f;

    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float radius = 0.3f;
    public float groundRayDist = 0.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spr;


    private void Awake()
    {
        obj = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {      

        movHor = Input.GetAxisRaw("Horizontal");
        isMoving = (movHor != 0f);
        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer) ||
            Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, wallLayer);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
            
        
        // Change animation from fall to run or iddle
        if (rb.velocity.y < -1.20)
        {
            isFalling = true;
        } else
        {
            isFalling = false;
        }

        // Die if fall
        if (transform.position.y < -10)
        {
            Game.obj.lostLife();
        }

        // Immunity
        if (isImmune)
        {
            spr.enabled = !spr.enabled;
            immuneTimeCnt -= Time.deltaTime;

            if (immuneTimeCnt <= 0)
            {
                isImmune = false;
                spr.enabled = true;
            }
        }
    

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFalling", isFalling);

        flip(movHor);
    }

    private void goImmune()
    {
        isImmune = true;
        immuneTimeCnt = immuneTime;
    }

    // Physics will be programmed here
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movHor * speed, rb.velocity.y);
    }

    void OnDestroy()
    {
        obj = null;
    }

    public void getDamage()
    {
        lives--;
        AudioManager.obj.playHit();
        UIManager.obj.updateLives();

        goImmune();

        if (lives <= 0)
        {
            FxManager.obj.showPop(transform.position);
            Game.obj.gameOver();
        }
    }

    public void jump()
    {
        if (!isGrounded) return;

        AudioManager.obj.playJump();

        rb.velocity = Vector2.up * jumpForce;
    }

    public void AddLive()
    {
        lives++;
        UIManager.obj.updateLives();
        
        if (lives > Game.obj.maxlives)
        {
            lives = Game.obj.maxlives;
        }
        
    }

    public void flip(float _xValue)
    {
        Vector3 theScale = transform.localScale;
        if (_xValue < 0)
        {
            theScale.x = Mathf.Abs(theScale.x) * -1;
        }
        else if (_xValue > 0)
        {
            theScale.x = Mathf.Abs(theScale.x);
        }

        transform.localScale = theScale;
    }
}
