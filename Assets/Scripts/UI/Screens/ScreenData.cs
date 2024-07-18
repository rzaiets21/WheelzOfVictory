using System;
using UI.Base;
using UI.Screens.Base;

namespace UI.Screens
{
    [Serializable]
    public class ScreenData : UIControlData<ScreenType, ScreenBase>
    {

    }

    public enum ScreenType
    {
        None = 0,
        Loading = 1,
        MainMenu = 2,
        DailyBonus = 3,
        Minigame = 4,
        LevelMap = 5,
        Shop = 6,
        UpgradeSkill = 7,
        Weapon = 8,
        Info = 9,
        FAQ = 10,
    }
}