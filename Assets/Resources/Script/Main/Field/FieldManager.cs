using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Field
{

    public class FieldManager : MonoBehaviour
    {
        public Transform prefab;
        public int width = 10;
        public int depth = 10;

        private const float CellWidth = 1.0f;

        /// <summary>
        /// ランダムな畑セルを返す
        /// </summary>
        /// <returns></returns>
        public static GameObject[] AllFarms()
        {
            return AllCells()
                .Where(x => x.GetComponent<Cell>().kind == Cell.Kind.Farm)
                .ToArray();
        }

        private static GameObject[] AllCells()
        {
            return GameObject.FindGameObjectsWithTag("Cell");
        }
    }

}
