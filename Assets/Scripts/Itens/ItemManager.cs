using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using TMPro;

//Cria o singleton ItemManager.
public class ItemManager : Singleton<ItemManager>
{
    // Variável para armazenar a quantidade de moedas
    public int coins;
    public TextMeshProUGUI uiTextCoins;

    // Start é chamado antes do primeiro frame
    private void Start()
    {
        // Chama o método Reset para inicializar as variáveis
        Reset();
    }

    // Método para resetar as variáveis
    private void Reset()
    {
        // Define a quantidade inicial de moedas como zero
        coins = 0;
        UpdateCoins();
    }

    // Método para adicionar uma quantidade específica de moedas
    public void AddAmount(int amount = 1)
    {
        // Adiciona a quantidade especificada à variável coins
        coins += amount;
        UpdateCoins();
    }
    void UpdateCoins()
    {
        UIInGameManager.UpdateTextCoins(coins.ToString());
    }
}
