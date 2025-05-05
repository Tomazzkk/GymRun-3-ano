using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    public float timer;
  
    private void Update()
    {
        

    }

    public void FeitoButton()
    {
        timer = 3;
        GameManager.instance.panelDica.SetActive(false);
        
    }


}
