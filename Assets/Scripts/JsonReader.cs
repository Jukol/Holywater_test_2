using UnityEngine;

public class JsonReader : MonoBehaviour, IDataReader

{
    [SerializeField] private TextAsset input;
    [SerializeField] private Questions _questions; 

    private void Start()
    {
        _questions = ReadFromJson();
    }

    public Questions ReadFromJson()
    {
        return JsonUtility.FromJson<Questions>(input.text);
    }
}
