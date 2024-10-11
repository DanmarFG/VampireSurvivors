using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillUp", menuName = "SkillUpSO")]

public class SkillupSO : ScriptableObject
{
    [SerializeField]
    PlayerStats stats;

    public string flairText;

    public Sprite picToDisplay;

    public bool addFireball = false;

    public void AddStats()
    {
        if(addFireball)
            UnitManager.Instance.player.StartFireball();
        UnitManager.Instance.player.GetComponent<Player>().AddStats(stats);

    }
}
