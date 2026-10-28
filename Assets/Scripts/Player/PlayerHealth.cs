using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private bool isDead;

    public void TakeDamage(int damage)
    {
        Die();
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        // Olayı fırlatıyoruz (UI bunu dinleyecek)
        GameEvents.TriggerPlayerDied();

        Destroy(gameObject);
    }
}