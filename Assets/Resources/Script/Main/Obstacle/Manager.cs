using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obstacle;
using System.Linq;

namespace Obstacle
{

    public class Manager
    {
        public Obj[] AllObstacle()
        {
            return GameObject.FindGameObjectsWithTag("Obstacle")
                .Select(x => x.GetComponent<Obj>())
                .ToArray();
        }

        public Manager(BeansFarm.Manager manager)
        {
            mManager = manager;
        }

        public void Awake()
        {
        }

        public void Start()
        {
        }

        public void Update()
        {
        }

        BeansFarm.Manager mManager;
    }

}
