using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColectableBase : MonoBehaviour
{
    // Tag que o objeto deve comparar para determinar se foi coletado pelo player
    public string tagToCompare = "Player";
    public new ParticleSystem particleSystem;
    public GameObject graphicItem;  
    public float timeToHide;

    [Header("Sounds")]
    public AudioSource audioSource;

    void Awake()
    {
        if (particleSystem != null)
        {
            particleSystem.transform.SetParent(null);
        }
    }
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
        if (graphicItem != null)
        {
            graphicItem.SetActive(false);
            Invoke("HideObject", timeToHide);
            OnCollect();
        }
    }

    protected virtual void OnCollect()
    {
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
        if (audioSource != null && audioSource.enabled)
        {
            audioSource.Play();
        }
    }
    
    private void HideObject()
    {
        gameObject.SetActive(false);
    }
}
