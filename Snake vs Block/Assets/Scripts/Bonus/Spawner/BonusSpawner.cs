using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Transform _container;
    [SerializeField] private int _repeatCount;
    [SerializeField] private int _distanceBetweenRandomLine;
    [Header ("Bonus")]
    [SerializeField] private Bonus _bonusTemplate;
    [SerializeField] private int _bonusSpawnChance;

    private BonusSpawnPoint[] _bonusSpawnPoints;

    private void Start()
    {
        _bonusSpawnPoints = GetComponentsInChildren<BonusSpawnPoint>();
        for (int i = 0; i < _repeatCount; i++)
        {
            MoveSpawner(_distanceBetweenRandomLine);
            GenerateRandomElements(_bonusSpawnPoints, _bonusTemplate.gameObject, _bonusSpawnChance);
        }
    }
    private void GenerateRandomElements(BonusSpawnPoint[] BonusspawnPoints, GameObject generatedElement, int BonusSpawnChance)
    {
        for (int i = 0; i < BonusspawnPoints.Length; i++)
        {
            if (Random.Range(0, 100) < BonusSpawnChance)
            {
                GameObject element = GenerateElement(BonusspawnPoints[i].transform.position, generatedElement);
            }
        }
    }
    private GameObject GenerateElement(Vector3 BonusspawnPoint, GameObject generatedElement)
    {
        BonusspawnPoint.y -= generatedElement.transform.localScale.y;
        return Instantiate(generatedElement, BonusspawnPoint, Quaternion.identity, _container);
    }
    private void MoveSpawner(int distanceY)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + distanceY, transform.position.z);
    }
}
