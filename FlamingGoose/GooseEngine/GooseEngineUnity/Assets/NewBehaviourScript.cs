using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

    Rect windowRect = new Rect(20, 20, 120, 50);

    GUI.WindowFunction windowFunction;



    void Start()
    {

        windowFunction = DoMyWindow;

    }



    void OnGUI()
    {

        windowRect = GUI.Window(0, windowRect, windowFunction, "My Window");

    }



    void DoMyWindow(int windowID)
    {

        if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))

            Debug.Log("Got a click");



        GUI.DragWindow();

    }
}
