using LibMMD.Motion;
using LibMMD.Unity3D;
using LibMMD.Util;
using System.Collections.Generic;
using UnityEngine;

namespace mmd2timeline
{
    // Token: 0x02000029 RID: 41
    internal class AssetBoneProcess
    {
        // Token: 0x060001F2 RID: 498 RVA: 0x0000A54A File Offset: 0x0000874A
        public void Init(Mmd2TimelinePersonAtom script)
        {
            this.m_MVRScript = script;
            this.m_PersonAtom = script.PersonAtom;
            this.InitHandPoint();
        }

        // Token: 0x060001F3 RID: 499 RVA: 0x0000A568 File Offset: 0x00008768
        public HashSet<int> GetMotionKeyFrameSet()
        {
            HashSet<int> hashSet = new HashSet<int>();
            if (this._motion == null)
            {
                return hashSet;
            }
            foreach (KeyValuePair<string, List<KeyValuePair<int, BoneKeyframe>>> keyValuePair in this._motion.BoneMotions)
            {
                foreach (KeyValuePair<int, BoneKeyframe> keyValuePair2 in keyValuePair.Value)
                {
                    hashSet.Add(keyValuePair2.Key);
                }
            }
            return hashSet;
        }

        // Token: 0x060001F4 RID: 500 RVA: 0x0000A618 File Offset: 0x00008818
        public List<int> GetMotionKeyFrames(float beginTime, float endTime)
        {
            List<int> list = new List<int>();
            if (this._motion == null)
            {
                return list;
            }
            HashSet<int> hashSet = new HashSet<int>();
            foreach (KeyValuePair<string, List<KeyValuePair<int, BoneKeyframe>>> keyValuePair in this._motion.BoneMotions)
            {
                foreach (KeyValuePair<int, BoneKeyframe> keyValuePair2 in keyValuePair.Value)
                {
                    hashSet.Add(keyValuePair2.Key);
                }
            }
            int num = (int)(beginTime * 30f);
            int num2 = (int)(endTime * 30f);
            foreach (int num3 in hashSet)
            {
                if (num3 >= num && num3 <= num2)
                {
                    list.Add(num3);
                }
            }
            list.Sort();
            return list;
        }

        // Token: 0x060001F5 RID: 501 RVA: 0x0000A730 File Offset: 0x00008930
        public void InitHandPoint()
        {
            string[] array = new string[]
            {
                "Carpal1",
                "Ring1",
                "Mid1"
            };
            Transform root = this.m_PersonAtom.transform.Find("rescale2/PhysicsModel");
            string[] array2 = new string[]
            {
                "l",
                "r"
            };
            this.m_HandPoints = new Transform[2];
            this.m_HandParentPoints = new Transform[2];
            for (int i = 0; i < 2; i++)
            {
                Transform transform = DazBoneMapping.SearchObjName(root, array2[i] + "Carpal1");
                Transform transform2 = transform.Find("mmd2timeline hand");
                if (transform2 == null)
                {
                    GameObject gameObject = new GameObject("mmd2timeline hand");
                    transform2 = gameObject.transform;
                    gameObject.transform.parent = transform;
                    gameObject.transform.localScale = Vector3.one;
                    gameObject.transform.localPosition = Vector3.zero;
                    gameObject.transform.localRotation = Quaternion.identity;
                    if (Settings.s_Debug)
                    {
                        GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        gameObject2.transform.parent = transform2;
                        gameObject2.transform.localPosition = Vector3.zero;
                        gameObject2.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                        gameObject2.GetComponent<MeshRenderer>().material.color = Color.red;
                        UnityEngine.Object.DestroyImmediate(gameObject2.GetComponent<Collider>());
                    }
                }
                transform2.position = this.GetHandPosition(array2[i] == "r");
                this.m_HandPoints[i] = transform2;
                this.m_HandParentPoints[i] = transform;
            }
        }

        // Token: 0x060001F6 RID: 502 RVA: 0x0000A8CC File Offset: 0x00008ACC
        public void BindToHand(int frame, MmdGameObject personMmd, string handType)
        {
            LogUtil.Log("BindToHand " + handType);
            this.m_BindedHand = null;
            if (string.Compare(handType, "RightHand", true) == 0)
            {
                this.m_BindedHand = "RightHand";
                this.Attach(frame, personMmd, handType);
                this.Process(frame);
                return;
            }
            if (string.Compare(handType, "LeftHand", true) == 0)
            {
                this.m_BindedHand = "LeftHand";
                this.Attach(frame, personMmd, handType);
                this.Process(frame);
            }
        }

