using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp.SoftBody
{
	public class DefaultSoftBodySolver : SoftBodySolver
	{
		public DefaultSoftBodySolver()
			: base(btDefaultSoftBodySolver_new())
		{
		}
		/*
		public void CopySoftBodyToVertexBuffer(SoftBody softBody, VertexBufferDescriptor vertexBuffer)
		{
			btDefaultSoftBodySolver_copySoftBodyToVertexBuffer(_native, softBody._native,
				vertexBuffer._native);
		}
		*/
	}
}
