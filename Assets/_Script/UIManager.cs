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

    public Text titleHighScore;

    public GameObject resultTextObject;
    public GameObject highScoreTextObject;
    public Text resultHighScore;
    public Text resultScore;

    private Animator resultUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
            titleHighScore = GameObject.Find("HighScore").GetComponent<Text>();
            settingsPanel = GameObject.Find("Canvas").transform.Find("SettingUI").gameObject;
        }
        else if (scene.name == "_MainScene")
        {
            GameObject canvasObject = GameObject.Find("Canvas");
            pausedPanel = canvasObject.transform.Find("PauseUI").gameObject;
            settingsPanel = canvasObject.transform.Find("PauseUI").transform.Find("SettingUI").gameObject;


            resultUI = canvasObject.transform.Find("ResultUI").gameObject.GetComponent<Animator>();
            resultTextObject = canvasObject.transform.Find("ResultUI").transform.Find("ResultText").gameObject;
            highScoreTextObject = canvasObject.transform.Find("ResultUI").transform.Find("ResultTextHighScore").gameObject;
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
}
