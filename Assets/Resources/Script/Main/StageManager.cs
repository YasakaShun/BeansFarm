using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BeansFarm
{
    public class StageManager
    {
        private enum State
        {
            Ready,
            Main,
            Result,
        }

        public int goalCount = 20;

        public bool IsResult
        {
            get
            {
                return mState == State.Result;
            }
        }

        public StageManager(BeansFarm.GameManager manager)
        {
            mManager = manager;
        }

        public void Awake()
        {
            mState = State.Ready;
        }

        public void Start()
        {

        }

        public void Update()
        {
            switch (mState)
            {
                case State.Ready:
                    mState = State.Main;
                    break;
                case State.Main:
                    if (Player.Manager.AllPlayer().Length >= goalCount)
                    {
                        mState = State.Result;
                    }
                    break;
                case State.Result:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private BeansFarm.GameManager mManager;
        private State mState;
    }
}
