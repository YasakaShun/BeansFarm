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
                if (isReached())
                {
                    startGatherWater();
                }
                break;
            case State.GatherWater:
                break;
            case State.ToCell:
                if (isReached())
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
        state = State.Wait;

        agent.enabled = false;

        StartCoroutine(wait());
    }

    private void startToSource()
    {
        state = State.ToSource;

        agent.enabled = true;

        var source = Source.GetRandomFountain();
        var pos = source.transform.position;
        pos.y = 0;
        agent.destination = pos;
        agent.stoppingDistance = source.transform.localScale.x * 0.5f + agent.radius;
    }

    private void startGatherWater()
    {
        state = State.GatherWater;

        agent.enabled = false;

        StartCoroutine(gatherWater());
    }

    private void startToCell()
    {
        state = State.ToCell;

        agent.enabled = true;

        var cell = Field.GetRandomCell();
        agent.destination = cell.transform.position;
        agent.stoppingDistance = 0;
    }

    private void startUseWater()
    {
        state = State.UseWater;

        agent.enabled = false;

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

    private bool isReached()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            return true;
        }
        return false;
    }

}
