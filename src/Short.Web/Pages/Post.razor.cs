using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MarkdownSharp;
using Microsoft.AspNetCore.Components;
using Short.Web.Shared;

namespace Short.Web.Pages
{
    public partial class Post : ShortPageDefault
    {
        [Parameter]
        public string PostName { get; set; }

        public string PostContent { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            HttpResponseMessage response = await HttpClient.GetAsync($"/posts/{PostName}.md");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/404");
            }

            string postMarkdown = await response.Content.ReadAsStringAsync();
            Markdown markdownConverter = new MarkdownSharp.Markdown();
            string postHtml = markdownConverter.Transform(postMarkdown);

            PostContent = postHtml;

            StateHasChanged();
        }
    }
}
