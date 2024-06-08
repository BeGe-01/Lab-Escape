using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private Transform startingRoomCamera;
    private float currentPosX;
    private float currentPosY;

    private Vector3 velocity = Vector3.zero;
    [Space]

    [Header("Camera Fade")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 2f;
    private bool fadeFinished = false;


    void Awake()
    {
        CameraFadeOut();
    }
    void Start()
    {
        if (startingRoomCamera != null)
        {
            currentPosX = startingRoomCamera.position.x;
            currentPosY = startingRoomCamera.position.y;
            transform.position = new Vector3(startingRoomCamera.position.x, startingRoomCamera.position.y, transform.position.z);
        }
        else
        {
            currentPosX = 0f;
            currentPosY = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, currentPosY, transform.position.z),
            ref velocity, speed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.transform.position.x;
        currentPosY = _newRoom.transform.position.y;
    }

    public void CameraFadeOut()
    {
        fadeFinished = false;
        StartCoroutine(FadeCanvas(canvasGroup, canvasGroup.alpha, 0, fadeDuration));
    }

    public void CameraFadeIn()
    {
        fadeFinished = false;
        StartCoroutine(FadeCanvas(canvasGroup, canvasGroup.alpha, 1, fadeDuration));
    }

    private IEnumerator FadeCanvas(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }
        cg.alpha = end;
        fadeFinished = true;
    }

    public IEnumerator WaitFade(Action callback)
    {
        while (!fadeFinished)
        {
            yield return null;
        }
        callback();
    }
}
