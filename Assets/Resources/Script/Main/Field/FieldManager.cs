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
        public static GameObject GetRandomFarm()
        {
            var farms = AllCells()
                .Where(x => x.GetComponent<Cell>().kind == Cell.Kind.Farm)
                .ToArray();

            if (farms.Any())
            {
                return farms[Random.Range(0, farms.Length)];
            }
            else
            {
                return null;
            }
        }

        private static GameObject[] AllCells()
        {
            return GameObject.FindGameObjectsWithTag("Cell");
        }
    }

}
