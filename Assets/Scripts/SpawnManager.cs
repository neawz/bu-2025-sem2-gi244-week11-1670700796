using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;

    //private Coroutine byeRoutine;

    void Start()
    {
        //InvokeRepeating(nameof(RandomSpawn), 0, 3);
        //byeRoutine = StartCoroutine(Goodbye());
        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        //if (Time.time > 5)
        //{
        //    //StopAllCoroutines(); Stop all Coroutines that THIS SCRIPT Started
        //    //StopCoroutine(byeRoutine);
        //}
    }

    void RandomSpawn()
    {
        var index = Random.Range(0, spawnPoints.Length);
        var spawnPoint = spawnPoints[index];
        Instantiate(enemyPrefab,
            spawnPoint.position,
            Quaternion.identity
        );
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            RandomSpawn();
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator Hello(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Hello " + Time.frameCount);
        yield return null;
    }

    IEnumerator Goodbye()
    {
        while (true)
        {
            Debug.Log("Bye " + Time.frameCount + " " + Time.time);
            yield return new WaitForSeconds(1f);
            yield return Hello(4);
            yield return new WaitForSeconds(1f);

            if (Time.time > 5)
            {
                yield break;
            }
        }
    }
}
