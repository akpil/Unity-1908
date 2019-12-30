using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class DataLoader : MonoBehaviour
{
    
    protected void LoadJsonData<T>(out T[] output, string location)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(location);
        if(textAsset != null)
        {
            string data = textAsset.text;
            if(!string.IsNullOrEmpty(data))
            {
                output = JsonConvert.DeserializeObject<T[]>(data);
                return;
            }
            else
            {
                Debug.LogWarning("File is empty : " + location);
            }
        }
        else
        {
            Debug.LogWarning("Wrong location or file name : " + location);
        }
        output = null;
    }
}
