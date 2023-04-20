using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using System;
using System.Threading.Tasks;

namespace Glitch_Bot.Slash_Commands
{
    public class MessageCommandsSL : ApplicationCommandModule
    {
        [SlashCommand("test", "This is a test slash command")]
        public async Task TestSlashCommand(InteractionContext ctx, [Choice("Choice1", "Choice1")][Option("String", "Type in anything you want")] string text)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("test"));
            var embedMessage = new DiscordEmbedBuilder()
            {
                Title = text
            };

            await ctx.Channel.SendMessageAsync(embed: embedMessage);
        }

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
        [SlashCommand("embedmessage", "creates an embed message")]
        public async Task EmbedMessageCommand(InteractionContext ctx, [Option("Title", "A title for your embed message")] string title,
                                                                      [Option("Description", "A description for your embed message")]string description)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Embed Message"));
            var embedMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithTitle(title)
                .WithDescription(description)
                .WithColor(DiscordColor.Azure)
                );

            await ctx.Channel.SendMessageAsync(embedMessage);
        }
    }
}
