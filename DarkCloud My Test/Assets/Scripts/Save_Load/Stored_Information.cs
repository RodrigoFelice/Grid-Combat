using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stored_Information : MonoBehaviour
{
    public static Stored_Information singleton;

    [Header("Save/Load Reference")]
    private SaveLoad_System SaveLoadSystem;
    
    [Header("Possible Scenario Tiles")]
    [HideInInspector] public Data_Skins saved_skin;
    public List<Data_Tiles> allScenario_DataTiles = new List<Data_Tiles>();

    [Header("Obstacle Tiles")]
    public List<GameObject> allScenario_DataObstacles = new List<GameObject>();

    [Header("All Enemies Models")]
    public List<GameObject> allPossible_Enemies = new List<GameObject>();

    [Header("Saved Informations")]
    [HideInInspector] public float timer;
    [HideInInspector] public int enemiesDefeated, rounds;


    void Awake()
    {
        SaveLoadSystem = GetComponent<SaveLoad_System>();

        if(singleton == null) singleton = this;

        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);  
    } 


    void Start() => SaveLoadSystem.SavePlayer(0f,0,0, Player.singleton.GetComponent<HealthSystem>().life);

    void Update() => timer += Time.deltaTime;

    public void Call_SaveFunction() => SaveLoadSystem.SavePlayer(timer, enemiesDefeated, rounds, Player.singleton.GetComponent<HealthSystem>().life);
    
    
    public void Increase_Enemies_Rounds()
    {
        rounds++;
        enemiesDefeated++;
        Call_SaveFunction();
    }

   
    public void Call_LoadFunction()
    {
        SaveLoadSystem.LoadPlayer();

        //Update all texts from end panel
        HUD_System.singleton.textEnemiesDefeated.text = "Enemies Defeated: " + enemiesDefeated;
        HUD_System.singleton.text_rounds.text = "Rounds: " + rounds;
        HUD_System.singleton.textDataRounds.text = "Rounds: " + rounds;

        // Convert float timer to hour, minutes and seconds.
        int hours = Mathf.FloorToInt(timer / 3600);
        int minutes = Mathf.FloorToInt((timer % 3600) / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        HUD_System.singleton.textPlayTime.text = "Play Time: " + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");      
    }    

}
