using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody; // Referência ao componente Rigidbody2D do jogador
    public HealthBase healthBase; // Referência a uma classe de saúde do jogador

    [Header("Setup Player")]
    public SOPlayerSetup sOPlayerSetup; // Referência a um ScriptableObject que contém configurações do jogador
    public Animator animator; // Referência ao componente Animator do jogador
    private Animator _currentPlayer; // Referência privada ao componente Animator do jogador

    [Header("Jump Collision Setup")]
    public new Collider2D collider2D; // Referência ao Collider2D do jogador
    public float distToGround; // Distância do jogador até o chão
    public float spaceToGround = 0.1f; // Espaço entre o jogador e o chão para considerá-lo "no chão"
    public ParticleSystem jumpVFX; // Efeito de partículas para o salto

    // Método chamado quando o script é inicializado
    private void Awake()
    {
        // Se existir uma referência a healthBase, registra um evento para quando o jogador morrer
        if (healthBase != null)
        {
            healthBase.OnKill += PlayerOnKill;
        }
        _currentPlayer = animator; // Atribui o componente Animator à variável privada _currentPlayer

        // Se existir uma referência a collider2D, calcula a distância até o chão
        if (collider2D != null)
        {
            distToGround = collider2D.bounds.extents.y;
        }
    }

    // Método para verificar se o jogador está no chão
    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector2.one, distToGround + spaceToGround);
    }

    // Método chamado a cada quadro
    private void Update()
    {
        IsGrounded();
        HandleMovement(); // Controla o movimento do jogador
        HandleJump(); // Controla o salto do jogador
    }

    // Método para controlar o movimento do jogador
    private void HandleMovement()
    {
        // Se a tecla D for pressionada, move o jogador para a direita
        if (Input.GetKey(KeyCode.D))
        {
            // Define a velocidade do jogador de acordo com a tecla Shift pressionada ou não
            myRigidBody.velocity = new Vector2(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? sOPlayerSetup.runSpeed : sOPlayerSetup.speed, myRigidBody.velocity.y);

            // Se o jogador estiver virado para a esquerda, vira-o para a direita
            if (myRigidBody.transform.localScale.x != 1)
            {
                DOTween.Kill(myRigidBody.transform);
                myRigidBody.transform.DOScaleX(1, sOPlayerSetup.duration);
            }
            _currentPlayer.SetBool(sOPlayerSetup.boolRun, true); // Ativa a animação de corrida
        }
        // Se a tecla A for pressionada, move o jogador para a esquerda
        else if (Input.GetKey(KeyCode.A))
        {
            // Define a velocidade do jogador de acordo com a tecla Shift pressionada ou não
            myRigidBody.velocity = new Vector2(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? -sOPlayerSetup.runSpeed : -sOPlayerSetup.speed, myRigidBody.velocity.y);

            // Se o jogador estiver virado para a direita, vira-o para a esquerda
            if (myRigidBody.transform.localScale.x != -1)
            {
                DOTween.Kill(myRigidBody.transform);
                myRigidBody.transform.DOScaleX(-1, sOPlayerSetup.duration);
            }
            _currentPlayer.SetBool(sOPlayerSetup.boolRun, true); // Ativa a animação de corrida
        }
        else
        {
            _currentPlayer.SetBool(sOPlayerSetup.boolRun, false); // Desativa a animação de corrida
        }

        // Aplica a fricção ao jogador para parar gradualmente quando não estiver se movendo
        if (myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity += sOPlayerSetup.friction;
        }
        else if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity -= sOPlayerSetup.friction;
        }
    }

    // Método para controlar o salto do jogador
    private void HandleJump()
    {
        // Se a tecla de espaço for pressionada e o jogador estiver no chão, ele salta
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            myRigidBody.velocity = Vector2.up * sOPlayerSetup.forceJump; // Aplica a força do salto
            //myRigidBody.transform.localScale = Vector2.one; // Reseta a escala do jogador
            //DOTween.Kill(myRigidBody.transform); // Cancela qualquer animação de escala pendente
            //HandScaleJump(); // Animação de escala durante o salto
            PlayJumpVFX(); // Reproduz o efeito de partículas do salto
        }
    }
    

    // Método para animar a escala durante o salto
    private void HandScaleJump()
    {
        // Anima a escala em Y e X com efeito de ida e volta
        myRigidBody.transform.DOScaleY(sOPlayerSetup.jumpScaleY, sOPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(sOPlayerSetup.ease);
        myRigidBody.transform.DOScaleX(sOPlayerSetup.jumpScaleX, sOPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(sOPlayerSetup.ease);
    }

    // Método para reproduzir o efeito de partículas do salto
    private void PlayJumpVFX()
    {
        if (jumpVFX != null)
        {
            jumpVFX.Play();
        }
    }

    // Método para lidar com a morte do jogador
    private void PlayerOnKill()
    {
        healthBase.OnKill -= PlayerOnKill; // Remove a inscrição do evento
        _currentPlayer.SetTrigger(sOPlayerSetup.deathTrigger); // Ativa a animação de morte
    }

    // Método para destruir o jogador
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
