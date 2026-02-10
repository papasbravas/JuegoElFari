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
    [SerializeField] private GameObject attackHitboxSingle;
    [SerializeField] private GameObject attackHitboxMulti;

    [Header("Invencible")]
    [SerializeField] public float invincibleDuration = 3f; // Duración de la invencibilidad
    [SerializeField] public bool isInvincible = false; // Indica si el jugador es invencible
    [SerializeField] public float invincibleCooldown = 25f; // Tiempo de recarga de la invencibilidad

    private Animator anima; // Referencia al componente Animator

    private void Start()
    {
        anima = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("Ataque normal activado");
            singleAttack();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Ataque múltiple activado");
            multipleAttack();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (canStun)
            {
                Debug.Log("Aturdimiento activado");
                areaStun();
                canStun = false; // Desactiva el aturdimiento hasta que se recargue
                //StartCoroutine(StunCooldown());
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isInvincible)
            {
                Debug.Log("Invencibilidad activada");
                invincibilityBuff();
                GetComponent<PlayerHealth>().isInvincible = true;
                StartCoroutine(invencibleCooldown());
            }
        }
    }


    void singleAttack()
    {
        StartCoroutine(ActivateHitbox());
    }

    IEnumerator ActivateHitbox()
    {
        attackHitboxSingle.SetActive(true);
        yield return new WaitForSeconds(0.2f); // tiempo del golpe
        attackHitboxSingle.SetActive(false);
    }


    void multipleAttack()
    {
        StartCoroutine(ActivateHitboxMult());
    }

    IEnumerator ActivateHitboxMult()
    {
        attackHitboxMulti.SetActive(true);
        yield return new WaitForSeconds(0.2f); // tiempo del golpe
        attackHitboxMulti.SetActive(false);
    }

    void areaStun()
    {
        StartCoroutine(stunDebuff());
    }

    IEnumerator stunDebuff()
    {
        // Obtiene todos los colliders dentro del radio de aturdimiento
        Collider[] hits = Physics.OverlapSphere(transform.position, stunRadius);
            
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Enemigo enemy = hit.GetComponent<Enemigo>(); // Obtiene el componente Enemy del enemigo
                if (enemy != null)
                {
                    enemy.ApplyStun(stunDuration);
                    Debug.Log("Enemigo aturdido: " + hit.name); // Imprime el nombre del enemigo aturdido en la consola
                }   
            }
        }

        yield return new WaitForSeconds(stunDuration); // Espera la duración del aturdimiento antes de permitir otro aturdimiento
        StartCoroutine(StunCooldown());
    }

    IEnumerator StunCooldown()
    { 
        Debug.Log("Cooldown de stun iniciado"); // Imprime un mensaje indicando que el cooldown ha comenzado
        yield return new WaitForSeconds(stunCoolddown); // Espera el tiempo de recarga del aturdimiento
        canStun = true; // Permite que el jugador pueda aturdir nuevamente
        Debug.Log("Stun listo otra vez"); // Imprime un mensaje indicando que el aturdimiento está listo nuevamente
    }

    void invincibilityBuff()
    {
        StartCoroutine(invencibleBuff());
    }

    IEnumerator invencibleBuff()
    {
        // Lógica para el buff de invencibilidad
        
        yield return new WaitForSeconds(invincibleDuration);
    }

    IEnumerator invencibleCooldown()
    {
        Debug.Log("Cooldown de invencibilidad iniciado"); // Imprime un mensaje indicando que el cooldown ha comenzado
        yield return new WaitForSeconds(invincibleCooldown); // Espera el tiempo de recarga de la invencibilidad
        GetComponent<PlayerHealth>().isInvincible = false;
        Debug.Log("Invencibilidad lista otra vez"); // Imprime un mensaje indicando que la invencibilidad está lista nuevamente
    }
}

