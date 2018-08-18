using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeansFarm
{

    public class GameManager : MonoBehaviour
    {
        public StageManager StageManager { get; private set; }
        public TouchManager TouchManager { get; private set; }
        public InfoManager InfoManager { get; private set; }
        public Player.Manager PlayerManager { get; private set; }
        public Obstacle.Manager ObstacleManager { get; private set; }

        public static GameManager Instance { get; private set; }

        void Awake()
        {
            Debug.Assert(Instance == null);
            Instance = this;

            StageManager = new StageManager(this);
            TouchManager = new TouchManager(this);
            InfoManager = new InfoManager(this);
            PlayerManager = new Player.Manager(this);
            ObstacleManager = new Obstacle.Manager(this);

            StageManager.Awake();
            TouchManager.Awake();
            InfoManager.Awake();
            PlayerManager.Awake();
            ObstacleManager.Awake();
        }

        void Start()
        {
            StageManager.Start();
            TouchManager.Start();
            InfoManager.Start();
            PlayerManager.Start();
            ObstacleManager.Start();
        }

        void Update()
        {
            StageManager.Update();
            TouchManager.Update();
            InfoManager.Update();
            PlayerManager.Update();
            ObstacleManager.Update();
        }

    }

}