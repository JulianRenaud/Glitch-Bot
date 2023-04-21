using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Threading.Tasks;

namespace Glitch_Bot.Commands
{
    internal class MessageCommands : BaseCommandModule
    {
        [Command("message")]
        [Cooldown(5, 10, CooldownBucketType.User)]
        public async Task SendMessage(CommandContext ctx, params string[] message)
        {
            var messageInString = string.Join(" ", message);
            await ctx.Channel.SendMessageAsync(messageInString);
        }
        [Command("embedmessage")]
        public async Task SendEmbedMessage(CommandContext ctx)
        {
            var embedMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithTitle("Title")
                .WithDescription("Description")
                .WithColor(DiscordColor.Azure)
                );

            await ctx.Channel.SendMessageAsync(embedMessage);
        }

        [Command("poll")]
        public async Task PollCommand(CommandContext ctx, int TimeLimit, string Option1, string Option2, string Option3, string Option4, params string[] Question)
        {
            var interactivity = ctx.Client.GetInteractivity();
            TimeSpan timer = TimeSpan.FromMinutes(TimeLimit);
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
        [Command("button")]
        public async Task ButtonTest(CommandContext ctx)
        {
            DiscordButtonComponent button1 = new DiscordButtonComponent(ButtonStyle.Primary, "1", "Button 1");
            DiscordButtonComponent button2 = new DiscordButtonComponent(ButtonStyle.Primary, "2", "Button 2");

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Azure)
                .WithTitle("This is a message with buttons")
                .WithDescription("Please select a button")
                )
                .AddComponents(button1)
                .AddComponents(button2);

            await ctx.Channel.SendMessageAsync(message);
        }

        [Command("help")]
        public async Task HelpCommand(CommandContext ctx)
        {
            var funButton = new DiscordButtonComponent(ButtonStyle.Success, "funButton", "Fun");
            var messageButton = new DiscordButtonComponent(ButtonStyle.Success, "messageButton", "Messages");
            var modButton = new DiscordButtonComponent(ButtonStyle.Success, "modButton", "Moderation(ADMIN ONLY)");
            var userButton = new DiscordButtonComponent(ButtonStyle.Success, "userButton", "User");
            var helpMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Azure)
                .WithTitle("Help Menu")
                .WithDescription("Please pick a button for more information on the commands")
                )
                .AddComponents(funButton, messageButton, modButton, userButton);

            await ctx.Channel.SendMessageAsync(helpMessage);
        }
    }
}
