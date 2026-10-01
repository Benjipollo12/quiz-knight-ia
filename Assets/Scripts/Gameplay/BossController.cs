using UnityEngine;

namespace QuizKnightIA.AI
{
    public class BossController : MonoBehaviour
    {
        public enum BossState
        {
            Idle,
            Chase,
            Attack,
            Question,
            Enraged,
            Defeated
        }

        public BossState currentState = BossState.Idle;
        public int maxHealth = 80;
        public int currentHealth;
        public int damage = 5;
        public float range = 2.5f;
        public float attackCooldown = 1.2f;

        private float attackTimer;
        private Transform player;
        private BossDecisionTree decisionTree;

        private void Start()
        {
            currentHealth = maxHealth;
            player = GameObject.FindWithTag("Player")?.transform;
            decisionTree = new BossDecisionTree();
        }

        private void Update()
        {
            if (currentState == BossState.Defeated)
            {
                return;
            }

            if (player == null)
            {
                return;
            }

            float distance = Vector2.Distance(transform.position, player.position);

            if (distance < range)
            {
                currentState = BossState.Chase;
            }

            switch (currentState)
            {
                case BossState.Chase:
                    HandleChase(distance);
                    break;
                case BossState.Attack:
                    HandleAttack();
                    break;
                case BossState.Question:
                    HandleQuestion();
                    break;
                case BossState.Enraged:
                    HandleEnraged();
                    break;
            }
        }

        private void HandleChase(float distance)
        {
            if (distance > range)
            {
                currentState = BossState.Idle;
                return;
            }

            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * Time.deltaTime * 1.5f;

            var decision = decisionTree.Evaluate(this, player);
            if (decision == BossDecisionTree.DecisionAction.Attack)
            {
                currentState = BossState.Attack;
            }
            else if (decision == BossDecisionTree.DecisionAction.Question)
            {
                currentState = BossState.Question;
            }
            else if (decision == BossDecisionTree.DecisionAction.SpecialAttack)
            {
                currentState = BossState.Enraged;
            }
        }

        private void HandleAttack()
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                Debug.Log("Jefe ataca: " + damage + " HP");
                attackTimer = attackCooldown;
                currentState = BossState.Chase;
            }
        }

        private void HandleQuestion()
        {
            Debug.Log("El jefe lanza una pregunta al jugador");
            currentState = BossState.Chase;
        }

        private void HandleEnraged()
        {
            damage = 10;
            Debug.Log("El jefe entra en estado enfurecido");
            currentState = BossState.Attack;
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                currentState = BossState.Defeated;
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.RegisterBossDefeat();
                }
                Debug.Log("Jefe derrotado");
            }
            else if (currentHealth <= maxHealth * 0.3f)
            {
                currentState = BossState.Enraged;
            }
        }
    }
}
