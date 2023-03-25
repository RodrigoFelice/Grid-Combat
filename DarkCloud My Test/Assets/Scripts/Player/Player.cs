using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player singleton;

    void Awake() => singleton = this;    
    
    [Header("Tile Settings")]
    Tile previousTile;

    [Header("Dettection Reference")]
    [SerializeField] GameObject squareColliderDettection;


    public void Player_Turn()
    {
       Enable_Disable_SquareCollider(true);
    }

    public void Enable_Disable_SquareCollider(bool newBool_Value) => squareColliderDettection.SetActive(newBool_Value);

    
    public void ChangePlayerPosition(Tile newTilePosition)
    {
        Get_The_Previous_Tile();
        Enable_Disable_SquareCollider(false);

        StartCoroutine(Delay_Before_ChangePosition(1f, newTilePosition));
    }

    IEnumerator Delay_Before_ChangePosition(float delayTime, Tile newTilePosition)
    {
        yield return new WaitForSeconds(delayTime);
        previousTile = newTilePosition;

        newTilePosition.GetComponent<BoxCollider2D>().enabled = false;
        transform.SetParent(newTilePosition.transform);

        End_Turn();
    }

    void Get_The_Previous_Tile()
    {
        previousTile = GetComponentInParent<Tile>();
        previousTile.GetComponent<BoxCollider2D>().enabled = true;
    }

    void End_Turn()
    {
        TurnSystem.singleton.Convert_String_To_GameState("EnemiesTurn");
    }
    
}
