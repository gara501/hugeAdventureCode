using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    public float movHor = -1f;
    public float speed = 4f;
    public bool isHit = false;

    public bool isGroundFloor = true;
    public bool isGroundFront = false;

    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float frontGroundRayDist = 0.52f;
    public float floorCheckY = 0.25f;
    public float frontCheck = 0.51f;
    public float frontDist = 0.001f;

    public int scoreGive = 50;

    public Transform groundDetection;

    private RaycastHit2D hit;
    private Animator anim;
    private Animation animationSlime;

    private float timeToDead = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        animationSlime = GetComponent<Animation>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.obj.gamePaused)
        {
            return;
        }

        // Flip sprite
        if (movHor < 0)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }

        /*
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, frontGroundRayDist);
        
        if (groundInfo.collider == false)
        {
            if (movHor > 0)
            {
                // movHor = movHor * -1;
                transform.eulerAngles = new Vector3(0, -180, 0);
                movHor = -1;
            } else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movHor = -1;
            }
            
        }
        */


        // Evitar precipicio
        
        isGroundFloor = (Physics2D.Raycast(
            new Vector3(transform.position.x, transform.position.y - floorCheckY, transform.position.z),
            new Vector3(movHor, 0, 0), frontGroundRayDist, groundLayer)
        );

        Debug.Log(transform.position.y - floorCheckY);
        Debug.Log(isGroundFloor);
        
        //isGroundFloor = Physics2D.Raycast(bc.bounds.center, Vector2.down, bc.bounds.extents.y);

        
        if (isGroundFloor)
        {
            movHor *= -1;
        }
        

        

        /*
        // Choque enemigo
        
        hit = Physics2D.Raycast(
            new Vector3(transform.position.x + movHor * frontCheck, transform.position.y, transform.position.z),
            new Vector3(movHor, 0, 0), frontDist);

        if (hit != null)
        {
            if (hit.transform != null)
            {
                if (hit.transform.CompareTag("Enemy") || hit.transform.CompareTag("Killer"))
                {
                    movHor = movHor * -1;
                }
            }
        }
        

        anim.SetBool("isHit", isHit);
        */

    }


    private void FixedUpdate()
    {
        if (!Game.obj.gamePaused)
        {
            rb.velocity = new Vector2(movHor * speed, rb.velocity.y);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        // Hit Player
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Player.obj.getDamage();
        }

        
        if (collision.gameObject.CompareTag("Walls"))
        {
            movHor *= -1;
        }
        
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Killer"))
        {
            movHor *= -1;
            
        }
        

    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy Enemy
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.obj.playEnemyHit();
            isHit = true;
           
            getKilled();
        }
    }

    private void getKilled()
    {
        FxManager.obj.showPop(transform.position);
        gameObject.SetActive(false);
    }

}
