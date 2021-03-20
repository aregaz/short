using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;

namespace Short.Web.Shared
{
    public partial class ShortPageDefault : ComponentBase
    {
        [Inject]
        public IWebAssemblyHostEnvironment HostEnvironment { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            SetPageTitle(Configuration["Website:Name"]);
        }

        protected void SetPageTitle(string title)
        {
            Console.WriteLine("Blazor setting Title to {0}", title);
            JsRuntime.InvokeAsync<string>("setTitle", new object[] {title});
        }
    }
}
