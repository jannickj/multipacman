using UnityEngine;

namespace Assets
{
	public class NewBehaviourScript : MonoBehaviour
	{
		private GUI.WindowFunction windowFunction;
		private Rect windowRect = new Rect(20, 20, 120, 50);


		private void Start()
		{
			windowFunction = DoMyWindow;
		}


		private void OnGUI()
		{
			windowRect = GUI.Window(0, windowRect, windowFunction, "My Window");
		}


		private void DoMyWindow(int windowID)
		{
			if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))

				Debug.Log("Got a click");


			GUI.DragWindow();
		}
	}
}