using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColectableBase : MonoBehaviour
{
    // Tag que o objeto deve comparar para determinar se foi coletado pelo player
    public string tagToCompare = "Player";

    // Método chamado quando outro objeto com colisor entra em contato com este objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que entrou em contato tem a tag especificada
        if (collision.transform.CompareTag(tagToCompare))
        {
            // Se tiver a tag correta, chama o método Collect()
            Collect();
        }
    }

    // Método virtual que define a ação de coletar o item
    protected virtual void Collect()
    {
        // Chama o método OnCollect() para realizar ações específicas de coleta
        OnCollect();
    }

    // Método virtual que define ações específicas de coleta
    protected virtual void OnCollect()
    {
        // Destroi o objeto após ser coletado
        Destroy(gameObject);
        // Exibe uma mensagem de log indicando que o objeto foi destruído após a coleta
        Debug.Log("Destruiuu");
    }
}
