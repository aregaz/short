using System;
using System.Text.RegularExpressions;

namespace Short.Parser
{
    public class PostParser
    {
        private const RegexOptions Options = RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.ECMAScript;

        public static Post Parse(string text)
        {
            Match metadataMatch = Regex.Match(
                text,
                @"<!--(?<metadata>[\s\S]*)-->",
                Options);

            if (!metadataMatch.Success)
                return new Post
                {
                    Content = text
                };

            PostMetadata metadata = ParseMetadata(metadataMatch.Value);

            return new Post
            {
                Metadata = metadata,
                Content = text.Substring(metadataMatch.Value.Length).Trim()
            };
        }

        private static PostMetadata ParseMetadata(string metadataText)
        {
            return new PostMetadata
            {
                Title = ParseSingleLineTextField(metadataText, "Title"),
                Author = ParseSingleLineTextField(metadataText, "Author"),
                CreatedDate = ParseDateField(metadataText, "Created"),
            };
        }

        private static DateTimeOffset? ParseDateField(string metadataText, string fieldName)
        {
            Match match = Regex.Match(
                metadataText,
                @$"{fieldName}:\s*(?<fieldValue>.*)\s*$",
                Options);

            if (!match.Groups["fieldValue"].Success)
                return null;

            return DateTimeOffset.TryParse(match.Groups["fieldValue"].Value, out DateTimeOffset createdDate)
                ? createdDate
                : null;
        }

        private static string ParseSingleLineTextField(string metadataText, string fieldName)
        {
            Match match = Regex.Match(
                metadataText,
                @$"{fieldName}:\s*(?<fieldValue>.*)\s*$",
                Options);

            if (!match.Groups["fieldValue"].Success)
                return null;

            return match.Groups["fieldValue"].Value.Trim('\r').Trim();
        }
    }
}
