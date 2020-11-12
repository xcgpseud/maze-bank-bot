using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;

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
            var embedBuilder = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Gold,
                Title = "Choose a role",
                Description = "Respond with the number which corresponds with the role you would like to grant.",
            };

            var i = 0;
            roles.ForEach(role => embedBuilder.AddField(
                $"{i++.ToString()}...",
                $"**{role.Name}**"
            ));

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
                    
                    if (num < roles.Count)
                    {
                        var role = roles.ElementAt(num);
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