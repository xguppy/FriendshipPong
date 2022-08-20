using FriendshipPong.Scripts.Services;
using Godot;

namespace FriendshipPong.Scripts.Game
{
    public class ArenaScript : Node2D
    {
        private Position2D _playerOnePosition;
        private Position2D _playerTwoPosition;
        private Position2D _ballPosition;

        private NetworkingService _networkingService;
    
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _networkingService = GetNode<NetworkingService>("/root/NetworkingService");
        
            _playerOnePosition = GetNode<Position2D>("PlayerPosition");
            _playerTwoPosition = GetNode<Position2D>("PlayerTwoPosition");
            _ballPosition = GetNode<Position2D>("BallPosition");
            var playerOneId = IsNetworkMaster() ? _networkingService.LocalUserId : _networkingService.RemoteUserId;
            var playerTwoId = IsNetworkMaster() ? _networkingService.RemoteUserId : _networkingService.LocalUserId;
            
            var ball = (KinematicBody2D)ResourceLoader.Load<PackedScene>("res://Scenes/GameObjects/Ball.tscn").Instance();
            ball.GlobalTransform = _ballPosition.GlobalTransform;
            ball.SetNetworkMaster(playerOneId);
            AddChild(ball);

            var player1 = (KinematicBody2D)ResourceLoader.Load<PackedScene>("res://Scenes/GameObjects/Padle.tscn").Instance();
            player1.Name = playerOneId.ToString();
            player1.SetNetworkMaster(playerOneId);
            player1.GlobalTransform = _playerOnePosition.GlobalTransform;
            AddChild(player1);
        
            var player2 = (KinematicBody2D)ResourceLoader.Load<PackedScene>("res://Scenes/GameObjects/Padle.tscn").Instance();
            player2.Name = playerTwoId.ToString();
            player2.SetNetworkMaster(playerTwoId);
            player2.GlobalTransform = _playerTwoPosition.GlobalTransform;
            AddChild(player2);
        }
    }
}
