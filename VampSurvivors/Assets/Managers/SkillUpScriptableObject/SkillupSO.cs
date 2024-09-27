using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillUp", menuName = "SkillUpSO")]

public class SkillupSO : ScriptableObject
{
    [SerializeField]
    PlayerStats stats;

    public string flairText;

    public Sprite picToDisplay;

    public void AddStats()
    {
        UnitManager.Instance.player.GetComponent<Player>().AddStats(stats);
    }
}
