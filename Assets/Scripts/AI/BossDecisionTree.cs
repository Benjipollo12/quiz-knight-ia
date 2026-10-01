using System.Collections.Generic;
using UnityEngine;

namespace QuizKnightIA.AI
{
    public class BossDecisionTree
    {
        public enum DecisionAction
        {
            Attack,
            Question,
            Retreat,
            SpecialAttack,
            Continue
        }

        public DecisionAction Evaluate(BossController boss, Transform player)
        {
            if (player == null)
            {
                return DecisionAction.Continue;
            }

            float distance = Vector2.Distance(boss.transform.position, player.position);
            if (distance < 1.5f)
            {
                return DecisionAction.Attack;
            }

            if (boss.currentHealth <= boss.maxHealth * 0.3f)
            {
                return DecisionAction.SpecialAttack;
            }

            return DecisionAction.Question;
        }
    }
}
