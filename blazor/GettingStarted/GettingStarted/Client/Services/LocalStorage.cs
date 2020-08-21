using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace GettingStarted.Client.Services
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IJSRuntime jsRuntime;

        public LocalStorage(IJSRuntime jsRuntime) => this.jsRuntime = jsRuntime;

        public async Task<T> GetProperty<T>(string propName) =>
            await jsRuntime.InvokeAsync<T>(Constants.BlazorLocalStorage, propName);

        public async Task<object> SetProperty<T>(string propName, T value) =>
            await jsRuntime.InvokeAsync<object>(Constants.BlazorLocalStorageSet, propName, value);

        public async Task<object> WatchAsync<T>(T instance) where T : class
            => await jsRuntime.InvokeAsync<object>(Constants.BlazorLocalStorageWatch, DotNetObjectReference.Create<T>(instance));
    }
}