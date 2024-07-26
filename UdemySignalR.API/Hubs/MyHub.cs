﻿using API.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using UdemySignalR.API.Models;

namespace UdemySignalR.API.Hubs
{
    public class MyHub : Hub
    {
        private readonly AppDbContext _context;
        public MyHub(AppDbContext context)
        {
            _context = context;
        }
        private static List<string> Names { get; set; } = new List<string>();
        private static int ClientCount { get; set; } = 0;
        public static int TeamCount { get; set; } = 7;
        public async Task SendProduct(Product p)
        {
            Clients.All.SendAsync("ReceiveProduct", p);
        }
        public async Task SendName(string name)
        {
            if (Names.Count >= TeamCount)
            {
                await Clients.Caller.SendAsync("Error", $"Takım {TeamCount} kişiden fazla olamaz.");
            }
            else
            {
                Names.Add(name);
                await Clients.All.SendAsync("ReceiveName", name);
            }
        }
        public async Task GetNames()
        {
            await Clients.All.SendAsync("ReceiveNames", Names);
        }
        //Groups
        public async Task AddToGroup(string teamName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, teamName);
        }
        public async Task SendNameByGroup(string Name, string teamName)
        {
            var team = _context.Teams.Where(x => x.Name == teamName).FirstOrDefault();
            if (team != null)
            {
                team.Users.Add(new User { Name = Name });
            }
            else
            {
                var newteam = new Team { Name = teamName };
                newteam.Users.Add(new User { Name = Name });
                _context.Teams.Add(newteam);
            }
            await _context.SaveChangesAsync();
            await Clients.Groups(teamName).SendAsync("ReceiveMessageByGroup", Name, team.Id);
        }
        public async Task GetNamesByGroup()
        {
            var teams = await _context.Teams.Include(x => x.Users).Select(x => new
            {
                teamId = x.Id,
                Users = x.Users.Select(u => new { u.Id, u.Name }).ToList()
            }).ToListAsync();
            await Clients.All.SendAsync("ReceiveNamesByGroup", teams);
        }
        public async Task RemoveToGroup(string teamName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, teamName);
        }
        public override async Task OnConnectedAsync()
        {
            ClientCount++;
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ClientCount--;
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
            await base.OnDisconnectedAsync(exception);
        }
    }
}