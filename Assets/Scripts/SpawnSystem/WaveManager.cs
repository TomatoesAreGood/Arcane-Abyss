using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public SpawnManager SpawnManager;
    private TextMeshProUGUI _waveDisplay;
    private float currentWaveNum;
    // Start is called before the first frame update
    void Start()
    {
        _waveDisplay = GetComponent<TextMeshProUGUI>();
        _waveDisplay.enabled = !_waveDisplay.enabled;
         currentWaveNum = SpawnManager.GetWaveNum();

    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnManager.GetWaveNum() != currentWaveNum)
        {
            currentWaveNum = SpawnManager.GetWaveNum();
            StartCoroutine(DisplayManager());

            Debug.Log("switch wave");
        }
    }

    IEnumerator DisplayManager()
    {
        _waveDisplay.enabled = true;
        _waveDisplay.text = $"WAVE: {SpawnManager.GetWaveNum()} \n SPAWN RATE : One Enemy Per {SpawnManager.GetSpawnRate()} Seconds";
        yield return new WaitForSeconds(3.5f);
        _waveDisplay.enabled = false;

    }
}
