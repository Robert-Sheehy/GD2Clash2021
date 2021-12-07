using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HCI_script : MonoBehaviour
{
    Vector3 lastMousePosition;
    Manager theManager;
    // Start is called before the first frame update
    void Start()
    {
        theManager = FindObjectOfType<Manager>();
    }

    // Update is called once per frame
    void Update()
    {

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
              
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(r, out hitinfo))
            {

                theManager.spawn_drag_at(hitinfo.point);

            }
        }

        lastMousePosition = Input.mousePosition;
    }
}
