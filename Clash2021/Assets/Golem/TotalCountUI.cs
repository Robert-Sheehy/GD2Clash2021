using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalCountUI : MonoBehaviour
{
    private static string UIMessage;

    Text UIMessageBox;

    private int totalArmy;

    private int totalBuildings;

    Manager theManager;

    // Start is called before the first frame update
    void Start()
    {
        theManager = FindObjectOfType<Manager>();

        UIMessageBox = GameObject.Find("UIMessageDisplay").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        totalArmy = theManager.numOfCharacters();
        totalBuildings = theManager.numOfBuildings();

        UIMessage = string.Format("Total Units: " + totalArmy + "         Total Buildings: " + totalBuildings);
        UIMessageBox.text = UIMessage;

    }
}
