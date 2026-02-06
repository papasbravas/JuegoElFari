using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private float damage; // Cantidad de daño a infligir

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>(); // Obtiene el componente HealthSystem del enemigo
            enemy.TakeDamage(damage); // Llama al método TakeDamage del enemigo para infligir daño
        }
    }
}
