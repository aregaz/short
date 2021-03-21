using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MarkdownSharp;
using Microsoft.AspNetCore.Components;
using Short.Parser;
using Short.Web.Shared;

namespace Short.Web.Pages
{
    public partial class Post : ShortPageDefault
    {
        [Parameter]
        public string PageName { get; set; }

        [Parameter]
        public DateTimeOffset? Created { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Author { get; set; }

        public string PostContent { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            HttpResponseMessage response = await HttpClient.GetAsync($"/posts/{PageName}.md");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/404");
            }

            string rawFileContent = await response.Content.ReadAsStringAsync();

            Parser.Post post = PostParser.Parse(rawFileContent);

            Markdown markdownConverter = new MarkdownSharp.Markdown();
            string postHtml = markdownConverter.Transform(post.Content);

            PostContent = postHtml;
            Title = post.Metadata.Title;
            Created = post.Metadata.CreatedDate;
            Author = post.Metadata.Author;

            StateHasChanged();
        }
    }
}
