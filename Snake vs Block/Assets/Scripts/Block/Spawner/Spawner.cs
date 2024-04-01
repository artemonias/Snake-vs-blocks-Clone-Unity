using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [Header ("General")]
    [SerializeField] private Transform _container;
    [SerializeField] private int _repeatCount;
    [SerializeField] private int _distanceBetweenFulline;
    [SerializeField] private int _distanceBetweenRandomLine;
    [Header ("Block")]
    [SerializeField] private Block _blockTemplate;
    [SerializeField] private int _blockSpawnChance;
    [Header ("Wall")]
    [SerializeField] private Wall _wallTemplate;
    [SerializeField] private int _wallSpawnChance;

    private BlockSpawnPoint[] _blockSpawnPoints;
    private WallSpawnPoint[] _wallSpawnPoints;

        private void Start()
    {
        _blockSpawnPoints = GetComponentsInChildren<BlockSpawnPoint>();
        _wallSpawnPoints = GetComponentsInChildren<WallSpawnPoint>();
        for(int i = 0; i < _repeatCount; i++)
        {
            MoveSpawner(_distanceBetweenFulline);
            GenerateRandomElements(_wallSpawnPoints, _wallTemplate.gameObject, _wallSpawnChance, _distanceBetweenFulline, _distanceBetweenFulline / 2f);
            GenerateFullLine(_blockSpawnPoints, _blockTemplate.gameObject);
            MoveSpawner(_distanceBetweenRandomLine);
            GenerateRandomElements(_wallSpawnPoints, _wallTemplate.gameObject, _wallSpawnChance, _distanceBetweenRandomLine, _distanceBetweenRandomLine / 2f);
            GenerateRandomElements(_blockSpawnPoints, _blockTemplate.gameObject, _blockSpawnChance);
        }
    }
    private void GenerateFullLine(SpawnPoint[] spawnPoints, GameObject generatedElement)
    {
        for (int i = 0; i < spawnPoints.Length;i++)
        {
            GenerateElement(spawnPoints[i].transform.position, generatedElement);
        }
    }
    private void GenerateRandomElements(SpawnPoint[] spawnPoints, GameObject generatedElement, int SpawnChance, float scaleY = 0.36f, float offsetY = 0)
    {
        for(int i=0; i < spawnPoints.Length; i++)
        {
            if(Random.Range(0,100) < SpawnChance)
            {
                GameObject element = GenerateElement(spawnPoints[i].transform.position, generatedElement, offsetY);
                element.transform.localScale = new Vector3(element.transform.localScale.x, scaleY, element.transform.localScale.z);
            }
        }
    }
    private GameObject GenerateElement(Vector3 spawnPoint, GameObject generatedElement, float offsetY = 0)
    {
        spawnPoint.y -= offsetY;
        return Instantiate(generatedElement, spawnPoint, Quaternion.identity, _container);
    }
    private void MoveSpawner(int distanceY)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + distanceY, transform.position.z);
    }
}
