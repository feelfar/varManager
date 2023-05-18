using MeshVR;
using mmd2timeline;
using MVR.FileManagementSecure;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

namespace MVRPlugin
{
    public class loadscene : MVRScript
    {
        // IMPORTANT - DO NOT make custom enums. The dynamic C# complier crashes Unity when it encounters these for
        // some reason

        // IMPORTANT - DO NOT OVERRIDE Awake() as it is used internally by MVRScript - instead use Init() function which
        // is called right after creation

        //private DateTime dtspan;
        private Atom coreControl;
        public override void Init()
        {
            try
            {
                // put init code in here
                SuperController.LogMessage("loadscene Loaded");
                coreControl = SuperController.singleton.GetAtomByUid("CoreControl");
                mmdPersons = new Dictionary<int, Mmd2TimelinePersonAtom>();
                
                // create custom JSON storable params here if you want them to be stored with scene JSON
                // types are JSONStorableFloat, JSONStorableBool, JSONStorableString, JSONStorableStringChooser
                // JSONStorableColor

            }
            catch (Exception e)
            {
                SuperController.LogError("Exception caught: " + e);
            }
        }

        // Start is called once before Update or FixedUpdate is called and after Init()
        void Start()
        {
            try
            {

            }
            catch (Exception e)
            {
                SuperController.LogError("Exception caught: " + e);
            }
        }
        Dictionary<int, Mmd2TimelinePersonAtom> mmdPersons;
        // Update is called with each rendered frame by Unity
        void Update()
        {
            StartCoroutine(LoopWaitLoadFile());
        }

