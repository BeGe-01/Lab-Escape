using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Movement move;
    private Collision coll;
    public ParticleSystem deathParticle;
    [HideInInspector]
    public SpriteRenderer sr;

    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponentInParent<Collision>();
        move = GetComponentInParent<Movement>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        anim.SetBool("onGround", coll.onGround);
        anim.SetBool("onWall", coll.onWall);
        anim.SetBool("onRightWall", coll.onRightWall);
        anim.SetBool("wallSlide", move.wallSlide);
        anim.SetBool("canMove", move.canMove);

    }

    public void SetHorizontalMovement(float x, float y, float yVel)
    {
        anim.SetFloat("HorizontalAxis", x);
        anim.SetFloat("VerticalAxis", y);
        anim.SetFloat("VerticalVelocity", yVel);
    }

    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void Flip(int side)
    {

        if (move.wallSlide)
        {
            if (side == -1 && sr.flipX)
                return;

            if (side == 1 && !sr.flipX)
            {
                return;
            }
        }

        bool state = (side == 1) ? false : true;
        sr.flipX = state;
    }

    public void Death()
    {
        deathParticle.transform.position = sr.transform.position;
        move.StopVelocity();
        deathParticle.Play();
    }

    public IEnumerator FadeIn()
    {
        move.StopVelocity();
        Color c = sr.color;
        for (float alpha = 0; alpha <= 1; alpha += 0.1f)
        {
            c.a = alpha;
            sr.color = c;
            yield return new WaitForSeconds(.05f);
        }
    }

    public IEnumerator FadeOut()
    {
        move.StopVelocity();
        Color c = sr.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            sr.color = c;
            yield return new WaitForSeconds(.05f);
        }
    }
}
