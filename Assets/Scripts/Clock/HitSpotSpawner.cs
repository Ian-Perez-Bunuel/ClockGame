using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class HitSpotSpawner : MonoBehaviour
{
    [Header("Needed Info")]
    [SerializeField] ScoreDisplay scoreDisplay;

    [Header("Spawning")]
    [SerializeField] GameObject radiusSetter;
    float radius;
    [SerializeField] float timeBetweenSpawns = 3f;

    [Header("Object Pooling")]
    [SerializeField] GameObject hitbarPrefab;
    // Object pool
    [SerializeField] int poolCapacity = 10;
    HitBar[] hitbarPool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Init pool
        hitbarPool = new HitBar[poolCapacity];
        for (int i = 0; i < poolCapacity; i++)
        {
            hitbarPool[i] = Instantiate(hitbarPrefab, transform).GetComponent<HitBar>();
            hitbarPool[i].Init(scoreDisplay);
        }

        radius = radiusSetter.transform.localScale.x * 0.75f;
        GetRandPointInCircle();

        StartCoroutine(SpawnHitbarsTimer());
    }

    void SpawnHitbar()
    {
        foreach (HitBar hitbar in hitbarPool)
        {
            if (!hitbar.gameObject.activeSelf)
            {
                Vector3 spawnPos = GetRandPointInCircle();
                Vector2 direction = ((Vector2)transform.position - (Vector2)spawnPos).normalized;
                float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                hitbar.Spawn(spawnPos, rotation);

                break;
            }
        }
    }

    public Vector2 GetRandPointOnCircle()
    {
        Vector2 centre = transform.position;

        float angleDegrees = Random.Range(0f, 360f);
        float angleRadians = angleDegrees * Mathf.Deg2Rad;

        return centre + new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians)) * radius;
    }

    public Vector2 GetRandPointInCircle()
    {
        Vector2 centre = transform.position;
        Vector2 randPoint = centre + Random.insideUnitCircle * radius;

        return randPoint;
    }

    IEnumerator SpawnHitbarsTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnHitbar();
        }
    }
}
