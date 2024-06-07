using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour

{
    public GameObject startPoint;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            player.GetComponentInChildren<Movement>().canMove = false;
            player.GetComponentInChildren<Movement>().isGrappling = false;
            player.GetComponentInChildren<GrapplingHook>().retracting = false;
            player.GetComponentInChildren<GrapplingHook>().isGrappling = false;
            player.GetComponentInChildren<GrapplingHook>().line.enabled = false;
            player.GetComponentInChildren<PlayerAnimation>().Death();
            player.GetComponentInChildren<Inventory>().ReturnItems();
            SaveManager.instance.Death();
            StartCoroutine(RespawnDelay(player, 0.1f));
        }
    }

    private IEnumerator RespawnDelay(GameObject player, float delay)
    {
        yield return new WaitForSeconds(delay);
        player.GetComponentInChildren<Movement>().canMove = true;
        player.transform.position = startPoint.transform.position;
    }

}
