using System;
using FriendshipPong.Scripts.Controls;
using Godot;

namespace FriendshipPong.Scripts.Game.GameObjectsLogic
{
    public class Ball : KinematicBody2D
    {
        private static readonly Random Random = new Random();
        private float _speed = 200;
        private Vector2 _direction = Vector2.Left;

        public override void _PhysicsProcess(float delta)
        {
            if (!IsNetworkMaster()) return;
            
            var collisionObject = MoveAndCollide(_direction * _speed * delta);
            if (collisionObject != null)
            {
                _speed *= 1.1f;
                _direction = _direction.Bounce(collisionObject.Collider is Player ? 
                    collisionObject.Normal.Rotated(Mathf.Deg2Rad(Random.Next(-15, 15)))
                    : collisionObject.Normal);
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
