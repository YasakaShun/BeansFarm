using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Player
{
    public class Manager
    {
        public Beans[] AllPlayer()
        {
            return GameObject.FindGameObjectsWithTag("Player")
                .Select(x => x.GetComponent<Beans>())
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

        private BeansFarm.Manager mManager;
    }
}
