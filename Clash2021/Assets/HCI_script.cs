using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HCI_script : MonoBehaviour
{
    Vector3 lastMousePosition;
    Manager theManager;
    private static string UIMessage;
    Text UIMessageBox;
    float MessageTimer;
    float MessageLife = 5;
    int MessageCount = 0;
    int MaxMessages = 5;

    // Start is called before the first frame update
    void Start()
    {
        theManager = FindObjectOfType<Manager>();
        UIMessageBox = GameObject.Find("UIMessageDisplay").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        MessageTimer += Time.deltaTime;
        if (MessageTimer >= MessageLife)
            DisplayMessage("");
      // UIMessage = string.Format("{0:G}", Count);

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            Camera.main.transform.position += Camera.main.transform.right * delta.x + Camera.main.transform.up*delta.y;

        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            Camera.main.transform.Rotate(Vector3.up, delta.x);

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(r, out hitinfo))
            {

                theManager.spawn_th_at(hitinfo.point);

                DisplayMessage("Spawning Town Hall");
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(r, out hitinfo))
            {

                theManager.spawn_drag_at(hitinfo.point);

                DisplayMessage("Spawning Dragon");
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(r, out hitinfo))
            {

                theManager.spawn_Cannon_at(hitinfo.point);

                DisplayMessage("Spawning Cannon");
            }
        }

        lastMousePosition = Input.mousePosition;
    }

    internal void DisplayMessage(string UIMessage)
    {

        if (UIMessage != "")
        {
            MessageTimer = 0;
            MessageCount++;
            
        }

        UIMessageBox.text = UIMessage;
    }
}
