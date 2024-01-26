using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    // 패널 참조를 위한 변수
    public GameObject settingsPanel;
    public GameObject pausedPanel;

    public Text score;
    public Text highscore;
    public Text countTanghulu;

    public Text resultHighScore;
    public Text resultScore;

    private Animator resultUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);// root GameObjects 에만 사용 가능하다고 경고 뜨긴 했는데 상관은 없을듯. 경고가 신경쓰이면 오브젝트 서로 분리해도 됨.
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndAssignUIElements(scene);
    }

    private void FindAndAssignUIElements(Scene scene)
    {
        // 로드되는 씬 이름에 따라 UI를 로드
        if (scene.name == "_TitleScene")
        {
            highscore = GameObject.Find("HighScore").GetComponent<Text>();
            settingsPanel = GameObject.Find("Canvas").transform.Find("SettingUI").gameObject;
        }
        else if (scene.name == "_MainScene")
        {
            GameObject canvasObject = GameObject.Find("Canvas");
            pausedPanel = canvasObject.transform.Find("PauseUI").gameObject;
            settingsPanel = GameObject.Find("SettingUI");

            score = canvasObject.transform.Find("Menu").transform.Find("ScoreUI").transform.Find("CurrentScore").GetComponent<Text>();
            highscore = canvasObject.transform.Find("Menu").transform.Find("ScoreUI").transform.Find("HighScore").GetComponent<Text>();
            countTanghulu = canvasObject.transform.Find("Menu").transform.Find("CountUI").transform.Find("CurrentScore").GetComponent<Text>();

            resultUI = canvasObject.transform.Find("ResultUI").gameObject.GetComponent<Animator>();
            resultScore = canvasObject.transform.Find("ResultUI").transform.Find("ScoreText").GetComponent<Text>();
            resultHighScore = canvasObject.transform.Find("ResultUI").transform.Find("HighScoreText").GetComponent<Text>();
        }

        // 씬 로드 이벤트 발생할 때마다 초기 상태를 비활성화로 설정
        SetPanelActive(settingsPanel, false);
        SetPanelActive(pausedPanel, false);

    }

    // 패널 활성화/비활성화를 위한 범용 메서드
    public void SetPanelActive(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
    public void SetPanelActive(GameObject panel, bool isActive)
    {
        if (panel != null)
        {
            panel.SetActive(isActive);
        }
    }

    // 각 패널을 위한 활성화/비활성화 메서드
    public void ToggleSettingsPanel()
    {
        SetPanelActive(settingsPanel, !settingsPanel.activeSelf);
    }
    public void ToggleSettingsPanel(bool isActive)
    {
        SetPanelActive(settingsPanel, isActive);
    }

    public void TogglePausedPanel()
    {
        SetPanelActive(pausedPanel, !pausedPanel.activeSelf);
    }
    public void TogglePausedPanel(bool isActive)
    {
        SetPanelActive(pausedPanel, isActive);
    }

    public void ToggleResultPanel()
    {
        resultUI.SetTrigger("GameOver");
    }
    //public void setCurrentScore()
    //{
    //    score.text = "현재 점수 " + GameManager.Instance.score.ToString();
    //}
    //public void setResultHighScore()
    //{
    //    resultHighScore.text = "최고 점수 " + GameManager.Instance.highScore.ToString();
    //}
    //public void setCount(int n)
    //{
    //    count.text = "탕후루\n" + n + " / 10";
    //}

    public void UpdateMainSceneMenuDisplay()
    {
        highscore.text = "최고점수 " + GameManager.Instance.highScore.ToString();
        score.text = "현재점수 " + GameManager.Instance.score.ToString();
        countTanghulu.text = "탕후루\n" + GameManager.Instance.tanghuluMade.ToString() + " / 10";
    }
}
