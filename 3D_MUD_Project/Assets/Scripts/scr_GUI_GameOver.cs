using UnityEngine;
using System.Collections;

public class scr_GUI_GameOver : MonoBehaviour 
{
    public Font GameOverFont;

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
        GUIStyle GameOverStyle = new GUIStyle(GUI.skin.label);
        GameOverStyle.fontSize = 100;
        GameOverStyle.font = GameOverFont;

        GUI.color = Color.red;

        GUI.Label(new Rect(Screen.width / 2 - 400, 10, 1000, 100), "GAME OVER!", GameOverStyle);
        
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 100, 50), "Retry ?"))
        {
            Application.LoadLevel(1);
            Player.Instance.ResetPlayer();
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 25, 100, 50), "Exit"))
        {
            Application.Quit();
        }
    }
}
