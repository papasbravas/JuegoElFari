using System.Collections;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    [Header("Daño y Aturdimiento")]
    [SerializeField] public float singleDamage = 10f; // Daño hecho por un ataque normal
    [SerializeField] public float multiDamage = 5f; // Daño hecho por un ataque múltiple
    [SerializeField] public float stunRadius = 5f; // Radio de aturdimiento
    [SerializeField] public float stunDuration = 2f; // Duración del aturdimiento
    [SerializeField] public float stunCoolddown = 10f; // Tiempo de recarga del aturdimiento
    [SerializeField] public bool canStun = true; // Indica si el jugador puede aturdir

    [Header("Invencible")]
    [SerializeField] public float invincibleDuration = 3f; // Duración de la invencibilidad
    [SerializeField] public bool isInvincible = false; // Indica si el jugador es invencible

    private Animator anima; // Referencia al componente Animator

    private void Start()
    {
        anima = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            anima.SetTrigger("NormalAttack"); // Activa la animación de ataque normal
        }
    }

    void singleAttack()
    {
        // Lógica para el ataque normal
    }

    void multipleAttack()
    {
        // Lógica para ataque múltiple
    }

    void areaStun()
    {
        // Lógica para aturdir en área
    }

    IEnumerator invencibleBuff()
    {
        // Lógica para el buff de invencibilidad
        yield return new WaitForSeconds(invincibleDuration);
    }
}
