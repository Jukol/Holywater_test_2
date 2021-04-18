using UnityEngine;
using System.IO;

public class JsonWriter : MonoBehaviour, IDataSender
{
    [SerializeField] DataProcessor _dataProcessor;
    public void WriteToJson()
    {
        string output = JsonUtility.ToJson(_dataProcessor.answers);
        File.WriteAllText(Application.persistentDataPath + "/output.txt", output);
    }
}
