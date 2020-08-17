using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using NotionWorld.IO;
using NotionWorld.Events;
using UnityEngine.UI;
using BehaviorDesigner.Runtime;
using ObjectPool = NotionWorld.Worlds.ObjectPool;

public class EnemySpawner : MonoBehaviour
{
    public TextAsset csvFile;

    public Text remainSecondsUI;

    public Image waveIcon;

    private DataTable spawnTable;

    public GameObject player;

    private const float refreshInterval = 1.0F;

    public float enterspeed;

    public float enterDistance;

    public float waveInterval = 10F;

    private bool spawnStarted = false;

    private bool inWaveTime = false;

    private void Awake()
    {
        spawnTable = DataTableCreator.Create(csvFile, "SpawnTable");
    }

    void Start()
    {
        StartCoroutine(SpawnCorotinue());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    private IEnumerator SpawnCorotinue()
    {
        WaitForSeconds second = new WaitForSeconds(refreshInterval);
        int i = 0;
        float timer = 0;
        DataRow row = null;
        float time = 0;
        while (i < spawnTable.Rows.Count)
        {
            if (row == null)
            {
                row = spawnTable.Rows[i];
                time = float.Parse(row["Time"] as string);
                if (!spawnStarted)
                {
                    spawnStarted = true;
                    StartCoroutine(WaveTipCorotinue(time));
                }
            }
            if (timer >= time)
            {
                string id = row["ID"] as string;
                Vector2 position = new Vector2(float.Parse(row["PositionX"] as string), float.Parse(row["PositionY"] as string));
                //StartCoroutine(SpawnEnemyCorotinue(id, position));
                SpawnEnemy(id, position);
                i++;
                row = null;
            }
            else
            {
                if (time - timer >= waveInterval && !inWaveTime)
                {
                    StartCoroutine(WaveTipCorotinue(time - timer));
                }
            }
            yield return second;
            timer += refreshInterval;
        }
        EventCenter.DispatchEvent(new SpawnFinishedEventArgs(timer));
    }

    private void SpawnEnemy(string id, Vector2 position)
    {
        var enemy = ObjectPool.GetObject(id, "Entities");

        enemy.transform.position = position;

        Behavior behavior = enemy.GetComponent<Behavior>();
        if (behavior != null)
        {
            behavior.SetVariableValue("TrackTarget", player);
            behavior.EnableBehavior();
            behavior.Start();
        }
    }

    private IEnumerator SpawnEnemyCorotinue(string id, Vector2 position)
    {
        var enemy = ObjectPool.GetObject(id, "Entities");
        int layer = enemy.layer;
        enemy.layer = LayerMask.NameToLayer("Ingore Collision");
        enemy.transform.position = position;
        Vector2 currentPosition = position;
        Vector2 targetPosition = Physics2D.Raycast(currentPosition, (Vector2)player.transform.position - currentPosition, 20, 1 << LayerMask.NameToLayer("Default")).point;

        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        float timer = ((targetPosition - currentPosition).magnitude + enterDistance) / enterspeed;
        Vector2 movement = (targetPosition - currentPosition).normalized * enterspeed * Time.fixedDeltaTime;
        while (timer > 0)
        {
            enemy.transform.position += (Vector3)movement;
            timer -= Time.fixedDeltaTime;
            yield return wait;
        }
        enemy.layer = layer;

        Behavior behavior = enemy.GetComponent<Behavior>();
        if (behavior != null)
        {
            behavior.SetVariableValue("TrackTarget", player);
            behavior.EnableBehavior();
            behavior.Start();
        }
    }

    private IEnumerator WaveTipCorotinue(float nextWaveTime)
    {
        remainSecondsUI.enabled = true;
        waveIcon.enabled = true;
        remainSecondsUI.text = Mathf.RoundToInt(nextWaveTime).ToString();
        inWaveTime = true;

        WaitForSeconds second = new WaitForSeconds(refreshInterval);
        while (nextWaveTime > 0)
        {
            yield return second;
            nextWaveTime -= refreshInterval;

            remainSecondsUI.text = Mathf.RoundToInt(nextWaveTime).ToString();
        }
        inWaveTime = false;

        remainSecondsUI.enabled = false;
        waveIcon.enabled = false;
    }
}
