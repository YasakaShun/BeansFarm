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
            UpdateAnim();
        }

        private void OnTriggerEnter(Collider hit)
        {
            if (hit.CompareTag("Item"))
            {
                var waterBall = hit.gameObject.GetComponent<WaterBall>();
                if (waterBall != null && waterBall.Parent == null)
                {
                    waterBall.Parent = this;
                    this.WaterBall = waterBall.gameObject;
                    waterBall.transform.position = this.transform.position + Vector3.up * 2;
                    waterBall.transform.rotation = Quaternion.identity;
                    waterBall.transform.parent = this.transform;
                }
            }
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
            return Agent.remainingDistance <= Agent.stoppingDistance;
        }

        public bool hasWaterBall()
        {
            return WaterBall != null;
        }

        private void UpdateAnim()
        {
            // NavMesh での移動速度にあわせてモーションを変える
            var vel = Agent.velocity;
            Anim.SetFloat("Speed", vel.magnitude);
        }

    }

}
