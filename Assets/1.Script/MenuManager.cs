using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [HideInInspector]
    public bool showingMenu;
    GameObject menu;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !showingMenu)
        {
            menu = Instantiate(Resources.Load<GameObject>("UI/UI_Menu"));
            menu.GetComponent<UI_Menu>().menuManager = gameObject;
            showingMenu = true;
            Managers.Game.GameOver = true;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && showingMenu)
        {
            Destroy(menu);
            showingMenu = false;
            Managers.Game.GameOver = false;
            Time.timeScale = 1;
        }
    }
}
