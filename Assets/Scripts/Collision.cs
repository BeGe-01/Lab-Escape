using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    [Space]

    [Header("Collision")]

    public Vector2 groundCollisionSize;
    public Vector2 wallCollisionSize;
    public Vector2 bottomOffset, rightOffset, leftOffset;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, groundCollisionSize, 0, groundLayer);
        onWall = Physics2D.OverlapBox((Vector2)transform.position + rightOffset, wallCollisionSize, 0, groundLayer)
            || Physics2D.OverlapBox((Vector2)transform.position + leftOffset, wallCollisionSize, 0, groundLayer);


        onRightWall = Physics2D.OverlapBox((Vector2)transform.position + rightOffset, wallCollisionSize, 0, groundLayer);
        onLeftWall = Physics2D.OverlapBox((Vector2)transform.position + leftOffset, wallCollisionSize, 0, groundLayer);


        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, groundCollisionSize);
        Gizmos.DrawWireCube((Vector2)transform.position + rightOffset, wallCollisionSize);
        Gizmos.DrawWireCube((Vector2)transform.position + leftOffset, wallCollisionSize);
    }
}