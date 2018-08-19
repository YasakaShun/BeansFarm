using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{

    public class Beans : MonoBehaviour
    {
        public GameObject WaterBall { get; set; }
        public int Life;
        public NavMeshAgent Agent { get; private set; }
        public Animator Anim { get; private set; }
        public Signal Signal { get; private set; }
        private IState state;

        void Start()
        {
            WaterBall = null;
            Agent = GetComponent<NavMeshAgent>();
            Anim = GetComponent<Animator>();
            Signal = Signal.None;
            state = null;

            StartCoroutine(UpdateLife());

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
            // アイテムとの衝突判定
            if (hit.CompareTag("Item"))
            {
                if (this.hasWaterBall())
                {
                    return;
                }

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

        public bool IsReachable(Vector3 pos)
        {
            var path = new UnityEngine.AI.NavMeshPath();
            if (!Agent.CalculatePath(pos, path))
            {
                return false;
            }
            if (path.status != UnityEngine.AI.NavMeshPathStatus.PathComplete)
            {
                return false;
            }

            return true;
        }

        public bool hasWaterBall()
        {
            return WaterBall != null;
        }

        public bool CanReceiveSignal(Signal signal)
        {
            if (Signal != Signal.None)
            {
                return false;
            }
            if (state == null)
            {
                return false;
            }
            return state.CanReceiveSignal(signal);
        }

        public void ReceiveSignal(Signal signal, GameObject gameObject)
        {
            Debug.Assert(CanReceiveSignal(signal));
            Signal = signal;
            state.OnReceiveSignal(signal, gameObject);
        }

        public void ResetSignal()
        {
            Signal = Signal.None;
        }

        private void UpdateAnim()
        {
            // NavMesh での移動速度にあわせてモーションを変える
            var vel = Agent.velocity;
            Anim.SetFloat("Speed", vel.magnitude);
        }

        /// <summary>
        /// 体力の自動減少
        /// </summary>
        /// <returns></returns>
        private IEnumerator UpdateLife()
        {
            while (true)
            {
                // カラー更新
                {
                    var materials = this.GetComponentsInChildren<SkinnedMeshRenderer>()
                        .Select(x => x.material)
                        .ToArray();

                    foreach (var material in materials)
                    {
                        var color = material.color;
                        float rate = this.Life / 100.0f;
                        float rate2 = Mathf.Sin(Mathf.Deg2Rad * 90.0f * rate);
                        color.g = rate2;
                        color.b = rate2;
                        material.color = color;
                    }
                }

                yield return new WaitForSeconds(5);
                this.Life -= 1;

                // Life 0 で死亡
                if (this.Life <= 0)
                {
                    // TODO: 死亡演出
                    if (this.hasWaterBall())
                    {
                        Destroy(this.WaterBall);
                    }
                    Destroy(this.gameObject);
                    yield break;
                }
            }
        }

    }

}
