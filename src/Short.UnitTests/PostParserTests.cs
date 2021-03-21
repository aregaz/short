using System;
using FluentAssertions;
using Short.Parser;
using Xunit;

namespace Short.UnitTests
{
    public class PostParserTests
    {
        [Fact]
        public void Parse_NoMetadata_OnlyContent()
        {
            // arrange
            string text =
                @"No metadata";

            // act
            Post post = PostParser.Parse(text);

            // assert
            post.Should().BeEquivalentTo(
                new
                {
                    Content = text,
                    Metadata = new
                    {
                        Title = (string) null,
                        Author = (string) null,
                        CreatedDate = (DateTimeOffset?) null
                    }
                });
        }

        [Fact]
        public void Parse_MetadataHasOnlyTitle_TitleAndContent()
        {
            // arrange
            string expectedTitle = "Test title";
            string expectedContent = "Content after metadata";

            string text =
@$"<!--
Title:    {expectedTitle}
-->


{expectedContent}";

            // act
            Post post = PostParser.Parse(text);

            // assert
            post.Should().BeEquivalentTo(
                new
                {
                    Content = expectedContent,
                    Metadata = new
                    {
                        Title = expectedTitle,
                        Author = (string) null,
                        CreatedDate = (DateTimeOffset?) null
                    }
                });
        }
        
        [Fact]
        public void Parse_MetadataHasOnlyAuthor_AuthorAndContent()
        {
            // arrange
            string expectedAuthor = "John Doe";
            string expectedContent = "Content after metadata";

            string text =
                @$"<!--
Author:    {expectedAuthor}
-->


{expectedContent}";

            // act
            Post post = PostParser.Parse(text);

            // assert
            post.Should().BeEquivalentTo(
                new
                {
                    Content = expectedContent,
                    Metadata = new
                    {
                        Title = (string) null,
                        Author = expectedAuthor,
                        CreatedDate = (DateTimeOffset?) null
                    }
                });
        }
        
        [Fact]
        public void Parse_MetadataHasOnlyCreated_CreatedAndContent()
        {
            // arrange
            string expectedContent = "Content after metadata";

            string createdString = "2021-03-21T17:59:35+02:00";
            DateTimeOffset expectedCreated = DateTimeOffset.Parse(createdString);

            string text =
                @$"<!--
Created:    {createdString}
-->


{expectedContent}";

            // act
            Post post = PostParser.Parse(text);

            // assert
            post.Should().BeEquivalentTo(
                new
                {
                    Content = expectedContent,
                    Metadata = new
                    {
                        Title = (string)null,
                        Author = (string) null,
                        CreatedDate = expectedCreated
                    }
                });
        }
        
        [Fact]
        public void Parse_MetadataHasTitleAuthorCreated_TitleAuthorCreatedAndContent()
        {
            // arrange
            string expectedTitle = "Test title";
            string expectedAuthor = "John Doe";
            string createdString = "2021-03-21T17:59:35+02:00";
            
            string expectedContent = "Content after metadata";

            DateTimeOffset expectedCreated = DateTimeOffset.Parse(createdString);

            string text =
                @$"<!--
Created:    {createdString}   
Title:   {expectedTitle} 
Author:{expectedAuthor}   

-->


{expectedContent}
  
";

            // act
            Post post = PostParser.Parse(text);

            // assert
            post.Should().BeEquivalentTo(
                new
                {
                    Content = expectedContent,
                    Metadata = new
                    {
                        Title = expectedTitle,
                        Author = expectedAuthor,
                        CreatedDate = expectedCreated
                    }
                });
        }
    }
}
