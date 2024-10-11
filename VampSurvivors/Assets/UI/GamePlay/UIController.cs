using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Button tryAgainButton, resumeButton;
    [SerializeField] Button[] quitButtons;
    [SerializeField] GameObject deathScreen, pauseScreen;
    [SerializeField] TMP_Text TimerText;



    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.OnPauseGame += PauseScreen;
        EventManager.Instance.OnPlayerDied += OpenDeathScreen;
        tryAgainButton.onClick.AddListener(() => GameManager.Instance.ResetGame());
        tryAgainButton.onClick.AddListener(() => GameManager.Instance.ChangeState(new STLoadScene(2)));
        tryAgainButton.onClick.AddListener(RemoveAllListeners);

        resumeButton.onClick.AddListener(ResumeGame);
        
        for(int i = 0; i < quitButtons.Length; i++)
        {
            quitButtons[i].onClick.AddListener(() => GameManager.Instance.ChangeState(new STLoadScene(1)));
            quitButtons[i].onClick.AddListener(RemoveAllListeners);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IngameTimer.minutes > 0)
            TimerText.text = $"{GameManager.Instance.IngameTimer.minutes}:{GameManager.Instance.IngameTimer.seconds}";
        else if (GameManager.Instance.IngameTimer.hours > 0)
            TimerText.text = $"{GameManager.Instance.IngameTimer.hours}:{GameManager.Instance.IngameTimer.minutes}:{GameManager.Instance.IngameTimer.seconds}";
        else
            TimerText.text = $"{GameManager.Instance.IngameTimer.seconds}";

    }

    void RemoveAllListeners()
    {
        for (int i = 0; i < quitButtons.Length; i++)
            quitButtons[i].onClick.RemoveAllListeners();

        tryAgainButton.onClick.RemoveAllListeners();
        resumeButton.onClick.RemoveAllListeners();

        EventManager.Instance.OnPauseGame -= PauseScreen;
    }

    public void OpenDeathScreen()
    {
        EventManager.Instance.OnPlayerDied -= OpenDeathScreen;
        deathScreen.SetActive(true);
    }

    void PauseScreen()
    {
        pauseScreen.SetActive(!pauseScreen.activeSelf);
    }
    void ResumeGame()
    {
        EventManager.Instance.PauseGame();
    }
}
