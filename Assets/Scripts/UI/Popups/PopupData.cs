using System;
using UI.Base;
using UI.Popups.Base;

namespace UI.Popups
{
    [Serializable]
    public class PopupData : UIControlData<PopupType, PopupBase>
    {
        
    }
    
    public enum PopupType
    {
        None = 0,
        NotEnoughCoins = 1,
        Win = 2,
        Defeat = 3,
    }
}