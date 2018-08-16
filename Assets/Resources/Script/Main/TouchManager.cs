using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager
{
    private static int MouseLeft() { return 0; }
    private static int MouseRight() { return 1; }
    private static int MouseMiddle() { return 2; }

    public TouchManager(BeansFarm.Manager manager)
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

            var pos = hit.point;
            Debug.Log(pos);

            //var players = AllPlayer();
        }
    }

    private BeansFarm.Manager mManager;
}
