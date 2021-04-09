using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float targetScale = 1f;
    public float timeToLerp = 0.25f;
    float scaleModifier = 1;
    float startValue;
    Vector3 startScale;
    [SerializeField] float scaleInc = 0.1f;
    [SerializeField] int fishEaten = 0;

    bool canScale = true;

    TextManager textManager;

    void Start()
    {
        startValue = scaleModifier;
        startScale = transform.localScale;
        textManager = GameObject.FindObjectOfType<TextManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fishEaten == 20)
        {
            GameObject win = GameObject.Find("Win Text");
            win.GetComponent<Text>().enabled = true;
            win.GetComponent<EndScreen>().enabled = true;


            EventManager.TriggerEvent("End");
        }
    }

    void Size(float increment)
    {
        if (canScale)
        {
            StartCoroutine(LerpFunction(targetScale + increment, timeToLerp));
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(col);
        if (col.gameObject.CompareTag("Enemy"))
        {
            if (col.gameObject.GetComponent<EnemyMovement>().healthy == true)
            {
                Size(scaleInc);
                fishEaten++;
            }
            else if (col.gameObject.GetComponent<EnemyMovement>().healthy == false)
            {
                Size(-scaleInc);
                GetComponent<CinemachineImpulseSource>().GenerateImpulse();
                if (fishEaten > 0) { fishEaten--; }
                else if (fishEaten == 0)
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
            }

            textManager.updateText(fishEaten);
            Destroy(col.gameObject);
        }
    }

    IEnumerator LerpFunction(float endValue, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            canScale = false;
            scaleModifier = Mathf.Lerp(startValue, endValue, time / duration);
            transform.localScale = startScale * scaleModifier;
            time += Time.deltaTime;

            yield return null;
        }

        startScale = transform.localScale;
        canScale = true;
    }
}
