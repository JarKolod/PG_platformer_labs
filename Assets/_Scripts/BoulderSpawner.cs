using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    private BoxCollider2D triggerZone;

    [SerializeField] private GameObject boulderPrefab;
    [SerializeField] private float spawningBouldersTime = 1f;
    [SerializeField] private float BoulderSpawnRate = 0.3f;

    [SerializeField] private BoxCollider2D spawningZoneColl;
    [SerializeField] private Canvas boulderHUD;
    [SerializeField] private TextMeshProUGUI startCountDown;
    [SerializeField] private TextMeshProUGUI timer;


    private bool hasBeenTriggered = false;

    private void Start()
    {
        triggerZone = GetComponent<BoxCollider2D>();
    }

    private IEnumerator spawnBoulders()
    {
        if (!hasBeenTriggered)
        {
            hasBeenTriggered = true;
            boulderHUD.gameObject.SetActive(true);

            Bounds spawnZone = spawningZoneColl.bounds;
            startCountDown.text = "3";
            yield return new WaitForSeconds(1f);
            startCountDown.text = "2";
            yield return new WaitForSeconds(1f);
            startCountDown.text = "1";
            yield return new WaitForSeconds(1f);
            startCountDown.text = " ";

            StartCoroutine(TimerBoulders());

            float timeCounter = 0f;
            while (timeCounter <= spawningBouldersTime)
            {
                Instantiate(boulderPrefab, new Vector2(Random.Range(spawnZone.min.x, spawnZone.max.x), spawningZoneColl.transform.position.y - spawnZone.size.y), Quaternion.identity);

                yield return new WaitForSeconds(BoulderSpawnRate);

                timeCounter += BoulderSpawnRate;
            }
        }

        hasBeenTriggered = false;
    }

    private IEnumerator TimerBoulders()
    {
        timer.text = spawningBouldersTime.ToString();
        float count = spawningBouldersTime;
        while(count > 0f)
        {
            count -= Time.deltaTime;
            timer.text = count.ToString();
            yield return null;
        }

        timer.text = " ";
        boulderHUD.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            StartCoroutine(spawnBoulders());
        }
    }
}
