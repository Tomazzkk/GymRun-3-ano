using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject creditosPanel;
    public GameObject panelMenu;
    public void PlayButton()
    {
        SceneManager.LoadScene("Mapa Floresta");
        Time.timeScale = 1.0f;
    }

    public void EntraCreditos()
    {
        creditosPanel.SetActive(true);
        panelMenu.SetActive(false);
    }
    
    public void VoltaCreditos()
    {
        creditosPanel.SetActive(false);
        panelMenu.SetActive(true);
    }

}
