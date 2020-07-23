using Microsoft.AspNetCore.Components;
using System;

namespace GettingStarted.Client.Pages
{
    public partial class DismissableAlert
    {
        private bool show;

        [Parameter]
        public bool Show
        {
            get => show;
            set
            {
                if (value == show) return;
                show = value;
                ShowChanged.InvokeAsync(show);
            }
        }
        
        [Parameter]
        public EventCallback<bool> ShowChanged { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public void Dismiss() => Show = false;
    }
}