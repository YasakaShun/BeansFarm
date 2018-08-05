using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{

    public class Beans : MonoBehaviour
    {
        public GameObject WaterBall { get; set; }
        public NavMeshAgent Agent { get; private set; }
        private Animator Anim { get; set; }
        private IState state;

        void Start()
        {
            Agent = GetComponent<NavMeshAgent>();
            Anim = GetComponent<Animator>();

            StateWait.ChangeState(this);
        }

        void Update()
        {
            state.Update();
        }

        void FixedUpdate()
        {
            updateAnim();
        }

        /// <summary>
        /// State変更。
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(IState newState)
        {
            if (state != null)
            {
                state.OnEnd();
            }
            state = newState;
            state.OnStart();
        }

        public bool isReached()
        {
            if (Agent.remainingDistance <= Agent.stoppingDistance)
            {
                return true;
            }
            return false;
        }

        public bool hasWaterBall()
        {
            return WaterBall != null;
        }

        private void updateAnim()
        {
            // NavMesh での移動速度にあわせてモーションを変える
            var vel = Agent.velocity;
            Anim.SetFloat("Speed", vel.magnitude);
        }

    }

}
