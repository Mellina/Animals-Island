using UnityEngine;
using System.Collections;

public class JumpFrog2 : MonoBehaviour {


	bool isJumping = false;
	bool isFalling = false;
	//float jumpSpeed = 2f;

	void Update(){


		if (Input.GetKeyDown(KeyCode.Space) &&  isJumping == false  && isFalling == false)
		{
			isJumping = true;

			Jump();
		}

		if (isJumping == true && isFalling == false)
		{
			Jump();
		}

		if (isFalling == true && isJumping == false)
		{
			Fall();
		}
	}


	public void Jump()
	{

		/*if (currJump < allowedJump)
		{
			float temp = 0.0f;
			temp = Mathf.Sin(Time.deltaTime) * jumpSpeed;
			currJump += temp;
			controller.Move(Vector3.up * temp * Time.deltaTime * jumpSpeed);
		}
		else
		{
			isJumping = false;
			isFalling = true;
		}*/
	}

	public void Fall()
	{
	/*	if (isGrounded == false)
		{
			float temp = 0.0f;
			temp = Mathf.Sin(Time.deltaTime) * jumpSpeed;
			currJump -= temp;
			controller.Move(Vector3.up * temp * Time.deltaTime * jumpSpeed * -1);
		}
		else
		{
			isFalling = false;
			viewScript.lockY = false;
			currJump = 0;
		}
		*/
	}

}
