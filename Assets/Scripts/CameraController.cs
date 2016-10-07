using UnityEngine;
using System.Collections;

// Reference: sample code from unity
public class CameraController : MonoBehaviour 
{
	// reference to the player's transform
	public Transform Player;

	// Distance in the vector axis the player can move before camera follows
	public Vector2 Margin;

	// How smoothly the camera catches up with it's target movement
	public Vector2 Smoothing;

	//Setting of a boxcollier for the level
	public BoxCollider2D Bounds;

	// holders for bounding box
	private Vector3 _min, _max;

    public bool IsFollowing { get; set; }

	// Called once in the lifetime of the script; initialize variables
	public void Start()
	{	
		// The bounds world space bounding area of the collider
		_min = Bounds.bounds.min;
		_max = Bounds.bounds.max;
	}

	// Update each frame
	public void  LateUpdate()
	{
		// where the is the camera position
		var x = transform.position.x;
		var y = transform.position.y;

		if (Player)
		{
			// if gone past the margin; assign new value to the x position
			if (Mathf.Abs(x - Player.position.x) > Margin.x)
			{
				x = Mathf.Lerp(x, Player.position.x, Smoothing.x * Time.deltaTime);
			}

			// if gone past the margin; assign new value to the y position
			if (Mathf.Abs(y - Player.position.y) > Margin.y)
			{
				y = Mathf.Lerp(y, Player.position.y, Smoothing.y * Time.deltaTime);
			}

			// grab the current camera halfwidth  
			var cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float) Screen.width / Screen.height);

			// clamps a value between a min and max  (value, min, max)
			x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
			y = Mathf.Clamp(y, _min.y + GetComponent<Camera>().orthographicSize, _max.y - GetComponent<Camera>().orthographicSize);

			// the new position for the camera 
			transform.position = new Vector3(x, y, transform.position.z);
		}
	}
}
