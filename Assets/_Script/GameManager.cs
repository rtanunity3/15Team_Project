using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState currentState;

    public int currentPhase = 1;
    public int tanghuluMade = 0;
    public int highScore = 0; // 얘는, PlayerPrefs 또는 json 등의 내용으로 초기화 할 듯
    public int score = 0;

    private int minFruitType = 0;
    private int maxFruitType = 2;
    int[] targetTanghulu = new int[3];

    [SerializeField] private GameObject fruitPrefab;
    private Vector3[] positions; // 프리팹이 생성될 위치

    // 데이터나 기타 변수 작성

    private void Awake()
    {
        // 싱글톤 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeGameState();

            // PlayerPrefs를 사용하는 경우
            highScore = PlayerPrefs.GetInt("HighScore", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeGameState()
    {
        // 게임 상태를 MainMenu로 초기화
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;

        // 상태 변경에 따른 추가 로직
        switch (currentState)
        {
            case GameState.MainMenu: // 메인 메뉴 로직
                Time.timeScale = 1; // 타임스케일 정상화
                // SoundManager.Instance.PlayMainMenuMusic(); // 메인메뉴 배경음악 재생
                break;

            case GameState.Playing: // 게임 시작 로직
                Time.timeScale = 1; // 타임스케일 정상화
                // SoundManager.Instance.PlayGameMusic(); // 현재 사운드가 게임중 사운드가 아니라면 게임중 사운드로 전환, (희망사항)배속 x1
                break;

            case GameState.Paused: // 일시 정지 관련 로직
                Time.timeScale = 0; // 타임스케일 일시정지
                // SoundManager.Instance.PauseMusic(); // (희망사항) 필요 시 배경음악 일시 정지 또는 볼륨 감소
                break;

            case GameState.GameOver: // 게임 종료 관련 로직
                Time.timeScale = 0; // (필요 시 조율) 타임스케일 일시정지
                // SoundManager.Instance.PlayGameOverSound(); // 게임 오버 사운드 재생
                break;
        }
    }

    // 게임시작 버튼이 눌렸을 때, ButtonManager에서 호출
    public void StartGame()
    {
        ResetData();
        StartCoroutine(LoadMainSceneAndInitialize());
    }

    // <<<--- 여기부터 씬 로딩 후에 목표 탕후루 생성을 위한 작업
    IEnumerator LoadMainSceneAndInitialize()
    {
        // 비동기 씬 로딩 시작
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("_MainScene");

        // 로딩 완료까지 대기
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 씬 로딩 완료 후 초기화 작업 수행
        ChangeState(GameState.Playing);
        GenerateTargetTanghulu();
    }

    // MainScene 비동기 로드
    public void LoadMainSceneAsync()
    {
        StartCoroutine(LoadMainSceneAndInitialize());
    }
    // --->>> 여기까지 StartGame() 수행

    // 세팅 버튼이 눌렸을 때, ButtonManager에서 호출(까진 안해도 되지만 일단 마련)
    public void ToggleSettings()
    {
        UIManager.Instance.ToggleSettingsPanel();
    }
    // 일시정지 버튼이 눌렸을 때, ButtonManager에서 호출
    public void PauseGame()
    {
        UIManager.Instance.TogglePausedPanel(); // 일시 정지 메뉴 토글
        ChangeState(GameState.Paused);
    }
    // 이어하기 버튼이 눌렸을 때, ButtonManager에서 호출
    public void ResumeGame()
    {
        UIManager.Instance.TogglePausedPanel(); // 일시 정지 메뉴 토글
        ChangeState(GameState.Playing);
    }
    // 다시하기 버튼이 눌렸을 때, ButtonManager에서 호출
    public void RestartGame()
    {
        ResetData();
        LoadMainScene();
        ChangeState(GameState.Playing);
    }
    // 메인메뉴로 버튼이 눌렸을 때, ButtonManager에서 호출
    public void BackMainMenu()
    {
        LoadTitleScene();
        ChangeState(GameState.MainMenu);
    }
    // 게임 종료 조건 달성 시, GameManager에서 호출
    public void GameOver()
    {
        UpdateHighScore(); // 최고 점수 업데이트 및 저장
        UIManager.Instance.ToggleResultPanel(); // 게임 오버 UI 활성화
        ChangeState(GameState.GameOver);
    }
    // 게임 Quit 메서드. 사용 안해도 되지만 일단 마련
    public void QuitGame()
    {
        Application.Quit();

        // 유니티 에디터에서 실행 중인 경우 에디터 플레이 모드 종료
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // MainScene 로드
    public void LoadMainScene()
    {
        SceneManager.LoadScene("_MainScene");
    }
    // TitleScene 로드
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("_TitleScene");
    }

    // 게임 재시작을 위한 게임 데이터 초기화 메서드
    public void ResetData()
    {
        currentPhase = 1;
        tanghuluMade = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        score = 0;
    }

    // 목표 탕후루 생성 및 표시, TODO: 씬에 보여주기
    public void GenerateTargetTanghulu()
    {
        // 랜덤한 과일 {currentPhase + 2}종류를 선택하여 목표 탕후루를 생성하고 표시하는 로직
        maxFruitType = currentPhase + 1;
        for (int i = 0; i < targetTanghulu.Length; i++)
        {
            targetTanghulu[i] = Random.Range(minFruitType, maxFruitType + 1);
        }

        // positions 배열이 초기화되지 않은 경우 "TanghuluUI" 기점으로 목표 탕후루를 표시할 위치를 새로 지정
        if (positions == null || positions.Length == 0)
        {
            Transform tanghuluUIPosition = GameObject.Find("TanghuluUI").transform.Find("Image");

            positions = new Vector3[]
            {
            new Vector3(tanghuluUIPosition.position.x, tanghuluUIPosition.position.y - 50, tanghuluUIPosition.position.z),
            new Vector3(tanghuluUIPosition.position.x, tanghuluUIPosition.position.y, tanghuluUIPosition.position.z),
            new Vector3(tanghuluUIPosition.position.x, tanghuluUIPosition.position.y + 50, tanghuluUIPosition.position.z)
            };
        }

        // 프리팹 생성 및 fruitNum 지정
        for (int i = 0; i < targetTanghulu.Length; i++)
        {
            GameObject fruit = Instantiate(fruitPrefab, positions[i], Quaternion.identity);
            Fruit fruitComponent = fruit.GetComponent<Fruit>();

            if (fruitComponent != null)
            {
                // fruitComponent.fruitNum = targetTanghulu[i]; // targetTanghulu 배열의 값으로 fruitNum 설정
                // fruitComponent의 넘버링만으로 스프라이트를 바꾸기 힘들다면, GameManager에서 작업.
            }
        }
    }

    // Player에서 과일 세개 쌓이면 매개변수로 넣어 호출 바람. 점수반영, 난이도 관리, 종료 조건 검사 수행
    // 매개변수 형식에 관해서는 추후 맞춰서 수정
    public void UpdateTanghuluProgress(int[] playerTanghulu)
    {
        tanghuluMade++;
        CalculateAndUpdateScore(playerTanghulu); // 플레이어 탕후루와 목표 탕후루를 비교하고 점수를 반영
        if (tanghuluMade % 4 == 0 || tanghuluMade % 7 == 0)
        {
            IncreaseDifficulty(); // 난이도 상승
        }
        // 또한 종료 조건 만족 시 결과 관련 메서드
        if (tanghuluMade >= 10)
        {
            ChangeState(GameState.GameOver);
        }
        GenerateTargetTanghulu();
    }
    public void UpdateTanghuluProgress() // 매개변수의 전달이 없을 경우, -1,-1,-1 전달
    {
        UpdateTanghuluProgress(new int[] { -1, -1, -1 });
    }

    // 난이도 상승
    private void IncreaseDifficulty()
    {
        currentPhase++;
    }

    // 점수 계산 및 업데이트
    private void CalculateAndUpdateScore(int[] playerTanghulu)
    {
        // 점수 계산 및 업데이트 로직
        int matchCount = 0; // 일치하는 인덱스의 개수를 저장할 변수

        // playerTanghulu와 targetTanghulu 배열 비교하여 일치하는 인덱스의 갯수 확인
        for (int i = 0; i < 3; i++)
        {
            if (playerTanghulu[i] == targetTanghulu[i])
            {
                matchCount++;
            }
        }
        // 일치하는 개수에 따른 점수 산정
        switch (matchCount)
        {
            case 1:
                score += 100; // 1개 일치 시 100점
                break;
            case 2:
                score += 300; // 2개 일치 시 300점
                break;
            case 3:
                score += 600; // 3개 일치 시 600점
                break;
                // 일치하는 개수가 0개인 경우 점수 증가 없음
        }
    }

    // 최고 점수 업데이트 및 저장
    private void UpdateHighScore()
    {
        if(score > highScore)
        {
            highScore = score;
            // 게임 종료시에도 로컬에 저장되도록 하는 메서드
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

}