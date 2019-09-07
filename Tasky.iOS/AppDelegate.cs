using System;
using Comet;
using Comet.iOS;
using Comet.iOS.Handlers;
using Foundation;
using UIKit;

namespace Tasky.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        UIWindow window;

        public override void FinishedLaunching(UIApplication application)
        {
            base.FinishedLaunching(application);
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
#if DEBUG
            Comet.Reload.Init();
#endif
            SetupComet();
            window = new UIWindow
            {
                RootViewController = new MainPage().ToViewController()
            };
            window.MakeKeyAndVisible();

            return true;
        }

        void SetupComet()
        {
            CometViewController.ContainerViewChanged += (vc, view) => SetViewControllerActions(vc);
            CometViewController.ContainerViewWillAppear += (vc,animated)=> SetViewControllerActions(vc);
        }
        void SetViewControllerActions(CometViewController vc)
        {
            var view = vc.CurrentView;
            var leftAction = view?.GetEnvironment<Tuple<string, Action>>(CometExtensions.LeftActionButtonKey)
                 ?? view?.BuiltView?.GetEnvironment<Tuple<string, Action>>(CometExtensions.LeftActionButtonKey);
            var rightAction = view?.GetEnvironment<Tuple<string, Action>>(CometExtensions.RightActionButtonKey)
                ?? view?.BuiltView?.GetEnvironment<Tuple<string, Action>>(CometExtensions.RightActionButtonKey);

            vc.NavigationItem.LeftBarButtonItem = leftAction == null ? null : new UIBarButtonItem(leftAction.Item1, UIBarButtonItemStyle.Plain, (s, e) =>
            {
                leftAction.Item2?.Invoke();
            });

            vc.NavigationItem.RightBarButtonItem = rightAction == null ? null : new UIBarButtonItem(rightAction.Item1, UIBarButtonItemStyle.Plain, (s, e) =>
            {
                rightAction.Item2?.Invoke();
            });
        }
    }
}

