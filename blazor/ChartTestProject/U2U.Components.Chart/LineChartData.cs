using System;
using System.Collections.Generic;
using System.Drawing;

namespace U2U.Components.Chart
{
    public class LineChartData
    {
        public string[] Labels { get; set; } = Array.Empty<string>();
        public DataSet[] Datasets { get; set; }

        public class DataSet
        {
            public string Label { get; set; }
            public List<Point> Data { get; set; } = null;
            public string BackgroundColor { get; set; }
            public string BorderColor { get; set; }
            public int BorderWidth { get; set; } = 2;
        }
    }
}