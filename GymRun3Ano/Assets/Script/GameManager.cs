using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mapSegmentPrefab;  // Prefab da parte do mapa
    public Transform player;             // Refer�ncia ao jogador
    public int maxSegments = 3;          // Quantidade m�xima de segmentos ativos
    public float segmentWidth = 20f;     // Largura de cada segmento
    public float offsetPos;

    private List<GameObject> activeSegments = new List<GameObject>();
    private Vector3 nextSpawnPosition;   // Pr�xima posi��o para spawnar um segmento

    void Start()
    {
        // Inicia criando os primeiros segmentos
        for (int i = 0; i < maxSegments; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        // Se o jogador ultrapassar a posi��o do �ltimo segmento criado
        if (player.position.x > nextSpawnPosition.x - (maxSegments * segmentWidth))
        {
            SpawnSegment();  // Cria um novo segmento na frente
           // Remove o segmento mais antigo
        }
    }

    void SpawnSegment()
    {
        GameObject newSegment = Instantiate(mapSegmentPrefab, nextSpawnPosition, Quaternion.identity);
        activeSegments.Add(newSegment);
        nextSpawnPosition.x += segmentWidth + offsetPos; // Atualiza posi��o para o pr�ximo segmento
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

}
