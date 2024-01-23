using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistanceManager : MonoBehaviour
{
    [SerializeField] string fileName;
    public FileDataHandler dataHandler;
    public static DataPersistanceManager instance;
    public GameData gameData;
    private List<IDataPersistance> dataPersistanceObjects;
    private void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }
    private void Start(){
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataPersistanceObjects = FindAllSaveObjects();
        LoadGame();
    }

    private void OnApplicationQuit(){
        SaveGame();
    }

    public void LoadGame(){
        
        //retreives data
        gameData = dataHandler.Load();

        //creates a new game if no gamedata
        if(this.gameData == null){
            NewGame();
        }

        //sets all loaded attributes of gameobjects
        foreach(IDataPersistance obj in dataPersistanceObjects){
            obj.LoadData(gameData);
        }
    }

    public void SaveGame(){
        foreach(IDataPersistance obj in dataPersistanceObjects){
            obj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    public void NewGame(){
        Debug.Log("creating new game");
        this.gameData = new GameData();
    }

    private List<IDataPersistance> FindAllSaveObjects(){
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();
        
        return new List<IDataPersistance>(dataPersistanceObjects);

    }



    
}
