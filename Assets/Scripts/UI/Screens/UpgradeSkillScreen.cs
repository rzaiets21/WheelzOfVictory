using MyOwn;
using MyOwn.Model;
using UI.Screens.Base;

namespace UI.Screens
{
    public sealed class UpgradeSkillScreen : ScreenWithScrollItems
    {
        protected override ItemData[] GetItems() => ItemsController.Instance.GetItemsData(ItemType.Upgrade);
    }
}