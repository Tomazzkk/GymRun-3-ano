using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  //  public GameObject mapSegmentPrefab;  // Prefab da parte do mapa
    public Transform player;             // Refer�ncia ao jogador
   // public int maxSegments = 3;          // Quantidade m�xima de segmentos ativos
    //public float segmentWidth = 10f;     // Largura de cada segmento
    //public float offsetPos;
    public int randomInt;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject pauseButton;
    //public List<GameObject> mapas = new List<GameObject>();
    //private List<GameObject> activeSegments = new List<GameObject>();
    //public Vector3 nextSpawnPosition; // Pr�xima posi��o para spawnar um segmento
    public GameObject panelDica;
    public int contdicas;
    public static GameManager instance;
    public GameObject gameOverPanel;
    public List<Sprite> images = new List<Sprite>();

    private void Awake()
    {
        instance = this;
    
    }
   

    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void VoltarButton()
    {
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void FeitoButton()
    {
        Time.timeScale = 1;
        panelDica.SetActive(false);
        Debug.Log("Bot�o FeitoButton foi pressionado!");
    }

    
    

}
