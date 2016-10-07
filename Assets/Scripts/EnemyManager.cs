using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public EnemyAI Enemy { get; private set; }
    
	public void Start()
    {
        Enemy = FindObjectOfType<EnemyAI>();
    }

    public void KillEnemy()
    {
        StartCoroutine(KillEnemyCo());
    }

    private IEnumerator KillEnemyCo()
    {
        Enemy.Kill();

        yield return new WaitForSeconds(2f);
    }
}
