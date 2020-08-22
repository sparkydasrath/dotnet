using Microsoft.JSInterop;

namespace U2U.Components.Chart
{
    public class ChartInterop : IChartInterop
    {
        public ChartInterop(IJSRuntime jsRuntime)
        {
            JSRuntime = jsRuntime;
        }

        public IJSRuntime JSRuntime { get; }

        public void CreateLineChart(string id, LineChartData data,
            ChartOptions options)
        {
            JSRuntime.InvokeAsync<string>("components.chart",
                id, data, options);
        }
    }
}