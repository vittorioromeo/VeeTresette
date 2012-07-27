#region
using System;
using System.Collections.Generic;
#endregion

namespace VeeTresette
{
    public static class Helper
    {
        static Helper()
        {
            Initialize();
        }

        public static Random Random { get; set; }

        public static void Initialize()
        {
            InitializeVariables();
        }

        public static void InitializeVariables()
        {
            Random = new Random();
        }

    }
}