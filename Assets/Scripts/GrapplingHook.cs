using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    LineRenderer line;
    private Movement move;

    [SerializeField] LayerMask grappableMask;
    [SerializeField] float maxDistance = 15f;
    [SerializeField] float grappleSpeed = 10f;
    [SerializeField] float grappleShootSpeed = 20f;
    [SerializeField] float grappleStopDistance = 2f;


    bool isGrappling = false;
    [HideInInspector] public bool retracting = false;

    Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        move = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.DrawRay(transform.position, (Vector2.right * new Vector2(move.side, transform.position.y)) * maxDistance, Color.green);


        if (Input.GetButtonDown("Fire1") && !isGrappling)
        {
            StartGrapple();
        }

        if (retracting)
        {
            Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);

            transform.position = grapplePos;

            line.SetPosition(0, transform.position);

            if (Vector2.Distance(transform.position, target) < grappleStopDistance)
            {
                retracting = false;
                isGrappling = false;
                move.isGrappling = false;
                line.enabled = false;
            }
        }
    }

    private void StartGrapple()
    {
        int grappleSide = move.side;
        if (move.wallSlide)
        {
            grappleSide *= -1;
        }

        Vector2 direction = Vector2.right * new Vector2(grappleSide, transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grappableMask);
        target = new Vector2(transform.position.x + (maxDistance * grappleSide), transform.position.y);
        Debug.Log(target);

        if (hit.collider != null)
        {
            target = hit.point;

        }
        move.isGrappling = true;
        isGrappling = true;
        line.enabled = true;
        line.positionCount = 2;

        StartCoroutine(Grapple(hit));
    }

    IEnumerator Grapple(RaycastHit2D hit)
    {
        move.canMove = false;
        float t = 0;
        float time = 5;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);

        Vector2 newPos;
        for (; t < time; t += grappleShootSpeed * Time.deltaTime)
        {
            newPos = Vector2.Lerp(transform.position, target, t / time);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, newPos);
            yield return null;
        }
        line.SetPosition(1, target);
        if (hit.collider != null)
        {
            retracting = true;
        }
        else
        {
            retracting = false;
            isGrappling = false;
            move.isGrappling = false;
            line.enabled = false;
        }
        move.canMove = true;
    }
}
