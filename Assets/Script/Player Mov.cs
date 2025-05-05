using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public bool imune;
    public float Tempo;

    private Rigidbody2D rb;
    public bool isGrounded;
    public List<AudioClip> audios;
    public static PlayerMovement instance;

    private void Awake()
    {
        
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (imune == true)

        {
            Tempo += Time.deltaTime;
            if (Tempo >= 5)
            {
                imune = false;
                Tempo = 0;
            }

        }
        
        if (GameObject.Find("Image").GetComponent<Image>().fillAmount <= 0.1f)
        {
            GameOver();
        }
        
       
        Move();
        Jump();
    }
    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);              
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);      
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            GetComponent<AudioSource>().clip = audios[0];
            GetComponent<AudioSource>().Play();
        }
    }

 
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.transform.CompareTag("Grounded"))
        {
            isGrounded = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.transform.CompareTag("Grounded"))
        {
            isGrounded = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("obj"))
        {
            GameObject.Find("Image").GetComponent<Image>().fillAmount += 0.05f;
        }

        if (collision.gameObject.CompareTag("ComidaRuim"))
        {
            GameObject.Find("Image").GetComponent<Image>().fillAmount -= 0.05f;
        }
        if (collision.gameObject.CompareTag("Comida rara"))
        {
            imune = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Objeto mortal") && imune == false)
        {
         
            
        }


    }

    public void GameOver()
    {
        
            GameManager.instance.gameOverPanel.SetActive(true);
            GameObject.Find("Image").GetComponent<Image>().fillAmount += 0.05f;
            Debug.Log("ta certo");
            Time.timeScale = 0;
        
    }

    public void BotaoVoltarGameOver()
    {
        GameManager.instance.gameOverPanel.SetActive(false);
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }



}