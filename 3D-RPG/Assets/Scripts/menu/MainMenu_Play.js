function OnGUI ()
{
    if (GUI.Button (Rect (Screen.with/2 - 50, Screen.height/2,90,20), "Play Game"))
    {
        Application.Loadlevel(1);
    }
     if (GUI.Button (Rect (Screen.with/2 - 50, Screen.height/2+20,90,20), "Exit Game"))
    {
        Application.Quit();
    }
}