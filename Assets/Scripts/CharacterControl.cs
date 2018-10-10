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
    Image filler;
    float health;
    float maxHealth;
    float previousHealth;
    float counter;
    float maxCounter;
    float throwForce;
    Vector3 startPos;
    public GameObject bonfire;
    public GameObject pickAxe;
    Transform throwPoint;
    

    void Start()
    {
        throwForce = 200f;
        throwPoint = GameObject.FindGameObjectWithTag("ThrowPoint").GetComponent<Transform>();
        filler = GameObject.FindGameObjectWithTag("Filler").GetComponent<Image>();
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
        startPos = transform.position;
    }

    private void Update()
    {
        ThrowAxe();
        AnimationHandle();       
        Walk();
        LightBonfire();
        Jump();
        Death();

        if (counter > maxCounter)
        {
            counter = 0;
            previousHealth = health;
        }
        else
        {
            counter += Time.deltaTime;
        }

        filler.fillAmount = Mathf.Lerp(previousHealth / maxHealth, health / maxHealth, counter / maxCounter);

    }

    void ThrowAxe()
    {
        if (Input.GetButtonDown("Fire1"))
        {          
            Instantiate(pickAxe, throwPoint.position, Quaternion.identity);
            
        }
    }

    private void Respawn()
    {
        
            if (Input.GetKeyDown(KeyCode.Return))
            {
            health = maxHealth;
            previousHealth = health;
        }
        
    }

    void LightBonfire()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Destroy(Instantiate(bonfire,gameObject.transform.position, Quaternion.identity),20f);
        }
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
        if (Input.GetButtonDown("Jump") && isGround)
        {
            
                isGround = false;            
                p_anim.SetTrigger("jump");
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);             
                rb.gravityScale = 8f;
            
        }
        
    }

    void AnimationHandle ()
    {

        if (isWalking)
        {
            p_anim.SetBool("isWalking", true);
        }
        else
        {
            p_anim.SetBool("isWalking", false);
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
        if (collision.collider.tag == "Ground")
        {           
            isGround = true;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {        
            isGround = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {       
            isGround = true;

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

        if (filler.fillAmount <= 0 || gameObject.transform.position.y < -100)
        {

            health = 0;
            transform.position = startPos;          
            Respawn();             
                  
        }
    }

}
