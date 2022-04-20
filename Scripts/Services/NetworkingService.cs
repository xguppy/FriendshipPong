using System;
using System.Net;
using Godot;

namespace FriendshipPong.Scripts.Services
{
    public class NetworkingService : Node
    {
        public int UserId { get; private set; } = -1;
    
        public Action OnConnection { get; set; }
    
        private const int Port = 5000;
        private const int ClientsCount = 1;

        public override void _Ready()
        {
            GetTree().Connect("network_peer_connected", this, nameof(Connected));
        }
    
        public void Host()
        {
            var server = new NetworkedMultiplayerENet();
            server.CreateServer(Port, ClientsCount);
            GetTree().NetworkPeer = server;
        }

        public void Join(string ipAddress)
        {
            if (!IPAddress.TryParse(ipAddress, out var ip)) return;
        
            var client = new NetworkedMultiplayerENet();
            client.CreateClient(ip.ToString(), Port);
            GetTree().NetworkPeer = client;
        }

        private void Connected(int clientId)
        {
            UserId = clientId;
            OnConnection.Invoke();
        }
    }
}
