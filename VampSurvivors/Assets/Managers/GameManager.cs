using GMStates;
using States;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public bool gameIsPaused = false, isInLevelUp = false;
        
        [SerializeField]
        private StateController stateController;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        
        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            stateController.SetStartState(new STLoadScene(1));

            EventManager.Instance.OnPauseGame += PauseGame;

        }

        public void ChangeState(IState state)
        {
            stateController.ChangeState(state);
        }

        public void LevelUp()
        {
            stateController.ChangeState(new STLevelUp());
        }

        public void PauseGame()
        {
            if(Time.timeScale != 0)
                stateController.ChangeState(new STPauseGame());
            else
                stateController.ChangeState(new STGamePlay());
        }
    } 
}

