using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomConnector : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                cam.GetComponent<CameraController>().MoveToNewRoom(nextRoom);
            }
            else
            {
                cam.GetComponent<CameraController>().MoveToNewRoom(previousRoom);
            }
        }
    }
}
