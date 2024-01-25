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

    //grape,
    //pear,
}

public class FruitManager : MonoBehaviour
{
    [SerializeField] private GameObject stick;
    [SerializeField] private GameObject[] fruit;

    private float spawnInterval = 0.4f;
    private ObjectPool objectPool;
    private int fruitsRandomMax = 5;


    private float elapsedTime; // 경과 시간. 일단 시간으로 체크해서 수량 증가

    private void Awake()
    {
        // 객체 초기화 및 생성
        if (stick != null && !GameObject.FindGameObjectWithTag(stick.tag))
        {
            Instantiate(stick, new Vector2(0, -4), Quaternion.identity);
        }
    }

    private void Start()
    {
        objectPool = GetComponent<ObjectPool>();

        InvokeRepeating(nameof(SpawnFruit), 0f, spawnInterval);
    }

    private void SpawnFruit()
    {
        // 랜덤 좌표
        float randomX = Random.Range(-3, 3);
        float randomY = Random.Range(5, 7);

        // 생성위치
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        int randomFruitIndex;
        // 일단 임시로 시간으로 바뀌에 함
        switch (GameManager.Instance.currentPhase)
        {
            case 2:
                randomFruitIndex = Random.Range(0, fruitsRandomMax + 1);
                break;
            case 3:
                randomFruitIndex = Random.Range(0, fruitsRandomMax + 2);
                break;
            default:
                randomFruitIndex = Random.Range(0, fruitsRandomMax);
                break;
        }

        // 과일 생성
        Instantiate(fruit[randomFruitIndex], spawnPosition, Quaternion.identity);
    }

    private void Update()
    {
        // 경과 시간 측정
        elapsedTime += Time.deltaTime;
    }
}
