using UnityEngine;

public class BatatinhaFrita : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Debug.Log(gameObject.name + " Velocidade: " + rb.linearVelocity.magnitude);
        if (!GameManager.Instance.canMove && rb.linearVelocity.magnitude > 0.1f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " foi eliminado!");
        animator.SetBool("Death", true);
        this.enabled = false; // Desativa o script para não continuar verificando
    }
}
