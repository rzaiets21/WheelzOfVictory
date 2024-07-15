using MyOwn;
using MyOwn.Model;
using UI.Screens.Base;

namespace UI.Screens
{
    public sealed class WeaponScreen : ScreenWithScrollItems
    {
        protected override ItemData[] GetItems() => ItemsController.Instance.GetItemsData(ItemType.Weapon);
    }
}