using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuizKnightIA.AI
{
    public class FiniteStateMachine<T>
    {
        private readonly Dictionary<T, Action> states = new Dictionary<T, Action>();
        private T currentState;

        public T CurrentState => currentState;

        public void AddState(T state, Action behavior)
        {
            states[state] = behavior;
        }

        public void SetState(T state)
        {
            currentState = state;
        }

        public void Update()
        {
            if (states.ContainsKey(currentState))
            {
                states[currentState]?.Invoke();
            }
        }
    }
}
