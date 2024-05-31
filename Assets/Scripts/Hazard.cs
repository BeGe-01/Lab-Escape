using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour

{
    public GameObject startPoint;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerAnimation anim = Player.GetComponentInChildren<PlayerAnimation>();
            anim.Death();
            StartCoroutine(RespawnDelay(0.1f));
        }
    }

    private IEnumerator RespawnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Player.transform.position = startPoint.transform.position;
    }
}
