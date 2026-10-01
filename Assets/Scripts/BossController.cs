using UnityEngine;

public class BossController : MonoBehaviour
{
    public enum BossState { Idle, Chase, Attack, Defeated }

    public BossState currentState = BossState.Idle;

    public int maxHealth = 80;
    public int currentHealth;
    public int damage = 5;
    public float detectionRange = 6f;
    public float attackRange = 1.5f;
    public float moveSpeed = 2.2f;

    public Transform player;
    public QuestionManager questionManager;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            currentState = BossState.Defeated;
            GameManager.instance.RegisterBossDefeat();
            Destroy(gameObject, 0.2f);
            return;
        }

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist < detectionRange)
            currentState = BossState.Chase;

        if (dist <= attackRange)
            currentState = BossState.Attack;

        if (currentState == BossState.Chase)
            ChasePlayer();

        if (currentState == BossState.Attack)
            AttackPlayer();
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    private void AttackPlayer()
    {
        if (questionManager != null && !questionManager.IsOpen())
        {
            questionManager.ShowRandomQuestion();
        }

        if (player != null)
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null)
                pc.TakeDamage(damage);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            currentState = BossState.Defeated;
    }
}
