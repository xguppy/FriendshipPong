using FriendshipPong.Scripts.Services;
using Godot;

namespace FriendshipPong.Scripts.UI.Menu
{
	public class MainMenu : MarginContainer
	{
		private NetworkingService _networkingService;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_networkingService = GetNode<NetworkingService>("/root/NetworkingService");
			_networkingService.OnConnection = () => GetTree().ChangeScene("res://Scenes/Levels/Arena.tscn");
		}

		public void OnExitPressed()
		{
			GetTree().Quit();
		}

		public void OnHostGamePressed()
		{
			_networkingService.Host();
		}

		public void OnJoinGamePressed()
		{
			GetTree().ChangeScene("res://Scenes/Menus/JoinMenu.tscn");
		}
	}
}
