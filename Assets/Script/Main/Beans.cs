using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Beans : MonoBehaviour
{
    public GameObject target;

    private NavMeshAgent agent;
    private Animator anim;

	void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.destination = target.transform.position;
    }

    void Update()
    {
        updateAI();
	}

    void FixedUpdate()
    {
        updateAnim();
    }

    private void updateAI()
    {
    }

    private void updateAnim()
    {
        // NavMesh での移動速度にあわせてモーションを変える
        var vel = agent.velocity;
        anim.SetFloat("Speed", vel.magnitude);
    }
}
