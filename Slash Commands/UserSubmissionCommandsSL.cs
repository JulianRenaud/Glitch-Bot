using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using OpenAI_API;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Glitch_Bot.Slash_Commands
{
    public class UserSubmissionCommandsSL : ApplicationCommandModule
    {
        //[SlashCommand("searchforimage", "searches for an image from Google Images")]
        //public async Task GoogleImageSearch(InteractionContext ctx, [Option("Search", "Your search input")] string search)
        //{
        //    await ctx.DeferAsync();
        //    var hidden = new hiddenAPIs();
        //    string apiKey = hidden.SearchAPIKey();
        //    string cseId = hidden.CseID();

        //    var customSearchService = new CustomsearchService(new BaseClientService.Initializer()
        //    {
        //        ApiKey = apiKey,
        //        ApplicationName = "Glitch-Bot"
        //    });

        //    var listRequest = customSearchService.Cse.List();
        //    listRequest.Cx = cseId;
        //    listRequest.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
        //    listRequest.Q = search;

        //    var Request = await listRequest.ExecuteAsync();
        //    var results = Request.Items;

        //    if (results == null || !results.Any())
        //    {
        //        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("No results found!"));
        //        return;
        //    }
        //    else
        //    {
        //        var firstResult = results.First();
        //        var embed = new DiscordEmbedBuilder()
        //        {
        //            Title = "Results for Search: " + search,
        //            ImageUrl = firstResult.Link,
        //            Color = DiscordColor.Azure
        //        };
        //        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
        //    }
        //}

        
        [SlashCommand("poll", "Create your own poll")]
        public async Task PollCommand(InteractionContext ctx, [Option("Poll-Question", "The main poll question/subject")] string Question,
                                                              [Option("Timelimit", "Timelimit in seconds set on your poll")] long TimeLimit,
                                                              [Option("Option1", "The first option for your poll")] string Option1,
                                                              [Option("Option2", "The second option for your poll")] string Option2,
                                                              [Option("Option3", "The third option for your poll")] string Option3,
                                                              [Option("Option4", "The fourth option for your poll")] string Option4)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("poll"));
            var interactivity = ctx.Client.GetInteractivity();
            TimeSpan timer = TimeSpan.FromSeconds(TimeLimit);
            DiscordEmoji[] optionEmojis = { DiscordEmoji.FromName(ctx.Client, ":one:", false),
                                            DiscordEmoji.FromName(ctx.Client, ":two:", false),
                                            DiscordEmoji.FromName(ctx.Client, ":three:", false),
                                            DiscordEmoji.FromName(ctx.Client, ":four:", false) };

            string optionsString = optionEmojis[0] + " | " + Option1 + "\n" +
                                   optionEmojis[1] + " | " + Option2 + "\n" +
                                   optionEmojis[2] + " | " + Option3 + "\n" +
                                   optionEmojis[3] + " | " + Option4;


            var pollMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Azure)
                .WithTitle(string.Join(" ", Question))
                .WithDescription(optionsString)
                );

            var putReactOn = await ctx.Channel.SendMessageAsync(pollMessage);

            foreach (var emoji in optionEmojis)
            {
                await putReactOn.CreateReactionAsync(emoji);
            }

            var result = await interactivity.CollectReactionsAsync(putReactOn, timer);

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;


            foreach (var emoji in result)
            {
                if (emoji.Emoji == optionEmojis[0])
                {
                    count1++;
                }
                if (emoji.Emoji == optionEmojis[1])
                {
                    count2++;
                }
                if (emoji.Emoji == optionEmojis[2])
                {
                    count3++;
                }
                if (emoji.Emoji == optionEmojis[3])
                {
                    count4++;
                }
            }



            int totalVotes = count1 + count2 + count3 + count4;

            string resultsString = optionEmojis[0] + " | " + count1 + " Votes \n" +
                                   optionEmojis[1] + " | " + count2 + " Votes \n" +
                                   optionEmojis[2] + " | " + count3 + " Votes \n" +
                                   optionEmojis[3] + " | " + count4 + "Votes \n\n" +
                                   "The total number of votes is " + totalVotes;

            var resultsMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Green)
                .WithTitle("Results of Poll")
                .WithDescription(resultsString)
                );

            await ctx.Channel.SendMessageAsync(resultsMessage);
        }
    }
}
