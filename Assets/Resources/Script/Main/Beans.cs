using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Beans : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private State state;
    private GameObject prefab;

    private GameObject waterBall = null;

    private enum State
    {
        Wait,
        ToSource,
        GatherWater,
        ToCell,
        UseWater,
        TERM
    }

	void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        prefab = (GameObject)Resources.Load("Prefab/Main/unitychan");

        startWait();
    }

    void Update()
    {
        updateState();
	}

    void FixedUpdate()
    {
        updateAnim();
    }

    private void updateState()
    {
        switch (state)
        {
            case State.Wait:
                break;
            case State.ToSource:
                if (transform.position.Equals(agent.destination))
                {
                    startGatherWater();
                }
                break;
            case State.GatherWater:
                break;
            case State.ToCell:
                if (transform.position.Equals(agent.destination))
                {
                    startUseWater();
                }
                break;
            case State.UseWater:
                break;
            default:
                Debug.Assert(false, "unknown state.");
                break;
        }
    }

    private void updateAnim()
    {
        // NavMesh での移動速度にあわせてモーションを変える
        var vel = agent.velocity;
        anim.SetFloat("Speed", vel.magnitude);
    }

    private void startWait()
    {
        agent.enabled = false;

        state = State.Wait;

        StartCoroutine(wait());
    }

    private void startToSource()
    {
        agent.enabled = true;

        var source = GameObject.FindGameObjectsWithTag("Source");
        agent.destination = source[0].transform.position;
        state = State.ToSource;
    }

    private void startGatherWater()
    {
        agent.enabled = false;

        state = State.GatherWater;

        StartCoroutine(gatherWater());
    }

    private void startToCell()
    {
        agent.enabled = true;

        var cell = Field.GetRandomCell();
        agent.destination = cell.transform.position;
        state = State.ToCell;
    }

    private void startUseWater()
    {
        agent.enabled = false;

        state = State.UseWater;

        StartCoroutine(useWater());
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        startToSource();
    }

    private IEnumerator gatherWater()
    {
        yield return new WaitForSeconds(1);

        createWaterBall();
        startToCell();
    }

    private IEnumerator useWater()
    {
        yield return new WaitForSeconds(1);

        if (waterBall != null)
        {
            Destroy(waterBall);
            waterBall = null;
        }
        //Instantiate(prefab, this.transform);
        startWait();
    }

    private void createWaterBall()
    {
        Debug.Assert(waterBall == null);
        waterBall = Instantiate(
            (GameObject)Resources.Load("Prefab/Main/WaterBall"),
            this.transform.position + Vector3.up * 2,
            Quaternion.identity,
            this.transform
            );
    }


}
