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
            await ctx.DeferAsync();

            var captionMessage = new DiscordEmbedBuilder()
            {
                Title = caption,
                ImageUrl = picture.Url,
                Color = DiscordColor.Azure,
            };
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(captionMessage));
        }
        [SlashCommand("cardgame", "Plays a card game against the bot")]
        public async Task SimpleCardCame(InteractionContext ctx)
        {
            await ctx.DeferAsync();

            var Card = new CardBuilder();

            var userCardMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Azure)
                .WithTitle("Your Card")
                .WithDescription("Your Draw: " + Card.UserCard)
                );


            var botCardMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Azure)
                .WithTitle("Card Game")
                .WithDescription("The bot's Draw: " + Card.BotCard)
                );


 
            if (Card.UserNumber > Card.BotNumber)
            {
                var winningMessage = new DiscordEmbedBuilder()
                {
                    Title = "**You Won The Game!**",
                    Color = DiscordColor.Green
                };
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(userCardMessage.Embed).AddEmbed(botCardMessage.Embed).AddEmbed(winningMessage));
                return;

            }
            else if (Card.UserNumber < Card.BotNumber)
            {
                var losingMessage = new DiscordEmbedBuilder()
                {
                    Title = "**You Lost The Game!**",
                    Color = DiscordColor.Red

                };
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(userCardMessage.Embed).AddEmbed(botCardMessage.Embed).AddEmbed(losingMessage));
                return;

            }
            else
            {
                var tieMessage = new DiscordEmbedBuilder()
                {
                    Title = "**You Tied The Game!**",
                    Color = DiscordColor.White

                };
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(userCardMessage.Embed).AddEmbed(botCardMessage.Embed).AddEmbed(tieMessage));
                return;

            }

            
        }
    }
}
