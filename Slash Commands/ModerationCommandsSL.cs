using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Threading.Tasks;

namespace Glitch_Bot.Slash_Commands
{
    public class ModerationCommandsSL : ApplicationCommandModule
    {
        [SlashCommand("ban", "Bans a user from the server")]
        public async Task Ban(InteractionContext ctx, [Option("User", "The user you want to ban")] DiscordUser user, [Option("Reason", "Reason for ban")] string reason = null)
        {
            await ctx.DeferAsync();

            if (ctx.Member.Permissions.HasPermission(Permissions.Administrator) || ctx.Member.Permissions.HasPermission(Permissions.BanMembers))
            {
                var member = (DiscordMember)user;
                await ctx.Guild.BanMemberAsync(member, 0, reason);

                var banMessage = new DiscordEmbedBuilder()
                {
                    Title = "Banned user: " + member.Username,
                    Description = "This member was banned by: " + ctx.User.Username + "\n" + "Reason: " + reason,
                    Color = DiscordColor.Red
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(banMessage));
            }
            else
            {
                var nonAdminMessage = new DiscordEmbedBuilder()
                {
                    Title = "Access Denied",
                    Description = "You need to have admin controls to execute this command!",
                    Color = DiscordColor.Red
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(nonAdminMessage));
            }
        }
        
        [SlashCommand("unban", "Unbans a user from the server")]
        public async Task Unban(InteractionContext ctx, [Option("User", "The user you want to unban")] DiscordUser user)
        {
            await ctx.DeferAsync();

            if (ctx.Member.Permissions.HasPermission(Permissions.Administrator) || ctx.Member.Permissions.HasPermission(Permissions.BanMembers))
            {
                var member = (DiscordMember)user;
                await ctx.Guild.UnbanMemberAsync(member);

                var banMessage = new DiscordEmbedBuilder()
                {
                    Title = "Unbanned user: " + member.Username,
                    Description = "This member was unbanned by: " + ctx.User.Username,
                    Color = DiscordColor.Red
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(banMessage));
            }
            else
            {
                var nonAdminMessage = new DiscordEmbedBuilder()
                {
                    Title = "Access Denied",
                    Description = "You need to have admin controls to execute this command!",
                    Color = DiscordColor.Red
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(nonAdminMessage));
            }
        }

        [SlashCommand("kick", "Kicks a user from the server")]
        public async Task Kick(InteractionContext ctx, [Option("User", "The user you want to kick")] DiscordUser user)
        {
            await ctx.DeferAsync();

            if (ctx.Member.Permissions.HasPermission(Permissions.Administrator) || ctx.Member.Permissions.HasPermission(Permissions.KickMembers))
            {
                var member = (DiscordMember)user;
                await member.RemoveAsync();

                var kickMessage = new DiscordEmbedBuilder()
                {
                    Title = member.Username + " was kicked from the server!",
                    Description = "This member was kicked by: " + ctx.User.Username,
                    Color = DiscordColor.Red
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(kickMessage));
            }
            else
            {
                var nonAdminMessage = new DiscordEmbedBuilder()
                {
                    Title = "Access Denied",
                    Description = "You need to have admin controls to execute this command!",
                    Color = DiscordColor.Red
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(nonAdminMessage));
            }
        }

        [SlashCommand("timeout", "Timeouts a user in the server")]

        public async Task Timeout(InteractionContext ctx, [Option("User", "The user you want to timeout")] DiscordUser user, [Option("Duration", "The ammount of seconds this user will be on timeout for")] long duration)
        {
            await ctx.DeferAsync();

            if (ctx.Member.Permissions.HasPermission(Permissions.Administrator) || ctx.Member.Permissions.HasPermission(Permissions.KickMembers))
            {
                var timeDuration = DateTime.Now + TimeSpan.FromSeconds(duration);
                var member = (DiscordMember)user;
                await member.TimeoutAsync(timeDuration);

                var timeoutMessage = new DiscordEmbedBuilder()
                {
                    Title = member.Username + "has been put on timeout!",
                    Description = "This member was put on timeout by: " + ctx.User.Username + "\n" + "Duration: " + TimeSpan.FromSeconds(duration).ToString(),
                    Color = DiscordColor.Red
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(timeoutMessage));
            }
            else
            {
                var nonAdminMessage = new DiscordEmbedBuilder()
                {
                    Title = "Access Denied",
                    Description = "You need to have admin controls to execute this command!",
                    Color = DiscordColor.Red
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(nonAdminMessage));
            }
        }

        [SlashCommand("createVC", "Creates a voice channel")]
        public async Task CreateVC(InteractionContext ctx, [Option("Channel-Name", "The channel name")] string channelName, [Option("Member-Limit", "add a user limit to this voice channel")] string channelLimit = null)
        {
            await ctx.DeferAsync();

            if (ctx.Member.Permissions.HasPermission(Permissions.Administrator) || ctx.Member.Permissions.HasPermission(Permissions.ManageChannels))
            {
                var channelUsersParse = int.TryParse(channelLimit, out var channelCount);

                if (channelLimit != null && channelUsersParse == true)
                {
                    await ctx.Guild.CreateVoiceChannelAsync(channelName, null, null, channelCount);

                    var successMsg = new DiscordEmbedBuilder()
                    {
                        Title = "Created voice channel" + channelName,
                        Description = "The channel was created with a limit of " + channelCount + "max users!",
                        Color = DiscordColor.Green
                    };

                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(successMsg));
                }
                else if (channelLimit == null)
                {
                    await ctx.Guild.CreateVoiceChannelAsync(channelName);

                    var successMsg = new DiscordEmbedBuilder()
                    {
                        Title = "Created voice channel" + channelName,
                        Description = "The channel was created with a limit of 0 max users!",
                        Color = DiscordColor.Green
                    };

                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(successMsg));
                }
                else if (channelUsersParse == false)
                {
                    var failMsg = new DiscordEmbedBuilder()
                    {
                        Title = "Invalid input",
                        Description = "Please provide a valid input for the user limit!",
                        Color = DiscordColor.Red
                    };

                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(failMsg));
                }
            }
            else
            {
                var nonAdminMessage = new DiscordEmbedBuilder()
                {
                    Title = "Access Denied",
                    Description = "You need to have admin controls to execute this command!",
                    Color = DiscordColor.Red
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(nonAdminMessage));
            }
        }
    }
}
