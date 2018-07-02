using UnityEngine;
using System.Collections;

public class JumpFrog : MonoBehaviour {


	//Cria um slider na hierarchy onde está o script.
	//É só adicionar o Range com o minimo e máximo valor possivel em cima da definicao da variavel
	[Range(0f, 10f)]
	public float moveSpeed = 4f;  // enemy move speed when moving

	// movement tracking
	[SerializeField]
	int _myWaypointIndex = 0; // used as index for My_Waypoints
	float _moveTime; 
	float _vx = 0f;
	bool _moving = true;
	public GameObject[] myWaypoints; // to define the movement waypoints

	// store references to components on the gameObject
	Transform _transform;
	Rigidbody2D _rigidbody;
	Animator _animator;
	Animation _animation;
	AudioSource _audio;

	public bool loopWaypoints = true; // should it loop through the waypoints

	[Tooltip("How much time is seconds to wait at each waypoint.")]
	public float waitAtWaypointTime = 1f;   // how long to wait at a waypoint


	void Awake() {
		// get a reference to the components we are going to be changing and store a reference for efficiency purposes
		_transform = GetComponent<Transform> ();

		_rigidbody = GetComponent<Rigidbody2D> ();
		if (_rigidbody==null) // if Rigidbody is missing
			Debug.LogError("Rigidbody2D component missing from this gameobject");

		_animator = GetComponent<Animator>();
		if (_animator == null) // if Animator is missing
			//	Debug.LogError("Animator component missing from this gameobject");

	
			_animation = GetComponent<Animation> ();


		// setup moving defaults
		_moveTime = 0f;
		_moving = true;

	}


	// if not stunned then move the enemy when time is > _moveTime
	void Update () {

			if (Time.time >= _moveTime) {
				EnemyMovement();

			} else {
			//	_animator.SetBool("Moving", false);
			}
	}

	// Move the enemy through its rigidbody based on its waypoints
	void EnemyMovement() {
		// if there isn't anything in My_Waypoints
		if ((myWaypoints.Length != 0) && (_moving)) {


			// make sure the enemy is facing the waypoint (based on previous movement)
			Flip (_vx);

			// determine distance between waypoint and enemy
			_vx = myWaypoints[_myWaypointIndex].transform.position.x-_transform.position.x;

			// if the enemy is close enough to waypoint, make it's new target the next waypoint
			if (Mathf.Abs(_vx) <= 0.05f) {
				// At waypoint so stop moving
				_rigidbody.velocity = new Vector2(0, 0);

				// increment to next index in array
				_myWaypointIndex++;

				// reset waypoint back to 0 for looping
				if(_myWaypointIndex >= myWaypoints.Length) {
					if (loopWaypoints)
						_myWaypointIndex = 0;
					else
						_moving = false;
				}

				// setup wait time at current waypoint
				_moveTime = Time.time + waitAtWaypointTime;
			} else {
				// enemy is moving
			//	_animator.SetBool("Moving", true);

				// Set the enemy's velocity to moveSpeed in the x direction.
				//só move o sapo se ele estiver pulando


				_animator.GetAnimatorTransitionInfo (0);


				if (_animator.GetCurrentAnimatorStateInfo (0).IsName ("animFrogJump") && AnimatorIsPlaying ()) {


					//Debug.LogError ("V    ");
				}
				_rigidbody.velocity = new Vector2(_transform.localScale.x * moveSpeed, _rigidbody.velocity.y);
			}

		}
	}

	bool AnimatorIsPlaying(){
		return _animator.GetCurrentAnimatorStateInfo(0).length >
			_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}

	// flip the enemy to face torward the direction he is moving in
	void Flip(float _vx) {

		// get the current scale
		Vector3 localScale = _transform.localScale;

		if ((_vx>0f)&&(localScale.x<0f))
			localScale.x*=-1;
		else if ((_vx<0f)&&(localScale.x>0f))
			localScale.x*=-1;

		// update the scale
		_transform.localScale = localScale;
	}
}
