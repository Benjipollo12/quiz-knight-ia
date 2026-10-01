using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
            player.transform.position = new Vector3(0f, 0f, 0f);
        }
    }
}
