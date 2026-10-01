using UnityEngine;

namespace QuizKnightIA.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Stats")]
        public int baseDamage = 8;
        public int projectileDamage = 5;
        public float attackCooldown = 0.7f;
        public float attackRange = 1.5f;
        public int maxHealth = 50;
        public int currentHealth;

        [Header("Attack State")]
        public bool canAttack = true;
        public bool hasProjectileAttack;
        public bool hasSpinAttack;
        public bool hasChargeAttack;

        private float attackTimer;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        private void Update()
        {
            if (!canAttack)
            {
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0f)
                {
                    canAttack = true;
                }
            }
        }

        public void BasicAttack()
        {
            if (!canAttack)
            {
                return;
            }

            canAttack = false;
            attackTimer = attackCooldown;
            Debug.Log("Ataque cuerpo a cuerpo: " + baseDamage + " HP");
        }

        public void ProjectileAttack()
        {
            if (!hasProjectileAttack)
            {
                return;
            }

            Debug.Log("Ataque de proyectil: " + projectileDamage + " HP");
        }

        public void SpinAttack()
        {
            if (!hasSpinAttack)
            {
                return;
            }

            Debug.Log("Ataque giratorio: daño múltiple");
        }

        public void ChargeAttack()
        {
            if (!hasChargeAttack)
            {
                return;
            }

            Debug.Log("Ataque cargado: preparación y ejecución");
        }

        public void TakeDamage(int damage)
        {
            currentHealth = Mathf.Max(0, currentHealth - damage);
            if (currentHealth <= 0)
            {
                GameManager.Instance.TriggerGameOver();
            }
        }

        public void Heal(int amount)
        {
            currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        }
    }
}