        // Token: 0x060001F7 RID: 503 RVA: 0x0000A944 File Offset: 0x00008B44
        public void Attach(int frame, MmdGameObject targetMmd, string attachBoneName)
        {
            this.m_FakeBone = null;
            if (string.Compare(attachBoneName, "None", true) == 0)
            {
                this.m_BindedHand = null;
                return;
            }
            this.m_BindedHand = attachBoneName;
            if (string.Compare(attachBoneName, "RightHand", true) == 0)
            {
                attachBoneName = "右手首";
            }
            else if (string.Compare(attachBoneName, "LeftHand", true) == 0)
            {
                attachBoneName = "左手首";
            }
            Transform transform = null;
            foreach (GameObject gameObject in targetMmd._bones)
            {
                if (gameObject.name == attachBoneName)
                {
                    transform = gameObject.transform;
                    break;
                }
            }
            if (transform == null)
            {
                LogUtil.LogError("Attach fail");
                return;
            }
            BonePose bonePose = this._motion.GetBonePose(this.m_BoneName, frame);
            Vector3 translation = bonePose.Translation;
            Quaternion rotation = bonePose.Rotation;
            string str = "bonePose ";
            Vector3 vector = translation;
            string str2 = vector.ToString();
            string str3 = " ";
            Quaternion quaternion = rotation;
            LogUtil.Log(str + str2 + str3 + quaternion.ToString());
            this.m_FakeBone = transform.Find("mmd2timeline bone for calc");
            if (this.m_FakeBone == null)
            {
                GameObject gameObject2 = new GameObject("mmd2timeline bone for calc");
                this.m_FakeBone = gameObject2.transform;
                this.m_FakeBone.SetParent(transform);
                this.m_FakeBone.localPosition = Vector3.zero;
                this.m_FakeBone.localRotation = Quaternion.identity;
                this.m_FakeBone.localScale = Vector3.one;
            }
            this.m_FakeBone.localPosition = translation;
            this.m_FakeBone.localRotation = rotation;
            if (Settings.s_Debug)
            {
                GameObject gameObject3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                gameObject3.transform.parent = this.m_FakeBone;
                gameObject3.transform.localPosition = Vector3.zero;
                gameObject3.transform.localScale = new Vector3(0.2f, 0.5f, 0.2f);
                gameObject3.GetComponent<MeshRenderer>().material.color = Color.green;
                UnityEngine.Object.DestroyImmediate(gameObject3.GetComponent<Collider>());
            }
        }

        // Token: 0x060001F8 RID: 504 RVA: 0x0000AB3C File Offset: 0x00008D3C
        private Vector3 GetHandPosition(bool right)
        {
            string[] array = new string[]
            {
                "Carpal1",
                "Ring1",
                "Mid1"
            };
            Transform root = this.m_PersonAtom.transform.Find("rescale2/PhysicsModel");
            Transform[] array2 = new Transform[3];
            for (int i = 0; i < 3; i++)
            {
                array2[i] = DazBoneMapping.SearchObjName(root, (right ? "r" : "l") + array[i]);
            }
            Vector3 lhs = array2[0].position - array2[1].position;
            Vector3 rhs = array2[2].position - array2[1].position;
            Vector3 normalized = Vector3.Cross(lhs, rhs).normalized;
            float d = Vector3.Distance(array2[0].position, array2[1].position);
            return (array2[0].position + array2[1].position) / 2f + (float)(right ? -1 : 1) * normalized * d * 0.6f;
        }

        // Token: 0x060001F9 RID: 505 RVA: 0x0000AC50 File Offset: 0x00008E50
        public void Process(int frame)
        {
            if (string.IsNullOrEmpty(this.m_BoneName))
            {
                return;
            }
            BonePose bonePose = this._motion.GetBonePose(this.m_BoneName, frame);
            Vector3 translation = bonePose.Translation;
            Quaternion quaternion = bonePose.Rotation;
            this.m_FakeBone.localPosition = translation;
            this.m_FakeBone.localRotation = quaternion;
            quaternion = this.m_FakeBone.rotation * Utility.quat;
            Vector3 assetInitPosition = this.m_MVRScript.AssetInitPosition;
            this.m_CUAAtom.mainController.transform.rotation = quaternion;
            if (string.Compare(this.m_BindedHand, "RightHand", true) == 0)
            {
                Transform transform = this.m_HandPoints[1];
                transform.rotation = quaternion;
                Matrix4x4 rhs = Matrix4x4.TRS(-assetInitPosition, Quaternion.identity, Vector3.one);
                Matrix4x4 matrix = transform.localToWorldMatrix * rhs;
                this.m_CUAAtom.mainController.transform.position = matrix.ExtractPosition();
                return;
            }
            if (string.Compare(this.m_BindedHand, "LeftHand", true) == 0)
            {
                Transform transform2 = this.m_HandPoints[0];
                transform2.rotation = quaternion;
                Matrix4x4 rhs2 = Matrix4x4.TRS(-assetInitPosition, Quaternion.identity, Vector3.one);
                Matrix4x4 matrix2 = transform2.localToWorldMatrix * rhs2;
                this.m_CUAAtom.mainController.transform.position = matrix2.ExtractPosition();
                return;
            }
            LogUtil.Log("no attach bone");
        }

        // Token: 0x040000D6 RID: 214
        private Mmd2TimelinePersonAtom m_MVRScript;

        // Token: 0x040000D7 RID: 215
        public MmdMotion _motion;

        // Token: 0x040000D8 RID: 216
        public string m_BoneName;

        // Token: 0x040000D9 RID: 217
        public Transform m_FakeBone;

        // Token: 0x040000DA RID: 218
        public Atom m_CUAAtom;

        // Token: 0x040000DB RID: 219
        public Atom m_PersonAtom;

        // Token: 0x040000DC RID: 220
        public string m_BindedHand;

        // Token: 0x040000DD RID: 221
        private Transform[] m_HandParentPoints;

        // Token: 0x040000DE RID: 222
        private Transform[] m_HandPoints;
    }
}
