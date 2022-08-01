using Microsoft.Xna.Framework;

namespace Common.ECS.Components
{
    public struct Follower
    {
        public Transform[] FollowTargets { get; private set; }
        public float TargetsDistance { get; private set; }
        public float FollowSpeed { get; private set; }
        public Vector3 Offset { get; private set; }
        public bool FollowX { get; private set; }
        public bool FollowY { get; private set; }
        public bool FollowZ { get; private set; }

        public Follower(float followSpeed, bool followX = false, bool followY = false, bool followZ = false, params Transform[] followTargets)
        {
            FollowTargets = followTargets;
            TargetsDistance = 0;
            FollowSpeed = followSpeed;
            Offset = Vector3.Zero;
            FollowX = followX;
            FollowY = followY;
            FollowZ = followZ;
        }

        public Follower(float followSpeed, Vector3 offset, params Transform[] followTargets)
        {
            FollowTargets = followTargets;
            TargetsDistance = 0;
            FollowSpeed = followSpeed;
            Offset = offset;
            FollowX = true;
            FollowY = true;
            FollowZ = true;
        }

        public void SetTargetsDistance(float targetsDistance)
        {
            TargetsDistance = targetsDistance;
        }
    }
}