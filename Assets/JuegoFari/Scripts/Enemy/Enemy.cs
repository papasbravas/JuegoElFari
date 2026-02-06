using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health; // Salud del enemigo

    public void TakeDamage(float damage)
    {
        health -= damage; // Resta el daño a la salud del enemigo
        Debug.Log(health); // Imprime la salud actual del enemigo en la consola
    }
}
