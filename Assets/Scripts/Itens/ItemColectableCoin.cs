using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColectableCoin : ItemColectableBase
{
    // Método sobrescrito da classe base para definir ações específicas ao coletar uma moeda
    protected override void OnCollect()
    {
        // Chama o método OnCollect da classe base para executar ações gerais de coleta
        base.OnCollect();
        // Adiciona uma unidade à quantidade de moedas gerenciada pelo ItemManager
        ItemManager.Instance.AddAmount();
    }
}
