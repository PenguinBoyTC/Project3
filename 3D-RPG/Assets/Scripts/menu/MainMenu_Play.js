function OnGUI ()
{
    if (GUI.Button (Rect (Screen.with/2 - 50, Screen.height/2,100,30), "Play"))
    {
        Application.Loadlevel(1);
    }
}