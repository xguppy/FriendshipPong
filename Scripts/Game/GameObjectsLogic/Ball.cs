using Godot;

namespace FriendshipPong.Scripts.Game.GameObjectsLogic
{
    public class Ball : KinematicBody2D
    {
        private const int Speed = 200;
        private Vector2 _velocity = Vector2.Zero;
    
        public override void _Ready()
        {
            if (!IsNetworkMaster()) return;
            
            GD.Randomize();
            _velocity.x = (new float[] { 1, -1 })[GD.Randi() % 2];
            _velocity.y =  (new float[] { 1, -1 })[GD.Randi() % 2];
        }

        public override void _PhysicsProcess(float delta)
        {
            if (!IsNetworkMaster()) return;
            
            var collisionObject = MoveAndCollide(_velocity * Speed * delta);
            if (collisionObject != null)
            {
                _velocity = _velocity.Bounce(collisionObject.Normal);
            }
            RpcUnreliable(nameof(SetPosition), GlobalTransform);
        }
    
        [Remote]
        private void SetPosition(Transform2D position)
        {
            GlobalTransform = position;
        }
    }
}
