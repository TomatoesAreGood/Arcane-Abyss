using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistanceManager : MonoBehaviour
{
    [SerializeField] private string _fileName;
    public FileDataHandler DataHandler;
    public static DataPersistanceManager Instance;
    public GameData GameData;
    private List<IDataPersistance> _dataPersistanceObjects;
    private void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }
    private void Start(){
        //find all objects that need data loaded in, load in data
        DataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
        _dataPersistanceObjects = FindAllSaveObjects();
        LoadGame();
    }

    private void OnApplicationQuit(){
        SaveGame();
    }

    public void LoadGame(){
        
        //retreives data
        GameData = DataHandler.Load();

        //creates a new game if no gamedata
        if(this.GameData == null){
            NewGame();
        }

        //sets all loaded attributes of gameobjects
        foreach(IDataPersistance obj in _dataPersistanceObjects){
            obj.LoadData(GameData);
        }
    }

    public void SaveGame(){
        foreach(IDataPersistance obj in _dataPersistanceObjects){
            obj.SaveData(ref GameData);
        }

        DataHandler.Save(GameData);
    }

    public void NewGame(){
        GameData = new GameData();
    }

    private List<IDataPersistance> FindAllSaveObjects(){
        //finds all objects in scene that have saved data
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistanceObjects);
    }



    
}
