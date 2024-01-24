using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int currentPhase = 1;
    public int tanghuluMade = 0;
    public int highScore = 0; // 얘는, PlayerPrefs 또는 json으로 저장한거에서 불러오는 내용으로 초기화?
    public int score = 0;

    // 데이터나 기타 변수 작성

    private void Awake()
    {
        // 싱글톤 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 게임 시작 메서드
    public void StartGame()
    {
        LoadMainScene();

        // 초기화 및 게임 시작에 필요한 로직
        StartDroppingFruits();
    }

    // MainScene 로드
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    // 과일 떨구기(로직 당장 생각이 안남.. 이후 구현 예정. 일단 메서드 명 참고)
    public void StartDroppingFruits()
    {
        // int currentPhase 에 따라 낙하하는 과일 설정
        // 과일이 떨어지기 시작하는 로직
    }

    // 목표 탕후루 생성 및 표시
    public void GenerateTargetTanghulu()
    {
        // int currentPhase 에 따라 목표 탕후루 설정
        // 랜덤한 과일 3종류를 선택하여 목표 탕후루를 생성하고 표시하는 로직(화면에 표시하는 로직은 아마 별도 메서드에 작성하여 여기서 호출)
    }

    // 목표 탕후루와 플레이어의 탕후루 검사 및 점수 반영
    public void CheckTanghuluAndScore()
    {
        // 플레이어 탕후루와 목표 탕후루를 비교하고 점수를 반영하는 로직
    }

    // 탕후루 만들기 진행 상황 업데이트
    public void UpdateTanghuluProgress()
    {
        tanghuluMade++;
        if (tanghuluMade % 4 == 0 || tanghuluMade % 7 == 0)
        {
            IncreaseDifficulty(); // 난이도 상승
        }

        if (tanghuluMade >= 10)
        {
            ShowEndingUI(); // Ending UI 표시
        }
    }

    // 난이도 상승
    public void IncreaseDifficulty()
    {
        currentPhase++;
        // 난이도(Phase)를 상승시키는 로직
    }

    // 점수 계산 및 업데이트
    public void CalculateAndUpdateScore()
    {
        // 점수 계산 및 업데이트 로직
    }

    // Ending UI 표시
    public void ShowEndingUI()
    {
        // 게임 종료 UI 표시 로직
    }

    // ...추가 게임 관리 로직들 작성
}