        private IEnumerator LoopWaitLoadFile()
        {
            if (SuperController.singleton.isLoading)
                yield return new WaitForSeconds(2f);


            string loadscenetxt = "Custom\\PluginData\\feelfar\\loadscene.json";
            if (FileManagerSecure.FileExists(loadscenetxt))
            {
                SuperController.LogMessage("find loadscene.json");

                JSONClass jsonForLoad = (JSONClass)JSON.Parse(FileManagerSecure.ReadAllText(loadscenetxt));
                //SuperController.LogMessage(atomforload.ToString());
                Thread.Sleep(1000);
                FileManagerSecure.DeleteFile(loadscenetxt);
                bool rescan = false;
                if (jsonForLoad.HasKey("rescan"))
                {
                    rescan = jsonForLoad["rescan"].AsBool;
                }
                if (rescan)
                {
                    SuperController.LogMessage("Rescan Packages...");
                    SuperController.singleton.RescanPackages();
                    //Thread.Sleep(10000);
                }
                JSONArray resources = jsonForLoad["resources"].AsArray;
                List<JSONClass> atompresets = new List<JSONClass>();
                List<JSONClass> atoms = new List<JSONClass>();
                List<JSONClass> atomSubscene = new List<JSONClass>();
                foreach (JSONClass resource in resources)
                {
                    string scenetype = "";
                    if (resource.HasKey("type"))
                    {
                        scenetype = resource["type"];
                    }
                    if (scenetype == "empytscene")
                    {
                        SuperController.singleton.NewScene();
                        yield return new WaitForSeconds(3f);
                        continue;
                    }
                    if (scenetype == "highheel")
                    {
                        atompresets.Add(resource);
                        continue;
                    }
                    string scenefile = "";
                    if (resource.HasKey("saveName"))
                    {
                        scenefile = resource["saveName"];
                    }

                    bool merge = false;
                    if (resource.HasKey("merge"))
                    {
                        merge = resource["merge"].AsBool;
                    }
                    if (merge)
                        SuperController.LogMessage("merge load enabled...");


                    switch (scenetype)
                    {
                        case "scenes":
                            if (FileManagerSecure.FileExists(scenefile))
                            {
                                SuperController.LogMessage("load " + scenetype + ": " + scenefile);
                                if (merge)
                                    SuperController.singleton.LoadMerge(scenefile);
                                else
                                    SuperController.singleton.Load(scenefile);
                                yield return new WaitForSeconds(2f);
                            }
                            break;
                        case "atom":
                            if (FileManagerSecure.FileExists(scenefile))
                                atoms.Add((JSONClass)JSONNode.LoadFromFile(scenefile));
                            break;
                        case "audio":
                            LoadAudio(scenefile);
                            break;
                        case "cameravmd":
                            Atom windowCameraAtom = null;
                            foreach (var atom in SuperController.singleton.GetAtoms())
                            {
                                if (atom.type == "WindowCamera")
                                {
                                    windowCameraAtom = atom;
                                    break;
                                }
                            }
                            if (windowCameraAtom == null)
                            {
                                SuperController.LogError("WindowCamera atom not found!");
                                break;
                            }
                            windowCameraAtom.Reset();
                            if (!FileManagerSecure.FileExists(scenefile))
                            {
                                foreach (var trig in SuperController.singleton.motionAnimationMaster.GetTriggers())
                                {
                                    if (trig.displayName == "cameraActive")
                                    {
                                        SuperController.singleton.motionAnimationMaster.RemoveTrigger(trig);
                                        break;
                                    }
                                }

                                break;
                            }
                            SuperController.LogMessage("WindowCamera Atom " + windowCameraAtom.uid + " found.");
                            string CameraPresetFullName = "Custom/Atom/WindowCamera/Preset_mmdloader.vap";
                            if (FileManagerSecure.FileExists(CameraPresetFullName, false))
                            {
                                FileManagerSecure.DeleteFile(CameraPresetFullName);
                            }
                            bool bAudioSourceControl = false;
                            if (resource.HasKey("AudioSourceControl"))
                            {
                                LogUtil.Log("AudioSourceControl:" + resource["AudioSourceControl"]);
                                bAudioSourceControl = resource["AudioSourceControl"].AsBool;
                            }

                            Mmd2TimelineCameraAtom mmdCamera = new Mmd2TimelineCameraAtom(windowCameraAtom);
                            string strAudioSourceControl = "";
                            if (bAudioSourceControl)
                            {
                                foreach (var audioatom in SuperController.singleton.GetAtoms())
                                {
                                    if (audioatom.type == "AudioSource")
                                    {
                                        strAudioSourceControl = audioatom.uid;
                                        break;
                                    }
                                }
                            }
                            mmdCamera.ImportVmd(scenefile, strAudioSourceControl);
                            yield return new WaitForSeconds(1f);
                            LoadPreset(windowCameraAtom, "Preset", false, CameraPresetFullName);
                            AnimationTimelineTrigger timelineTrigger = null;
                            foreach (var trig in SuperController.singleton.motionAnimationMaster.GetTriggers())
                            {
                                if (trig.displayName == "cameraActive")
                                {
                                    timelineTrigger = trig;
                                    break;
                                }
                            }
                            if (timelineTrigger == null)
                            {
                                timelineTrigger = SuperController.singleton.motionAnimationMaster.AddAndReturnTrigger();
                                timelineTrigger.displayName = "cameraActive";
                            }
                            string triggerjsonstr = String.Format("{{\"displayName\":\"cameraActive\",\"startActions\":[{{\"name\":\"cameraEmbody\",\"receiverAtom\":\"{0}\",\"receiver\":\"plugin#1_Embody\",\"receiverTargetName\":\"Active\",\"boolValue\":\"true\"}},{{\"name\":\"Playcameramotion\",\"receiverAtom\":\"{0}\",\"receiver\":\"plugin#0_VamTimeline.AtomPlugin\",\"receiverTargetName\":\"Play camera motion\"}}],\"transitionActions\":[],\"endActions\":[],\"startTime\":\"0\",\"endTime\":\"0\"}}",
                                windowCameraAtom.uid);
                            JSONClass jsontrigger = (JSONClass)JSON.Parse(triggerjsonstr);
                            timelineTrigger.RestoreFromJSON(jsontrigger);
                            yield return new WaitForSeconds(1f);
                            break;
                        case "atomSubscene":
                            atomSubscene.Add((JSONClass)JSONNode.LoadFromFile(scenefile));
                            yield return new WaitForSeconds(1f);
                            break;

                        default:
                            atompresets.Add(resource);
                            break;
                    }

                }
                if (atoms.Count > 0)
                {
                    base.StartCoroutine(this.AddAtoms(atoms));
                }
                if (atomSubscene.Count > 0)
                {
                    base.StartCoroutine(this.AddAtoms(atomSubscene, true));
                }
                if (atompresets.Count > 0)
                {
                   
                    List<Atom> personAtoms = new List<Atom>();
                    foreach (var atom in SuperController.singleton.GetAtoms())
                    {
                        if (atom.type == "Person" && atom.on)
                        {
                            personAtoms.Add(atom);
                        }
                    }
                    foreach (JSONClass resource in atompresets)
                    {
                        bool merge = false;
                        if (resource.HasKey("merge"))
                        {
                            merge = resource["merge"].AsBool;
                        }
                        if (merge)
                            SuperController.LogMessage("merge load enabled...");
                        string characterGender = "female";
                        if (resource.HasKey("characterGender"))
                        {
                            characterGender = resource["characterGender"].Value;
                            SuperController.LogMessage("Character gender is " + characterGender.ToString());
                        }
                        bool forMale = (characterGender == "male" || characterGender == "futa");
                        bool ignoreGender = false;
                        if (resource.HasKey("ignoreGender"))
                        {
                            ignoreGender = resource["ignoreGender"].AsBool;
                            SuperController.LogMessage("Ignore Gender is " + ignoreGender.ToString());
                        }

                        int personOrder = 1;
                        if (resource.HasKey("personOrder"))
                        {
                            personOrder = resource["personOrder"].AsInt;
                        }
                        int personOrder2 = personOrder;
                        while (SuperController.singleton.isLoading)
                        {
                            yield return new WaitForSeconds(1f);
                        }
                        //Thread.Sleep(2000);
                        string scenetype = "";
                        if (resource.HasKey("type"))
                        {
                            scenetype = resource["type"];
                        }
                        string scenefile = "";
                        if (resource.HasKey("saveName"))
                        {
                            scenefile = resource["saveName"];
                        }
                        bool findatom = false;
                        Atom atom = null;
                        foreach (var personatom in personAtoms)
                        {
                            if (IsMale(personatom) == forMale || ignoreGender)
                            {
                                personOrder2--;
                                if (personOrder2 > 0)
                                    continue;
                                findatom = true;
                                atom = personatom;
                                SuperController.LogMessage("Find Person " + atom.name);
                                break;
                                //atom.SetOn(false);
                                //atom.hidden = true;
                            }
                        }
                        if (findatom)
                        {
                            if (scenetype == ("hairstyle"))
                            {
                                LoadPreset(atom, "HairPresets", merge, scenefile);
                            }
                            if (scenetype == ("morphs"))
                            {
                                LoadPreset(atom, "MorphPresets", merge, scenefile);
                            }
                            if (scenetype == ("skin"))
                            {
                                LoadPreset(atom, "SkinPresets", merge, scenefile);
                            }
                            if (scenetype == ("clothing"))
                            {
                                LoadPreset(atom, "ClothingPresets", merge, scenefile);
                            }
                            if (scenetype == ("plugin"))
                            {
                                LoadPreset(atom, "PluginPresets", merge, scenefile);
                            }
                            if (scenetype == ("breast"))
                            {
                                LoadPreset(atom, "FemaleBreastPhysicsPresets", merge, scenefile);
                            }
                            if (scenetype == ("glute"))
                            {
                                LoadPreset(atom, "FemaleGlutePhysicsPresets", merge, scenefile);
                            }
                            if (scenetype == ("pose"))
                            {
                                SuperController.singleton.motionAnimationMaster.StopPlayback();
                                SuperController.singleton.motionAnimationMaster.ResetAnimation();
                                atom.ResetPhysical();
                                if (scenefile.EndsWith(".json"))
                                    LoadLegacyl(atom, "pose", scenefile);
                                else
                                    LoadPreset(atom, "PosePresets", merge, scenefile);
                            }
                            if (scenetype == ("looks"))
                            {
                                if (scenefile.EndsWith(".json"))
                                {
                                    if (scenefile.IndexOf("/Person/full/") >= 0)
                                        LoadLegacyl(atom, "full", scenefile);
                                    else
                                        LoadLegacyl(atom, "appearance", scenefile);
                                }
                                else
                                {
                                    LoadPreset(atom, "AppearancePresets", merge, scenefile);
                                }
                            }
                            if (scenetype == ("animation"))
                            {
                                SuperController.singleton.motionAnimationMaster.StopPlayback();
                                SuperController.singleton.motionAnimationMaster.SeekToBeginning();
                                SuperController.LogMessage("loaded Storable " + FileManagerSecure.GetFullPath(scenefile));
                                JSONClass atomitem = (JSONClass)JSONNode.LoadFromFile(FileManagerSecure.GetFullPath(scenefile));
                                List<string> storids = atom.GetStorableIDs();
                                JSONClass jcempty = new JSONClass();
                                foreach (string storid in storids)
                                {
                                    if (storid.EndsWith("Animation"))
                                    {
                                        JSONStorable storable = atom.GetStorableByID(storid);
                                        storable.RestoreFromJSON(jcempty);
                                    }
                                }
                                LoadStorableFile(atom, atomitem["storables"].AsArray);
                                SuperController.singleton.motionAnimationMaster.RestoreFromJSON((JSONClass)atomitem["motionAnimationMaster"]);
                                SuperController.singleton.motionAnimationMaster.StartPlayback();
                            }
                            if (scenetype == "highheel")
                            {
                                atom.ResetPhysical();
                                atom.SetOn(false);
                                int atomid = atom.GetInstanceID();
                                if (!mmdPersons.ContainsKey(atomid))
                                {
                                    Mmd2TimelinePersonAtom mmdPerson = new Mmd2TimelinePersonAtom(atom);
                                    mmdPersons[atomid] = mmdPerson;
                                }
                                /*
                                string pluginPresetFullName = "Custom/Atom/Person/Plugins/Preset_mmdloader.vap";
                                if (FileManagerSecure.FileExists(pluginPresetFullName, false))
                                {
                                    FileManagerSecure.DeleteFile(pluginPresetFullName);
                                }
                                */
                                if (resource.HasKey("enableHeel"))
                                {
                                    LogUtil.Log("enableHeel:" + resource["enableHeel"].AsBool.ToString());
                                    mmdPersons[atomid].enableHeel.SetVal(resource["enableHeel"].AsBool);
                                }
                                if (resource.HasKey("footJointDriveXTargetAdjust"))
                                {
                                    LogUtil.Log("footJointDriveXTargetAdjust:" + resource["footJointDriveXTargetAdjust"].AsFloat.ToString());
                                    mmdPersons[atomid].footJointDriveXTargetAdjust.SetVal(resource["footJointDriveXTargetAdjust"].AsFloat);
                                }
                                if (resource.HasKey("toeJointDriveXTargetAdjust"))
                                {
                                    LogUtil.Log("toeJointDriveXTargetAdjust:" + resource["toeJointDriveXTargetAdjust"].AsFloat.ToString());
                                    mmdPersons[atomid].toeJointDriveXTargetAdjust.SetVal(resource["toeJointDriveXTargetAdjust"].AsFloat);
                                }
                                if (resource.HasKey("holdRotationMaxForceAdjust"))
                                {
                                    LogUtil.Log("holdRotationMaxForceAdjust:" + resource["holdRotationMaxForceAdjust"].AsFloat.ToString());
                                    mmdPersons[atomid].holdRotationMaxForceAdjust.SetVal(resource["holdRotationMaxForceAdjust"].AsFloat);
                                }
                                if (resource.HasKey("straightLeg"))
                                {
                                    LogUtil.Log("straightLeg:" + resource["straightLeg"].AsFloat.ToString());
                                    mmdPersons[atomid].StraightLeg(resource["straightLeg"].AsFloat);
                                }
                                if (resource.HasKey("straightLegWorkAngle"))
                                {
                                    LogUtil.Log("straightLegWorkAngle:" + resource["straightLegWorkAngle"].AsFloat.ToString());
                                    mmdPersons[atomid].SetStraightLegWorkAngle(resource["straightLegWorkAngle"].AsFloat);
                                }

                                mmdPersons[atomid].InitAtom();
                                yield return null;
                                float posX = 0, posY = 0, posZ = 0;
                                if (resource.HasKey("posX"))
                                {
                                    LogUtil.Log("posX:" + resource["posX"].AsFloat.ToString());
                                    posX = resource["posX"].AsFloat;
                                }
                                if (resource.HasKey("posY"))
                                {
                                    LogUtil.Log("posY:" + resource["posY"].AsFloat.ToString());
                                    posY = resource["posY"].AsFloat;
                                }
                                if (resource.HasKey("posZ"))
                                {
                                    LogUtil.Log("posZ:" + resource["posZ"].AsFloat.ToString());
                                    posZ = resource["posZ"].AsFloat;
                                }
                                mmdPersons[atomid].pos.SetVal(new Vector3(posX, posY, posZ));
                                yield return null;
                                atom.SetOn(true);


                            }
                            if (scenetype == "personvmd")
                            {
                                atom.ResetPhysical();
                                atom.collisionEnabled = false;
                                int atomid = atom.GetInstanceID();
                                if (!mmdPersons.ContainsKey(atomid))
                                {
                                    Mmd2TimelinePersonAtom mmdPerson = new Mmd2TimelinePersonAtom(atom);

                                    mmdPersons[atomid] = mmdPerson;
                                }
                                string presetFileName = "Preset_mmdloader" + personOrder.ToString() + ".vap";
                                string pluginPresetFullName = "Custom/Atom/Person/Plugins/"+ presetFileName;
                                if (FileManagerSecure.FileExists(pluginPresetFullName, false))
                                {
                                    FileManagerSecure.DeleteFile(pluginPresetFullName);
                                    //yield return new WaitForSeconds(0.5f);
                                }
                                bool bIsTest = false;
                                if (resource.HasKey("isTest"))
                                {
                                    LogUtil.Log("isTest:" + resource["isTest"]);
                                    bIsTest = resource["isTest"].AsBool;
                                }
                                if (bIsTest)
                                {
                                    foreach (var pmc in atom.presetManagerControls)
                                    {
                                        if (pmc.name == "PluginPresets")
                                        {
                                            var pm = pmc.GetComponent<PresetManager>();
                                            JSONClass pluginEmpty = (JSONClass)JSON.Parse("{\"setUnlistedParamsToDefault\":\"true\",\"storables\":[{\"id\":\"PluginManager\",\"plugins\":{}}]}");
                                            pm.LoadPresetFromJSON(pluginEmpty);
                                            yield return new WaitForSeconds(0.5f);
                                            break;
                                        }
                                    }
                                }
                                if (resource.HasKey("ignoreFace"))
                                {
                                    LogUtil.Log("ignoreFace:" + resource["ignoreFace"].AsBool.ToString());
                                    mmdPersons[atomid].IgnoreFace(resource["ignoreFace"].AsBool);
                                }
                                if (resource.HasKey("enableHeel"))
                                {
                                    LogUtil.Log("enableHeel:" + resource["enableHeel"].AsBool.ToString());
                                    mmdPersons[atomid].enableHeel.SetVal(resource["enableHeel"].AsBool);
                                }
                                if (resource.HasKey("footJointDriveXTargetAdjust"))
                                {
                                    LogUtil.Log("footJointDriveXTargetAdjust:" + resource["footJointDriveXTargetAdjust"].AsFloat.ToString());
                                    mmdPersons[atomid].footJointDriveXTargetAdjust.SetVal(resource["footJointDriveXTargetAdjust"].AsFloat);
                                }
                                if (resource.HasKey("toeJointDriveXTargetAdjust"))
                                {
                                    LogUtil.Log("toeJointDriveXTargetAdjust:" + resource["toeJointDriveXTargetAdjust"].AsFloat.ToString());
                                    mmdPersons[atomid].toeJointDriveXTargetAdjust.SetVal(resource["toeJointDriveXTargetAdjust"].AsFloat);
                                }
                                if (resource.HasKey("holdRotationMaxForceAdjust"))
                                {
                                    LogUtil.Log("holdRotationMaxForceAdjust:" + resource["holdRotationMaxForceAdjust"].AsFloat.ToString());
                                    mmdPersons[atomid].holdRotationMaxForceAdjust.SetVal(resource["holdRotationMaxForceAdjust"].AsFloat);
                                }
                                if (resource.HasKey("straightLeg"))
                                {
                                    LogUtil.Log("straightLeg:" + resource["straightLeg"].AsFloat.ToString());
                                    mmdPersons[atomid].StraightLeg(resource["straightLeg"].AsFloat);
                                }
                                if (resource.HasKey("straightLegWorkAngle"))
                                {
                                    LogUtil.Log("straightLegWorkAngle:" + resource["straightLegWorkAngle"].AsFloat.ToString());
                                    mmdPersons[atomid].SetStraightLegWorkAngle(resource["straightLegWorkAngle"].AsFloat);
                                }
                                mmdPersons[atomid].InitAtom();

                                mmdPersons[atomid].ImportVmd(scenefile);
                                if (resource.HasKey("personvmd2"))
                                {
                                    LogUtil.Log("personvmd2:" + resource["personvmd2"]);
                                    mmdPersons[atomid].ImportVmd(resource["personvmd2"]);
                                }

                                bool bAudioSourceControl = false;
                                if (resource.HasKey("AudioSourceControl"))
                                {
                                    LogUtil.Log("AudioSourceControl:" + resource["AudioSourceControl"]);
                                    bAudioSourceControl = resource["AudioSourceControl"].AsBool;
                                }
                                string strAudioSourceControl = "";
                                if (bAudioSourceControl)
                                {
                                    foreach (var audioatom in SuperController.singleton.GetAtoms())
                                    {
                                        if (audioatom.type == "AudioSource")
                                        {
                                            strAudioSourceControl = audioatom.uid;
                                            break;
                                        }
                                    }
                                }
                                float posX = 0, posY = 0, posZ = 0;
                                if (resource.HasKey("posX"))
                                {
                                    LogUtil.Log("posX:" + resource["posX"].AsFloat.ToString());
                                    posX = resource["posX"].AsFloat;
                                }
                                if (resource.HasKey("posY"))
                                {
                                    LogUtil.Log("posY:" + resource["posY"].AsFloat.ToString());
                                    posY = resource["posY"].AsFloat;
                                }
                                if (resource.HasKey("posZ"))
                                {
                                    LogUtil.Log("posZ:" + resource["posZ"].AsFloat.ToString());
                                    posZ = resource["posZ"].AsFloat;
                                }
                                mmdPersons[atomid].pos.SetVal(new Vector3(posX, posY, posZ));
                                float sampleSpeed = 1;
                                if (resource.HasKey("sampleSpeed"))
                                {
                                    LogUtil.Log("sampleSpeed:" + resource["sampleSpeed"]);
                                    sampleSpeed = resource["sampleSpeed"].AsFloat;
                                }

                                mmdPersons[atomid].sampleSpeed.SetVal(sampleSpeed);
                                var anijson = mmdPersons[atomid].StartPlay(strAudioSourceControl, bIsTest);
                                if (!bIsTest)
                                {
                                    mmdPersons[atomid].SavePluginPreset(presetFileName);
                                    LoadPreset(atom, "PluginPresets", false, pluginPresetFullName);
                                    yield return new WaitForSeconds(1f);
                                    AnimationTimelineTrigger timelineTrigger = null;
                                    foreach (var trig in SuperController.singleton.motionAnimationMaster.GetTriggers())
                                    {
                                        if (trig.displayName == "person1MotionActive")
                                        {
                                            timelineTrigger = trig;
                                            break;
                                        }
                                    }
                                    if (timelineTrigger == null)
                                    {
                                        timelineTrigger = SuperController.singleton.motionAnimationMaster.AddAndReturnTrigger();
                                        timelineTrigger.displayName = "person1MotionActive";
                                    }
                                    string triggerjsonstr = String.Format("{{\"displayName\":\"person1MotionActive\",\"startActions\":[{{\"name\":\"Playperson1motion\",\"receiverAtom\":\"{0}\",\"receiver\":\"plugin#0_VamTimeline.AtomPlugin\",\"receiverTargetName\":\"Play\"}}],\"transitionActions\":[],\"endActions\":[],\"startTime\":\"0\",\"endTime\":\"0\"}}",
                                        atom.uid);
                                    JSONClass jsontrigger = (JSONClass)JSON.Parse(triggerjsonstr);
                                    timelineTrigger.RestoreFromJSON(jsontrigger);
                                    yield return new WaitForSeconds(0.5f);
                                    //mmdPersons[atomid].InitAtom();
                                    mmdPersons[atomid].pos.SetVal(new Vector3(posX, posY, posZ));
                                }
                                atom.collisionEnabled = true;
                                //

                            }

                            else
                                SuperController.LogError("Error load Preset,Unable to match the right atom");


                        }

                    }


                }
            }
            else
                yield return new WaitForSeconds(2f);

        }

