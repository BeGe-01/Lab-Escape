using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomConnector : MonoBehaviour
{
    public enum ConnectorType
    {
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop,
    }
    [SerializeField] private ConnectorType type;
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
        if (collision.CompareTag("Player"))
        {
            switch (type)
            {
                case ConnectorType.LeftToRight:
                    {
                        if (collision.transform.position.x < transform.position.x)
                        {
                            cam.GetComponent<CameraController>().MoveToNewRoom(nextRoom);
                        }
                        else
                        {
                            cam.GetComponent<CameraController>().MoveToNewRoom(previousRoom);
                        }
                        break;
                    }
                case ConnectorType.RightToLeft:
                    {
                        if (collision.transform.position.x > transform.position.x)
                        {
                            cam.GetComponent<CameraController>().MoveToNewRoom(nextRoom);
                        }
                        else
                        {
                            cam.GetComponent<CameraController>().MoveToNewRoom(previousRoom);
                        }
                        break;
                    }
                case ConnectorType.TopToBottom:
                    {
                        if (collision.transform.position.y > transform.position.y)
                        {
                            cam.GetComponent<CameraController>().MoveToNewRoom(nextRoom);
                        }
                        else
                        {
                            cam.GetComponent<CameraController>().MoveToNewRoom(previousRoom);
                        }
                        break;
                    }
                case ConnectorType.BottomToTop:
                    {
                        if (collision.transform.position.y < transform.position.y)
                        {
                            cam.GetComponent<CameraController>().MoveToNewRoom(nextRoom);
                        }
                        else
                        {
                            cam.GetComponent<CameraController>().MoveToNewRoom(previousRoom);
                        }
                        break;
                    }
            }
        }
    }
}
