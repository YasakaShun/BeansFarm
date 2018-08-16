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
        public Obstacle.Manager ObstacleManager { get; private set; }

        void Awake()
        {
            TouchManager = new TouchManager(this);
            PlayerManager = new Player.Manager(this);
            ObstacleManager = new Obstacle.Manager(this);

            TouchManager.Awake();
            PlayerManager.Awake();
            ObstacleManager.Awake();
        }

        void Start()
        {
            TouchManager.Start();
            PlayerManager.Start();
            ObstacleManager.Start();
        }

        void Update()
        {
            TouchManager.Update();
            PlayerManager.Update();
            ObstacleManager.Update();
        }
    }

}