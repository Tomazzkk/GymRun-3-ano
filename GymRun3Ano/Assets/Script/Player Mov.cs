using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMov : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    public bool isGrounded;
    public List<AudioClip> audios;
    public static PlayerMov instance;
    [SerializeField] public Animator Animator;

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
        Move();
        Jump();
        VirarJogador();
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
            GameObject.Find("Image").GetComponent<Image>().fillAmount += 0.99f;
        }

        if (collision.gameObject.CompareTag("ComidaRuim"))
        {
            GameObject.Find("Image").GetComponent<Image>().fillAmount -= 0.05f;
            if (GameObject.Find("Image").GetComponent<Image>().fillAmount <= 0.01f)
            {
               GameOver();
            }
        }

            

            if (collision.gameObject.CompareTag("ObjMortal"))
            {
                 if (GameObject.Find("Image").GetComponent<Image>().fillAmount <= 0.98f)
                 {

                  GameOver();

                 }

            if (GameObject.Find("Image").GetComponent<Image>().fillAmount >= 0.99f)
            {
                GameObject.Find("Image").GetComponent<Image>().fillAmount -= 0.3f;
            }

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

    public void VirarJogador()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Animator.SetBool("Running", true);
            transform.localScale = new Vector3(-1.903597f, 1.903597f, 1.903597f);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            Animator.SetBool("Running", true);
            transform.localScale = new Vector3(1.903597f, 1.903597f, 1.903597f);
        }
        else
        {
            Animator.SetBool("Running", false);
        }
    }


}