        private void LoadAudio(string scenefile)
        {
            URLAudioClipManager.singleton.RemoveAllClips();
            Atom audioSourceAtom = null;
            foreach (var atom in SuperController.singleton.GetAtoms())
            {
                if (atom.type == "AudioSource")
                {
                    audioSourceAtom = atom;
                    break;
                }
            }
            if (audioSourceAtom == null)
            {
                //base.StartCoroutine(SuperController.singleton.AddAtomByType("AudioSource", "AudioSource", false, false, false));
                //audioSourceAtom = SuperController.singleton.GetAtomByUid("AudioSource");
                SuperController.LogError("Please Add a AudioSource atom first!");
                return;
            }
            SuperController.LogMessage("AudioSource Atom " + audioSourceAtom.uid + " found.");
            if (!FileManagerSecure.FileExists(scenefile))
            {
                AudioSourceControl source = audioSourceAtom.GetStorableByID("AudioSource") as AudioSourceControl;
                source.Stop();
                foreach (var trig in SuperController.singleton.motionAnimationMaster.GetTriggers())
                {
                    if (trig.displayName == "AnimaStartAudio")
                    {
                        SuperController.singleton.motionAnimationMaster.RemoveTrigger(trig);
                        break;
                    }
                }
                return;
            }
            
            string audiodisname = scenefile.Substring(scenefile.LastIndexOf('/'));
            NamedAudioClip audioClip;
            audioClip = URLAudioClipManager.singleton.GetClip(audiodisname);
            if (audioClip == null)
            {
                URLAudioClipManager.singleton.QueueClip(scenefile);
                audioClip = URLAudioClipManager.singleton.GetClip(audiodisname);
            }
            SuperController.LogMessage("Audio Clip " + audioClip.uid + " added.");

            AnimationTimelineTrigger timelineTrigger = null;
            foreach (var trig in SuperController.singleton.motionAnimationMaster.GetTriggers())
            {
                if (trig.displayName == "AnimaStartAudio")
                {
                    timelineTrigger = trig;
                    break;
                }
            }
            if (timelineTrigger == null)
            {
                timelineTrigger = SuperController.singleton.motionAnimationMaster.AddAndReturnTrigger();
                timelineTrigger.displayName = "AnimaStartAudio";
            }
            string triggerjsonstr = String.Format("{{\"displayName\":\"AnimaStartAudio\",\"startActions\":[{{\"name\":\"AnimaStartAudio\",\"receiverAtom\":\"{0}\",\"receiver\":\"AudioSource\",\"receiverTargetName\":\"PlayNow\",\"audioClipType\":\"URL\",\"audioClipCategory\":\"web\",\"audioClip\":\"{1}\"}}],\"transitionActions\":[],\"endActions\":[],\"startTime\":\"0\",\"endTime\":\"0\"}}",
                audioSourceAtom.uid, audiodisname);
            JSONClass audiotrigger = (JSONClass)JSON.Parse(triggerjsonstr);
            timelineTrigger.RestoreFromJSON(audiotrigger);
        }

