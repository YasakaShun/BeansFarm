using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beans : MonoBehaviour
{
    public GameObject target;
    UnityEngine.AI.NavMeshAgent agent;

	// Use this for initialization
	void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        updateAI();
	}

    private void updateAI()
    {
    }
}
