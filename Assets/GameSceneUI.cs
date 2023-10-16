using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{
    [SerializeField] NotifyUseCard notifyUseCard;
    [SerializeField] TS_script tS_Script;
    public UIText GameStateUIText, SendUI;
    public PlayerStatetip playerTooltip { get; private set; }
    public Finger_Guessing FInger_Guessing { get; private set; }
    public BattleUi BattleUI { get; private set; }

    private void Awake()
    {
        FInger_Guessing = FindFirstObjectByType<Finger_Guessing>();
        BattleUI = FindFirstObjectByType<BattleUi>();
        playerTooltip = GetComponentInChildren<PlayerStatetip>();
    }
}
