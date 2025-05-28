using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mapSegmentPrefab;  // Prefab da parte do mapa
    public Transform player;             // Referência ao jogador
    public int maxSegments = 3;          // Quantidade máxima de segmentos ativos
    public float segmentWidth = 10f;     // Largura de cada segmento
    public float offsetPos;
    public int randomInt;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject pauseButton;
    public List<GameObject> mapas = new List<GameObject>();
    private List<GameObject> activeSegments = new List<GameObject>();
    public Vector3 nextSpawnPosition; // Próxima posição para spawnar um segmento
    public GameObject panelDica;
    public int contdicas;
    public static GameManager instance;
    public GameObject gameOverPanel;
    
    private void Awake()
    {
        instance = this;
    
    }
    void Start()
    {
        randomInt = 0;
        GameObject newSegment = Instantiate(mapSegmentPrefab, nextSpawnPosition, Quaternion.identity);
        activeSegments.Add(newSegment);
        nextSpawnPosition.x += segmentWidth + offsetPos; // Atualiza posição para o próximo segmento
        //RemoveOldSegment();
        // Inicia criando os primeiros segmentos
        for (int i = 0; i < maxSegments; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        // Se o jogador ultrapassar a posição do último segmento criado
        if (player.position.x > nextSpawnPosition.x - (maxSegments * segmentWidth))
        {
            SpawnSegment();  // Cria um novo segmento na frente
           // Remove o segmento mais antigo
        }
    }

    void SpawnSegment()
    {
        
        GameObject newSegment = Instantiate(mapas[randomInt], nextSpawnPosition, Quaternion.identity);
        activeSegments.Add(newSegment);
        nextSpawnPosition.x += segmentWidth + offsetPos; // Atualiza posição para o próximo segmento
        randomInt = Random.Range(1, mapas.Count);
        RemoveOldSegment();
    }

    void RemoveOldSegment()
    {
        if (activeSegments.Count > maxSegments)
        {
            GameObject oldSegment = activeSegments[0];
            activeSegments.RemoveAt(0);
            Destroy(oldSegment);
        }
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
        Debug.Log("Botão FeitoButton foi pressionado!");
    }

    
    

}
