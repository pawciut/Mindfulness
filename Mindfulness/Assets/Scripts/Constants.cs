using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Constants
{

    public const string DateFormat = "yyyy-MM-dd HH:mm";
    public const string Player_Pref_Score = "score";
    public const string Player_Pref_Time = "time";
    public const string Player_Pref_Music = "music";

    public const int Name_Char_Limit = 10;
    public const int Score_Char_Limit = 15;
    public const string Highscore_Col_Spacing = "   ";

    public const string Focus_Animator_Parameter = "Active";



    public const string MenuScene = "MenuScene";
    public const string Level1Scene = "Level1";
    public const string CreditsScene = "Credits";
    public const string HowToPlayScene = "Level0";
    public const string HighscoreScene = "Highscore";
    public const string HighscoresScene = "Highscores";


    public static string FormatTime(float time)
    {
        int hours = ((int)time / 3600) % 24;
        int minutes = ((int)time / 60) % 60;
        int seconds = (int)time % 60;
        return String.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }
}