using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int speed;
    public int jumpForce;
    public int lives;
    public int levelTime;

    private Rigidbody2D rb;

    private GameObject foot;
    private SpriteRenderer sprite;
    private Animator animator;

    private float startTime;
    private float spentTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //find the reference to the foot gameoject child
        foot = transform.Find("foot").gameObject; 

         sprite= gameObject.transform.Find("player-idle-1").GetComponent<SpriteRenderer>();

        //get the animator controller
        animator = gameObject.transform.Find("player-idle-1").GetComponent<Animator>();

        startTime = Time.time;
    }

    private void FixedUpdate()
    {
        //get the value from -1 to 1 of the horizontal move
        float inputX = Input.GetAxis("Horizontal");
        //apply phisic velocity to the object with the move value * speed
        //the y coordenate is the same
        rb.velocity = new Vector2 (inputX * speed, rb.velocity.y);
    }

    private void Update()
    {

        Flip();
        PlayerAnimate();
        spentTime = (int)(Time.time - startTime);
        

    }
    /// <summary>
    /// Check if touching the ground
    /// </summary>
    /// <returns>if touching or not</returns>
    private bool TouchGround()
    {
        //Send a imaginary line down 0.2f distance 
        RaycastHit2D hit = Physics2D.Raycast(foot.transform.position , Vector2.down, 0.2f);
        
        // touching something
        return hit.collider != null;

    }

    /// <summary>
    /// Reduce lives from player
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        lives -= damage;
        Debug.Log(lives);
        if (lives == 0)
        {
            //GameManager.instance.Win = false;
        }
    }

    /// <summary>
    /// Control by code all the animation states
    /// </summary>
    private void PlayerAnimate()
    {
        //player jumping
        if (!TouchGround()) animator.Play("playerJump");

        //player running
        else if (TouchGround() && Input.GetAxisRaw("Horizontal") != 0) animator.Play("playerRunnig");

        //player idle
        else if (TouchGround() && Input.GetAxisRaw("Horizontal") == 0) animator.Play("playerIdle");
    }


    private void Flip()
    {
        //pressing space and touching the ground
        if (Input.GetKeyDown(KeyCode.Space) && TouchGround())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        Moving();
    }

    private Boolean Moving()
    {
        if (rb.velocity.x > 0) sprite.flipX = false;
        else if (rb.velocity.x < 0) sprite.flipX = true;

        return sprite.flipX;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            //destroy the PowerUps
            Destroy(collision.gameObject);
            //and after 0.1 seconds show the info
            Invoke(nameof(InfoPowerUp), 0.1f);
        }
    }

    private void InfoPowerUp()
    {
        int pwupNum = GameObject.FindGameObjectsWithTag("PowerUp").Length;

         //write in HUD how many PowerUp left
        Debug.Log("PowerUps: " + pwupNum);

        if (pwupNum == 0)
        {
            //GameManager.instance.Win = true;
        }
    }

}
