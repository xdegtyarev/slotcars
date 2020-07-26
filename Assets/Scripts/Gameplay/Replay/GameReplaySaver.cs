using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameReplaySaver : MonoBehaviour {
    public static GameReplaySaver INSTANCE;

    private ReplayHandler currentReplayHandler;
    
    private void Awake() {
        INSTANCE = this;
        this.currentReplayHandler = new ReplayHandler();
    }

    public void AddControlPoint(Transform transform, float currentPosition) {
        this.currentReplayHandler.Add(transform, currentPosition);
    }

    private const string REPLAY_NAME = "/replay8.list";
    public void Save(float totalTime) {
        BinaryFormatter binary = new BinaryFormatter();
        
        FileStream fStream = File.Create(Application.persistentDataPath + REPLAY_NAME);
        this.currentReplayHandler.totalTime = totalTime;
        binary.Serialize(fStream, this.currentReplayHandler);
        fStream.Close();
        Debug.Log("Replay saved");
    }

    public ReplayHandler Load() {       
        if (!File.Exists(Application.persistentDataPath + REPLAY_NAME)) {
            return null;
        }
        
        BinaryFormatter binary = new BinaryFormatter();
        FileStream fStream = File.Open(Application.persistentDataPath + REPLAY_NAME, FileMode.Open);
        ReplayHandler replayHandler = (ReplayHandler) binary.Deserialize(fStream);
        fStream.Close();

        Debug.Log("LOADED : " + replayHandler._serializableTransforms.Count);
        return replayHandler;
    }
}