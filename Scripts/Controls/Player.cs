using Godot;

namespace FriendshipPong.Scripts.Controls
{
	public class Player : KinematicBody2D
	{
		private const int Speed = 200;

		public override void _PhysicsProcess(float delta)
		{
			if (!IsNetworkMaster()) return;
		
			var movingVector = Vector2.Zero;
			if (Input.IsActionPressed("ui_up"))
			{
				movingVector.y += -1;
			}
			if (Input.IsActionPressed("ui_down"))
			{
				movingVector.y += 1;
			}

			movingVector.Normalized();
			movingVector *= Speed;
		
			MoveAndSlide(movingVector, Vector2.Up);
			RpcUnreliable(nameof(SetPosition), GlobalTransform);
		}
	
		[Remote]
		private void SetPosition(Transform2D position)
		{
			GlobalTransform = position;
		}
	}
}
