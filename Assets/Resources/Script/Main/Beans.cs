using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Beans : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private State state;
    private GameObject playerPrefab;
    private GameObject waterBallPrefab;

    private GameObject targetFountain = null;
    private GameObject targetCell = null;
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
        playerPrefab = (GameObject)Resources.Load("Prefab/Main/unitychan");
        waterBallPrefab = (GameObject)Resources.Load("Prefab/Main/WaterBall");

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

        targetFountain = Source.GetRandomFountain();
        var pos = targetFountain.transform.position;
        pos.y = 0;
        agent.destination = pos;
        agent.stoppingDistance = targetFountain.transform.localScale.x * 0.5f + agent.radius;
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

        targetCell = Field.GetRandomCell();
        agent.destination = targetCell.transform.position;
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

        Debug.Assert(targetFountain != null);

        var power = targetFountain.GetComponent<Fountain>().GetPower();
        createWaterBall(power);
        startToCell();
    }

    private IEnumerator useWater()
    {
        yield return new WaitForSeconds(1);

        if (targetCell != null)
        {
            if (waterBall != null)
            {
                var cellScript = targetCell.GetComponent<Cell>();
                cellScript.waterPower +=
                    waterBall.GetComponent<WaterBall>().power;
                if (30.0f < cellScript.waterPower)
                {
                    Instantiate(playerPrefab, this.transform);
                }
                Destroy(waterBall);
                waterBall = null;
            }
            targetCell = null;

            startWait();
        }
        else
        {
            if (waterBall != null)
            {
                startToCell();
            }

        }
    }

    private void createWaterBall(float power)
    {
        Debug.Assert(waterBall == null);
        waterBall = Instantiate(
            waterBallPrefab,
            this.transform.position + Vector3.up * 2,
            Quaternion.identity,
            this.transform
            );
        waterBall.GetComponent<WaterBall>().power = power;
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
