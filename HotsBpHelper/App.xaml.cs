﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HotsBpHelper.Settings;

namespace HotsBpHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string AppPath;
        public static Position MyPosition;

        public static bool Debug;

        public static string Language = CultureInfo.CurrentCulture.Name;
    }
}
