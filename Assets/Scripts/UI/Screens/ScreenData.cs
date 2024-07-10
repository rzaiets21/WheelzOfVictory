using System;
using UI.Screens.Base;
using UnityEngine;

namespace UI.Screens
{
    [Serializable]
    public class ScreenData
    {
        [field: SerializeField] public ScreenBase ScreenPrefab { get; private set; }
        [field: SerializeField] public ScreenType ScreenType { get; private set; }
    }

    public enum ScreenType
    {
        None = 0,
        Loading = 1,
        MainMenu = 2,
        DailyBonus = 3,
        Minigame = 4,
        LevelMap = 5,
    }
}