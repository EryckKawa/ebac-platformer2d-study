using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    // Vida inicial do objeto
    public int startLife = 10;
    // Indica se o objeto deve ser destruído ao ser morto
    public bool isDestroyOnKill = false;
    // Tempo de espera antes de destruir o objeto após ser morto
    public float delayToKill = .2f;

    // Vida atual do objeto
    private float _currentLife;
    // Indica se o objeto está morto
    private bool _isDead = false;

    // Referência para o componente FlashColor para animação de flash ao receber dano
    [SerializeField] private FlashColor _flashColor;

    // Método chamado quando o objeto é inicializado
    private void Awake()
    {
        // Inicializa o objeto
        Init();
        // Verifica se o componente FlashColor está presente
        if (_flashColor == null)
        {
            // Se não estiver, tenta obtê-lo do próprio objeto
            _flashColor = GetComponent<FlashColor>();
        }
    }

    // Método para inicializar a vida do objeto
    private void Init()
    {
        // Define que o objeto não está morto
        _isDead = false;
        // Define a vida atual como a vida inicial
        _currentLife = startLife;
    }

    // Método para receber dano
    public void Damage(int damage)
    {
        // Verifica se o objeto já está morto e não processa o dano se estiver
        if (_isDead)
        {
            return;
        }

        // Reduz a vida do objeto de acordo com o dano recebido
        _currentLife -= damage;
        // Verifica se a vida do objeto chegou a zero ou menos
        if (_currentLife <= 0)
        {
            // Se sim, chama o método Kill para "matar" o objeto
            Kill();
        }

        // Verifica se o componente FlashColor está presente e chama o método Flash para animação de flash
        if (_flashColor != null)
        {
            _flashColor.Flash();
        }
    }

    // Método para "matar" o objeto
    private void Kill()
    {
        // Define que o objeto está morto
        _isDead = true;

        // Verifica se o objeto deve ser destruído ao ser morto
        if (isDestroyOnKill)
        {
            // Se sim, destrói o objeto após um tempo de espera
            Destroy(gameObject, delayToKill);
        }
    }
}
