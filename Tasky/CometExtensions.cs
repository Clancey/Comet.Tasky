using System;
using Comet;

namespace Tasky
{
    public static class CometExtensions
    {
        public const string LeftActionButtonKey = "Navigation.LeftActionButtonKey";
        public static T SetLeftActionButton<T>(this T view, Tuple<string,Action> action)
            where T: View
        {
            view.SetEnvironment(LeftActionButtonKey, action, false);
            return view;
        }
        public const string RightActionButtonKey = "Navigation.RightActionButtonKey";
        public static T SetRightActionButton<T>(this T view, Tuple<string, Action> action)
            where T : View
        {
            view.SetEnvironment(RightActionButtonKey, action, false);
            return view;
        }
    }
}
