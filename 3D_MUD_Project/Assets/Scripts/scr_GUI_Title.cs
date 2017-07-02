using UnityEngine;
using System.Collections;

public class scr_GUI_Title : MonoBehaviour 
{
    public Font TitleFont;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnGUI()
    {
        Cursor.visible = true;
        GUIStyle TitleStyle = new GUIStyle(GUI.skin.label);
        TitleStyle.fontSize = 100;
        TitleStyle.font = TitleFont;

        GUI.color = Color.red;

        GUI.Label(new Rect(Screen.width / 2 - 400, 10, 2000, 100), "SKELETONZ!!!", TitleStyle);

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 100, 50), "New Game"))
        {
            Application.LoadLevel(1);
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 25, 100, 50), "Exit"))
        {
            Application.Quit();
        }
    }
}
