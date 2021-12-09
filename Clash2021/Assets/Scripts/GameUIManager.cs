using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameUIManager : MonoBehaviour
{

    private static string UIMessage;
    Text UIMessageBox;
    float Count;

    // Start is called before the first frame update
    void Start()
    {
        
        UIMessageBox = GameObject.Find("UIMessageDisplay").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
      
        Count += Time.deltaTime;
        UIMessage = string.Format("{0:G}", Count);
        UIMessageBox.text = UIMessage;
    }
}
