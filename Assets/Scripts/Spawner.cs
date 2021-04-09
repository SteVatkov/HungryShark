using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    float time = 0;
    [SerializeField] float duration = 3f;
    float durationOffset;
    float durationDecrease;
    bool canSpawn = true;

    [SerializeField] GameObject fishPrefab;

    private void OnEnable()
    {
        EventManager.StartListening("End", endSpawn);
    }

    // Start is called before the first frame update
    void Start()
    {
        durationOffset = Random.Range(0.5f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSpawn) { return; }
        time += Time.deltaTime;
        if(time > duration + durationOffset - durationDecrease)
        {
            spawnFish();
            time = 0;
            durationOffset = Random.Range(1, 4);
            durationDecrease += 0.02f;
        }
    }

    void spawnFish()
    {
        Vector3 pos = new Vector3(transform.position.x, Random.Range(-3, 3), 0);
        GameObject fish = Instantiate(fishPrefab, pos, transform.rotation) as GameObject;
        fish.GetComponent<EnemyMovement>().healthy = randomHealthy();
    }

    bool randomHealthy()
    {
        
        if (Random.value >= 0.6)
        {
            return true;
        }
        return false;
        
    }

    void endSpawn()
    {
        canSpawn = false;
    }
}
