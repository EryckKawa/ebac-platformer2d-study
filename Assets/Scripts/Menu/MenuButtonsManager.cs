using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MenuButtonsManager : MonoBehaviour
{
    public List<GameObject> buttons;

    [Header("Animation")]
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private float delayTime = 0.5f;
    [SerializeField] private Ease ease = Ease.OutBack;

    private void ShowButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            GameObject button = buttons[i];
            button.SetActive(true);
            button.transform.DOScale(1, duration).SetDelay(i * delayTime).SetEase(ease);
        }
    }

    private void OnEnable()
    {
        HideButtons();
        ShowButtons();
    }

    private void HideButtons()
    {
        foreach (GameObject b in buttons)
        {
            b.transform.localScale = Vector3.zero;
            b.SetActive(false);
        }
    }
}
