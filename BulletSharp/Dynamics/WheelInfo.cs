using System;
using BulletSharp.Math;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public struct WheelInfoConstructionInfo
	{
        public bool IsFrontWheel;
        public Vector3 ChassisConnectionCS;
        public Scalar FrictionSlip;
        public Scalar MaxSuspensionForce;
        public Scalar MaxSuspensionTravelCm;
        public Scalar SuspensionRestLength;
        public Scalar SuspensionStiffness;
        public Vector3 WheelAxleCS;
        public Vector3 WheelDirectionCS;
        public Scalar WheelRadius;
        public Scalar WheelsDampingCompression;
        public Scalar WheelsDampingRelaxation;
	}

    public struct RaycastInfo
    {
        public Vector3 ContactNormalWS;
        public Vector3 ContactPointWS;
        public Object GroundObject;
        public Vector3 HardPointWS;
        public bool IsInContact;
        public Scalar SuspensionLength;
        public Vector3 WheelAxleWS;
        public Vector3 WheelDirectionWS;
    }

    public class WheelInfo
    {
        public WheelInfo(WheelInfoConstructionInfo ci)
        {
            SuspensionRestLength1 = ci.SuspensionRestLength;
            MaxSuspensionTravelCm = ci.MaxSuspensionTravelCm;

            WheelsRadius = ci.WheelRadius;
            SuspensionStiffness = ci.SuspensionStiffness;
            WheelsDampingCompression = ci.WheelsDampingCompression;
            WheelsDampingRelaxation = ci.WheelsDampingRelaxation;
            ChassisConnectionPointCS = ci.ChassisConnectionCS;
            WheelDirectionCS = ci.WheelDirectionCS;
            WheelAxleCS = ci.WheelAxleCS;
            FrictionSlip = ci.FrictionSlip;
            Steering = 0;
            EngineForce = 0;
            Rotation = 0;
            DeltaRotation = 0;
            Brake = 0;
            RollInfluence = 0.1f;
            IsFrontWheel = ci.IsFrontWheel;
            MaxSuspensionForce = ci.MaxSuspensionForce;

            //ClientInfo = IntPtr.Zero;
            //ClippedInvContactDotSuspension = 0;
            WorldTransform = Matrix.Identity;
            //WheelsSuspensionForce = 0;
            //SuspensionRelativeVelocity = 0;
            //SkidInfo = 0;
            RaycastInfo = new RaycastInfo();
        }

        public void UpdateWheel(RigidBody chassis, RaycastInfo raycastInfo)
        {
            if (raycastInfo.IsInContact)
            {
                Scalar project = Vector3.Dot(raycastInfo.ContactNormalWS, raycastInfo.WheelDirectionWS);
                Vector3 chassis_velocity_at_contactPoint;
                Vector3 relpos = raycastInfo.ContactPointWS - chassis.CenterOfMassPosition;
                chassis_velocity_at_contactPoint = chassis.GetVelocityInLocalPoint(relpos);
                Scalar projVel = Vector3.Dot(raycastInfo.ContactNormalWS, chassis_velocity_at_contactPoint);
                if (project >= -0.1f)
                {
                    SuspensionRelativeVelocity = 0;
                    ClippedInvContactDotSuspension = 1.0f / 0.1f;
                }
                else
                {
                    Scalar inv = -1.0f / project;
                    SuspensionRelativeVelocity = projVel * inv;
                    ClippedInvContactDotSuspension = inv;
                }

            }

            else    // Not in contact : position wheel in a nice (rest length) position
            {
                RaycastInfo.SuspensionLength = SuspensionRestLength;
                SuspensionRelativeVelocity = 0;
                RaycastInfo.ContactNormalWS = -raycastInfo.WheelDirectionWS;
                ClippedInvContactDotSuspension = 1.0f;
            }
        }

        public Scalar SuspensionRestLength
        {
            get { return SuspensionRestLength1; }
        }

        public bool IsFrontWheel;
        public Scalar Brake;
        public Vector3 ChassisConnectionPointCS;
        public IntPtr ClientInfo;
        public Scalar ClippedInvContactDotSuspension;
        public Scalar DeltaRotation;
        public Scalar EngineForce;
        public Scalar FrictionSlip;
        public Scalar MaxSuspensionForce;
        public Scalar MaxSuspensionTravelCm;
        public RaycastInfo RaycastInfo;
        public Scalar RollInfluence;
        public Scalar Rotation;
        public Scalar SkidInfo;
        public Scalar Steering;
        public Scalar SuspensionRelativeVelocity;
        public Scalar SuspensionRestLength1;
        public Scalar SuspensionStiffness;
        public Vector3 WheelAxleCS;
        public Vector3 WheelDirectionCS;
        public Scalar WheelsDampingCompression;
        public Scalar WheelsDampingRelaxation;
        public Scalar WheelsRadius;
        public Scalar WheelsSuspensionForce;
        public Matrix WorldTransform;
    }
}
