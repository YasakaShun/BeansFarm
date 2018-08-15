using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class Cell : MonoBehaviour
    {
        public float waterPower;
        public Kind kind;

        private IState state;
        public ICustom custom { get; set; }

        public bool IsReady { get; set; }

        public enum Kind
        {
            Normal,   // 通常
            Farm,     // 畑
            Fountain, // 池
        }

        private void Awake()
        {
            IsReady = false;
        }

        void Start()
        {
            switch (kind)
            {
                case Kind.Normal:
                    Normal.StateInit.ChangeState(this);
                    break;
                case Kind.Farm:
                    Farm.StateInit.ChangeState(this);
                    break;
                case Kind.Fountain:
                    Fountain.StateInit.ChangeState(this);
                    break;
                default:
                    Debug.Assert(false, "[Cell] unknown state.");
                    break;
            }
        }

        void Update()
        {
            state.Update();
        }

        /// <summary>
        /// 状態遷移
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

        public void GiveWater(WaterBall waterBall)
        {
            this.waterPower += waterBall.Power;
            IsReady = true;
        }

    }
}
