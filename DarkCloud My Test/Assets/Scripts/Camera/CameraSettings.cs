using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    Camera cam;
    [SerializeField] GridManager gridManager;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.transform.position = new Vector3((float)gridManager.width/2 - 0.5f, (float)gridManager.height/2 - 0.5f, -10);
    }

}
