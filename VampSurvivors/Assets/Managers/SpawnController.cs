using System.Collections;
using Managers;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            UnitManager.Instance.FindEnemy(UnitType.Bat).GetComponent<Unit>().Spawn(GetRandomSpawnPoint());
            yield return new WaitForSeconds(0.5f);
        }
    }

    Vector2 GetRandomSpawnPoint()
    {
        var x = UnityEngine.Random.Range(-12.0f, 4.5f);
        var y = UnityEngine.Random.Range(12.0f, -6.25f);
        
        return new Vector2(x,y);
    }
}
