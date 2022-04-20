using FriendshipPong.Scripts.Services;
using Godot;

namespace FriendshipPong.Scripts.UI.Menu
{
    public class JoinMenu : MarginContainer
    {
        private LineEdit _ipAddressText;
        private NetworkingService _networkingService;
    
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _networkingService = GetNode<NetworkingService>("/root/NetworkingService");
            _networkingService.OnConnection = () => GetTree().ChangeScene("res://Scenes/Levels/Arena.tscn");
            _ipAddressText = GetNode<LineEdit>("VBoxContainer/HBoxContainer/IpAddressText");
        }

        public void OnReturnMainMenuPressed()
        {
            GetTree().ChangeScene("res://Scenes/Menus/MainMenu.tscn");
        }

        public void OnConnectPressed()
        {
            _networkingService.Join(_ipAddressText.Text);
        }
    }
}
