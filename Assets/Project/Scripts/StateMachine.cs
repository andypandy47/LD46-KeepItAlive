using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace StateMachine
{
    public class StateMachine<T>
    {
        public State<T> currentStateInstance { get; private set; }
        public State currentState;
        public T owner;

        public StateMachine(T _o)
        {
            owner = _o;
            currentStateInstance = null;
        }

        public virtual void ChangeState(State<T> newState)
        {
            if (currentStateInstance != null)
                currentStateInstance.ExitState(owner);

            currentStateInstance = newState;

            currentStateInstance.EnterState(owner);
        }

        public void Update()
        {
            if (currentStateInstance != null)
                currentStateInstance.UpdateState(owner);
        }

        public void GetState()
        {

        }
    }

    public abstract class State<T>
    {
        protected BaseController controller;
        public State state { get; protected set; }
        public abstract void EnterState(T owner);
        public abstract void ExitState(T owner);
        public abstract void UpdateState(T owner);
    }
}

