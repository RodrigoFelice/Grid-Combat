using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class EventsSystem : MonoBehaviour
{
    [Header("Game Events")]
    public UnityEvent End_This_Level;


    public void ChangeScene(string nameScene) => SceneManager.LoadScene(nameScene);

}
