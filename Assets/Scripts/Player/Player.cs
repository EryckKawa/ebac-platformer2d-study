using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public HealthBase healthBase;

    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(-.1f, 0);
    public float speed;
    public float runSpeed;
    public float forceJump;

    [Header("Animation Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;

    public float animationDuration = .3f;
    public Ease ease = Ease.OutBounce;

    [Header("Animation Player")]
    public string boolRun = "Run";
    public string deathTrigger = "Death";
    public Animator animator;
    public float duration = .5f;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += PlayerOnKill;
        }
    }

    private void PlayerOnKill()
    {
        healthBase.OnKill -= PlayerOnKill;
        animator.SetTrigger(deathTrigger);
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            myRigidBody.velocity = new Vector2(Input.GetKey(KeyCode.LeftShift) ||
            Input.GetKey(KeyCode.RightShift) ? runSpeed : speed, myRigidBody.velocity.y);

            if (myRigidBody.transform.localScale.x != 1)
            {
                DOTween.Kill(myRigidBody.transform);
                myRigidBody.transform.DOScaleX(1, duration);
            }
            animator.SetBool(boolRun, true);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            myRigidBody.velocity = new Vector2(Input.GetKey(KeyCode.LeftShift) ||
            Input.GetKey(KeyCode.RightShift) ? -runSpeed : -speed, myRigidBody.velocity.y);

            if (myRigidBody.transform.localScale.x != -1)
            {
                DOTween.Kill(myRigidBody.transform);
                myRigidBody.transform.DOScaleX(-1, duration);
            }
            animator.SetBool(boolRun, true);

        }
        else
        {
            animator.SetBool(boolRun, false);
        }

        if (myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity += friction;
        }
        else if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity -= friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.velocity = Vector2.up * forceJump;
            myRigidBody.transform.localScale = Vector2.one;

            DOTween.Kill(myRigidBody.transform);

            //HandScaleJump();
        }
    }

    private void HandScaleJump()
    {
        myRigidBody.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myRigidBody.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
