using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GM_SmartTryout
{
    public class CustomTransitionNavPage : SharedTransitionNavigationPage
    {
        public CustomTransitionNavPage()
        {
            //Code to make the NavigationPage translucent, if you don't want that you can remove it
            //On<iOS>().SetIsNavigationBarTranslucent(true);
            BarBackgroundColor = Color.Transparent;
            BarTextColor = Color.Black;
            //On<iOS>().SetHideNavigationBarSeparator(true);
        }

        public bool IgnoreLayoutChange { get; set; } = false;

        protected override void OnSizeAllocated(double width, double height)
        {
            if (!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
}
