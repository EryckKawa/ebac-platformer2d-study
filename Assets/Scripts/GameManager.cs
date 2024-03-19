using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using DG.Tweening;
public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    public GameObject player;

    [Header("Enemies")]
    public List<GameObject> enemies;

    [Header("References")]
    public Transform spawnPoint;
    private GameObject _currentPlayer;

    [Header("Animations")]
    public float duration = .2f;
    public float delayTime = 1f;
    public Ease ease = Ease.OutBack;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        _currentPlayer = player;
        _currentPlayer.transform.position = spawnPoint.transform.position;
        _currentPlayer.transform.DOScale(0, duration).SetEase(ease).From().SetDelay(delayTime);
    }
}
