using System.Collections.Generic;
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
        public bool useDecisionTree = true;

        private float attackTimer;
        private Transform player;
        private readonly List<string> path = new List<string>();

        private void Start()
        {
            currentHealth = maxHealth;
            player = GameObject.FindWithTag("Player")?.transform;
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

            if (currentState == BossState.Idle && distance < range)
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

            if (useDecisionTree)
            {
                var tree = new BossDecisionTree();
                var result = tree.Evaluate(this, player);
                if (result == BossDecisionTree.DecisionAction.Attack)
                {
                    currentState = BossState.Attack;
                }
                else if (result == BossDecisionTree.DecisionAction.Question)
                {
                    currentState = BossState.Question;
                }
            }
            else if (distance <= 1.2f)
            {
                currentState = BossState.Attack;
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

        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                currentState = BossState.Defeated;
                GameManager.Instance.RegisterBossDefeat();
                Debug.Log("Jefe derrotado");
            }
            else if (currentHealth <= maxHealth * 0.3f)
            {
                currentState = BossState.Enraged;
            }
        }

        public List<string> ComputePath(string start, string goal)
        {
            path.Clear();
            path.Add(start);
            path.Add(goal);
            return new List<string>(path);
        }
    }
}
