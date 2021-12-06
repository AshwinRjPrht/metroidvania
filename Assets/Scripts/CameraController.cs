using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController player;
    public BoxCollider2D boundsBox;

    private float hfHeight, hfWidth;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        hfHeight = Camera.main.orthographicSize;
        hfWidth = hfHeight * Camera.main.aspect;
    }

    
    void Update()
    {
        if(player != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x, boundsBox.bounds.min.x + hfWidth, boundsBox.bounds.max.x - hfWidth),
                Mathf.Clamp(player.transform.position.y, boundsBox.bounds.min.y +hfHeight, boundsBox.bounds.max.y - hfHeight),
                transform.position.z);
        }
    }
}
