using UnityEngine;
using System.Collections;

public class scr_GUI_GamePlay : MonoBehaviour 
{

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
        Cursor.visible = false;
        GUI.Box(new Rect(0, 0, 300, 25), "Armor: " + Player.Instance.DamageReduction + "     Health: " + Player.Instance.CurrentHealth + "/" + Player.Instance.MaxHealth);
        GUI.Box(new Rect(0, 25, 150, 25), "Level: " + Player.Instance.Level);
        GUI.Box(new Rect(150, 25, 150, 25), "XP: " + Player.Instance.XP + "/100");
        GUI.Box(new Rect(0, 50, 300, 25), "Damage Range: " + Player.Instance.MinDamage + " - " + Player.Instance.MaxDamage);
        GUI.Box(new Rect(0, 75, 150, 25), "Critical Chance: " + Player.Instance.CritChance + "%");
        GUI.Box(new Rect(150, 75, 150, 25), "Critical Multiplyer: " + Player.Instance.CritBonusDamage);
    }
}
