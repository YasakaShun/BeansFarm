using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeansFarm
{

    public class Manager : MonoBehaviour
    {
        private static int MouseLeft() { return 0; }
        private static int MouseRight() { return 1; }
        private static int MouseMiddle() { return 2; }

        public TouchManager TouchManager { get; private set; }
        public Player.Manager PlayerManager { get; private set; }

        void Awake()
        {
            TouchManager = new TouchManager(this);
            PlayerManager = new Player.Manager(this);

            TouchManager.Awake();
            PlayerManager.Awake();
        }

        void Start()
        {
            TouchManager.Start();
            PlayerManager.Start();
        }

        void Update()
        {
            TouchManager.Update();
            PlayerManager.Update();
        }
    }

}