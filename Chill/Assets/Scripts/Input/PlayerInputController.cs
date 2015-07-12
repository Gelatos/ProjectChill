using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour {
	
	//_______________________________________________ [PROTECTED VARIABLES]
	[SerializeField] 
	protected MovementController movementController;
	[SerializeField] 
	protected AttackController attackController;

	protected float minimumDirectionalInput = 0.1F;
	protected float moveDirection;
	protected float lastMoveDirection;
	protected float dashTimer = 0.0F;

	#region Getters and Setters
	
	public MovementController MovementControl {
		get {
			return movementController;
		}
		set {
			movementController = value;
		}
	}

	public AttackController AttackControl {
		get {
			return attackController;
		}
		set {
			attackController = value;
		}
	}

	#endregion

	#region Mono Functions

	// Update is called once per frame
	void Update () {

		// ========================================= [Movement Control]

		moveDirection = (Input.GetAxis ("Horizontal"));

		// Dash Control
		if (Input.GetButtonDown ("Horizontal")) {
			if (dashTimer <= 0.0F || Mathf.Sign (moveDirection) != Mathf.Sign (lastMoveDirection)) {
				lastMoveDirection = moveDirection;
				dashTimer = 0.5F;
			} else {
				dashTimer = 0.0F;
				movementController.Dash ();
			}
		}
		if (dashTimer > 0.0F) {
			dashTimer -= Time.deltaTime;
		}

		// move the character
		movementController.Move (moveDirection);

		// Jump Control
		if (Input.GetButtonDown("Jump")) {
			movementController.Jump ();
		}

		// ========================================= [Attack Control]

		if (Input.GetButtonDown ("Fire1")) {
			// determine the directional
			float hor = Input.GetAxis ("Horizontal");
			float vert = Input.GetAxis ("Vertical");

			// determine what kind of att
			AttackController.AttackDirection dir;
			if (Mathf.Abs (hor) > Mathf.Abs (vert) && Mathf.Abs (hor) > minimumDirectionalInput) {
				dir = AttackController.AttackDirection.Side;
			} else if (Mathf.Abs (vert) > Mathf.Abs (hor) && Mathf.Abs (vert) > minimumDirectionalInput) {
				if (vert > 0) {
					dir = AttackController.AttackDirection.Up;
				} else {
					dir = AttackController.AttackDirection.Down;
				}
			} else {
				dir = AttackController.AttackDirection.Neutral;
			}

			attackController.StartAttack (dir, AttackController.AttackTrigger.Trigger01);
		}
	}

	#endregion
}
