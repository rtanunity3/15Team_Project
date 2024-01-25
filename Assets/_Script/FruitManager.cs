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

    private float spawnInterval = 0.4f;
    private int fruitsRandomMax = 5;

    //private float elapsedTime; // 경과 시간. 일단 시간으로 체크해서 수량 증가
    private List<GameObject>[] objectPool;
    private int objectPoolSize = 10; // 과일당 풀 사이즈

    private void Awake()
    {
        InitObjectPool();

        // 객체 초기화 및 생성
        if (stick != null && !GameObject.FindGameObjectWithTag(stick.tag))
        {
            Instantiate(stick, new Vector2(0, -4), Quaternion.identity);
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnFruit), 0f, spawnInterval);
    }

    //private void Update()
    //{
    //    // 경과 시간 측정
    //    elapsedTime += Time.deltaTime;
    //}

    private void InitObjectPool()
    {
        objectPool = new List<GameObject>[fruitPrefabs.Length];
        Debug.Log($"{fruitPrefabs.Length}");

        // i = enum.FruitsType 과 동일하게
        for (int i = 0; i < fruitPrefabs.Length; i++)
        {
            objectPool[i] = new List<GameObject>();

            for (int j = 0; j < objectPoolSize; j++)
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
        float randX = Random.Range(-3, 3);
        float randY = Random.Range(5, 7);

        // 생성위치
        Vector3 spawnPosition = new Vector3(randX, randY, 0f);

        int randFruitIndex;

        int phase = GameManager.Instance != null ? GameManager.Instance.currentPhase : 1;
        switch (phase)
        {
            case 1:
                randFruitIndex = Random.Range(0, fruitsRandomMax);
                break;
            case 2:
                randFruitIndex = Random.Range(0, fruitsRandomMax + 1);
                break;
            default:
                randFruitIndex = Random.Range(0, fruitsRandomMax + 2);
                break;
        }


        GameObject fruit = GetPooledObject(randFruitIndex) ?? InstantiateFruit(randFruitIndex);
        fruit.transform.position = spawnPosition;
        fruit.SetActive(true);
    }

    private GameObject GetPooledObject(int fruitIndex)
    {
        if (fruitIndex < 0 || fruitIndex >= objectPool.Length)
        {
            Debug.Log($"{fruitIndex} / {objectPool.Length}");
            return null;
        }

        for (int i = 0; i < objectPool[fruitIndex].Count; i++)
        {
            if (!objectPool[fruitIndex][i].activeInHierarchy)
            {
                return objectPool[fruitIndex][i];
            }
        }
        return null;
    }

}
