using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Controls the Laser Sight for the player's aim
/// </summary>
public class JumpTrajectory : MonoBehaviour
{
	// Reference to the LineRenderer we will use to display the simulated path
	public LineRenderer sightLine;
	public LayerMask rayMask;
	float gravityMultiplier;
	Color originalStartColor;
	public float lineFadeSpeed;
	// Reference to a Component that holds information about fire strength, location of cannon, etc.
	public PlayerJump playerJump;

	// Number of segments to calculate - more gives a smoother line
	public int segmentCount = 20;

	// Length scale for each segment
	public float segmentScale = 10f;

	// gameobject we're actually pointing at (may be useful for highlighting a target, etc.)
	private Collider2D _hitObject;
	public Collider2D hitObject { get { return _hitObject; } }

	JellySprite j;

	void Awake(){
		j = playerJump.GetComponent<JellySprite>();
		gravityMultiplier = j.m_GravityScale;
		originalStartColor = sightLine.startColor;
	}

	void FixedUpdate()
	{
		if(playerJump.startDrag && playerJump.jumpForce.magnitude >= 100f){
			FadeLine(false);
			simulatePath();
		}else if(!playerJump.startDrag || playerJump.jumpForce.magnitude < 100f) {

			FadeLine(true);
		}
	}


	void FadeLine (bool fadeOut){
		Color sColor = sightLine.startColor;
		if (fadeOut) {
			if (sightLine.startColor.a > 0) {
				sColor.a -= Time.deltaTime * lineFadeSpeed;
				sightLine.startColor = sColor;
			}
		}else if(!fadeOut){
			if (sightLine.startColor.a < originalStartColor.a) {
				sColor.a += Time.deltaTime * lineFadeSpeed;
				sightLine.startColor = sColor;
			}
		}
	}

	/// <summary>
	/// Simulate the path of a launched ball.
	/// Slight errors are inherent in the numerical method used.
	/// </summary>
	void simulatePath()
	{
		Vector2[] segments = new Vector2[segmentCount];

		// The first line point is wherever the player's cannon, etc is
		segments[0] = transform.position;

		// The initial velocity
		Vector2 segVelocity = playerJump.jumpForce * Time.deltaTime;

		// reset our hit object
		_hitObject = null;

		for (int i = 1; i < segmentCount; i++)
		{
			// Time it takes to traverse one segment of length segScale (careful if velocity is zero)
			float segTime = (segVelocity.sqrMagnitude != 0) ? segmentScale / segVelocity.magnitude : 0;

			// Add velocity from gravity for this segment's timestep
			segVelocity = segVelocity + Physics2D.gravity * gravityMultiplier * j.m_Mass *5.5f* segTime;

			// Check to see if we're going to hit a physics object
			RaycastHit2D hit = Physics2D.Raycast(segments[i - 1], segVelocity, segmentScale, rayMask);
			if (hit.collider != null)
			{
				// remember who we hit
				_hitObject = hit.collider;


				// set next position to the position where we hit the physics object
				segments[i] = segments[i - 1] + segVelocity.normalized * hit.distance;
				// correct ending velocity, since we didn't actually travel an entire segment
				segVelocity = segVelocity - Physics2D.gravity * gravityMultiplier * (segmentScale - hit.distance) / segVelocity.magnitude;
				// flip the velocity to simulate a bounce
				//segVelocity = Vector2.Reflect(segVelocity, hit.normal);

				/*
				 * Here you could check if the object hit by the Raycast had some property - was 
				 * sticky, would cause the ball to explode, or was another ball in the air for 
				 * instance. You could then end the simulation by setting all further points to 
				 * this last point and then breaking this for loop.
				 */

			}
			// If our raycast hit no objects, then set the next position to the last one plus v*t
			else 
			{
				segments[i] = segments[i - 1] + segVelocity * segTime;

			}
		}

		// At the end, apply our simulations to the LineRenderer

		// Set the colour of our path to the colour of the next ball
		//	Color startColor = Color.red;
		//	Color endColor = startColor;
		//	startColor.a = 1;
		//	endColor.a = 0;
		//	sightLine.SetColors(startColor, endColor);

		sightLine.positionCount = segmentCount;

		for (int i = 0; i < segmentCount; i++)
			sightLine.SetPosition(i, segments[i]);
	}
}
