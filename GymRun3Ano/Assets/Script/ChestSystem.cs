using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChestSystem : MonoBehaviour
{
    public GameObject _bauAberto;
    public GameObject _bauFechado;
    public GameObject _panelChest;
    public bool _colliderChest = false;
    public static ChestSystem instance;
    public GameObject ChestPanel;
    public bool _openChest = false;


    public void Awake()
    {
        instance = this;
            
    }

    private void Start()
    {
        
    }
    private void Update()
    {
  
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_openChest)
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
            ChestPanel.SetActive(false);
            Debug.Log("Bau saiu da colisao");
        }
    }

   
}
