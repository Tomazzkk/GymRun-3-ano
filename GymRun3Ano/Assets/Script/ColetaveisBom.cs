using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetaveisBom : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PainelDica(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager.instance.quantcoletada++;

        }
    }

    public void PainelDica()
    {
        if (GameManager.instance.quantcoletada >= 1)
        {
            GameManager.instance.panelDica.SetActive(true);
            Time.timeScale = 1;
        }
                
    }
}
