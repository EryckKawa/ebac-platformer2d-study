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

    [Header("Jump Collision Setup")]
    public new Collider2D collider2D;
    public float distToGround;
    public float spaceToGround = 0.1f;
    public ParticleSystem jumpVFX;
    public bool canJump = true;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += PlayerOnKill;
        }
        _currentPlayer = animator;

        if (collider2D != null)
        {
            distToGround = collider2D.bounds.extents.y;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector2.one, distToGround + spaceToGround);
    }

    private void Update()
    {
        IsGrounded();
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? sOPlayerSetup.runSpeed : sOPlayerSetup.speed, myRigidBody.velocity.y);
            if (myRigidBody.transform.localScale.x != 1)
            {
                DOTween.Kill(myRigidBody.transform);
                myRigidBody.transform.DOScaleX(1, sOPlayerSetup.duration);
            }
            _currentPlayer.SetBool(sOPlayerSetup.boolRun, true);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidBody.velocity = new Vector2(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? -sOPlayerSetup.runSpeed : -sOPlayerSetup.speed, myRigidBody.velocity.y);
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
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            myRigidBody.velocity = Vector2.up * sOPlayerSetup.forceJump;
            PlayJumpVFX();
        }
    }

    private void PlayJumpVFX()
    {
        if (jumpVFX != null)
        {
            jumpVFX.Play();
        }
    }

    private void PlayerOnKill()
    {
        healthBase.OnKill -= PlayerOnKill;
        _currentPlayer.SetTrigger(sOPlayerSetup.deathTrigger);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
