using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkins : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] all_WarriorParts;

    [Space(50)]

    [SerializeField] Data_Skins[] warrior_Skins; 

    void Start()
    {
        //if player does not have any skin saved
        if(Stored_Information.singleton.saved_skin == null)
        {
            Random_Warrior_SkinData();
            return;
        }

        //otherwise the player already had a previous skin saved 
        Change_Warrior_Skins(Stored_Information.singleton.saved_skin);
    }

    void Random_Warrior_SkinData()
    {
        int randomSkin = Random.Range(0, warrior_Skins.Length);
        Change_Warrior_Skins(warrior_Skins[randomSkin]);
    }

    void Change_Warrior_Skins(Data_Skins dataChoosen)
    {
        Stored_Information.singleton.saved_skin = dataChoosen;

        //change every skin part 
        for (int i = 0; i < dataChoosen.skin_parts.Length; i++)
        {
            all_WarriorParts[i].sprite = dataChoosen.skin_parts[i];
        }
    }
}
