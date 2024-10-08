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

            EventManager.Instance.GamePlay();
        }

        public void UpdateState()
        {
        }

        public void OnExit()
        {
        }
    }
}
