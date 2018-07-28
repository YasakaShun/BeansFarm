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
        updateAnim();
	}

    private void updateAI()
    {
    }

    private void updateAnim()
    {
        // anim.SetFloat("Speed", 0.1f);
        // anim.SetFloat("Direction", 0.1f);
    }
}
