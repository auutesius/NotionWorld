using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using NotionWorld.IO;
using NotionWorld.Events;
using UnityEngine.UI;
using ObjectPool = NotionWorld.Worlds.ObjectPool;

public class EnemySpawner : MonoBehaviour
{
    public TextAsset csvFile;

    public Text remainSecondsUI;

    public Image waveIcon;

    private DataTable spawnTable;

    public GameObject player;

    private const float refreshInterval = 1.0F;

    public float startInterval = 3F;

    public float waveInterval = 10F;

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
        StartCoroutine(WaveTipCorotinue(startInterval));
        yield return new WaitForSeconds(startInterval);
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
        var bulletObj = ObjectPool.GetObject("Spawn", "SkillBullets");
        var bullet = bulletObj.GetComponent<Spawn>();
        bullet.Target = player;
        bullet.gameObjectName = id;
        bullet.point = position;
        bullet.Launch(position, Vector2.zero);
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
