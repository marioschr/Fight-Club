using UnityEngine;

public class GameConstants : MonoBehaviour // Script με τις σταθερές του παιχνιδιού
{
    public const string PLAYER_READY = "IsPlayerReady";
    public const string PLAYER_LOADED_LEVEL = "PlayerLoadedLevel";
    public const int MAIN_MENU_INDEX = 0, CHARACTER_SELECT_INDEX = 1, GAME_SCENE_INDEX = 2, PRACTICE_SCENE_INDEX = 3;
    public static Color GetColor(int colorChoice)
    {
        switch (colorChoice)
        {
            case 0: return Color.red;
            case 1: return Color.blue;
        }
        return Color.black;
    }
}