        protected IEnumerator AddAtoms(List<JSONClass> jatoms, bool setSubScene = false)
        {
            IEnumerator enumerator7 = jatoms.GetEnumerator();
            try
            {
                while (enumerator7.MoveNext())
                {
                    object obj = enumerator7.Current;
                    JSONClass jsonclass = (JSONClass)obj;
                    string uid = jsonclass["id"];
                    Atom atomByUid = SuperController.singleton.GetAtomByUid(uid);
                    if (atomByUid != null)
                    {
                        atomByUid.PreRestore();
                    }
                }
            }
            finally
            {
                IDisposable disposable;
                if ((disposable = (enumerator7 as IDisposable)) != null)
                {
                    disposable.Dispose();
                }
            }

            Physics.autoSimulation = false;
            IEnumerator enumerator8 = jatoms.GetEnumerator();
            try
            {
                while (enumerator8.MoveNext())
                {
                    object obj2 = enumerator8.Current;
                    JSONClass jatom = (JSONClass)obj2;
                    string auid = jatom["id"];
                    string type = jatom["type"];

                    Atom a = SuperController.singleton.GetAtomByUid(auid);
                    if (a == null)
                    {
                        yield return base.StartCoroutine(SuperController.singleton.AddAtomByType(type, auid, false, false, false));
                        a = SuperController.singleton.GetAtomByUid(auid);
                    }
                    else if (a.type != type)
                    {
                        SuperController.LogError(string.Concat(new string[]
                        {
                            "Atom ",
                            a.name,
                            " already exists, but uses different type ",
                            a.type,
                            " compared to requested ",
                            type
                        }), true, true);
                    }
                    if (a != null)
                    {
                        a.SetOn(true);
                    }
                }
            }
            finally
            {
                IDisposable disposable2;
                if ((disposable2 = (enumerator8 as IDisposable)) != null)
                {
                    disposable2.Dispose();
                }
            }
            yield return null;
            Physics.Simulate(0.01f);
            yield return null;
            IEnumerator enumerator9 = jatoms.GetEnumerator();
            try
            {
                while (enumerator9.MoveNext())
                {
                    object obj3 = enumerator9.Current;
                    JSONClass jsonclass2 = (JSONClass)obj3;
                    string text = jsonclass2["id"];
                    string str = jsonclass2["type"];
                    Atom atomByUid2 = SuperController.singleton.GetAtomByUid(text);
                    if (atomByUid2 != null)
                    {
                        atomByUid2.RestoreTransform(jsonclass2, true);
                    }
                    else
                    {
                        SuperController.LogError("Failed to find atom " + text + " of type " + str, true, true);
                    }
                }
            }
            finally
            {
                IDisposable disposable3;
                if ((disposable3 = (enumerator9 as IDisposable)) != null)
                {
                    disposable3.Dispose();
                }
            }
            IEnumerator enumerator10 = jatoms.GetEnumerator();
            try
            {
                while (enumerator10.MoveNext())
                {
                    object obj4 = enumerator10.Current;
                    JSONClass jsonclass3 = (JSONClass)obj4;
                    string uid2 = jsonclass3["id"];
                    Atom atomByUid3 = SuperController.singleton.GetAtomByUid(uid2);
                    if (atomByUid3 != null)
                    {
                        atomByUid3.RestoreParentAtom(jsonclass3);
                    }
                }
            }
            finally
            {
                IDisposable disposable4;
                if ((disposable4 = (enumerator10 as IDisposable)) != null)
                {
                    disposable4.Dispose();
                }
            }
            IEnumerator enumerator11 = jatoms.GetEnumerator();
            try
            {
                while (enumerator11.MoveNext())
                {
                    object obj5 = enumerator11.Current;
                    JSONClass jsonclass4 = (JSONClass)obj5;
                    string text2 = jsonclass4["id"];
                    Atom atomByUid4 = SuperController.singleton.GetAtomByUid(text2);
                    if (atomByUid4 != null)
                    {
                        atomByUid4.Restore(jsonclass4, true, true, true, null, false, false, true, false);
                    }
                    else
                    {
                        SuperController.LogError("Could not find atom by uid " + text2, true, true);
                    }
                }
            }
            finally
            {
                IDisposable disposable5;
                if ((disposable5 = (enumerator11 as IDisposable)) != null)
                {
                    disposable5.Dispose();
                }
            }
            IEnumerator enumerator12 = jatoms.GetEnumerator();
            try
            {
                while (enumerator12.MoveNext())
                {
                    object obj6 = enumerator12.Current;
                    JSONClass jsonclass5 = (JSONClass)obj6;
                    string text3 = jsonclass5["id"];
                    Atom atomByUid5 = SuperController.singleton.GetAtomByUid(text3);
                    if (atomByUid5 != null)
                    {
                        atomByUid5.LateRestore(jsonclass5, true, true, true, false, true, false);
                    }
                    else
                    {
                        SuperController.LogError("Could not find atom by uid " + text3, true, true);
                    }
                }
            }
            finally
            {
                IDisposable disposable6;
                if ((disposable6 = (enumerator12 as IDisposable)) != null)
                {
                    disposable6.Dispose();
                }
            }
            if (setSubScene)
            {
                yield return base.StartCoroutine(SuperController.singleton.AddAtomByType("SubScene", "SubSceneVarManager", false, false, false));

                IEnumerator enumerator13 = jatoms.GetEnumerator();
                try
                {
                    while (enumerator13.MoveNext())
                    {
                        object obj6 = enumerator13.Current;
                        JSONClass jsonclass5 = (JSONClass)obj6;
                        string text3 = jsonclass5["id"];
                        Atom atomByUid5 = SuperController.singleton.GetAtomByUid(text3);
                        if (atomByUid5 != null)
                        {
                            atomByUid5.SetParentAtom("SubSceneVarManager");
                        }
                        else
                        {
                            SuperController.LogError("Could not find atom by uid " + text3, true, true);
                        }
                    }
                }
                finally
                {
                    IDisposable disposable6;
                    if ((disposable6 = (enumerator13 as IDisposable)) != null)
                    {
                        disposable6.Dispose();
                    }
                }
            }

            foreach (Atom atom8 in SuperController.singleton.GetAtoms())
            {
                atom8.PostRestore();
            }


            Physics.autoSimulation = true;

            yield return null;
        }
        private void LoadPreset(Atom atom, string presetType, bool merge, string presetFile)
        {
            JSONStorable js = atom.GetStorableByID(presetType);

            JSONStorableBool loadOnSelectJSON = js.GetBoolJSONParam("loadPresetOnSelect");
            bool preState = loadOnSelectJSON.val;
            loadOnSelectJSON.val = false;

            JSONStorableUrl presetPathJSON = js.GetUrlJSONParam("presetBrowsePath");
            string pathPreState = presetPathJSON.val;
            presetPathJSON.val = SuperController.singleton.NormalizePath(presetFile);

            if (merge) js.CallAction("MergeLoadPreset");
            else js.CallAction("LoadPreset");

            //presetPathJSON.val = pathPreState;
            //loadOnSelectJSON.val = preState;
            SuperController.LogMessage("loaded " + presetType + ": " + presetFile);
        }
        private void SetAnimationStorableIds()
        {
            List<string> animationStorableIds = new List<string>();
            foreach (string id in containingAtom.GetStorableIDs())
            {
                if (id.EndsWith("Animation"))
                {
                    animationStorableIds.Add(id);
                }
            }
        }
        private void LoadStorableFile(Atom atom, JSONArray storablesArray)
        {
            foreach (JSONClass storablesitem in storablesArray)
            {
                LoadStorableFile(atom, storablesitem);
            }

        }
        private void LoadStorableFile(Atom atom, JSONClass storablesitem)
        {
            if (storablesitem.HasKey("id"))
            {
                LogUtil.Log("LoadStorableFile storablesitem.HasKey(\"id\")" + storablesitem["id"].Value);
                JSONStorable storable = atom.GetStorableByID(storablesitem["id"].Value);
                if (storable != null)
                    storable.RestoreFromJSON(storablesitem);
                else
                {
                    LogUtil.Log("LoadStorableFile storable id " + storablesitem["id"].Value + " not exist in  atom ");
                    JSONStorable js = new JSONStorable();
                    js.RestoreFromJSON(storablesitem);
                    atom.RegisterAdditionalStorable(js);
                }
            }
        }
        private void LoadLegacyl(Atom atom, string presetType, string presetFile)
        {
            if (presetType == "pose")
                atom.LoadPhysicalPreset(presetFile);
            if (presetType == "full")
                atom.LoadPreset(presetFile);
            if (presetType == "appearance")
                atom.LoadAppearancePreset(presetFile);
            SuperController.LogMessage("loaded " + presetType + ": " + presetFile);
        }

