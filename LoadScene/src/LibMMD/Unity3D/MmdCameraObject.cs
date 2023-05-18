using System;
using LibMMD.Motion;
using LibMMD.Reader;
using UnityEngine;

namespace LibMMD.Unity3D
{
	// Token: 0x0200000B RID: 11
	public class MmdCameraObject : MonoBehaviour
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000047F4 File Offset: 0x000029F4
		public static MmdCameraObject CreateGameObject(string name = "MMDCameraObject")
		{
			GameObject gameObject = new GameObject(name);
			MmdCameraObject mmdCameraObject = gameObject.AddComponent<MmdCameraObject>();
			GameObject gameObject2 = new GameObject("Camera");
			gameObject2.transform.SetParent(gameObject.transform);
			mmdCameraObject.m_CameraTf = gameObject2.transform;
			return mmdCameraObject;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00004836 File Offset: 0x00002A36
		public bool LoadCameraMotion(string path)
		{
			if (path == null)
			{
				this._cameraMotion = null;
				return false;
			}
			this._cameraMotion = new VmdReader2().ReadCameraMotion(path);
			if (this._cameraMotion.KeyFrames.Count == 0)
			{
				return false;
			}
			this.ResetMotion();
			return true;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00004870 File Offset: 0x00002A70
		public void ResetMotion()
		{
			this._playTime = 0.0;
			this.Playing = false;
			this.Refresh();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000488E File Offset: 0x00002A8E
		public void SetPlayPos(double pos)
		{
			this._playTime = pos;
			this.Refresh();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000048A0 File Offset: 0x00002AA0
		private void Update()
		{
			if (!this.Playing || this._cameraMotion == null)
			{
				return;
			}
			float deltaTime = Time.deltaTime;
			this._playTime += (double)deltaTime;
			this.Refresh();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000048DC File Offset: 0x00002ADC
		private void Refresh()
		{
			if (this._cameraMotion == null)
			{
				return;
			}
			CameraPose cameraPose = this._cameraMotion.GetCameraPoseByFrame((int)(this._playTime*30f));
			if (cameraPose == null)
			{
				return;
			}
			base.transform.localPosition = cameraPose.Position;
			base.transform.localRotation = Quaternion.Euler(-57.2957764f * cameraPose.Rotation);
			this.m_CameraTf.transform.localPosition = new Vector3(0f, 0f, cameraPose.FocalLength);
			this.m_Control.transform.SetPositionAndRotation(this.m_CameraTf.position, this.m_CameraTf.rotation);
			this.m_CameraControl.cameraFOV = cameraPose.Fov;
		}

		// Token: 0x0400000B RID: 11
		public CameraMotion _cameraMotion;

		// Token: 0x0400000C RID: 12
		public bool Playing;

		// Token: 0x0400000D RID: 13
		private double _playTime;

		// Token: 0x0400000E RID: 14
		private Camera _camera;

		// Token: 0x0400000F RID: 15
		public Camera[] cameras;

		// Token: 0x04000010 RID: 16
		public Transform m_CameraTf;

		// Token: 0x04000011 RID: 17
		public CameraControl m_CameraControl;

		// Token: 0x04000012 RID: 18
		private Quaternion quat = new Quaternion(0f, 1f, 0f, 0f);

		// Token: 0x04000013 RID: 19
		public FreeControllerV3 m_Control;
	}
}
