using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{

	private CharacterController controller;
	private Vector3 direction;
	public float forwardSpeed;

	private int desiredLane = 1; //0:left 1:middle 2:right
	public float laneDistance = 4; //the distance between two lanes

	public float jumpForce;
	public float Gravity = -20;

	void Start () 
	{
		controller = GetComponent<CharacterController> ();
		
	}
	

	void Update () 
	{
		if (!PlayerManager.isGameStarted)
			return;
		
		direction.z = forwardSpeed;


		if (controller.isGrounded) 
		{
			direction.y = -1;
			if (Input.GetKeyDown (KeyCode.UpArrow)) { 
				Jump ();
			}
		} else 
		{
			direction.y += Gravity *Time.deltaTime;
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) 
		{
			desiredLane++;
			if (desiredLane == 10)
				desiredLane = 5;
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow))
		{
			desiredLane--;
			if (desiredLane == -1)
				desiredLane = 0;
		}

		Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

		if (desiredLane == 0) {
			targetPosition += Vector3.left * laneDistance;
		} else if (desiredLane == 2) 
		{
			targetPosition += Vector3.right * laneDistance;
		}


		if (transform.position == targetPosition)
			return;
		Vector3 diff = targetPosition - transform.position;
		Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
		if (moveDir.sqrMagnitude < diff.sqrMagnitude)
			controller.Move (moveDir);
		else
			controller.Move (diff);
	}

	private void FixedUpdate()
	{
		if (!PlayerManager.isGameStarted)
			return;
		controller.Move (direction * Time.fixedDeltaTime);
	}

	private void Jump()
	{
		direction.y = jumpForce;
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.transform.tag == "Obstacle") 
		{
			PlayerManager.gameOver = true;
			FindObjectOfType<AudioManager> ().PlaySound ("Crash");
		}
	}
}
