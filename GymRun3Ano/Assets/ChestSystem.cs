using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSystem : MonoBehaviour
{
    public GameObject _bauAberto;
    public GameObject _bauFechado;
    public GameObject _panelChest;
    public bool _colliderChest = false;
    public static ChestSystem instance;


    public void Awake()
    {
        instance = this;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _colliderChest = true;
            _panelChest.SetActive(true);
            Debug.Log("Bau colidiu com player");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _colliderChest = false;
            _panelChest.SetActive(false);
            Debug.Log("Bau saiu da colisao");
        }
    }
}
