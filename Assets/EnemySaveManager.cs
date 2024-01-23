using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySaveManager : MonoBehaviour, IDataPersistance
{
    [SerializeField] GameObject enemy; //0
    [SerializeField] GameObject enemyPouncer; // 1
    [SerializeField] GameObject enemyShaman; // 2
    [SerializeField] GameObject enemySkeleton; // 3
    [SerializeField] GameObject enemyTank;  // 4


   public static EnemySaveManager instance;
   public List<Enemy> allEnemies;
   private List<Vector3> enemyPositions;
   private List<int> enemyIDs;
    private List<Enemy> GetEnemies(){
        IEnumerable<Enemy> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<Enemy>();
        
        return new List<Enemy>(dataPersistanceObjects);

    }

    public void LoadData(GameData data)
    {  
        
        List<Enemy> enemies = GetEnemies();

        foreach(Enemy enemy in enemies){
            Destroy(enemy.gameObject);
        }

        for(int i = 0; i < data.enemyTypes.Count; i++){
            if(data.enemyTypes[i] == 0){
                Instantiate(enemy).transform.position = data.enemyPositions[i];
            }else if(data.enemyTypes[i] == 1){
                Instantiate(enemyPouncer).transform.position = data.enemyPositions[i];
            }else if(data.enemyTypes[i] == 2){
                Instantiate(enemyShaman).transform.position = data.enemyPositions[i];
            }else if(data.enemyTypes[i] == 3){
                Instantiate(enemySkeleton).transform.position = data.enemyPositions[i];
            }else if(data.enemyTypes[i] == 4){
                Instantiate(enemyTank).transform.position = data.enemyPositions[i];
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        for(int i = 0; i < allEnemies.Count ;i++){
            enemyPositions.Add(allEnemies[i].transform.position);
            enemyIDs.Add(allEnemies[i].EnemyID);
        }
        data.enemyPositions = enemyPositions;
        data.enemyTypes = enemyIDs;
    }

    private void Awake(){
    if(instance == null){
        instance = this;
    }else{
        Destroy(gameObject);
    }
    enemyPositions = new List<Vector3>();
    enemyIDs = new List<int>();
   }




}