        private void LoadPreset(string presetType, bool merge, string presetFile, bool formale = false, bool futaasfemale = false, int personOrder = 1)
        {
            SuperController.LogMessage((merge ? " Merge LoadPreset " : "LoadPreset ") + presetType + ":" + presetFile);

            //bool formale = ForMale(presetFile, futaasfemale);
            bool findatom = false;
            foreach (var atom in SuperController.singleton.GetAtoms())
            {
                if (atom.type == "Person" && atom.on)
                {
                    SuperController.LogMessage("Find Person " + (IsMale(atom, futaasfemale) ? "Male :" : "Female :") + atom.name);
                    if (IsMale(atom, futaasfemale) == formale)
                    {
                        personOrder--;
                        if (personOrder > 0)
                            continue;
                        //atom.SetOn(false);
                        JSONStorable js = atom.GetStorableByID(presetType);

                        JSONStorableBool loadOnSelectJSON = js.GetBoolJSONParam("loadPresetOnSelect");
                        bool preState = loadOnSelectJSON.val;
                        loadOnSelectJSON.val = false;

                        JSONStorableUrl presetPathJSON = js.GetUrlJSONParam("presetBrowsePath");
                        string pathPreState = presetPathJSON.val;
                        presetPathJSON.val = SuperController.singleton.NormalizePath(presetFile);

                        if (merge) js.CallAction("MergeLoadPreset");
                        else js.CallAction("LoadPreset");

                        presetPathJSON.val = pathPreState;
                        loadOnSelectJSON.val = preState;
                        //atom.SetOn(true);
                        findatom = true;
                        SuperController.LogMessage("loaded Preset: " + presetFile);
                        break;
                    }
                }
            }
            if (!findatom)
                SuperController.LogError("Error load Preset " + presetFile + ",Unable to match the right atom");

        }

