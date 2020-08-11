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

    private DataTable spawnTable;

    public GameObject player;

    private const float interval = 1.0F;

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
        WaitForSeconds second = new WaitForSeconds(interval);
        int i = 0;
        float timer = 0;
        DataRow row = null;
        float time = 0;
        while (i < spawnTable.Rows.Count)
        {
            if(row == null)
            {
                row = spawnTable.Rows[i];
                time = float.Parse(row["Time"] as string);
                if(!spawnStarted)
                {
                    spawnStarted = true;
                    StartCoroutine(WaveTipCorotinue(time));
                }
            }
            if (timer >= time)
            {
                string id = row["ID"] as string;
                Vector2 position = new Vector2(float.Parse(row["PositionX"] as string), float.Parse(row["PositionY"] as string));
                SpawnEnemy(id, position);
                i++;
                row = null;
            }
            else
            {
                if(time - timer >= waveInterval && !inWaveTime)
                {
                    StartCoroutine(WaveTipCorotinue(time - timer));
                }
            }
            yield return second;
            timer += interval;
        }
        EventCenter.DispatchEvent(new SpawnFinishedEventArgs(timer));
    }

    private void SpawnEnemy(string id, Vector2 position)
    {
        var enemy = ObjectPool.GetObject(id, "Entities");
        enemy.transform.position = position;

        Behavior behavior = enemy.GetComponent<Behavior>();
        if(behavior != null)
        {
            behavior.SetVariableValue("TrackTarget", player);
            behavior.EnableBehavior();
            behavior.Start();
        }
    }

    private IEnumerator WaveTipCorotinue(float nextWaveTime)
    {
        remainSecondsUI.gameObject.SetActive(true);
        remainSecondsUI.text = Mathf.RoundToInt(nextWaveTime).ToString();
        inWaveTime = true;
        
        WaitForSeconds second = new WaitForSeconds(interval);
        while(nextWaveTime > 0)
        {
            yield return second;
            nextWaveTime -= interval;

            remainSecondsUI.text = Mathf.RoundToInt(nextWaveTime).ToString();
        }
        inWaveTime = false;

        remainSecondsUI.gameObject.SetActive(false);
    }
}
