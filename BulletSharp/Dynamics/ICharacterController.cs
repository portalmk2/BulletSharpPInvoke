using BulletSharp.Math;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public interface ICharacterController : IAction
	{
        bool CanJump { get; }
        bool OnGround { get; }

        void Jump();
        void Jump(Vector3 dir);
        void PlayerStep(CollisionWorld collisionWorld, Scalar deltaTime);
        void PreStep(CollisionWorld collisionWorld);
        void Reset(CollisionWorld collisionWorld);
        void SetUpInterpolate(bool value);
        void SetVelocityForTimeInterval(ref Vector3 velocity, Scalar timeInterval);
        void SetWalkDirection(ref Vector3 walkDirection);
        void Warp(ref Vector3 origin);
	}
}
