using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "ebac-platformer2d-study/SOPlayerSetup")]
public class SOPlayerSetup : ScriptableObject
{
    
    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(-.1f, 0);
    public float speed;
    public float runSpeed;
    public float forceJump;

    [Header("Animation Setup")]
    public float jumpScaleY;
    public float jumpScaleX;
    public float animationDuration;
    public Ease ease = Ease.OutBounce;


    [Header("Animation Player")]
    public string boolRun = "Run";
    public string deathTrigger = "Death";
    public float duration = .5f;
}