        private void LoadLegacyl(string presetType, string presetFile, bool formale = false, bool futaasfemale = false, int personOrder = 1)
        {
            SuperController.LogMessage("LoadLegacyl " + presetType + ":" + presetFile);

            //bool formale = ForMale(presetFile, futaasfemale);
            bool findatom = false;
            foreach (var atom in SuperController.singleton.GetAtoms())
            {
                if (atom.type == "Person" && atom.on)
                {
                    SuperController.LogMessage("Find Person " + (IsMale(atom, futaasfemale) ? "Male :" : "Female :") + atom.name);
                    if (IsMale(atom, futaasfemale) == formale)
                    {
                        personOrder--;
                        if (personOrder > 0)
                            continue;
                        //atom.SetOn(false);
                        if (presetType == "pose")
                            atom.LoadPhysicalPreset(presetFile);
                        if (presetType == "full")
                            atom.LoadPreset(presetFile);
                        if (presetType == "appearance")
                            atom.LoadAppearancePreset(presetFile);
                        findatom = true;
                        //atom.SetOn(true);
                        SuperController.LogMessage("loaded Preset: " + presetFile);
                        break;
                    }
                }
            }
            if (!findatom)
                SuperController.LogError("Error load Preset " + presetFile + ",Unable to match the right atom");

        }

