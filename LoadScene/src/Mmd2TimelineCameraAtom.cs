using LibMMD.Motion;
using LibMMD.Unity3D;
using MVR.FileManagementSecure;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace mmd2timeline
{
    // Token: 0x0200002B RID: 43
    internal class Mmd2TimelineCameraAtom
    {
        public Mmd2TimelineCameraAtom(Atom cameraAtom)
        {
            m_CameraAtom = cameraAtom;
            Init();
        }
        public void Init()
        {
            try
            {
                this.m_CameraControl = (CameraControl)this.m_CameraAtom.GetStorableByID("CameraControl");
                this.m_CameraControl.SetBoolParamValue("cameraOn", true);
                this.m_CameraControl.SetBoolParamValue("showHUDView", true);

                this.playProgress = new JSONStorableFloat("Preview", 0f, new JSONStorableFloat.SetFloatCallback(this.SetProgress), 0f, 1f, true, true);

                this.sampleRateChooser = new JSONStorableStringChooser("Sample Mode", new List<string>
                {
                    "EveryFrame",
                    "KeyFrame"
                }, "KeyFrame", "Sample Mode");
                this.startTime = new JSONStorableFloat("Start Time", 0f, new JSONStorableFloat.SetFloatCallback(this.SetStartTime), 0f, 10f, true, true);

                this.endTime = new JSONStorableFloat("End Time", 0f, new JSONStorableFloat.SetFloatCallback(this.SetEndTime), 0f, 10f, true, true);

            }
            catch (Exception ex)
            {
                string str = "[mmd2timeline]:Exception caught: ";
                Exception ex2 = ex;
                SuperController.LogError(str + ((ex2 != null) ? ex2.ToString() : null));
            }
        }
        // Token: 0x060001FF RID: 511 RVA: 0x0000AE4B File Offset: 0x0000904B
        private void Start()
        {
        }

        // Token: 0x06000200 RID: 512 RVA: 0x0000AE50 File Offset: 0x00009050


        // Token: 0x06000202 RID: 514 RVA: 0x0000B11C File Offset: 0x0000931C
        private void SetProgress(float val)
        {
            this.m_MmdCamera.SetPlayPos((double)val);
        }

        // Token: 0x06000203 RID: 515 RVA: 0x0000B12C File Offset: 0x0000932C
        private void ImportCameraVmd()
        {
            try
            {
                SuperController.singleton.GetMediaPathDialog(delegate (string path)
                {
                    if (string.IsNullOrEmpty(path))
                    {
                        return;
                    }
                    this.ImportVmd(path);
                }, "vmd", "Saves", false, true, false, null, false, null, true, false);
            }
            catch (Exception arg)
            {
                SuperController.LogError(string.Format("[mmd2timeline]: Failed to open file dialog: {0}", arg));
            }
        }

        // Token: 0x06000204 RID: 516 RVA: 0x0000B188 File Offset: 0x00009388
        private void SetStartTime(float val)
        {
            this.m_MmdCamera.SetPlayPos((double)val);
            float max = this.endTime.max;
            this.endTime.min = val;
            this.endTime.max = max;
        }

        // Token: 0x06000205 RID: 517 RVA: 0x0000B1C6 File Offset: 0x000093C6
        private void SetEndTime(float val)
        {
            this.endTime.val = val;
        }

        // Token: 0x06000206 RID: 518 RVA: 0x0000B1D4 File Offset: 0x000093D4
        public void ImportVmd(string path,string audioSource="")
        {
            GameObject gameObject = new GameObject("mmd2timeline camera root");
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            gameObject.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            this.m_MmdCamera = MmdCameraObject.CreateGameObject("MMDCameraObject");
            this.m_MmdCamera.transform.SetParent(gameObject.transform);
            this.m_MmdCamera.transform.localPosition = Vector3.zero;
            this.m_MmdCamera.transform.localScale = Vector3.one;
            this.m_MmdCamera.transform.localRotation = Quaternion.identity;
            this.m_MmdCamera.m_CameraControl = this.m_CameraControl;
            this.m_MmdCamera.m_Control = this.m_CameraAtom.mainController;
            this.m_MmdCamera.LoadCameraMotion(path);
            int key = this.m_MmdCamera._cameraMotion.KeyFrames[this.m_MmdCamera._cameraMotion.KeyFrames.Count - 1].Key;
            Debug.Log("lastFrame " + key.ToString());
            this.endTime.max = (float)((double)key / 30.0);
            this.endTime.val = this.endTime.max;
            this.startTime.max = this.endTime.max;
            this.playProgress.max = this.endTime.max;
            Sample(audioSource);
        }

        // Token: 0x06000207 RID: 519 RVA: 0x0000B378 File Offset: 0x00009578
        public void Sample(string audioSource)
        {
            this.json = new TimelineJson();
            this.json.AtomType = "WindowCamera";
            this.json.Clips = new List<TimelineClipJson>();
            TimelineClipJson timelineClipJson = new TimelineClipJson();
            this.json.Clips.Add(timelineClipJson);
            timelineClipJson.AnimationName = "camera motion";
            timelineClipJson.AnimationLength = (this.endTime.val - this.startTime.val).ToString();
            if (!string.IsNullOrEmpty(audioSource))
                timelineClipJson.AudioSourceControl = audioSource;
            timelineClipJson.Controllers = new List<TimelineControlJson>();
            TimelineControlJson timelineControlJson = new TimelineControlJson();
            timelineControlJson.Controller = "control";
            timelineClipJson.Controllers.Add(timelineControlJson);
            timelineClipJson.FloatParams = new List<FloatParamsJson>();
            FloatParamsJson floatParamsJsonFOV = new FloatParamsJson();
            floatParamsJsonFOV.Storable = "CameraControl";
            floatParamsJsonFOV.Name = "FOV";
            floatParamsJsonFOV.Min = "10";
            floatParamsJsonFOV.Max = "100";
            timelineClipJson.FloatParams.Add(floatParamsJsonFOV);
            List<KeyValuePair<int, CameraKeyframe>> keyFrames = this.m_MmdCamera._cameraMotion.KeyFrames;
            float startTimeval = this.startTime.val;
            float endTimeval = this.endTime.val;
            int startFrame = 0;
            int maxFrame = keyFrames[keyFrames.Count - 1].Key;
            int endFrame = maxFrame;
            for (int i = 0; i < keyFrames.Count; i++)
            {
                int key2 = keyFrames[i].Key;
                if ((float)key2 / 30f >= startTimeval)
                {
                    startFrame = key2;
                    break;
                }
            }
            for (int j = keyFrames.Count - 1; j >= 0; j--)
            {
                int key3 = keyFrames[j].Key;
                if ((float)key3 / 30f <= endTimeval)
                {
                    endFrame = key3;
                    break;
                }
            }
            Debug.Log("maxFrame " + maxFrame.ToString());
            Debug.Log("startFrame " + startFrame.ToString());
            Debug.Log("endFrame " + endFrame.ToString());
            HashSet<int> hashsetFrames = new HashSet<int>();
            foreach (KeyValuePair<int, CameraKeyframe> keyValuePair in this.m_MmdCamera._cameraMotion.KeyFrames)
            {
                if (keyValuePair.Key >= startFrame && keyValuePair.Key <= endFrame)
                {
                    hashsetFrames.Add(keyValuePair.Key);
                }
            }
            for (int curKeyFrame=startFrame;curKeyFrame<endFrame;curKeyFrame++)
            {
                CameraPose cameraPose = this.m_MmdCamera._cameraMotion.GetCameraPoseByFrame(curKeyFrame);
                float num3 = (float)curKeyFrame / 30f;
                float time = num3 - startTimeval;
                if (cameraPose != null)
                {
                    TimelineFrameJson timelineFrameJsonFOV = new TimelineFrameJson();
                    timelineFrameJsonFOV.t = time.ToString();
                    timelineFrameJsonFOV.v = cameraPose.Fov.ToString();
                    timelineFrameJsonFOV.c = "3";
                    timelineFrameJsonFOV.i = timelineFrameJsonFOV.v;
                    timelineFrameJsonFOV.o = timelineFrameJsonFOV.v;
                    floatParamsJsonFOV.Value.Add(timelineFrameJsonFOV);
                    this.m_MmdCamera.transform.localPosition = cameraPose.Position;
                    this.m_MmdCamera.transform.localRotation = Quaternion.Euler(-57.2957764f * cameraPose.Rotation);
                    this.m_MmdCamera.m_CameraTf.transform.localPosition = new Vector3(0f, 0f, cameraPose.FocalLength);
                    this.m_MmdCamera.m_Control.transform.SetPositionAndRotation(this.m_MmdCamera.m_CameraTf.position, this.m_MmdCamera.m_CameraTf.rotation);
                    if (curKeyFrame < maxFrame)
                    {
                        int num4 = maxFrame;
                        string curveType = "3";
                        if (hashsetFrames.Contains(curKeyFrame) && hashsetFrames.Contains(curKeyFrame + 1))
                            curveType = "8";
                        if (hashsetFrames.Contains(curKeyFrame) && !hashsetFrames.Contains(curKeyFrame + 1) && hashsetFrames.Contains(curKeyFrame - 1))
                            curveType = "10";
                        timelineFrameJsonFOV.c = curveType;
                        this.RecordController(time, this.m_MmdCamera.m_Control, timelineControlJson, curveType);
                       
                        /*
                        if (text != "3")
                        {
                            for (int l = k; l < num4; l += 5)
                            {
                                num3 = (float)l / 30f;
                                cameraPose = this.m_MmdCamera._cameraMotion.GetCameraPoseByFrame(k);
                                time = num3 - startTimeval;
                                this.m_MmdCamera.transform.localPosition = cameraPose.Position;
                                this.m_MmdCamera.transform.localRotation = Quaternion.Euler(-57.2957764f * cameraPose.Rotation);
                                this.m_MmdCamera.m_CameraTf.transform.localPosition = new Vector3(0f, 0f, cameraPose.FocalLength);
                                this.m_MmdCamera.m_Control.transform.SetPositionAndRotation(this.m_MmdCamera.m_CameraTf.position, this.m_MmdCamera.m_CameraTf.rotation);
                                this.RecordController(time, this.m_MmdCamera.m_Control, timelineControlJson, "3");
                            }
                        }
                        */
                    }
                    else
                    {
                        string text = "2";
                        this.RecordController(time, this.m_MmdCamera.m_Control, timelineControlJson, text);
                    }

                }
            }
            string strCamenaMmd2Timeline = "{\"setUnlistedParamsToDefault\":\"true\",\"storables\":[{\"id\":\"PhysicsMaterialControl\",\"dynamicFriction\":\"0.6\",\"staticFriction\":\"0.6\",\"bounciness\":\"0\",\"frictionCombine\":\"Average\",\"bounceCombine\":\"Average\"},{\"id\":\"CollisionTrigger\",\"triggerEnabled\":\"false\",\"invertAtomFilter\":\"false\",\"useRelativeVelocityFilter\":\"false\",\"invertRelativeVelocityFilter\":\"false\",\"relativeVelocityFilter\":\"1\",\"trigger\":{\"startActions\":[],\"transitionActions\":[],\"endActions\":[]}},{\"id\":\"scale\",\"scale\":\"1\"},{\"id\":\"CameraControl\",\"cameraOn\":\"false\",\"useAudioListener\":\"false\",\"useAsMainCamera\":\"false\",\"showHUDView\":\"false\",\"FOV\":\"40\",\"maskSelection\":\"mask1\"},{\"id\":\"PluginManager\",\"plugins\":{\"plugin#0\":\"AcidBubbles.Timeline.latest:/Custom/Scripts/AcidBubbles/Timeline/VamTimeline.AtomAnimation.cslist\",\"plugin#1\":\"AcidBubbles.Embody.latest:/Custom/Scripts/AcidBubbles/Embody/Embody.cslist\"}},{\"id\":\"plugin#0_VamTimeline.AtomPlugin\",\"enabled\":\"true\",\"pluginLabel\":\"\",\"Animation\":{\"Speed\":\"1\",\"Weight\":\"1\",\"Master\":\"0\",\"SyncWithPeers\":\"1\",\"SyncSubsceneOnly\":\"0\",\"TimeMode\":\"2\",\"LiveParenting\":\"1\",\"ForceBlendTime\":\"0\",\"Clips\":$$$clips$$$},\"Options\":{\"AutoKeyframeAllControllers\":\"0\",\"Snap\":\"0.1\",\"Locked\":\"0\",\"ShowPaths\":\"1\"}},{\"id\":\"plugin#1_Embody\",\"enabled\":\"true\",\"ActivateOnLoad\":\"false\",\"pluginLabel\":\"\",\"ReturnToSpawnPoint\":\"\",\"Version\":\"3\",\"Diagnostics\":{},\"Automation\":{\"ToggleKey\":\"None\"},\"WorldScale\":{},\"Passenger\":{\"PositionOffset\":{\"x\":\"0\",\"y\":\"0\",\"z\":\"0\"},\"RotationOffset\":{\"x\":\"0\",\"y\":\"0\",\"z\":\"0\"}},\"Triggers\":{\"startActions\":[],\"transitionActions\":[],\"endActions\":[]}}]}";
            JSONClass jc = this.json.ToJsonClass();
            JSONArray ja = jc["Clips"] as JSONArray;
            StringBuilder stringBuilder = new StringBuilder(100000);
            ja.ToString(string.Empty, stringBuilder);
            strCamenaMmd2Timeline = strCamenaMmd2Timeline.Replace("$$$clips$$$", stringBuilder.ToString());
            //return (JSONClass)JSON.Parse(strCamenaMmd2Timeline);
            string pathCameraPreset = "Custom/Atom/WindowCamera";
            if (!FileManagerSecure.DirectoryExists(pathCameraPreset, false))
            {
                FileManagerSecure.CreateDirectory(pathCameraPreset);
            }
            string CameraPresetFullName = pathCameraPreset + "/Preset_mmdloader.vap";
            if (FileManagerSecure.FileExists(CameraPresetFullName, false))
            {
                FileManagerSecure.DeleteFile(CameraPresetFullName);
            }
            FileManagerSecure.WriteAllText(CameraPresetFullName, strCamenaMmd2Timeline);



            /*
            this.json = new TimelineJson();
            this.json.AtomType = "WindowCamera";
            this.json.Clips = new List<TimelineClipJson>();
            TimelineClipJson timelineClipJson = new TimelineClipJson();
            this.json.Clips.Add(timelineClipJson);
            timelineClipJson.AnimationName = "camera motion";
            timelineClipJson.AnimationLength = (this.endTime.val - this.startTime.val).ToString();
            if (!string.IsNullOrEmpty(audioSource))
                timelineClipJson.AudioSourceControl = audioSource;
            timelineClipJson.Controllers = new List<TimelineControlJson>();
            TimelineControlJson timelineControlJson = new TimelineControlJson();
            timelineControlJson.Controller = "control";
            timelineClipJson.Controllers.Add(timelineControlJson);
            timelineClipJson.FloatParams = new List<FloatParamsJson>();
            FloatParamsJson floatParamsJson = new FloatParamsJson();
            floatParamsJson.Storable = "CameraControl";
            floatParamsJson.Name = "FOV";
            floatParamsJson.Min = "10";
            floatParamsJson.Max = "100";
            timelineClipJson.FloatParams.Add(floatParamsJson);
            List<KeyValuePair<int, CameraKeyframe>> keyFrames = this.m_MmdCamera._cameraMotion.KeyFrames;
            float val = this.startTime.val;
            float val2 = this.endTime.val;
            int num = 0;
            int key = keyFrames[keyFrames.Count - 1].Key;
            int num2 = key;
            for (int i = 0; i < keyFrames.Count; i++)
            {
                int key2 = keyFrames[i].Key;
                if ((float)key2 / 30f >= val)
                {
                    num = key2;
                    break;
                }
            }
            for (int j = keyFrames.Count - 1; j >= 0; j--)
            {
                int key3 = keyFrames[j].Key;
                if ((float)key3 / 30f <= val2)
                {
                    num2 = key3;
                    break;
                }
            }
            Debug.Log("maxFrame " + key.ToString());
            Debug.Log("startFrame " + num.ToString());
            Debug.Log("endFrame " + num2.ToString());
            HashSet<int> hashSet = new HashSet<int>();
            foreach (KeyValuePair<int, CameraKeyframe> keyValuePair in this.m_MmdCamera._cameraMotion.KeyFrames)
            {
                if (keyValuePair.Key >= num && keyValuePair.Key <= num2)
                {
                    hashSet.Add(keyValuePair.Key);
                }
            }
            for (int k = num; k <= num2; k++)
            {
                if (!(this.sampleRateChooser.val != "EveryFrame") || hashSet.Contains(k))
                {
                    float num3 = (float)k / 30f;
                    CameraPose cameraPose = this.m_MmdCamera._cameraMotion.GetCameraPose((double)num3);
                    float time = num3 - val;
                    if (cameraPose != null)
                    {
                        TimelineFrameJson timelineFrameJson = new TimelineFrameJson();
                        timelineFrameJson.t = time.ToString();
                        timelineFrameJson.v = cameraPose.Fov.ToString();
                        timelineFrameJson.c = "3";
                        timelineFrameJson.i = timelineFrameJson.v;
                        timelineFrameJson.o = timelineFrameJson.v;
                        floatParamsJson.Value.Add(timelineFrameJson);
                        this.m_MmdCamera.transform.localPosition = cameraPose.Position;
                        this.m_MmdCamera.transform.localRotation = Quaternion.Euler(-57.2957764f * cameraPose.Rotation);
                        this.m_MmdCamera.m_CameraTf.transform.localPosition = new Vector3(0f, 0f, cameraPose.FocalLength);
                        this.m_MmdCamera.m_Control.transform.SetPositionAndRotation(this.m_MmdCamera.m_CameraTf.position, this.m_MmdCamera.m_CameraTf.rotation);
                        if (k < key)
                        {
                            if (this.sampleRateChooser.val != "EveryFrame")
                            {
                                int num4 = key;
                                string text = this.GuessType(keyFrames, k, ref num4);
                                timelineFrameJson.c = text;
                                this.RecordController(time, this.m_MmdCamera.m_Control, timelineControlJson, text);
                                if (text != "3")
                                {
                                    for (int l = k; l < num4; l += 5)
                                    {
                                        num3 = (float)l / 30f;
                                        cameraPose = this.m_MmdCamera._cameraMotion.GetCameraPose((double)num3);
                                        time = num3 - val;
                                        this.m_MmdCamera.transform.localPosition = cameraPose.Position;
                                        this.m_MmdCamera.transform.localRotation = Quaternion.Euler(-57.2957764f * cameraPose.Rotation);
                                        this.m_MmdCamera.m_CameraTf.transform.localPosition = new Vector3(0f, 0f, cameraPose.FocalLength);
                                        this.m_MmdCamera.m_Control.transform.SetPositionAndRotation(this.m_MmdCamera.m_CameraTf.position, this.m_MmdCamera.m_CameraTf.rotation);
                                        this.RecordController(time, this.m_MmdCamera.m_Control, timelineControlJson, "3");
                                    }
                                }
                            }
                            else
                            {
                                string text = "2";
                                this.RecordController(time, this.m_MmdCamera.m_Control, timelineControlJson, text);
                            }
                        }
                        else
                        {
                            string text = "2";
                            this.RecordController(time, this.m_MmdCamera.m_Control, timelineControlJson, text);
                        }
                    }
                }
            }
            string strCamenaMmd2Timeline = "{\"setUnlistedParamsToDefault\":\"true\",\"storables\":[{\"id\":\"PhysicsMaterialControl\",\"dynamicFriction\":\"0.6\",\"staticFriction\":\"0.6\",\"bounciness\":\"0\",\"frictionCombine\":\"Average\",\"bounceCombine\":\"Average\"},{\"id\":\"CollisionTrigger\",\"triggerEnabled\":\"false\",\"invertAtomFilter\":\"false\",\"useRelativeVelocityFilter\":\"false\",\"invertRelativeVelocityFilter\":\"false\",\"relativeVelocityFilter\":\"1\",\"trigger\":{\"startActions\":[],\"transitionActions\":[],\"endActions\":[]}},{\"id\":\"scale\",\"scale\":\"1\"},{\"id\":\"CameraControl\",\"cameraOn\":\"false\",\"useAudioListener\":\"false\",\"useAsMainCamera\":\"false\",\"showHUDView\":\"false\",\"FOV\":\"40\",\"maskSelection\":\"mask1\"},{\"id\":\"PluginManager\",\"plugins\":{\"plugin#0\":\"AcidBubbles.Timeline.latest:/Custom/Scripts/AcidBubbles/Timeline/VamTimeline.AtomAnimation.cslist\",\"plugin#1\":\"AcidBubbles.Embody.latest:/Custom/Scripts/AcidBubbles/Embody/Embody.cslist\"}},{\"id\":\"plugin#0_VamTimeline.AtomPlugin\",\"enabled\":\"true\",\"pluginLabel\":\"\",\"Animation\":{\"Speed\":\"1\",\"Weight\":\"1\",\"Master\":\"0\",\"SyncWithPeers\":\"1\",\"SyncSubsceneOnly\":\"0\",\"TimeMode\":\"2\",\"LiveParenting\":\"1\",\"ForceBlendTime\":\"0\",\"Clips\":$$$clips$$$},\"Options\":{\"AutoKeyframeAllControllers\":\"0\",\"Snap\":\"0.1\",\"Locked\":\"0\",\"ShowPaths\":\"1\"}},{\"id\":\"plugin#1_Embody\",\"enabled\":\"true\",\"ActivateOnLoad\":\"false\",\"pluginLabel\":\"\",\"ReturnToSpawnPoint\":\"\",\"Version\":\"3\",\"Diagnostics\":{},\"Automation\":{\"ToggleKey\":\"None\"},\"WorldScale\":{},\"Passenger\":{\"PositionOffset\":{\"x\":\"0\",\"y\":\"0\",\"z\":\"0\"},\"RotationOffset\":{\"x\":\"0\",\"y\":\"0\",\"z\":\"0\"}},\"Triggers\":{\"startActions\":[],\"transitionActions\":[],\"endActions\":[]}}]}";
            JSONClass jc = this.json.ToJsonClass();
            JSONArray ja = jc["Clips"] as JSONArray;
            StringBuilder stringBuilder = new StringBuilder(100000);
            ja.ToString(string.Empty, stringBuilder);
            strCamenaMmd2Timeline = strCamenaMmd2Timeline.Replace("$$$clips$$$", stringBuilder.ToString());
            //return (JSONClass)JSON.Parse(strCamenaMmd2Timeline);
            string pathCameraPreset = "Custom/Atom/WindowCamera";
            if (!FileManagerSecure.DirectoryExists(pathCameraPreset, false))
            {
                FileManagerSecure.CreateDirectory(pathCameraPreset);
            }
            string CameraPresetFullName = pathCameraPreset + "/Preset_mmdloader.vap";
            if (FileManagerSecure.FileExists(CameraPresetFullName, false))
            {
                FileManagerSecure.DeleteFile(CameraPresetFullName);
            }
            FileManagerSecure.WriteAllText(CameraPresetFullName, strCamenaMmd2Timeline);
            
			FileBrowser fileBrowserUI = SuperController.singleton.fileBrowserUI;
			fileBrowserUI.SetTitle("Export animation");
			fileBrowserUI.fileRemovePrefix = null;
			fileBrowserUI.hideExtension = false;
			fileBrowserUI.keepOpen = false;
			fileBrowserUI.fileFormat = "json";
			fileBrowserUI.defaultPath = "Saves\\PluginData\\animations";
			fileBrowserUI.showDirs = true;
			fileBrowserUI.shortCuts = null;
			fileBrowserUI.browseVarFilesAsDirectories = false;
			fileBrowserUI.SetTextEntry(true);
			fileBrowserUI.Show(new FileBrowserCallback(this.ExportFileSelected), true);
			fileBrowserUI.ActivateFileNameField();
			*/
        }

        // Token: 0x06000208 RID: 520 RVA: 0x0000B960 File Offset: 0x00009B60
        private string GuessType(List<KeyValuePair<int, CameraKeyframe>> list, int currentFrame, ref int nextFrame)
        {
            int maxFrame = list[list.Count - 1].Key;
            string result = "3";
            nextFrame = maxFrame;
            int prevFrame = 0;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].Key < currentFrame)
                {
                    prevFrame = list[i].Key;
                    break;
                }
            }
            for (int j = 0; j < list.Count; j++)
            {
                if (list[j].Key > currentFrame)
                {
                    nextFrame = list[j].Key;
                    break;
                }
            }
            if (nextFrame == currentFrame + 1)
            {
                result = "8";
            }
            else if (prevFrame == currentFrame - 1 && nextFrame > currentFrame + 1)
            {
                result = "10";
            }
            return result;
        }

        // Token: 0x06000209 RID: 521 RVA: 0x0000BA2C File Offset: 0x00009C2C
        private void ExportFileSelected(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            if (!path.ToLower().EndsWith(".json"))
            {
                path += ".json";
            }
            string text = this.json.ToJsonClass().ToString();
            string path2 = "Saves/PluginData/animations/mmd2timeline/";
            if (!FileManagerSecure.DirectoryExists(path2, false))
            {
                FileManagerSecure.CreateDirectory(path2);
            }
            FileManagerSecure.WriteAllText(path, text);
        }

        // Token: 0x0600020A RID: 522 RVA: 0x0000BA8C File Offset: 0x00009C8C
        private void RecordController(float time, FreeControllerV3 freeController, TimelineControlJson json, string type)
        {
            Transform transform = freeController.transform;
            TimelineFrameJson timelineFrameJson = new TimelineFrameJson();
            timelineFrameJson.t = time.ToString();
            TimelineFrameJson timelineFrameJson2 = timelineFrameJson;
            Vector3 localPosition = transform.localPosition;
            timelineFrameJson2.v = localPosition.x.ToString();
            timelineFrameJson.c = type;
            timelineFrameJson.i = timelineFrameJson.v;
            timelineFrameJson.o = timelineFrameJson.v;
            json.X.Add(timelineFrameJson);
            TimelineFrameJson timelineFrameJson3 = new TimelineFrameJson();
            timelineFrameJson3.t = time.ToString();
            TimelineFrameJson timelineFrameJson4 = timelineFrameJson3;
            localPosition = transform.localPosition;
            timelineFrameJson4.v = localPosition.y.ToString();
            timelineFrameJson3.c = type;
            timelineFrameJson3.i = timelineFrameJson3.v;
            timelineFrameJson3.o = timelineFrameJson3.v;
            json.Y.Add(timelineFrameJson3);
            TimelineFrameJson timelineFrameJson5 = new TimelineFrameJson();
            timelineFrameJson5.t = time.ToString();
            TimelineFrameJson timelineFrameJson6 = timelineFrameJson5;
            localPosition = transform.localPosition;
            timelineFrameJson6.v = localPosition.z.ToString();
            timelineFrameJson5.c = type;
            timelineFrameJson5.i = timelineFrameJson5.v;
            timelineFrameJson5.o = timelineFrameJson5.v;
            json.Z.Add(timelineFrameJson5);
            TimelineFrameJson timelineFrameJson7 = new TimelineFrameJson();
            timelineFrameJson7.t = time.ToString();
            TimelineFrameJson timelineFrameJson8 = timelineFrameJson7;
            Quaternion localRotation = transform.localRotation;
            timelineFrameJson8.v = localRotation.x.ToString();
            timelineFrameJson7.c = type;
            timelineFrameJson7.i = timelineFrameJson7.v;
            timelineFrameJson7.o = timelineFrameJson7.v;
            json.RotX.Add(timelineFrameJson7);
            TimelineFrameJson timelineFrameJson9 = new TimelineFrameJson();
            timelineFrameJson9.t = time.ToString();
            TimelineFrameJson timelineFrameJson10 = timelineFrameJson9;
            localRotation = transform.localRotation;
            timelineFrameJson10.v = localRotation.y.ToString();
            timelineFrameJson9.c = type;
            timelineFrameJson9.i = timelineFrameJson9.v;
            timelineFrameJson9.o = timelineFrameJson9.v;
            json.RotY.Add(timelineFrameJson9);
            TimelineFrameJson timelineFrameJson11 = new TimelineFrameJson();
            timelineFrameJson11.t = time.ToString();
            TimelineFrameJson timelineFrameJson12 = timelineFrameJson11;
            localRotation = transform.localRotation;
            timelineFrameJson12.v = localRotation.z.ToString();
            timelineFrameJson11.c = type;
            timelineFrameJson11.i = timelineFrameJson11.v;
            timelineFrameJson11.o = timelineFrameJson11.v;
            json.RotZ.Add(timelineFrameJson11);
            TimelineFrameJson timelineFrameJson13 = new TimelineFrameJson();
            timelineFrameJson13.t = time.ToString();
            TimelineFrameJson timelineFrameJson14 = timelineFrameJson13;
            localRotation = transform.localRotation;
            timelineFrameJson14.v = localRotation.w.ToString();
            timelineFrameJson13.c = type;
            timelineFrameJson13.i = timelineFrameJson13.v;
            timelineFrameJson13.o = timelineFrameJson13.v;
            json.RotW.Add(timelineFrameJson13);
        }

        // Token: 0x040000DF RID: 223
        private CameraControl m_CameraControl;

        private Atom m_CameraAtom;

        // Token: 0x040000E0 RID: 224
        public JSONStorableStringChooser sampleRateChooser;

        // Token: 0x040000E1 RID: 225
        public JSONStorableFloat playProgress;

        // Token: 0x040000E2 RID: 226
        public JSONStorableFloat startTime;

        // Token: 0x040000E3 RID: 227
        public JSONStorableFloat endTime;

        // Token: 0x040000E4 RID: 228
        private MmdCameraObject m_MmdCamera;

        // Token: 0x040000E5 RID: 229
        private Quaternion quat = new Quaternion(0f, 1f, 0f, 0f);

        // Token: 0x040000E6 RID: 230
        private TimelineJson json;

        // Token: 0x040000E7 RID: 231
        private const string _saveExt = "json";

        // Token: 0x040000E8 RID: 232
        private const string _saveFolder = "Saves\\PluginData\\animations";


    }
}
