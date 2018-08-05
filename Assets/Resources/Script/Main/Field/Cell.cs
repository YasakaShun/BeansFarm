using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class Cell : MonoBehaviour
    {
        public float waterPower;
        public Kind kind;

        public enum Kind
        {
            Normal,   // 通常
            Farm,     // 畑
            Fountain, // 池
        }

        void Start()
        {
        }

        void Update()
        {

        }
    }
}
