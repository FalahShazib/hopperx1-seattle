﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBehaviour : MonoBehaviour
{
	public GameObject[] checkpoints;

	private int currCheckpointIndex = 0;
	private GameObject currCheckpoint;
	private Animator anim;
	private GameObject oldCheckpoint;
	private Collider mostRecentTrigger;
	private bool travelling = true;

	// ////////////////////////////////////////////////////////////////////////////////
	// This is an added check to allow us to choose when to jump into the hole
	// Uncomment at the relevant step
	//private bool holeClear;
	//private GameObject itemToMove;


    // Start is called before the first frame update and is used for initialization
    void Start()
    {
		anim = GetComponentInChildren<Animator>();
		// selecting and looking at the first checkpoint
		currCheckpoint = checkpoints[currCheckpointIndex];
		transform.LookAt(currCheckpoint.transform);

		// /////////////////////////////////////////////////////////////////////////
		// This statement sets the hopping parameter to true triggering the hops
		// Uncomment it at the relevant step
		//anim.SetBool("hopping", true);
    }

    // Update is called once per frame
    void Update()
    {
		SetCheckpoint();

		// ////////////////////////////////////////////////////////////////////////
		// This is for checking user Input
		// Uncomment at a later step
		//CheckForClick();

		// angles rabbit towards checkpoint
		if (travelling)
		{
			Vector3 targetPosition = new Vector3(currCheckpoint.transform.position.x, 2.36f, currCheckpoint.transform.position.z);
			transform.LookAt(targetPosition, Vector3.up);
		}
    }

	// updates target checkpoint when we've reached the current one
	private void SetCheckpoint()
	{
		float dist = Vector3.Distance(currCheckpoint.transform.position, transform.position);
		if (dist < 5 && currCheckpointIndex < checkpoints.Length -1)
		{
			if (currCheckpoint.gameObject.name != "tree"
				&& currCheckpoint.gameObject.name != "bush"
				&& currCheckpoint.gameObject.name != "rock")
			{
				currCheckpointIndex++;
			}
			currCheckpoint = checkpoints[currCheckpointIndex];
		}
		if (dist < 3.6 && currCheckpointIndex == checkpoints.Length -1)
		{
			anim.SetBool("hopping", false);
			travelling = false;

			// ////////////////////////////////////////////////////////////////////////////
			// Comment this out or delete it when implementing hole checking
			anim.SetBool("Jump", true);

			// ////////////////////////////////////////////////////////////////////////////
			// The following code checks if the hole is clear before jumping
			// Uncomment at the relevant step
			//if (holeClear)
			//{
				//anim.SetBool("Jump", true);
			//}
		}
	}

	private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.name == "tree" || other.gameObject.name == "bush" || other.gameObject.name == "rock")
			&& (mostRecentTrigger != other))
		{
			transform.LookAt(other.gameObject.transform.position);
			anim.SetBool("hopping", false);
			int explore = Random.Range(1, 3);
			anim.SetInteger("explore", explore);
			if (currCheckpoint != other.gameObject)
			{
				oldCheckpoint = currCheckpoint;
				currCheckpoint = other.gameObject;
			}
			// to avoid getting stuck in a loop
			mostRecentTrigger = other;
		}
		if (other.gameObject.name == "coverbush")
		{
			// //////////////////////////////////////////////////////////
			// This code detects the cover bush
			// Uncomment at the relevant step
			//holeClear = false;
			//itemToMove = other.gameObject;
		}
    }

	private void RenableHopping()
	{
		currCheckpoint = oldCheckpoint;
		anim.SetInteger("explore", 0);
		anim.SetBool("hopping", true);
	}

	// This function checks for clicking
	private void CheckForClick()
	{
		if (Input.GetMouseButtonDown(0) && !travelling)
		{
			// /////////////////////////////////////////////////////////////////
			// Causes our bush to disappear and allow rabbit to jump
			// Uncomment at the relevant step
			//itemToMove.SetActive(false);
			//holeClear = true;
		}
	}
}
