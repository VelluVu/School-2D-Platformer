using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour {

    Rigidbody2D rb;
    float moveSpeed;
    float jumpForce;
    bool isWalking;
    bool isJumping;
    public bool isGround;
    int jumpTimes;
    Animator p_anim;
    public Image filler;
    float health;
    float maxHealth;
    float previousHealth;
    float counter;
    float maxCounter;
    

    void Start()
    {
        
        counter = 0;
        maxCounter = 1;
        maxHealth = 100f;
        health = maxHealth;
        previousHealth = health;
        isGround = false;
        jumpTimes = 0;
        isWalking = false;
        isJumping = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        p_anim = gameObject.GetComponent<Animator>();
        moveSpeed = 25f;
        jumpForce = 50f;
    }

    private void Update()
    {
        
        AnimationHandle();
      
            jumpTimes = 0;
            Walk();

            Jump();
        
        Death();

        if (counter > maxCounter )
        {
            counter = 0;
            previousHealth = health;
        } else
        {
            counter += Time.deltaTime;
        }

        filler.fillAmount = Mathf.Lerp(previousHealth/maxHealth, health/maxHealth, counter/maxCounter);
        //filler.fillAmount = health / maxHealth;
        /*switch (Input.GetKey())
        {
            case KeyCode.RightArrow:
                break;
            default:
                break;
        }*/
        
    }

    void Walk()
    {

        
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
        
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isWalking = true;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isWalking = true;
        }
        
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            isWalking = true;
        } else
        {
            isWalking = false;
        }
       
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGround && jumpTimes == 0)
        {
            
                isGround = false;
                p_anim.SetTrigger("jump");
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpTimes++;
                rb.gravityScale = 8f;
            
        }
        
    }

    void AnimationHandle ()
    {

        if (isWalking && isGround)
        {
            p_anim.SetBool("isWalking", true);
        }
        else
        {
            p_anim.SetBool("isWalking", false);
        }
        
                      
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            jumpTimes = 0;
            isGround = true;
        } 
            
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.tag == "Trap")
        {
            TakeDamage(20);
        }
        if(collision.collider.tag == "LevelEnd")
        {
            SceneManager.LoadScene("Map");
        }
    }

    public void TakeDamage(float dmg)
    {
        previousHealth = filler.fillAmount * maxHealth;
        counter = 0;
        health -= dmg;
        
    }

    public void Death ()
    {
        if (filler.fillAmount <= 0)
        {
            health = 0;           
            Destroy(gameObject);
        }
    }

}
