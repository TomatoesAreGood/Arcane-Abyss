using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _enemiesText;
    [SerializeField] private TextMeshProUGUI _wavesText;
    [SerializeField] private TextMeshProUGUI _timeText;

    public void Enable(){
        gameObject.SetActive(true);
    }

    public void Disable(){
        gameObject.SetActive(false);
    }
    
    private void OnEnable() 
    {
        FinalStats.instance.PigeonHoleSort();
        UpdateEnemiesText();
        UpdateWavesText();
        UpdateTimeText();
    }

    private void UpdateEnemiesText()
    {
        string enemiesTxt = "";
        foreach (KeyValuePair<string, int> kvp in FinalStats.instance.pigeonHoleSortedEnemies)
        {
            enemiesTxt += kvp.Key + ": " + kvp.Value + "\n";
        }
        _enemiesText.text = enemiesTxt;
    }

    private void UpdateWavesText(){
        _wavesText.text = "waves survived: " + WaveManager.instance.currentWaveNum;
    }

    private void UpdateTimeText(){
        _timeText.text = "Time survived: " + Time.time;
    }

}
