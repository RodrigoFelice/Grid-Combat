using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Life Settings")]
    public int life = 3, maxLife;
    [SerializeField] Transform pivot; 

    [Header("Control_And_States Settings")]
    [SerializeField] string nextTurnState, endGameState;
    [HideInInspector] public bool canBeSelected;


    void Start()
    {
        UpdateLifeBar(life, maxLife);
    }

    public void UpdateLifeBar(float currentHealth, float maxHealth) 
    {
        pivot.transform.localScale = new Vector3(currentHealth/maxHealth,1f,transform.localScale.z);

        if(pivot.transform.localScale.x <= 0)  pivot.transform.localScale = new Vector3(0f ,1f,transform.localScale.z);
    } 

    //MOUSE CLICK
     void OnMouseDown()
    {
        if(canBeSelected) TakeDamage();
    }

   
    public void TakeDamage()
    {
        life--;
        canBeSelected = false;
        UpdateLifeBar(life, maxLife);

        if(life <= 0)
        {
            StartCoroutine(Die(1f));
        }

        TurnSystem.singleton.Convert_String_To_GameState(nextTurnState);    
    }
    

    IEnumerator Die(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TurnSystem.singleton.Convert_String_To_GameState(endGameState);    
        this.gameObject.SetActive(false);
    }

}
