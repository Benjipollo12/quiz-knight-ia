using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float attackRange = 1.2f;
    public float attackCooldown = 0.7f;
    public Transform attackPoint;
    public LayerMask bossLayer;

    public int maxHealth = 50;
    public int currentHealth = 50;
    public int baseDamage = 8;
    public int questionHearts = 3;

    private Rigidbody2D rb;
    private float nextAttackTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        AttackInput();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void AttackInput()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, bossLayer);
        foreach (var hit in hits)
        {
            BossController boss = hit.GetComponent<BossController>();
            if (boss != null)
                boss.TakeDamage(baseDamage);
        }
    }

    public void ApplyQuestionFeedback(bool correct)
    {
        if (correct)
        {
            baseDamage += 2;
            currentHealth = Mathf.Min(maxHealth, currentHealth + 5);
            Debug.Log("BUFF: +2 daño y +5 HP");
        }
        else
        {
            questionHearts--;
            currentHealth -= 5;
            Debug.Log("DEBUFF: -1 corazón y -5 HP");
        }

        if (currentHealth <= 0)
        {
            GameManager.instance.TriggerGameOver();
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            GameManager.instance.TriggerGameOver();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
