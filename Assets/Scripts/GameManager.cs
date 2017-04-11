// Do not put MonoBehaviour or it will get Destroyed across levels

public class GameManager
{
    // singleton. static reference
	private static GameManager _instance;
    private int _lives = 11;

    // check to see if the _instance is not null and if is null create new _instance
	public static GameManager Instance {get{return _instance ?? (_instance = new GameManager());}}

	public int Points
    {
        get;

        private set;
    }


    public int Lives
    {
        get
        {
            return _lives;
        }
        set
        {
            _lives = 11;
        }
    }   

    // private constructor. nobody but GameManager can instance itself
    // bypass the auto contructor 
	private GameManager()
	{
	}

	public void Reset()
	{
		Points = 0;
	}

	public void ResetPoints(int points)
	{
		Points = points;
	}

    public void SubtractLife()
    {
        _lives -= 1;
    }

    public void AddLife()
    {
        _lives += 1;
    }

	public void AddPoints(int pointsToAdd)
	{
		Points += pointsToAdd;
	}
}
