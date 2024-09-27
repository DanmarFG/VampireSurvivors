using Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillupController : MonoBehaviour
{
    public List<SkillupSO> skillsList;
    public List<GameObject> Tables;

    private void OnEnable()
    {
        AddSkillsToTable();
    }

    public void AddSkillsToTable()
    {
        for (int i = 0; i <= 2; i++)
        {
            int n = i;

            Tables[i].transform.GetChild(0).GetComponent<Image>().sprite = skillsList[n].picToDisplay;
            Tables[i].transform.GetChild(1).GetComponent<TMP_Text>().text = skillsList[n].flairText;
            Tables[i].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => skillsList[n].AddStats());
            Tables[i].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.StartGame());
            Tables[i].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => RemoveListenerOnCLick(n,n));
            
        }
    }

    void RemoveListenerOnCLick(int i, int n)
    {
        Tables[i].transform.GetChild(2).GetComponent<Button>().onClick.RemoveListener(() => skillsList[n].AddStats());
    }
}
