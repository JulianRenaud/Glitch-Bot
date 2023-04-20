using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Glitch_Bot.Engine.LevelSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch_Bot.Commands
{
    public class UserCommands : BaseCommandModule
    {
        [Command("profile")]
        [Aliases("pr")]
        public async Task ProfileCommand(CommandContext ctx)
        {
            string username = ctx.User.Username;
            ulong guildID = ctx.Guild.Id;
            var userDetails = new DUser()
            {
                UserName = username,
                GuildID = ctx.Guild.Id,
                avatarURL = ctx.User.AvatarUrl,
                Level = 1,
                XP = 0,
                MaxXP = 225
            };

            var levelEngine = new LevelEngine();
            var doesExist = levelEngine.CheckUserExists(username, guildID);

            if (doesExist == false)
            {
                var isStored = levelEngine.StoreDUserDetails(userDetails);

                if (isStored == true)
                {
                    var successMessage = new DiscordEmbedBuilder()
                    {
                        Title = "Successfully created profile",
                        Description = "You have created a profile with JAAK_Bot!",
                        Color = DiscordColor.Green
                    };
                    await ctx.Channel.SendMessageAsync(embed: successMessage);

                    var pulledUser = levelEngine.GetUser(username, guildID);

                    var profile = new DiscordMessageBuilder()
                        .AddEmbed(new DiscordEmbedBuilder()

                        .WithColor(DiscordColor.Azure)
                        .WithTitle(pulledUser.UserName + "'s Profile")
                        .WithThumbnail(pulledUser.avatarURL)
                        .AddField("---Level---", pulledUser.Level.ToString())
                        .AddField("---XP---", pulledUser.XP.ToString() + "/" + pulledUser.MaxXP.ToString())
                        );

                    await ctx.Channel.SendMessageAsync(profile);
                }
                else
                {
                    var failedMessage = new DiscordEmbedBuilder()
                    {
                        Title = "Error Occured",
                        Description = "Something went wrong while trying to create a profile!\nPlease try again later.",
                        Color = DiscordColor.Red
                    };
                    await ctx.Channel.SendMessageAsync(embed: failedMessage);
                }
                
            }
            else
            {
                var pulledUser = levelEngine.GetUser(username, guildID);

                var profile = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Azure)
                    .WithTitle(pulledUser.UserName + "'s Profile")
                    .WithThumbnail(pulledUser.avatarURL)
                    .AddField("---Level---", pulledUser.Level.ToString())
                    .AddField("---XP---", pulledUser.XP.ToString() + "/" + pulledUser.MaxXP.ToString())
                    );

                await ctx.Channel.SendMessageAsync(profile);

            }
        }
    }
}
