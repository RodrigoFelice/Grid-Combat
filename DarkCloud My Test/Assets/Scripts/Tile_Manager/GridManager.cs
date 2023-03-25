using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Resolution Screen/Height")]
    public int width, height;
    Transform playerTile, enemyTile;

    [Header("Scenario Settings")]
    [SerializeField] CreateThings CreateThingsReference;
    int index;
    Data_Tiles ScenarioChoosen;
    
    [Header("All Scenario Tiles")]
    List<GameObject> allGameTiles = new List<GameObject>();


    void Start()
    {
        raffle_RandomScenario();
        Generate_Tiles();
    }

    void raffle_RandomScenario()
    {
        //raffle one of the possible scenarios
        index = Random.Range(0,Stored_Information.singleton.allScenario_DataTiles.Count);
        ScenarioChoosen = Stored_Information.singleton.allScenario_DataTiles[index];
        Stored_Information.singleton.allScenario_DataTiles.Remove(ScenarioChoosen);

        //obstacles!
        GetComponent<CreateThings>().obstaclePrefab = Stored_Information.singleton.allScenario_DataObstacles[index];
        Stored_Information.singleton.allScenario_DataObstacles.Remove(GetComponent<CreateThings>().obstaclePrefab);
    }

   void Generate_Tiles()
   {
        for (int collumn = 0; collumn < width; collumn++)
        {
            for (int row = 0; row < height; row++)
            {
                //CREATE TILES
                var spawnedTitle = Instantiate(CreateThingsReference.tilePrefab, new Vector3(collumn,row),Quaternion.identity, transform);
                spawnedTitle.name = $"Tile {collumn} {row}";

                var offset = (collumn + row) % 2 == 1;

                spawnedTitle.dataTile_choosen = ScenarioChoosen;
                spawnedTitle.Init(offset);

                //PLAYER CREATE
                if(collumn == 2 && row == 4)
                {
                    playerTile = spawnedTitle.transform;
                    Create_The_Player();
                    continue;
                }
                //ENEMIE CREATE
                if(collumn == 13 && row == 4)
                {
                    enemyTile = spawnedTitle.transform;
                    Create_The_Enemie();
                    continue;
                }
                
                //OBSTACLE CREATION
                allGameTiles.Add(spawnedTitle.gameObject);
                Create_Obstacle_Or_Not();
            }
        } 
   }

   void Create_Obstacle_Or_Not()
   {
     int canOrNot = Random.Range(0,13);

     if (canOrNot < 2)
     {
        int tileChoosen = Random.Range(0,allGameTiles.Count - 1);
        allGameTiles[tileChoosen].GetComponent<BoxCollider2D>().enabled = false;
        Instantiate(CreateThingsReference.obstaclePrefab, new Vector3(allGameTiles[tileChoosen].transform.position.x,allGameTiles[tileChoosen].transform.position.y),Quaternion.identity,transform);
        allGameTiles.Remove(allGameTiles[tileChoosen]);
     }
   }

   void Create_The_Player()
   {
        Instantiate(CreateThingsReference.player, new Vector3(playerTile.position.x, playerTile.position.y + 0.5f),Quaternion.identity, playerTile);
        playerTile.GetComponent<BoxCollider2D>().enabled = false;
   }

    void Create_The_Enemie()
   {
        int randomEnemie = Random.Range(0,Stored_Information.singleton.allPossible_Enemies.Count);
        GetComponent<CreateThings>().enemie = Stored_Information.singleton.allPossible_Enemies[randomEnemie];
        Stored_Information.singleton.allPossible_Enemies.Remove(GetComponent<CreateThings>().enemie);
        
        Instantiate(CreateThingsReference.enemie, new Vector3(enemyTile.position.x, enemyTile.position.y + 0.5f),Quaternion.identity, enemyTile);
        enemyTile.GetComponent<BoxCollider2D>().enabled = false;
   }

}
