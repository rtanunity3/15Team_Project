using System.Collections.Generic;
using UnityEngine;

public enum FruitsType
{
    Apple,
    Blueberry,
    Orange,
    Strawberry,
    Banana,
    Bomb,
    GoldenApple,
}

public class FruitManager : MonoBehaviour
{
    [SerializeField] private GameObject stick;
    [SerializeField] private GameObject[] fruitPrefabs;
    private List<GameObject>[] objectPool;
    private const int objectPoolSize = 7; // 과일당 풀 사이즈
    private const float spawnInterval = 0.4f;
    private const float fixedYPosition = -4f;
    private const int zero = 0;

    private void Awake()
    {
        InitObjectPool();

        // 객체 초기화 및 생성
        if (stick != null && !GameObject.FindGameObjectWithTag(stick.tag))
        {
            Instantiate(stick, new Vector2(zero, fixedYPosition), Quaternion.identity);
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnFruit), zero, spawnInterval);
    }

    private void InitObjectPool()
    {
        objectPool = new List<GameObject>[fruitPrefabs.Length];

        // i = enum.FruitsType 과 동일하게
        for (int i = zero; i < fruitPrefabs.Length; i++)
        {
            objectPool[i] = new List<GameObject>();

            for (int j = zero; j < objectPoolSize; j++)
            {
                GameObject fruit = InstantiateFruit(i);
                fruit.SetActive(false);
                objectPool[i].Add(fruit);
            }
        }
    }
    private GameObject InstantiateFruit(int fruitIndex)
    {
        // 새로운 과일 오브젝트 생성
        GameObject fruit = Instantiate(fruitPrefabs[fruitIndex]);
        objectPool[fruitIndex].Add(fruit);
        return fruit;
    }

    private void SpawnFruit()
    {
        // 랜덤 좌표
        float randX = Random.Range(-2.8f, 2.8f);
        float randY = Random.Range(5, 7);

        // 생성위치
        Vector3 spawnPosition = new Vector3(randX, randY, zero);

        int randFruitIndex;

        int phase = GameManager.Instance != null ? GameManager.Instance.currentPhase : 1;
        switch (phase)
        {
            case 1:
                randFruitIndex = Random.Range(zero, 5);
                break;
            case 2:
                randFruitIndex = Random.Range(zero, 6);
                break;
            default:
                randFruitIndex = Random.Range(zero, 7);
                break;
        }

        GameObject fruit = GetPooledObject(randFruitIndex) ?? InstantiateFruit(randFruitIndex);
        fruit.transform.position = spawnPosition;
        fruit.SetActive(true);
    }

    private GameObject GetPooledObject(int fruitIndex)
    {
        if (fruitIndex < zero || fruitIndex >= objectPool.Length)
        {
            return null;
        }

        for (int i = zero; i < objectPool[fruitIndex].Count; i++)
        {
            if (!objectPool[fruitIndex][i].activeInHierarchy)
            {
                return objectPool[fruitIndex][i];
            }
        }
        return null;
    }

}
