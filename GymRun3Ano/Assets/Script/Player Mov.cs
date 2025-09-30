using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerMov : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 8f;
    public LayerMask groundLayer;
    private float nextDamageTime = 0f;
    public float damageCooldown = 2f;
    Animator anim;
    public ChestSystem bauColidido;
    private Rigidbody2D rb;
    public bool isGrounded;
    public List<AudioClip> audios;
    public static PlayerMov instance;
    [SerializeField] public Animator Animator;
    public float dano= 1 ;
    public Light2D Light;
    public int _desafioAlimento;
    public TextMeshProUGUI _TextProgresso;
    public GameObject _imageHeart;
    public GameObject _desafioObj;
    public float climbSpeed = 3f; // Velocidade de subida na escada
    public CapsuleCollider2D collider;
    public bool EstaNaescada;
    private void Awake()
    {
        EstaNaescada = false;
        instance = this;
        _desafioAlimento = 0;
       
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Light = GetComponentInChildren<Light2D>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Move();
        Jump();
        VirarJogador();
        OpenChest();
        Agachar();
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetBool("Soco", true);
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            anim.SetBool("Soco", false);
        }
        
        
       
    }
    /*
    public void DesafioHeart(){
        _TextProgresso.text = "(" +  _desafioAlimento.ToString() + "/10)";
        if(_desafioAlimento >= 10){
            
            _imageHeart.SetActive(true);
             Invoke("AumentaVida", 1.15f);
             _desafioAlimento =0;
        }
    }
    */
    public void AumentaVida(){
        GameObject.Find("VidaImage").GetComponent<Image>().fillAmount += 0.10f;
        _desafioObj.GetComponent<Animation>().Play() ;
        _imageHeart.SetActive(false);


    }
    public void Agachar()
    {
        if (Input.GetKeyDown(KeyCode.S)){
            anim.SetBool("Agachado", true );
            collider.GetComponent<CapsuleCollider2D>().size =  new Vector2(0.45f, 0.62f);
            speed = 3;
        }
        if (Input.GetKeyUp(KeyCode.S)){
            anim.SetBool("Agachado", false);
            collider.GetComponent<CapsuleCollider2D>().size =  new Vector2(0.45f, 0.98f);
        }


    }
    public void OpenChest()
    {
        if(bauColidido== null)
            return; 
        if (bauColidido._colliderChest)
        {
            if (Input.GetKey(KeyCode.E) && !ChestSystem.instance._openChest)
            {

                bauColidido._bauAberto.GetComponent<SpriteRenderer>().sprite = bauColidido._bauFechado.GetComponent<SpriteRenderer>().sprite;
                Debug.Log("BauAberto");
                bauColidido._openChest = true;
                bauColidido.ChestPanel.SetActive(true);
                
            }

        }
    }

    
    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);              
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (EstaNaescada)
        {
            float moveInputY = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x,moveInputY *speed);
            anim.SetBool("SubindoEscada", true);

        }
        if (!EstaNaescada)
        {
            anim.SetBool("SubindoEscada", false);
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetBool("Jumping", true);
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
            anim.SetBool("Jumping", false);
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Inimigo") && Time.time >= nextDamageTime)
        {
            Image vida = GameObject.Find("VidaImage").GetComponent<Image>();

            vida.fillAmount -= 0.05f;
            nextDamageTime = Time.time + damageCooldown;
            //CameraShake.instance.ShakeCamera(1f, 0.1f);

            if (vida.fillAmount <= 0.01f)
            {
                GameOver();
            }
        }

        if(collision.gameObject.transform.CompareTag("Inimigo") && Input.GetKeyDown(KeyCode.Mouse0))
        {
            collision.gameObject.GetComponent<EnemyFollow>().vida -= dano;
           
        }

    }

    public void TrocaDeMapa()
    {
        if(SceneManager.GetActiveScene().name == "ScenePowerStation" )
        {
            SceneManager.LoadScene("Mapa Floresta");
        }else if(SceneManager.GetActiveScene().name == "Mapa Floresta")
        {
            SceneManager.LoadScene("ScenePowerStation");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            TrocaDeMapa();
        }
        if (collision.gameObject.CompareTag("obj"))
        {
            GameObject.Find("VidaImage").GetComponent<Image>().fillAmount += 0.05f;
            _desafioAlimento++;
            Destroy(collision.gameObject);
            //SceneManager.GetActiveScene(). como vericar a cena atual
        }

        if (collision.gameObject.CompareTag("ComidaRuim"))
        {
            //CameraShake.instance.ShakeCamera(1f, 0.1f);
            GameObject.Find("VidaImage").GetComponent<Image>().fillAmount -= 0.05f;
            Destroy(collision.gameObject);
           
            if (GameObject.Find("VidaImage").GetComponent<Image>().fillAmount <= 0.01f)
            {
               GameOver();
            }
        }if (collision.gameObject.CompareTag("morre"))
        {
            GameOver();
        }

            if (collision.gameObject.CompareTag("ObjMortal"))
            {
                 if (GameObject.Find("VidaImage").GetComponent<Image>().fillAmount <= 0.98f)
                 {

                  GameOver();

                 }

            if (GameObject.Find("VidaImage").GetComponent<Image>().fillAmount >= 0.99f)
            {
                GameObject.Find("VidaImage").GetComponent<Image>().fillAmount -= 0.3f;
            }

            }

        if (collision.gameObject.CompareTag("ColliderLight"))
        {
            InvokeRepeating("ApagaDevagar",0, 0.03f);
          
            
       } if (collision.gameObject.CompareTag("ColliderExitLight"))
        {
            InvokeRepeating("AtivaDevagar",0, 0.03f);
            
       }
        if (collision.gameObject.CompareTag("ColliderEscada"))
        {
            EstaNaescada = true;
           
        }
       

    }

  
    public void ApagaDevagar() 
    {
        if (Light.pointLightOuterRadius<=10)
        {
            CancelInvoke("ApagaDevagar");
        }
        else
        {
            Light.pointLightOuterRadius--;
        }
    }public void AtivaDevagar() 
    {
        if (Light.pointLightOuterRadius>=56)
        {
            CancelInvoke("AtivaDevagar");
        }
        else
        {
            Light.pointLightOuterRadius++;
        }
    }
    public void GameOver()
    {
       
            GameManager.instance.gameOverPanel.SetActive(true);
            GameObject.Find("VidaImage").GetComponent<Image>().fillAmount += 0.05f;
            Debug.Log("ta certo");
            Time.timeScale = 0;
         
    }

    public void BotaoVoltarGameOver()
    {
        GameManager.instance.gameOverPanel.SetActive(false);
        SceneManager.LoadScene("ScenePowerStation");
        Time.timeScale = 1;
    }

    public void VirarJogador()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Animator.SetBool("Running", true);
            GetComponent<SpriteRenderer>().flipX = true;
           // transform.localScale = new Vector3(-1.903597f, 1.903597f, 1.903597f);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            Animator.SetBool("Running", true);
           // transform.localScale = new Vector3(1.903597f, 1.903597f, 1.903597f);
        }
        else
        {
            Animator.SetBool("Running", false);
        }
    }

   public  void ButtonSpeed()
    {
       speed = 14;
        ChestSystem.instance.ChestPanel.SetActive(false) ;
        ChestSystem.instance._openChest = true;
    }

    public void ButtonDamage()
    {
       dano = 2;
        ChestSystem.instance._openChest = true;
        ChestSystem.instance.ChestPanel.SetActive(false);
    }

   public  void buttonLife()
    {
        GameObject.Find("VidaImage").GetComponent<Image>().fillAmount += 0.10f;
        ChestSystem.instance._openChest = true;
        ChestSystem.instance.ChestPanel.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ColliderEscada"))
        {
            EstaNaescada=false;
        }

    }

}