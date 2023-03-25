using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TurnSystem : MonoBehaviour
{
    public static TurnSystem singleton;

    void Awake() => singleton = this;    

    public enum GameStates{PlayerTurn, EnemiesTurn, Player_Won, Enemie_Won, GameFinished}
    
    [Header("Turn States")]
    public GameStates gameStates;

    [Header("Enemie References")]
    [HideInInspector] public GameObject enemie;

    [Header("Turn Delay Time")]
    public float delayTime = 1f;

    void Start() 
    {
        Stored_Information.singleton.Call_LoadFunction();
        Convert_String_To_GameState("PlayerTurn");
    } 
        

    public void Convert_String_To_GameState(string stringState)
    {
        GameStates.TryParse(stringState, out gameStates);
        StartCoroutine(Change_Game_State(gameStates));
    }


    IEnumerator Change_Game_State(GameStates newState)
    {
        Player.singleton.Enable_Disable_SquareCollider(false);

        yield return new WaitForSeconds(delayTime);
        gameStates = newState;

        switch (gameStates)
        {
            case GameStates.PlayerTurn:
                Player.singleton.Player_Turn();
                HUD_System.singleton.text_gameStates.text = "Your Turn";
                break;

            case GameStates.EnemiesTurn:
                enemie.GetComponent<Enemie_Behaviour>().Enemies_Turn();
                HUD_System.singleton.text_gameStates.text = "Enemie's Turn";
                break;

            case GameStates.Player_Won:
                HUD_System.singleton.text_gameStates.text = "Enemie's Defeated!";
                Stored_Information.singleton.Increase_Enemies_Rounds();

                StartCoroutine(Can_Change_Level(2f));
                //StartCoroutine(Appear_LevelProgress_HUD(4f));
                break;
                
            case GameStates.Enemie_Won:
                HUD_System.singleton.text_gameStates.text = "You were defeated...";
                Stored_Information.singleton.Call_SaveFunction();
                Invoke("AppearFinalHud",2f);
                break;

            case GameStates.GameFinished:
                HUD_System.singleton.text_gameStates.text = "All Challenges Complete";
                Stored_Information.singleton.Call_SaveFunction();
                break;

            default:
                Debug.LogError("ERROR !!");
                break;
        }
    }

    IEnumerator Appear_LevelProgress_HUD(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        HUD_System.singleton.change_LevelPanel.SetActive(true);
    }

    public void Close_LevelProgress_HUD()
    {
        HUD_System.singleton.change_LevelPanel.SetActive(false);
        StartCoroutine(Can_Change_Level(1f));
    }

    IEnumerator Can_Change_Level(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
       
       //IF HAVE MORE LEVELS TO BE LOADED
        if( Stored_Information.singleton.allScenario_DataTiles.Count <= 0)
        {
            Convert_String_To_GameState("GameFinished");
            yield return new WaitForSeconds(3f);
            AppearFinalHud();
        }
        //OTHERWISE THE GAME FINISH´S
        else
        {
            HUD_System.singleton.text_gameStates.text = "Changing Level...";
            yield return new WaitForSeconds(3f);
            GetComponent<EventsSystem>().End_This_Level.Invoke();
        }
    }

     void AppearFinalHud() => HUD_System.singleton.endPanel.SetActive(true);
}
