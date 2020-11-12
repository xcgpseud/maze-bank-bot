using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using MazeBankBot.App.Helpers;

namespace MazeBankBot.App.Services
{
    public class RoleService
    {
        public List<DiscordRole> SearchForRoles(DiscordGuild guild, string search)
        {
            return guild
                .Roles
                .ToList()
                .FindAll(x => x
                    .Name
                    .ToLower()
                    .Contains(search.ToLower())
                );
        }

        public async Task GiveRole(CommandContext ctx, DiscordMember member, DiscordRole role)
        {
            await ctx.Guild.GrantRoleAsync(member, role);
            await ctx.RespondAsync($"The user {member.Mention} has been granted the role: **{role.Name}**");
        }

        public async Task ChooseRole(CommandContext ctx, DiscordMember member, List<DiscordRole> roles)
        {
            // Get an array of word-representative numbers so we can use the emojis for the numbers
            var range = Enumerable.Range(1, roles.Count);

            var numWords = Enumerable.Range(1, roles.Count)
                .Select(num =>
                    NumberRepresentationHelper.NumberToSingleWordArray(num)
                        .Select(x => $":{x}:")
                ).ToArray();

            var desc = "Respond with the number which corresponds with the role you would like to grant.\n\n";

            for (var i = 0; i < roles.Count; i++)
            {
                var emojis = String.Join(" ", numWords[i]);
                desc += $"{emojis}: **{roles[i].Name}**\n";
            }

            var embedBuilder = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Gold,
                Title = "Choose a role",
                Description = desc,
            };

            await ctx.RespondAsync(embed: embedBuilder.Build());

            // Handle the response, they have 1 minute to reply
            var response = await ctx.Client.GetInteractivityModule().WaitForMessageAsync(
                msg => msg.Author.Id == ctx.Message.Author.Id, // Make sure it's the same person
                TimeSpan.FromMinutes(1)
            );

            if (response != null)
            {
                // Parse their response as an integer
                try
                {
                    var num = int.Parse(response.Message.Content);

                    if (num <= roles.Count)
                    {
                        var role = roles.ElementAt(num - 1);
                        await GiveRole(ctx, member, role);
                    }
                    else
                    {
                        await ctx.RespondAsync("You provided an invalid role ID.");
                    }
                }
                catch (Exception)
                {
                    await ctx.RespondAsync("You did not provide a number. Please start again.");
                }
            }
        }
    }
}