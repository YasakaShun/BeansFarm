using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obstacle;

namespace Obstacle
{

    public class Obj : MonoBehaviour
    {
        public enum Kind
        {
            Soft,
            Normal,
            Hard,
        }

        public int Hp { get; set; }
        public Kind kind = Kind.Normal;
        private IState state;

        private void Awake()
        {
            switch (kind)
            {
                case Kind.Soft:
                    Hp = 5;
                    break;
                case Kind.Normal:
                    Hp = 10;
                    break;
                case Kind.Hard:
                    Hp = 20;
                    break;
                default:
                    Hp = 1;
                    Debug.Assert(false, "unknown Objstacle.Kind");
                    break;
            }
        }

        private void Start()
        {
            StateInit.ChangeState(this);
        }

        private void Update()
        {
            state.Update();
        }

        public void ChangeState(IState newState)
        {
            if (state != null)
            {
                state.OnEnd();
            }
            state = newState;
            state.OnStart();
        }

        public void Damage(int damage)
        {
            Hp -= damage;

            if (Hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
