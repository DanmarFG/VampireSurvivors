using GMStates;
using Managers;
using States;
using UnityEngine;
using UnityEngine.SceneManagement;

public class STLoadScene : IState
{
    int sceneToLoad;

    AsyncOperation loadingOperation;

    public STLoadScene(int scene)
    {
        sceneToLoad = scene;
    }

    public void OnEnter()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
    }

    public void OnExit()
    {
        
    }

    public void UpdateState()
    {
        if(loadingOperation.isDone)
        {
            switch (sceneToLoad)
            {
                case 0:
                    break;
                case 1:
                    GameManager.Instance.ChangeState(new STMainMenu());
                    break;
                case 2:
                    GameManager.Instance.ChangeState(new STGamePlay());
                    break;
            }
        }
    }
}
