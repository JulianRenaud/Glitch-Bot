using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using OpenAI_API;
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
        [SlashCommand("AskGPT", "Asks chatGPT a question")]
        public async Task ChatGPT(InteractionContext ctx, [Option("Question", "What you want to ask ChatGPT")] string query)
        {
            await ctx.DeferAsync();

            var hidden = new hiddenAPIs();
            var api = new OpenAIAPI(hidden.ChatGPTAPI());

            var chat = api.Chat.CreateConversation();
            chat.AppendSystemMessage("Type in a question!");

            chat.AppendUserInput(query);

            string response = await chat.GetResponseFromChatbot();

            var outputEmbed = new DiscordEmbedBuilder()
            {
                Title = "Result From Question: " + query,
                Description = response,
                Color = DiscordColor.White
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(outputEmbed));
        }
    }

}
