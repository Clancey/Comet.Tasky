﻿using System;
using UIKit;

namespace Tasky.iOS
{
    public static class UIViewExtensions
    {
        public static UIViewController GetParentViewController(this UIView view)
        {
            var nextResponder = view.NextResponder;
            if (nextResponder is UIViewController)
                return (UIViewController)nextResponder;
            else if (nextResponder is UIView)
                return GetParentViewController((UIView)nextResponder);
            return null;
        }
    }
}
