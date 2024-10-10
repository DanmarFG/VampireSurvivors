using Managers;
using States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GMStates
{
    public class STGamePlay : IState
    {
        public void OnEnter()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));

            GameObject.Find("DancingCat").SetActive(PlayerPrefs.GetInt("Gif") == 1);

            EventManager.Instance.OnMinute += UpdateGameStuff;

            EventManager.Instance.GamePlay();

            GameManager.Instance.StartTimer();
        }

        public void UpdateState()
        {
            
        }

        void UpdateGameStuff()
        {
            GameManager.Instance.timeFactor = 0.0506f * GameManager.Instance.currentDifficulty;
            GameManager.Instance.stageFactor = Mathf.Pow(1.15f, GameManager.Instance.stagesCompleted);
            GameManager.Instance.coef = (GameManager.Instance.playerFactor + GameManager.Instance.IngameTimer.minutes * GameManager.Instance.timeFactor) * GameManager.Instance.stageFactor;
        }

        public void OnExit()
        {
            EventManager.Instance.OnMinute -= UpdateGameStuff;
            GameManager.Instance.StopTimer();
        }
    }
}
