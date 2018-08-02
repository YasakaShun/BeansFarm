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

    private enum State
    {
        Wait,
        ToSource,
        ToCell,
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
        updateAI();
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
                startToSource();
                break;
            case State.ToSource:
                if (transform.position.Equals(agent.destination))
                {
                    startToCell();
                }
                break;
            case State.ToCell:
                if (transform.position.Equals(agent.destination))
                {
                    Instantiate(prefab, this.transform);
                    startWait();
                }
                break;
            default:
                Debug.Assert(false, "unknown state.");
                break;
        }
    }

    private void updateAI()
    {
        switch (state)
        {
            case State.Wait:
                break;
            case State.ToSource:
                break;
            case State.ToCell:
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
    }

    private void startToSource()
    {
        agent.enabled = true;

        var source = GameObject.FindGameObjectsWithTag("Source");
        agent.destination = source[0].transform.position;
        state = State.ToSource;
    }

    private void startToCell()
    {
        agent.enabled = true;

        var cell = Field.GetRandomCell();
        agent.destination = cell.transform.position;
        state = State.ToCell;
    }


}
