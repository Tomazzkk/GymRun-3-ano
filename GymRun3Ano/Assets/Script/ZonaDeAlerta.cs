using UnityEngine;

public class ZonaDeAlerta : MonoBehaviour
{
    public EnemyFollow inimigo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inimigo.ComecarPerseguir();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inimigo.PararDePerseguir();
        }
    }
}
