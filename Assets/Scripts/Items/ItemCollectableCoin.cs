using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemCollectableCoin : ItemCollectableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddAmount();

        // Define a escala final da moeda (pode ser ajustada conforme necessário)
        Vector3 finalScale = transform.localScale * 1.5f;

        // Cria uma animação de escala utilizando o DOTween
        transform.DOScale(finalScale, 0.3f) // Escala para a escala final em 0.3 segundos
            .SetEase(Ease.OutBack) // Aplica uma interpolação de retorno (bounce)
            .OnComplete(() => // Define uma ação a ser executada quando a animação estiver completa
            {
                // Destroi o objeto após a animação de escala
                Destroy(gameObject);
            });
    }
}
