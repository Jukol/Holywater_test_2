using UnityEngine;

public class JsonReader : MonoBehaviour, IDataReader

{
    [SerializeField] private TextAsset _input;

    public Questions ReadFromJson()
    {   
        return JsonUtility.FromJson<Questions>(_input.text);
    }
}
