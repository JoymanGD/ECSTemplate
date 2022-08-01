using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Common.ECS.Components;
using DefaultEcs.Command;

namespace Common.ECS.Systems
{
    [With(typeof(Transform))]
    [With(typeof(Follower))]
    public partial class FollowingSystem : AEntitySetSystem<GameTime>
    {
        private EntityCommandRecorder EntityCommandRecorder = new EntityCommandRecorder();
        
        [Update]
        private void Update(ref Transform transform, ref Follower follower, GameTime gameTime, in Entity entity)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector3 targetCenterPosition = GetTargetPosition(follower.FollowTargets);

            float targetsDistance = GetTargetsDistance(targetCenterPosition, follower.FollowTargets);
            follower.SetTargetsDistance(targetsDistance);
            
            Vector3 followingPosition = transform.Position;

            if(follower.FollowX)
            {
                followingPosition.X = targetCenterPosition.X;
            }

            if(follower.FollowY)
            {
                followingPosition.Y = targetCenterPosition.Y;
            }

            if(follower.FollowZ)
            {
                followingPosition.Z = targetCenterPosition.Z;
            }

            followingPosition += follower.Offset;

            Vector3 moveVector = followingPosition - transform.Position;

            var distance = moveVector.Length();
            
            if(distance > 0.01f)
            {
                if(EntityCommandRecorder.Size > 0) EntityCommandRecorder.Clear();
                
                var followSpeed = MathHelper.Clamp(distance, 0, follower.FollowSpeed);
                moveVector.Normalize();
                EntityCommandRecorder.Record(entity).Set(new Translation(moveVector, followSpeed * elapsedTime));
            
                if(EntityCommandRecorder.Size > 0) EntityCommandRecorder.Execute();
            }
        }

        private Vector3 GetTargetPosition(Transform[] followTargets)
        {
            Vector3 targetPosition = Vector3.Zero;

            foreach (var item in followTargets)
            {
                targetPosition += item.Position;
            }

            targetPosition /= followTargets.Length;

            return targetPosition;
        }

        private float GetTargetsDistance(Vector3 targetsCenterPosition, Transform[] followTargets)
        {
            float targetsDistance = 0;

            foreach (var item in followTargets)
            {
                var distance = Vector3.Distance(item.Position, targetsCenterPosition);
                targetsDistance += distance;
            }

            targetsDistance /= followTargets.Length;

            return targetsDistance;
        }
    }
}