using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler{
    private string dataPath = "";
    private string dataFileName = "";
    public string fullPath;

    public FileDataHandler(string dataPath, string dataFileName){
        this.dataPath = dataPath;
        this.dataFileName = dataFileName;
        this.fullPath = Path.Combine(dataPath, dataFileName);
    }

    public GameData Load(){
        //get full path of saved location
        string fullpath = Path.Combine(dataPath, dataFileName);

        GameData loadedData = null;
        
        if(File.Exists(fullpath)){
            try{
                //retrieve json data
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullpath, FileMode.Open)){
                    using(StreamReader reader = new StreamReader(stream)){
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //convert json to gamedata
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                
            }catch(Exception e){
                throw new ArgumentException("Could not load file: " + fullpath + "\n" +"Exception: " + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data){
        //get full path of saved location
        string fullpath = Path.Combine(dataPath, dataFileName);
        try{
            //create directory for saved data
            Directory.CreateDirectory(Path.GetDirectoryName(fullpath));

            //serialize data
            string jsonData = JsonUtility.ToJson(data, true);

            //writes data to file
            using(FileStream stream = new FileStream(fullpath, FileMode.Create)){
                using(StreamWriter writer = new StreamWriter(stream)){
                    writer.Write(jsonData);
                }
            }

        }catch(Exception e){
            throw new ArgumentException("Could not save file: " + fullpath + "\n" +"Exception: " + e);
        }
    }
}
