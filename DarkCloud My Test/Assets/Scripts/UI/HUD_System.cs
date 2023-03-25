using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD_System : MonoBehaviour
{
    public static HUD_System singleton;

    void Awake() => singleton = this;    

    [Header("Narrator Text")]
    public TMP_Text text_gameStates;

    [Header("Round's Text")]
    public TMP_Text text_rounds;

    [Header("Change Level Panel")]
    public GameObject change_LevelPanel;

    [Header("End Panel Settings")]
    public GameObject endPanel;
    public TMP_Text textPlayTime, textEnemiesDefeated, textDataRounds;
}
