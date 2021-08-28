using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace CSharFrompJavascriptBlazor
{
    public partial class CSharpFromJS
    {
        private DotNetObjectReference<CSharpFromJS> _objectReference;
        public int WindowWidth { get; set; }


        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected override void OnInitialized()
        {
            _objectReference = DotNetObjectReference.Create(this);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await InitWindowWidthListener();
            }
        }

        [JSInvokable]
        public void UpdateWindowWidth(int windowWidth)
        {
            WindowWidth = windowWidth;
            StateHasChanged();
        }

        private async Task InitWindowWidthListener()
        {
            await JSRuntime.InvokeVoidAsync("AddWindowWidthListener", _objectReference);
        }

        public async ValueTask DisposeAsync()
        {
            await JSRuntime.InvokeVoidAsync("RemoveWindowWidthListener", _objectReference);
            _objectReference?.Dispose();
        }
    }
}