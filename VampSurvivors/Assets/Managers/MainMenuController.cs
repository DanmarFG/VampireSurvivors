using Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [Header("AllTimeStats")]
    [SerializeField] TMP_Text killstext;
    [SerializeField] TMP_Text RunText, CoinsCollectedText;

    // Start is called before the first frame update
    void Start()
    {
        killstext.text = $"Kills\n{GameManager.Instance.alltimeStats.kills}";
        RunText.text = $"Current run\n{GameManager.Instance.alltimeStats.runs}";
        CoinsCollectedText.text = $"Coins collected\n{GameManager.Instance.alltimeStats.coinsCollected}";
    }
}
