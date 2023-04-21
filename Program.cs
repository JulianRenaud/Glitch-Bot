using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using Glitch_Bot.Commands;
using Glitch_Bot.Engine.LevelSystem;
using Glitch_Bot.Slash_Commands;
using Glitch_Bot.YouTube;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Glitch_Bot
{
    public sealed class Program
    {
        public TimeSpan Timespan;
        public static DiscordClient Client { get; private set; }
        public static InteractivityExtension Interactivity { get; private set; }
        public static CommandsNextExtension Commands { get; private set; }

        private static YouTubeVideo _video = new YouTubeVideo();
        private static YouTubeVideo temp = new YouTubeVideo();
        private static YTEngine _YouTubeEngine = new YTEngine();
        static async Task Main(string[] args)
        {
            //var bot = new Bot(); //void method
            //bot.RunAsync().GetAwaiter().GetResult();
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync();

            var configJson = JsonConvert.DeserializeObject<ConfigJSON>(json);

            var config = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            Client = new DiscordClient(config);
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            Client.Ready += OnClientReady;
            Client.ComponentInteractionCreated += ButtonPressResponse;
            Client.MessageCreated += MessageSendHandler;

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            var slashCommandsConfig = Client.UseSlashCommands();

            //Prefix Based Commands
            Commands.RegisterCommands<FunCommands>();
            Commands.RegisterCommands<MessageCommands>();
            Commands.RegisterCommands<UserCommands>();

            //Slash Commands
            slashCommandsConfig.RegisterCommands<MessageCommandsSL>(890627352206381117);
            slashCommandsConfig.RegisterCommands<MessageCommandsSL>(1098718313057628312);
            slashCommandsConfig.RegisterCommands<FunCommandsSL>(890627352206381117);
            slashCommandsConfig.RegisterCommands<FunCommandsSL>(1098718313057628312);
            slashCommandsConfig.RegisterCommands<ModerationCommandsSL>(890627352206381117);
            slashCommandsConfig.RegisterCommands<ModerationCommandsSL>(1098718313057628312);
            slashCommandsConfig.RegisterCommands<UserSubmissionCommandsSL>(890627352206381117);
            slashCommandsConfig.RegisterCommands<UserSubmissionCommandsSL>(1098718313057628312);


            Commands.CommandErrored += OnCommandError;

            await Client.ConnectAsync();


            await StartVideoUploadNotifier(Client, 1095103862689497188);
            await Task.Delay(-1);
        }


        private static async Task MessageSendHandler(DiscordClient sender, MessageCreateEventArgs e)
        {
            var get = new LevelEngine();
            var xp = get.GetLatestTime(e.Author.Username, e.Guild.Id);
            if (xp == true)
            {

                var levelEngine = new LevelEngine();
                var addedXP = levelEngine.AddXP(e.Author.Username, e.Guild.Id);
                if (levelEngine.levelledUp == true)
                {
                    var levelledUp = new DiscordEmbedBuilder()
                    {
                        Title = e.Author.Username + " has leveled up!!!",
                        Description = "Level: " + (levelEngine.GetUser(e.Author.Username, e.Guild.Id).Level - 1).ToString() + " --> " + levelEngine.GetUser(e.Author.Username, e.Guild.Id).Level.ToString(),
                        Color = DiscordColor.Green
                    };
                    await e.Channel.SendMessageAsync(embed: levelledUp);

                }
            }
        }

        private static async Task ButtonPressResponse(DiscordClient sender, ComponentInteractionCreateEventArgs e)
        {
            if (e.Interaction.Data.CustomId == "1")
            {
                await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent("Button 1 Clicked!"));
            }
            else if (e.Interaction.Data.CustomId == "2")
            {
                await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent("Button 2 Clicked!"));

            }
            else if (e.Interaction.Data.CustomId == "funButton")
            {
                string CommandsList = "PREFIX COMMANDS(-) \n" +
                                         "-add [number1] [number2] -> adds two numbers together \n" +
                                         "-subtract [number1] [number2] -> subtracts two numbers from each other \n" +
                                         "-cardgame(cd) -> plays a simple card game with a bot \n\n" +
                                         "SLASH COMMANDS(/) \n" +
                                         "/caption -> Gives any inserted image a caption";

                await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().AddEmbed(new DiscordEmbedBuilder()
                {
                    Title = "Fun Commands",
                    Description = CommandsList
                }));
            }
            else if (e.Interaction.Data.CustomId == "messageButton")
            {
                string CommandsList = "PREFIX COMMANDS(-) \n" +
                                             "-message [UserInput] -> sends a message of your input \n\n" +
                                             "SLASH COMMANDS(/) \n" +
                                             "/embedmessage [title] [description] -> creates a custom embed message \n" +
                                             "/askGPT [question/] -> Asks a question to ChatGPT Open AI";


                await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().AddEmbed(new DiscordEmbedBuilder()
                {
                    Title = "Message Commands",
                    Description = CommandsList
                }));
            }
            else if (e.Interaction.Data.CustomId == "modButton")
            {
                string CommandsList = "PREFIX COMMANDS(-) \n" +
                                             "There are no current moderation commands for prefix\n\n" +
                                             "SLASH COMMANDS(/) \n" +
                                             "/ban [user] [reason] -> Bans a member from the server \n" +
                                             "/unban [user] -> Unbans a member from the server \n" +
                                             "/kick [user] -> Kicks a member from the server \n" +
                                             "/timeout [user] -> Timeouts a member in the server";


                await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().AddEmbed(new DiscordEmbedBuilder()
                {
                    Title = "Moderation Commands",
                    Description = CommandsList
                }));
            }
            else if (e.Interaction.Data.CustomId == "userButton")
            {
                string CommandsList = "PREFIX COMMANDS(-) \n" +
                                             "-profile(-pr) -> creates/views your profile\n\n" +
                                             "SLASH COMMANDS(/) \n" +
                                             "/poll [poll-question] [timelimit] [option1] [option2] [option3] [option4] -> creates a poll with a time limit and four choices";


                await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().AddEmbed(new DiscordEmbedBuilder()
                {
                    Title = "User Submission Commands",
                    Description = CommandsList
                }));
            }
        }

        private static Task OnClientReady(DiscordClient sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

        private static async Task OnCommandError(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            if (e.Exception is ChecksFailedException)
            {
                var castedException = (ChecksFailedException)e.Exception;
                string cooldownTimer = string.Empty;

                foreach (var check in castedException.FailedChecks)
                {
                    var cooldown = (CooldownAttribute)check;
                    TimeSpan timeleft = cooldown.GetRemainingCooldown(e.Context);
                    cooldownTimer = timeleft.ToString(@"hh\:mm\:ss");
                }

                var cooldownMessage = new DiscordEmbedBuilder()
                {
                    Title = "Wait for the Cooldown to End",
                    Description = "Remaining time: " + cooldownTimer,
                    Color = DiscordColor.Red
                };

                await e.Context.Channel.SendMessageAsync(embed: cooldownMessage);
            }
        }
        private static async Task StartVideoUploadNotifier(DiscordClient client, ulong channelIdToNotify)
        {
            var timer = new Timer(420000);
            timer.Elapsed += async (sender, e) =>
            {
                _video = _YouTubeEngine.GetLatestVideo();
                var lastCheckedAt = DateTime.Now;


                if (_video != null)
                {
                    if (temp.videoTitle == _video.videoTitle)
                    {
                        Console.WriteLine("Same video detected!!!");
                    }
                    else if (_video.PublishedAt < lastCheckedAt)
                    {
                        var guild = await client.GetGuildAsync(890627352206381117);
                        var roleMention = guild.GetRole(999011799787634878).Mention;

                        var message = roleMention + "\n" +
                                        $"NEW VIDEO | **{_video.videoTitle}** \n" +
                                        $"Published at: {_video.PublishedAt} \n" +
                                        "URL: " + _video.videoUrl;

                        await client.GetChannelAsync(channelIdToNotify).Result.SendMessageAsync(message);
                        temp = _video;
                    }
                }
            };
            timer.Start();
        }

    }
}
