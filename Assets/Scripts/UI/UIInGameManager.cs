using System.Collections;
using System.Collections.Generic;
using Core.Singleton;
using TMPro;
using UnityEngine;

public class UIInGameManager : Singleton<UIInGameManager>
{
    public TextMeshProUGUI uiTextCoins;

    public static void UpdateTextCoins(string coins)
    {
        Instance.uiTextCoins.text = coins;
    }
}
