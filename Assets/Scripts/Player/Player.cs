using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public HealthBase healthBase;

    [Header("Setup Player")]
    public SOPlayerSetup sOPlayerSetup;
    public Animator animator;
    private Animator _currentPlayer;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += PlayerOnKill;
        }
        _currentPlayer = animator;
    }

    private void PlayerOnKill()
    {
        healthBase.OnKill -= PlayerOnKill;
        _currentPlayer.SetTrigger(sOPlayerSetup.deathTrigger);
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
            Input.GetKey(KeyCode.RightShift) ? sOPlayerSetup.runSpeed : sOPlayerSetup.speed, myRigidBody.velocity.y);

            if (myRigidBody.transform.localScale.x != 1)
            {
                DOTween.Kill(myRigidBody.transform);
                myRigidBody.transform.DOScaleX(1, sOPlayerSetup.duration);
            }
            _currentPlayer.SetBool(sOPlayerSetup.boolRun, true);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            myRigidBody.velocity = new Vector2(Input.GetKey(KeyCode.LeftShift) ||
            Input.GetKey(KeyCode.RightShift) ? -sOPlayerSetup.runSpeed : -sOPlayerSetup.speed, myRigidBody.velocity.y);

            if (myRigidBody.transform.localScale.x != -1)
            {
                DOTween.Kill(myRigidBody.transform);
                myRigidBody.transform.DOScaleX(-1, sOPlayerSetup.duration);
            }
            _currentPlayer.SetBool(sOPlayerSetup.boolRun, true);

        }
        else
        {
            _currentPlayer.SetBool(sOPlayerSetup.boolRun, false);
        }

        if (myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity += sOPlayerSetup.friction;
        }
        else if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity -= sOPlayerSetup.friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.velocity = Vector2.up * sOPlayerSetup.forceJump;
            myRigidBody.transform.localScale = Vector2.one;

            DOTween.Kill(myRigidBody.transform);

            HandScaleJump();
        }
    }

    private void HandScaleJump()
    {
        myRigidBody.transform.DOScaleY(sOPlayerSetup.jumpScaleY, sOPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(sOPlayerSetup.ease);
        myRigidBody.transform.DOScaleX(sOPlayerSetup.jumpScaleX, sOPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(sOPlayerSetup.ease);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
