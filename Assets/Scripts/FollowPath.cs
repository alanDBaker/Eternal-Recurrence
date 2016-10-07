using UnityEngine;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour 
{

	public enum FollowType {MoveTowards, Lerp}

	public FollowType Type = FollowType.MoveTowards;
	public PathDefinitions Path;
	public float speed = 1;
	public float MaxDistanceToGoal = 0.1f;

	private IEnumerator<Transform> _currentPoint = null;

	public void Start()
	{
		if (Path == null)
		{
			return;
		}

		_currentPoint = Path.GetPathsEnumrator();
		_currentPoint.MoveNext();

		if (_currentPoint.Current == null)
			return;

		transform.position = _currentPoint.Current.position;
	}

	public void Update()
	{
		if (_currentPoint == null || _currentPoint.Current == null)
			return;

		if (Type == FollowType.MoveTowards)
			transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime * speed);
		if (Type == FollowType.Lerp)
			transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * speed);

		var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;

		if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
			_currentPoint.MoveNext();
	}

}
