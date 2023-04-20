using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using OpenAI_API;
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
