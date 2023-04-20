using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Glitch_Bot.External_Classes;
using System.Threading.Tasks;

namespace Glitch_Bot.Commands
{
    public class FunCommands : BaseCommandModule
    {
        [Command("admin")]
        [RequireRoles(RoleCheckMode.MatchNames, "Admin")]
        public async Task TestCommand(CommandContext ctx)
        {
            if (ctx.Channel.Id == 1032379626951016552)
            {
                await ctx.Channel.SendMessageAsync("woohoo party time! admins da best!");

            }
            else
            {
                await ctx.Channel.SendMessageAsync("This command is not executable here!");
            }
        }
        

        [Command("add")]
        public async Task Addition(CommandContext ctx, int number1, int number2)
        {
            int answer = number1 + number2;
            await ctx.Channel.SendMessageAsync(answer.ToString());
        }

        [Command("subtract")]
        public async Task Subtract(CommandContext ctx, int number1, int number2)
        {
            int answer = number1 - number2;
            await ctx.Channel.SendMessageAsync(answer.ToString());
        }

        [Command("cardgame")]
        [Aliases("cd")]
        public async Task SimpleCardCame(CommandContext ctx)
        {
            

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
