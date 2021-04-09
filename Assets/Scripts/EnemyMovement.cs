using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float speed = 1f;
    [SerializeField] float minSpeed = 1f;
    [SerializeField] float maxSpeed = 2f;
    public bool healthy;
    Vector3 startScale;
    float scaleModifier;

    float time = 0;
    [SerializeField] float despawnDuration = 15f;


    private void OnEnable()
    {
        EventManager.StartListening("End", endSpawn);
    }

    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
        scaleModifier = Random.Range(0.7f, 1.3f);
        transform.localScale = startScale * scaleModifier;

        speed = Random.Range(minSpeed, maxSpeed);

        if (healthy)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (!healthy)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;

        time += Time.deltaTime;
        if (time > despawnDuration)
        {
            Destroy(gameObject);
        }
    }

    void endSpawn()
    {
        Destroy(gameObject);
    }
}
