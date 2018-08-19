using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouchManager
{
    private static int MouseLeft() { return 0; }
    private static int MouseRight() { return 1; }
    private static int MouseMiddle() { return 2; }

    public TouchManager(BeansFarm.GameManager manager)
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
        if (Input.GetMouseButtonDown(TouchManager.MouseLeft()))
        {
            RaycastHit hit = new RaycastHit();
            bool ret = Physics.Raycast(
                Camera.main.ScreenPointToRay(Input.mousePosition),
                out hit,
                100.0f
                );
            if (!ret)
            {
                return;
            }

            if (hit.collider.gameObject.CompareTag("Item"))
            {
                var waterBall = hit.collider.gameObject.GetComponent<WaterBall>();
                if (waterBall.Parent != null)
                {
                    return;
                }

                var signal = Player.Signal.ToWaterBall;
                var players = Player.Manager.AllPlayer()
                    .Where(x => x.CanReceiveSignal(signal))
                    .Where(x => x.IsReachable(waterBall.transform.position))
                    .ToArray();
                if (!players.Any())
                {
                    return;
                }

                players.First().ReceiveSignal(signal, waterBall.gameObject);
            }
            else if (hit.collider.gameObject.CompareTag("Obstacle"))
            {
                var signal = Player.Signal.ToObstacle;
                var players = Player.Manager.AllPlayer()
                    .Where(x => x.CanReceiveSignal(signal))
                    //.Where(x => x.IsReachable(obstacle))
                    .ToArray();
                if (!players.Any())
                {
                    return;
                }

                players.First().ReceiveSignal(signal, hit.collider.gameObject);
            }
            else if (hit.collider.gameObject.CompareTag("Cell"))
            {
                var cell = hit.collider.gameObject.GetComponent<Field.Cell>();
                if (cell.kind != Field.Cell.Kind.Farm)
                {
                    return;
                }

                var signal = Player.Signal.ToFarm;
                var players = Player.Manager.AllPlayer()
                    .Where(x => x.CanReceiveSignal(signal))
                    .Where(x => x.IsReachable(cell.transform.position))
                    .ToArray();
                if (!players.Any())
                {
                    return;
                }

                Player.Beans player = players.First();
                float min = 10000.0f;
                foreach (var p in players)
                {
                    float dist = Vector3.Distance(p.transform.position, cell.transform.position);
                    if (dist < min)
                    {
                        min = dist;
                        player = p;
                    }
                }

                player.ReceiveSignal(signal, cell.gameObject);
            }
        }
    }

    private BeansFarm.GameManager mManager;
}
