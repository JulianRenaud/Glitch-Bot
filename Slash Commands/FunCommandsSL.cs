using DSharpPlus;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Glitch_Bot.External_Classes;
using System.Threading.Tasks;

namespace Glitch_Bot.Slash_Commands
{
    internal class FunCommandsSL : ApplicationCommandModule
    {
        [SlashCommand("caption", "Give any image a caption")]
        public async Task CaptionCommand(InteractionContext ctx, [Option("Caption", "The caption you want the image to have")] string caption,
                                                                 [Option("Image", "The image you want to upload")] DiscordAttachment picture)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Caption"));

            var captionMessage = new DiscordEmbedBuilder()
            {
                Title = caption,
                ImageUrl = picture.Url,
                Color = DiscordColor.Azure,
            };
            await ctx.Channel.SendMessageAsync(embed: captionMessage);
        }
        [SlashCommand("cardgame", "Plays a card game against the bot")]
        public async Task SimpleCardCame(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Cardgame"));

            var Card = new CardBuilder();

            var userCardMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Azure)
                .WithTitle("Your Card")
                .WithDescription("Your Draw: " + Card.UserCard)
                );


            await ctx.Channel.SendMessageAsync(userCardMessage);



            var botCardMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Azure)
                .WithTitle("Card Game")
                .WithDescription("The bot's Draw: " + Card.BotCard)
                );


            await ctx.Channel.SendMessageAsync(botCardMessage);

            if (Card.UserNumber > Card.BotNumber)
            {
                var winningMessage = new DiscordEmbedBuilder()
                {
                    Title = "**You Won The Game!**",
                    Color = DiscordColor.Green
                };
                await ctx.Channel.SendMessageAsync(embed: winningMessage);
                return;

            }
            else if (Card.UserNumber < Card.BotNumber)
            {
                var losingMessage = new DiscordEmbedBuilder()
                {
                    Title = "**You Lost The Game!**",
                    Color = DiscordColor.Red

                };
                await ctx.Channel.SendMessageAsync(embed: losingMessage);
                return;

            }
            else
            {
                var tieMessage = new DiscordEmbedBuilder()
                {
                    Title = "**You Tied The Game!**",
                    Color = DiscordColor.White

                };
                await ctx.Channel.SendMessageAsync(embed: tieMessage);
                return;

            }
        }
    }
}
