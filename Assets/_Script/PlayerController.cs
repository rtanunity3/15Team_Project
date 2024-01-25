using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private GameObject[] fruitsOnStick;
    [SerializeField] private float moveSpeed = 10f;
    private float fixedYPosition = -4f;
    private Rigidbody2D _rigidbody;

    private Vector2 lastMousePosition; // 마우스 마지막 위치
    private float arrivalThreshold = 0.1f; // 이동정지를 위한 마우스 최소거리

    Stack<Fruit> fruitStack = new Stack<Fruit>(); // 꽂힌 과일. 폭탄맞으면 후열부터 삭제

    private void Awake()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveStick(lastMousePosition);
    }

    public void OnAim(InputValue value)
    {
        Vector2 targetPos = _camera.ScreenToWorldPoint(value.Get<Vector2>());
        //Debug.Log(targetPos);
        targetPos.y = fixedYPosition; // y축 고정
        if (targetPos != lastMousePosition)
        {
            lastMousePosition = targetPos;
        }
        MoveStick(targetPos);
    }

    private void MoveStick(Vector2 targetPos)
    {
        // 현재 위치에서 목표 위치까지 부드럽게 이동
        Vector2 newPosition = Vector2.Lerp(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPosition);

        // 도착 여부 확인 및 움직임 멈춤
        if (Vector2.Distance(transform.position, targetPos) < arrivalThreshold)
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject fruitObj = collision.gameObject;
        if (fruitObj == null)
        {
            Debug.LogWarning("OnTriggerEnter fruit null");
            return;
        }

        Fruit fruit = fruitObj.GetComponent<Fruit>();
        Debug.Log($"충돌 {fruit.type}");
        if (fruit.type == FruitsType.Bomb)
        {
            fruit.gameObject.SetActive(false); // 폭탄은 무조건 숨기기
            PopFruit();
        }
        else
        {
            PushFruit(fruit);
        }

        // 스택이 풀로 차면 완료됐다고 알리고 막대초기화
        if (fruitStack.Count == 3)
        {
            // TODO: 완료됐다고 게임 매니저에게 알림
            int[] playerTanghulu = StackToArray(fruitStack);
            GameManager.Instance.UpdateTanghuluProgress(playerTanghulu);

            Debug.Log($"완료 {playerTanghulu.Length}/3");
            foreach (var number in playerTanghulu)
            {
                Debug.Log(number);
            }

            // TODO : 3개 되자마자 빠른속도로 사라진다. 뭔가 만들었다고 이펙트나 딜레이같은것 넣어야할듯
            // 초기화
            InitFruit();
        }
    }

    private void InitFruit()
    {
        foreach (GameObject fruit in fruitsOnStick)
        {
            fruit.SetActive(false);
        }
        fruitStack.Clear();
    }

    private void PopFruit()
    {
        // NOTE: 아무것도 없는 경우 효과만 나오고 실제 데이터변화는 없음
        if (fruitStack.Count > 0)
        {
            // 스틱에서 마지막꺼 비활성화
            fruitsOnStick[fruitStack.Count - 1].SetActive(false);

            fruitStack.Pop();
        }
    }

    private void PushFruit(Fruit fruit)
    {
        if (fruitStack.Count < 3)
        {
            fruit.gameObject.SetActive(false); // 떨어지던것 숨기고
            fruitStack.Push(fruit);
            // 그려주기
            SpriteRenderer fruitSpriteRenderer = fruitsOnStick[fruitStack.Count - 1].GetComponent<SpriteRenderer>();
            fruitSpriteRenderer.sprite = Resources.Load<Sprite>($"Fruits/{fruit.type}");
            fruitsOnStick[fruitStack.Count - 1].SetActive(true);
        }
    }

    private int[] StackToArray(Stack<Fruit> stack)
    {
        // 스택의 크기만큼 배열 생성
        int[] array = new int[3];
        for (int i = 2; i >= 0; i--)
        {
            array[i] = (int)stack.Pop().type;
        }
        return array;
    }
}