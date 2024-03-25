using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FlashColor : MonoBehaviour
{
    // Lista de renderizadores de sprites que serão afetados pela animação de flash
    public List<SpriteRenderer> spriteRenderers;
    // Cor para flash
    public Color color = Color.red;
    // Duração da animação de flash
    public float duration = 0.1f;

    // Referência para a Tween atual em execução
    private Tween _currentTween;

    // Método chamado na Unity Editor quando um valor da classe é alterado
    private void OnValidate()
    {
        // Inicializa a lista de renderizadores de sprites
        spriteRenderers = new List<SpriteRenderer>();
        // Adiciona todos os renderizadores de sprites filhos ao lista
        foreach (var children in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderers.Add(children);
        }
    }

    // Método para iniciar a animação de flash
    public void Flash()
    {
        // Verifica se já há uma animação de flash em execução
        if (_currentTween != null)
        {
            // Cancela a animação atual
            _currentTween.Kill();
            // Define a cor de todos os sprites de volta para branca
            spriteRenderers.ForEach(i => i.color = Color.white);
        }

        // Inicia a animação de flash para cada sprite na lista
        foreach (var s in spriteRenderers)
        {
            // Cria uma nova animação de flash usando DOTween para o sprite atual
            _currentTween = s.DOColor(color, duration).SetLoops(2, LoopType.Yoyo);
        }
    }
}
