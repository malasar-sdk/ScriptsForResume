using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.CloudSave;
using UnityEngine;

public class SaverLoader : MonoBehaviour
{
    [SerializeField]
    private string keyNameToSaveAnonim, textForSave, textKeysAnonim;

    [SerializeField, Header("Нужно от 3 слов")]
    private List<string> textList;

    [SerializeField]
    private TMP_Text txtToSave, txtKeysList;

    public async void SaveDataAnonim()
    {
        Dictionary<string, object> data = new Dictionary<string, object>() { { keyNameToSaveAnonim, textForSave} };
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);

        Debug.Log($"Saved anonim!");
    }

    public async void LoadDataAnonim()
    {
        Dictionary<string, string> data = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { keyNameToSaveAnonim });
        txtToSave.text = $"Anonym: {data[keyNameToSaveAnonim]}";
        Debug.Log($"Loaded anonim: {data[keyNameToSaveAnonim]}");
    }

    public async void DeleteDataAnonim()
    {
        await CloudSaveService.Instance.Data.ForceDeleteAsync(keyNameToSaveAnonim);
        Debug.Log($"Deleted anonim key {keyNameToSaveAnonim}");
    }

    public async void GetAllKeysAnonim()
    {
        List<string> keysAnonim = await CloudSaveService.Instance.Data.RetrieveAllKeysAsync();
        textKeysAnonim = "";

        for (int i = 0; i < keysAnonim.Count; i++)
        {
            textKeysAnonim += keysAnonim[i];
        }

        txtKeysList.text = $"Anonim: {textKeysAnonim}";
        Debug.Log($"Keys loaded Anonom: {textKeysAnonim}");
    }

    public void GenerateText()
    {
        textForSave = "";

        for (int i = 0; i < 3; i++)
        {
            int numRand = Random.Range(0, textList.Count);
            textForSave += $"{textList[numRand]} ";
        }

        txtToSave.text = textForSave;
    }
}
