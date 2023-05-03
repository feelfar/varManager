using System;
using System.Collections.Generic;
using LibMMD.Model;
using UnityEngine;

namespace LibMMD.Motion
{
	// Token: 0x0200001D RID: 29
	public class MotionPlayer
	{
		// Token: 0x06000109 RID: 265 RVA: 0x000086CC File Offset: 0x000068CC
		public MotionPlayer(MmdMotion motion, Poser poser)
		{
			this._motion = motion;
			this._poser = poser;
			MmdModel model = poser.Model;
			for (int i = 0; i < model.Bones.Length; i++)
			{
				string name = model.Bones[i].Name;
				if (motion.IsBoneRegistered(name))
				{
					this._boneMap.Add(new KeyValuePair<string, int>(name, i));
				}
			}
			for (int j = 0; j < model.Morphs.Length; j++)
			{
				string name2 = model.Morphs[j].Name;
				if (motion.IsMorphRegistered(name2))
				{
					this._morphMap.Add(new KeyValuePair<string, int>(name2, j));
				}
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00008788 File Offset: 0x00006988
		public void SeekFrame(int frame)
		{
			foreach (KeyValuePair<string, int> keyValuePair in this._morphMap)
			{
				this._poser.SetMorphPose(keyValuePair.Value, this._motion.GetMorphPose(keyValuePair.Key, frame));
			}
			foreach (KeyValuePair<string, int> keyValuePair2 in this._boneMap)
			{
				
				this._poser.SetBonePose(keyValuePair2.Value, this._motion.GetBonePose(keyValuePair2.Key, frame));
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000885C File Offset: 0x00006A5C
		public void SeekTime(double time)
		{
			foreach (KeyValuePair<string, int> keyValuePair in this._morphMap)
			{
				this._poser.SetMorphPose(keyValuePair.Value, this._motion.GetMorphPose(keyValuePair.Key, time));
			}
			foreach (KeyValuePair<string, int> keyValuePair2 in this._boneMap)
			{
				this._poser.SetBonePose(keyValuePair2.Value, this._motion.GetBonePose(keyValuePair2.Key, time));
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00008930 File Offset: 0x00006B30
		public void CalculateMorphVertexOffset(MmdModel model, double time, Vector3[] output)
		{
			if (output.Length != model.Vertices.Length)
			{
				throw new ArgumentException("model vertex count not equals to output array length");
			}
			Array.Clear(output, 0, output.Length);
			foreach (KeyValuePair<string, int> keyValuePair in this._morphMap)
			{
				MorphPose morphPose = this._motion.GetMorphPose(keyValuePair.Key, time);
				MotionPlayer.UpdateVertexOffsetByMorph(model, keyValuePair.Value, morphPose.Weight, output);
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000089C8 File Offset: 0x00006BC8
		private static void UpdateVertexOffsetByMorph(MmdModel model, int index, float rate, Vector3[] output)
		{
			if (rate < 1E-07f)
			{
				return;
			}
			Morph morph = model.Morphs[index];
			Morph.MorphType type = morph.Type;
			if (type == Morph.MorphType.MorphTypeGroup)
			{
				foreach (Morph.GroupMorph groupMorph in morph.MorphDatas)
				{
					MotionPlayer.UpdateVertexOffsetByMorph(model, groupMorph.MorphIndex, groupMorph.MorphRate * rate, output);
				}
				return;
			}
			if (type != Morph.MorphType.MorphTypeVertex)
			{
				return;
			}
			foreach (Morph.VertexMorph vertexMorph in morph.MorphDatas)
			{
				output[vertexMorph.VertexIndex] = output[vertexMorph.VertexIndex] + vertexMorph.Offset * rate;
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00008A78 File Offset: 0x00006C78
		public double GetMotionTimeLength()
		{
			return (double)this._motion.Length * 30.0;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00008A90 File Offset: 0x00006C90
		public int GetMotionFrameLength()
		{
			return this._motion.Length;
		}

		// Token: 0x0400006B RID: 107
		private readonly List<KeyValuePair<string, int>> _boneMap = new List<KeyValuePair<string, int>>();

		// Token: 0x0400006C RID: 108
		private readonly List<KeyValuePair<string, int>> _morphMap = new List<KeyValuePair<string, int>>();

		// Token: 0x0400006D RID: 109
		private readonly MmdMotion _motion;

		// Token: 0x0400006E RID: 110
		private readonly Poser _poser;
	}
}
