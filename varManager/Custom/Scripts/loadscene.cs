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


                    string loadscenetxt = "Custom\\PluginData\\feelfar\\loadscene.txt";
                    if (FileManagerSecure.FileExists(loadscenetxt))
                    {
                        SuperController.LogMessage("find loadscene.txt");
                        string atomforload = FileManagerSecure.ReadAllText(loadscenetxt);
						Thread.Sleep(1000);
                        FileManagerSecure.DeleteFile(loadscenetxt);
                        if(atomforload == "rescan")
                        {
							SuperController.LogMessage("RescanPackages");
                            SuperController.singleton.RescanPackages();
                            return;
                        }
                        
                        string[] strLoadscene = atomforload.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        if (strLoadscene.Length >= 2)
                        {
                            string scenetype = strLoadscene[0];
                            if (scenetype.StartsWith("rescan_"))
                            {
								SuperController.LogMessage("RescanPackages");
                                SuperController.singleton.RescanPackages();
                            }
                            bool merge = false;
                            string scenefile = strLoadscene[1];
                            if (scenefile.EndsWith("_merge"))
                            {
                                merge = true;
                                scenefile = scenefile.Substring(0, scenefile.Length - 6);
                            }
                            if (FileManagerSecure.FileExists(scenefile))
                            {
                                if (scenetype.EndsWith("scenes"))
                                {
                                    if (merge)
                                        SuperController.singleton.LoadMerge(scenefile);
                                    else
                                        SuperController.singleton.Load(scenefile);
                                    SuperController.LogMessage("load scene: " + scenefile);
                                }
                                if (scenetype.EndsWith("hairstyle"))
                                {
                                    LoadPreset("HairPresets", merge, scenefile);
                                }
                                if (scenetype.EndsWith("morphs"))
                                {
                                    LoadPreset("MorphPresets", merge, scenefile);
                                }
                                if (scenetype.EndsWith("skin"))
                                {
                                    LoadPreset("SkinPresets", merge, scenefile);
                                }
                                if (scenetype.EndsWith("clothing"))
                                {
                                    LoadPreset("ClothingPresets", merge, scenefile);
                                }
                                if (scenetype.EndsWith("pose"))
                                {
                                    if (scenefile.EndsWith(".json"))
                                        LoadLegacyl("pose", scenefile);
                                    else
                                        LoadPreset("PosePresets", merge, scenefile);
                                }
                                if (scenetype.EndsWith("looks"))
                                {
                                    if (scenefile.EndsWith(".json"))
                                    {
                                        if (scenefile.IndexOf("/Person/full/") >= 0)
                                            LoadLegacyl("full", scenefile);
                                        else
                                            LoadLegacyl("appearance", scenefile);
                                    }
                                    else
                                        LoadPreset("AppearancePresets", merge, scenefile);
                                }

                            }
                            else
                            {
                                SuperController.LogError("no found " + strLoadscene[1]);
                            }
                        }
                        else
                        {
                            SuperController.LogError("unknown format");
                        }
                    }

                }

            }
            catch (Exception e)
            {
                SuperController.LogError("Exception caught: " + e);
            }
        }
        private void LoadPreset(string presetType, bool merge, string presetFile)
        {
            SuperController.LogMessage((merge ? " Merge LoadPreset " : "LoadPreset ") + presetType + ":" + presetFile);

            bool formale = ForMale(presetFile);
            bool findatom = false;
            foreach (var atom in SuperController.singleton.GetAtoms())
            {
                if (atom.type == "Person" && atom.on)
                {

                    SuperController.LogMessage("Find Person " + (IsMale(atom) ? "Male :" : "Female :") + atom.name);
                    if (IsMale(atom) == formale)
                    {
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

        private static bool ForMale(string presetFile)
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
                    if (resultString.StartsWith("male") || resultString.StartsWith("lee")
                          || resultString.StartsWith("julian") || resultString.StartsWith("futa"))
                    {
                        formale = true;
                    }
                }
            }

            return formale;
        }

        private void LoadLegacyl(string presetType, string presetFile)
        {
            SuperController.LogMessage("LoadLegacyl " + presetType + ":" + presetFile);

            bool formale = ForMale(presetFile);
            bool findatom = false;
            foreach (var atom in SuperController.singleton.GetAtoms())
            {
                if (atom.type == "Person" && atom.on)
                {
                    SuperController.LogMessage("Find Person " + (IsMale(atom) ? "Male :" : "Female :") + atom.name);
                    if (IsMale(atom) == formale)
                    {
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

        public bool IsMale(Atom atom)
        {
            bool isMale = false;
            // If the peson atom is not "On", then we cant determine their gender it seems as GetComponentInChildren<DAZCharacter> just returns null
            if (atom.on) isMale = atom.GetComponentInChildren<DAZCharacter>().name.StartsWith("male") ||
                    atom.GetComponentInChildren<DAZCharacter>().name.StartsWith("jarlee") ||
                    atom.GetComponentInChildren<DAZCharacter>().name.StartsWith("jarjulian") ||
                    atom.GetComponentInChildren<DAZCharacter>().name.StartsWith("Futa");
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