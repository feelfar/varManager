using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.Threading;
using MeshVR;
using MVR.FileManagementSecure;
using UnityEngine.SceneManagement;
using MVR.FileManagement;
using System.Text.RegularExpressions;

namespace MVRPlugin
{
    public class loadscene : MVRScript
    {

        // IMPORTANT - DO NOT make custom enums. The dynamic C# complier crashes Unity when it encounters these for
        // some reason

        // IMPORTANT - DO NOT OVERRIDE Awake() as it is used internally by MVRScript - instead use Init() function which
        // is called right after creation

        private DateTime dtspan;
        public override void Init()
        {
            try
            {
                // put init code in here
                SuperController.LogMessage("loadscene Loaded");
                dtspan = DateTime.Now;

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

        // Update is called with each rendered frame by Unity
        void Update()
        {
            try
            {
                if ((DateTime.Now - dtspan).Seconds >= 2)
                {
                    dtspan = DateTime.Now;
                    if (SuperController.singleton.isLoading) return;


                    string loadscenetxt = "Custom\\PluginData\\feelfar\\loadscene.json";
                    if (FileManagerSecure.FileExists(loadscenetxt))
                    {
                        SuperController.LogMessage("find loadscene.json");

                        JSONClass atomforload = (JSONClass)JSON.Parse(FileManagerSecure.ReadAllText(loadscenetxt));
                        //SuperController.LogMessage(atomforload.ToString());
                        Thread.Sleep(1000);
                        FileManagerSecure.DeleteFile(loadscenetxt);
                        bool rescan = false;
                        if (atomforload.HasKey("rescan"))
                        {
                            rescan = atomforload["rescan"].AsBool;
                        }
                        if (rescan)
                        {
                            SuperController.LogMessage("Rescan Packages...");
                            SuperController.singleton.RescanPackages();
                            //Thread.Sleep(10000);
                        }
                        bool merge = false;
                        if (atomforload.HasKey("merge"))
                        {
                            SuperController.LogMessage("merge load enabled...");
                            merge = atomforload["merge"].AsBool;
                        }
                        string characterGender = "female";
                        if (atomforload.HasKey("characterGender"))
                        {
                            characterGender = atomforload["characterGender"].Value;
                            SuperController.LogMessage("Character gender is " + characterGender.ToString());
                        }
                        bool futaAsFemale = false;
                        if (atomforload.HasKey("futaAsFemale"))
                        {
                            futaAsFemale = atomforload["futaAsFemale"].AsBool;
                            SuperController.LogMessage("futaAsFemale is " + futaAsFemale.ToString());
                        }
                        bool forMale = (characterGender == "male" || (characterGender == "futa" && (!futaAsFemale)));
                        int personOrder = 1;
                        if (atomforload.HasKey("personOrder"))
                        {
                            personOrder = atomforload["personOrder"].AsInt;
                        }
                        JSONArray resources = atomforload["resources"].AsArray;
                        List<JSONClass> atompresets = new List<JSONClass>();
                        foreach (JSONClass resource in resources)
                        {
                            while (SuperController.singleton.isLoading)
                            {
                                Thread.Sleep(1000);
                            }
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
                            if (string.IsNullOrEmpty(scenetype) || string.IsNullOrEmpty(scenefile))
                                continue;

                            if (FileManagerSecure.FileExists(scenefile))
                            {
                                if (scenetype == ("scenes"))
                                {
                                    SuperController.LogMessage("load " + scenetype + ": " + scenefile);
                                    if (merge)
                                        SuperController.singleton.LoadMerge(scenefile);
                                    else
                                        SuperController.singleton.Load(scenefile);

                                }
                                else
                                {
                                    atompresets.Add(resource);
                                    //SuperController.LogMessage((merge ? " Merge LoadPreset " : "LoadPreset ") + presetType + ":" + presetFile);

                                    //bool formale = ForMale(presetFile, futaasfemale);

                                }
                            }
                            else
                            {
                                SuperController.LogError("no found " + scenefile);
                            }
                        }
                        if (atompresets.Count > 0)
                        {
                            bool findatom = false;
                            foreach (var atom in SuperController.singleton.GetAtoms())
                            {
                                if (atom.type == "Person" && atom.on)
                                {
                                    if (IsMale(atom, futaAsFemale) == forMale)
                                    {
                                        personOrder--;
                                        if (personOrder > 0)
                                            continue;
                                        findatom = true;
                                        SuperController.LogMessage("Find Person " + forMale + atom.name);
                                        //atom.SetOn(false);
										atom.hidden = true;
                                        foreach (JSONClass resource in atompresets)
                                        {
                                            while (SuperController.singleton.isLoading)
                                            {
                                                Thread.Sleep(1000);
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
                                            if (string.IsNullOrEmpty(scenetype) || string.IsNullOrEmpty(scenefile))
                                                continue;

                                            if (FileManagerSecure.FileExists(scenefile))
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
                                            }
                                        }
                                        //atom.SetOn(true);
										atom.hidden = false;
                                        break;
                                    }
                                }
                            }
                            if (!findatom)
                                SuperController.LogError("Error load Preset,Unable to match the right atom");
                        }
                    }
                }

            }
            catch (Exception e)
            {
                SuperController.LogError("Exception caught: " + e);
            }
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
            SuperController.LogMessage("loaded "+ presetType+": " + presetFile);
        }
        private void LoadLegacyl(Atom atom, string presetType, string presetFile)
        {
            if (presetType == "pose")
                atom.LoadPhysicalPreset(presetFile);
            if (presetType == "full")
                atom.LoadPreset(presetFile);
            if (presetType == "appearance")
                atom.LoadAppearancePreset(presetFile);
			SuperController.LogMessage("loaded "+ presetType+": " + presetFile);
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
                        atom.SetOn(false);
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
                        atom.SetOn(true);
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
                        atom.SetOn(false);
                        if (presetType == "pose")
                            atom.LoadPhysicalPreset(presetFile);
                        if (presetType == "full")
                            atom.LoadPreset(presetFile);
                        if (presetType == "appearance")
                            atom.LoadAppearancePreset(presetFile);
                        findatom = true;
                        atom.SetOn(true);
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