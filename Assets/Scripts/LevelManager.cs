using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

// the methods OrderBy() and ToList()
using System.Linq;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour 
{
    // properties
	public static LevelManager Instance {get; private set;}

	public Player Player {get; private set;}
    public NonShooterPlayer NonShooterPlayer {get; private set;}
	public CameraController Camera {get; private set;}
	public TimeSpan RunningTime {get {return DateTime.UtcNow - _started;}}
	public int CurrentTimeBonus
	{
		get
		{
			var secondDifference = (int) (BonusCutoffSeconds - RunningTime.TotalSeconds);
			return Mathf.Max(0, secondDifference) * BonusSecondMultiplier;
		}
	}

	private List<CheckPoint> _checkpoints;
	private int _currentCheckpointIndex;
    private DateTime _started;
	private int _savePoints;
    //private int _livesLeft;

    //unity interface
	public CheckPoint DebugSpawn;
	public int BonusCutoffSeconds;
	public int BonusSecondMultiplier;

	public void Awake()
	{
		_savePoints = GameManager.Instance.Points;
        //_livesLeft = GameManager.Instance.Lives;

        // other objects in level will be able to use this current object reference
		Instance = this;
	}


	public void Start()
	{
	    _checkpoints = FindObjectsOfType<CheckPoint>().OrderBy(t => t.transform.position.y).ToList();

        //Flag for debug spawn
		_currentCheckpointIndex = _checkpoints.Count > 0 ? 0 : -1;

        // cache local for player and camera
		Player = FindObjectOfType<Player>();
		Camera = FindObjectOfType<CameraController>();

		_started = DateTime.UtcNow;

	/*	var listeners = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerRespawnListener>();

		{
			for (var i = _checkpoints.Count - 1; i >= 0; i--)
			{
				var distance = ((MonoBehaviour)listener).transform.position.y - _checkpoints[i].transform.position.y;
				if (distance < 0)
					continue;

				_checkpoints[i].AssignObjectToCheckpoint(listener);
				break;
			}
		}*/

 // preprossesor directives, only works on editor
 #if UNITY_EDITOR
		if (DebugSpawn != null)
			   DebugSpawn.SpawnPlayer(Player);
		else if (_currentCheckpointIndex != -1)
			        _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);
 #else
		if (_currentCheckpointIndex != -1)
			_checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);
 #endif 
	}

	public void Update()
	{
		var isAtLastCheckpoint = _currentCheckpointIndex + 1 >= _checkpoints.Count;
		if (isAtLastCheckpoint)
			return;

		var distanceToNextCheckpoint = _checkpoints[_currentCheckpointIndex + 1].transform.position.y - Player.transform.position.y;
		if (distanceToNextCheckpoint >= 0)
			return; 

		_checkpoints[_currentCheckpointIndex].PlayerLeftCheckpoint();

		_currentCheckpointIndex++;

		_checkpoints[_currentCheckpointIndex].PlayerHitCheckpoint();

		GameManager.Instance.AddPoints(CurrentTimeBonus);

        // cache current points and lives
		_savePoints = GameManager.Instance.Points;
       // _livesLeft = GameManager.Instance.Lives;

        //start the current time bonus
		_started = DateTime.UtcNow; 
	}

	public void GotoNextLevel(string levelName)
	{
		StartCoroutine(GotoNextLevelCo(levelName));
	}

	private IEnumerator GotoNextLevelCo(string levelName)
	{
		Player.FinishLevel();

		//GameManager.Instance.AddPoints(CurrentTimeBonus);

		//FloatingText.Show("Level complete!", "CheckpointText", new CenteredTextPositioner(.2f));
		yield return new WaitForSeconds(0.5f);

		//FloatingText.Show(string.Format("{0} points!", GameManager.Instance.Points), "CheckpointText", new CenteredTextPositioner(.1f));		

		if (string.IsNullOrEmpty(levelName))
			SceneManager.LoadScene("StartScreen");
		else
            SceneManager.LoadScene(levelName); 
			
	} 

	public void KillPlayer()
	{
        //unity method to span multiple frames
		StartCoroutine(KillPlayerCo());
	}

    private IEnumerator KillPlayerCo()
	{
		Player.Kill();
        Camera.IsFollowing = false;

        //yield execution back into unity for time set
        yield  return new WaitForSeconds(0.5f);

		Camera.IsFollowing = true;

        // check to see if at a valid checkpoint
		if (_currentCheckpointIndex != -1)
			_checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);

		_started = DateTime.UtcNow;
         
		GameManager.Instance.ResetPoints(_savePoints);

        GameManager.Instance.SubtractLife();

        if (GameManager.Instance.Lives == 0)
        {
            Player.Kill();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Loser page");
        }
	} 
}


