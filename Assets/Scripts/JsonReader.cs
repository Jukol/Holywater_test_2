using UnityEngine;

public class JsonReader : MonoBehaviour, IDataReader

{
    [SerializeField] private TextAsset _input;
    [SerializeField] private Questions _questions;

    private void Start()
    {
        _questions = ReadFromJson();
    }

    public Questions ReadFromJson()
    {   
        return JsonUtility.FromJson<Questions>(_input.text);
    }
}
