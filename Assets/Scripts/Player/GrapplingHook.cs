using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [HideInInspector] public LineRenderer line;
    private Movement move;

    [SerializeField] LayerMask grappableMask;
    [SerializeField] float maxDistance = 10;
    [SerializeField] float grappleSpeed = 40;
    [SerializeField] float grappleShootSpeed = 40;
    [SerializeField] float grappleStopDistance = 1f;


    public bool isGrappling = false;
    public bool retracting = false;

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


        if (Input.GetButtonDown("Fire1") && !isGrappling && move.canMove)
        {
            StartGrapple();
        }

        if (retracting && isGrappling)
        {
            Vector2 grapplePos = Vector2.MoveTowards(transform.position, target, grappleSpeed * Time.deltaTime);

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
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);

        Vector2 newPos;
        while (line.GetPosition(1).x != target.x && isGrappling)
        {
            newPos = Vector2.MoveTowards(line.GetPosition(1), target, grappleShootSpeed * Time.deltaTime);
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
