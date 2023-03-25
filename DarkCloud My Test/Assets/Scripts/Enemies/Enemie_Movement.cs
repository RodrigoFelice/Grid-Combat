using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie_Movement : MonoBehaviour
{
    [Header("Target Settings")]
    Transform target; 

    [Header("Enemies Detections")]
    public GameObject EnemieRadar;
    Enemies_Detection enemie_Detection;

    [Header("Distance Settings")]
    Vector2 playerPosition, tilePosition;
    float distance;

    [Header("All possible distances")]
    List<float> allPossibleDistances = new List<float>();

    void Start()
    {
        enemie_Detection = EnemieRadar.GetComponent<Enemies_Detection>();
        target = Player.singleton.transform;
    }

    public void Start_Enemie_Movement()
    {
        allPossibleDistances.Clear();
        distance = 0f;
        playerPosition = target.transform.position;

        Get_All_Tiles_Distance();
    }


    //Store all Tiles Around that were acessible from Detection´s collider.
    public void Get_All_Tiles_Distance()
    {
        //Get all of possible Tiles around, and calculate the distance between every Tile from the player
        for (int i = 0; i < enemie_Detection.colisores.Count; i++)
        {
            tilePosition = enemie_Detection.colisores[i].transform.position;
            distance = Vector2.Distance(playerPosition, tilePosition);

            allPossibleDistances.Add(distance); 
        }

        Choose_The_Best_Path();
    }

    void Choose_The_Best_Path()
    {
        int index = 0; 

        //Loop through every possible distance
        for (int i = 0; i < allPossibleDistances.Count; i++)
        {
            //choose the shortest distance between player and enemie of all options
            if (allPossibleDistances[i] < distance)
            {
                distance = allPossibleDistances[i];
                index = i;
            }
        }
        StartCoroutine(MoveTheEnemie(index));
    }

    IEnumerator MoveTheEnemie(int tileChoosen)
    {
        yield return new WaitForSeconds(1f);

        //Enable the previous Tile Collider
        Transform boxParentCollider = transform.parent;
        boxParentCollider.GetComponent<BoxCollider2D>().enabled = true;

        //Change the enemie position to next Tile, and make him a child of the object
        transform.SetParent(enemie_Detection.colisores[tileChoosen].transform);
        transform.position = new Vector3(transform.parent.position.x,transform.parent.position.y + 0.5f, transform.parent.position.z);

        //Disable the current Tile Collider, so it´s not acessible to be choosen by mouse or player
        enemie_Detection.colisores[tileChoosen].GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine(Reset_Radar());
    }

    IEnumerator Reset_Radar()
    {
        EnemieRadar.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        EnemieRadar.SetActive(true);
        GetComponent<Enemie_Behaviour>().EndTurn();
    }
}