        private static bool ForMale(string presetFile, bool futaAsFemale = false)
        {
            bool formale = false;
            string presetstr = SuperController.singleton.ReadFileIntoString(presetFile);
            int indexcharacter = presetstr.IndexOf("\"character\"", StringComparison.OrdinalIgnoreCase);
            if (indexcharacter >= 0)
            {
                presetstr = presetstr.Substring(indexcharacter);
                int indexcharacter2 = presetstr.IndexOf(",", StringComparison.OrdinalIgnoreCase);
                presetstr = presetstr.Substring(0, indexcharacter2);
                string resultString = "";
                try
                {
                    resultString = Regex.Match(presetstr, @"\x3A\s*\x22(?<character>.*?)\x22", RegexOptions.Multiline).Groups["character"].Value;
                }
                catch (ArgumentException ex)
                {
                    // Syntax error in the regular expression
                }
                if (!string.IsNullOrEmpty(resultString))
                {
                    resultString = resultString.Trim().ToLower();
                    formale = resultString.StartsWith("male") ||
                            resultString.StartsWith("lee") ||
                            resultString.StartsWith("jarlee") ||
                            resultString.StartsWith("julian") ||
                            resultString.StartsWith("jarjulian");
                    if (!futaAsFemale)
                        formale = formale || resultString.StartsWith("futa");

                }
            }

            return formale;
        }

        public bool IsMale(Atom atom, bool futaAsFemale = false)
        {
            bool isMale = false;
            string atomCharacter = atom.GetComponentInChildren<DAZCharacter>().name.ToLower();
            // If the peson atom is not "On", then we cant determine their gender it seems as GetComponentInChildren<DAZCharacter> just returns null
            isMale = atomCharacter.StartsWith("male") ||
                     atomCharacter.StartsWith("lee") ||
                     atomCharacter.StartsWith("jarlee") ||
                     atomCharacter.StartsWith("julian") ||
                     atomCharacter.StartsWith("jarjulian");
            if (!futaAsFemale)
                isMale = isMale || atomCharacter.StartsWith("futa");
            return (isMale);
        }

        // FixedUpdate is called with each physics simulation frame by Unity
        void FixedUpdate()
        {
            try
            {
                // put code in here
            }
            catch (Exception e)
            {
                SuperController.LogError("Exception caught: " + e);
            }
        }

        // OnDestroy is where you should put any cleanup
        // if you registered objects to supercontroller or atom, you should unregister them here
        void OnDestroy()
        {
        }

    }
}