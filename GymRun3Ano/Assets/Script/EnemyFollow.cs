using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player,pontoA,pontoB; // Refer�ncia ao jogador
    public float detectionRange = 30f; // Dist�ncia para come�ar a seguir
    public float moveSpeed = 5f;
    public bool estaNoPontoA = true;
    public Animator anim;
    private bool seguir = false;
    public Vector2 direction;
    public float vida = 3;

    public static EnemyFollow instance;

    private void Awake()
    {
        instance = this;
    }
    public void ComecarPerseguir()

    
    {
        
        seguir = true;
        anim.SetBool("Detectou", true);
    }


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void PararDePerseguir()
    {
        seguir = false;
        anim.SetBool("Detectou", false);
    }


    void Update()
    {
        if ( player == null) return;
        if(this.transform.position.x == pontoB.position.x) 
        {
        estaNoPontoA = false;
        }else if(this.transform.position.x == pontoA.position.x) 
        {
        estaNoPontoA=true;
        }

        if (seguir == false && estaNoPontoA)
        {
            direction = (pontoB.position - transform.position).normalized;
            transform.position = new Vector2(transform.position.x + direction.x * moveSpeed * Time.deltaTime, transform.position.y);
        }
        else if (!estaNoPontoA && !seguir)
        {
            direction = (pontoA.position - transform.position).normalized;
            transform.position = new Vector2(transform.position.x + direction.x * moveSpeed * Time.deltaTime, transform.position.y);
        }
        else 
        {
            direction = (player.position - transform.position).normalized;
            transform.position = new Vector2(transform.position.x + direction.x * moveSpeed * Time.deltaTime, transform.position.y);
        }

        if(vida <= 0)
        {
            Destroy(gameObject);
        }
        


    }

}
