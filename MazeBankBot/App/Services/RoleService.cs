using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Mapster;
using MazeBankBot.App.DataModels;
using MazeBankBot.App.Helpers;
using MazeBankBot.Database.Repositories;

namespace MazeBankBot.App.Services
{
    public class RoleService
    {
        private readonly RoleRequestRepository _roleRequestRepository;

        public RoleService() => _roleRequestRepository = new RoleRequestRepository();

        public async Task<RoleRequestModel> CreateRoleRequest(ulong roleId, ulong userId)
        {
            var entity = await _roleRequestRepository.Create(roleId, userId);

            return entity.Adapt<RoleRequestModel>();
        }

        public async Task<RoleRequestModel> ApproveRoleRequest(CommandContext ctx, int id)
        {
            var entity = await _roleRequestRepository.ApproveRoleRequest(id);
            var user = ctx.Guild.Members.FirstOrDefault(x =>
                x.Value.Id == entity.UserId
            ).Value;
            var role = ctx.Guild.Roles.FirstOrDefault(x =>
                x.Value.Id == entity.RoleId
            ).Value;

            if (user != null && role != null)
            {
                await GiveRole(ctx, user, role);
            }

            return entity.Adapt<RoleRequestModel>();
        }

        public List<DiscordRole> SearchForRoles(DiscordGuild guild, string search)
        {
            return guild
                .Roles.Select(x => x.Value)
                .ToList()
                .FindAll(x => x
                    .Name
                    .ToLower()
                    .Contains(search.ToLower())
                );
        }

        public async Task GiveRole(CommandContext ctx, DiscordMember member, DiscordRole role)
        {
            await member.GrantRoleAsync(role);
            await ctx.RespondAsync($"The user {member.Mention} has been granted the role: **{role.Name}**");
        }

        public async Task<DiscordRole> ChooseRole(CommandContext ctx, DiscordMember member, List<DiscordRole> roles)
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
            var response = await ctx.Client.GetInteractivity().WaitForMessageAsync(
                msg => msg.Author.Id == ctx.Message.Author.Id, // Make sure it's the same person
                TimeSpan.FromMinutes(1)
            );

            if (!response.TimedOut)
            {
                // Parse their response as an integer
                try
                {
                    var num = int.Parse(response.Result.Content);

                    if (num <= roles.Count)
                    {
                        var role = roles.ElementAt(num - 1);
                        return role;
                        // await GiveRole(ctx, member, role);
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

            return null;
        }
    }
}