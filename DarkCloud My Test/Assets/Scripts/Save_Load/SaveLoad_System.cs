using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SaveLoad_System : MonoBehaviour
{
   private string filePath;

    void Awake() => filePath = Application.persistentDataPath + "/Data.json";

    public void SavePlayer(float previousTime, int previousEnemiesDefeated, int previousRounds, int previousLife)
    {
        All_Data_Saved dataClass = new All_Data_Saved();
        dataClass.playTime = previousTime;
        dataClass.enemiesDefeated = previousEnemiesDefeated;
        dataClass.dataRounds = previousRounds;

        dataClass.playerLife = previousLife;

        // Converte a instância da classe PlayerData em uma string JSON
        string jsonString = JsonUtility.ToJson(dataClass);

        // Salva a string JSON em um arquivo
        File.WriteAllText(filePath, jsonString);

        GetComponent<Stored_Information>().Call_LoadFunction();
    }

    public void LoadPlayer()
    {
        // Verifica se o arquivo existe
        if (File.Exists(filePath))
        {
            // Carrega o arquivo JSON e converte a string JSON em uma instância da classe
            string jsonString = File.ReadAllText(filePath);
            All_Data_Saved dataClass = JsonUtility.FromJson<All_Data_Saved>(jsonString);

            Stored_Information stored_Information = GetComponent<Stored_Information>();

            // Atualiza o código "Stored_Information" com os dados carregados da classe 
            stored_Information.timer = dataClass.playTime;
            stored_Information.enemiesDefeated = dataClass.enemiesDefeated;
            stored_Information.rounds = dataClass.dataRounds;

            Player.singleton.GetComponent<HealthSystem>().life = dataClass.playerLife;
        }
        else  Debug.Log("Player data file not found at " + filePath);
    }
}
