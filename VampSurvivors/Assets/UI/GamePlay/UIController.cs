using Managers;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Button tryAgainButton, resumeButton;
    [SerializeField] Button[] quitButtons;
    [SerializeField] GameObject deathScreen, pauseScreen;



    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.OnPauseGame += PauseScreen;
        tryAgainButton.onClick.AddListener(() => GameManager.Instance.ChangeState(new STLoadScene(2)));
        tryAgainButton.onClick.AddListener(RemoveAllListeners);

        resumeButton.onClick.AddListener(ResumeGame);
        
        for(int i = 0; i < quitButtons.Length; i++)
        {
            quitButtons[i].onClick.AddListener(() => GameManager.Instance.ChangeState(new STLoadScene(1)));
            quitButtons[i].onClick.AddListener(RemoveAllListeners);
        }
    }

    void RemoveAllListeners()
    {
        for (int i = 0; i < quitButtons.Length; i++)
            quitButtons[i].onClick.RemoveAllListeners();

        tryAgainButton.onClick.RemoveAllListeners();
        resumeButton.onClick.RemoveAllListeners();
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
