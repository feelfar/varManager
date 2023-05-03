using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleJSON;
using varManager.Properties;

namespace varManager
{
    public partial class FormAnalysis : Form
    {
        private string varName;
        private string entryName;
        private List<JSONClass> listJsonPerson;
       JSONClass jsonCoreControl;
        
        private int personOrder = 0;
        private bool ignoreGender = false;
        private string characterGender;
        public Form1 form1;
        public string VarName { get => varName; set => varName = value; }
        public string EntryName { get => entryName; set => entryName = value; }
        public bool IgnoreGender { get => ignoreGender; set => ignoreGender = value; }
        public int PersonOrder { get => personOrder; set => personOrder = value; }
        public string CharacterGender { get => characterGender; set => characterGender = value; }

        public List<JSONClass> saveNames;
        private List<TreeNode> atomnodes;
        private string[] poseControlIDs = { "hipControl", "pelvisControl", "chestControl", "headControl", "rHandControl", "lHandControl", "rFootControl", "lFootControl", "neckControl", "eyeTargetControl", "rNippleControl", "lNippleControl", "rElbowControl", "lElbowControl", "rKneeControl", "lKneeControl", "rToeControl", "lToeControl", "abdomenControl", "abdomen2Control", "rThighControl", "lThighControl", "rArmControl", "lArmControl", "rShoulderControl", "lShoulderControl" };
        private string[] poseObjectIDs = { "hip", "pelvis", "rThigh", "rShin", "rFoot", "rToe", "lThigh", "lShin", "lFoot", "lToe", "LGlute", "RGlute", "abdomen", "abdomen2", "chest", "lPectoral", "rPectoral", "rCollar", "rShldr", "rForeArm", "rHand", "lCollar", "lShldr", "lForeArm", "lHand", "neck", "head"};
        private Dictionary<string, List<string>> parentAtoms;
        private readonly string[] sceneBaseAtoms = { "CoreControl", "PlayerNavigationPanel", "VRController", "WindowCamera" };
        public FormAnalysis()
        {
            InitializeComponent();
            listJsonPerson = new List<JSONClass>();
            parentAtoms = new Dictionary<string, List<string>>();
        }
        public static string GetCharacterGender(string character)
        {
            string isMale = "Female";
            character= character.ToLower();
            // If the peson atom is not "On", then we cant determine their gender it seems as GetComponentInChildren<DAZCharacter> just returns null
            if (character.StartsWith("male") ||
                    character.StartsWith("lee") ||
                    character.StartsWith("jarlee") ||
                    character.StartsWith("julian") ||
                    character.StartsWith("jarjulian"))
            {
                isMale = "Male";
            }
            if (character.StartsWith("futa"))
            {
                isMale = "Futa";
            }
            return (isMale);
        }

        private void FormAnalysis_Load(object sender, EventArgs e)
        {
            labelSceneName.Text = varName + ":/" + entryName;
            AnalsisJson();
            if (listBoxAtom.Items.Count > 0)
            {
                listBoxAtom.SelectedIndex = 0;
            }
            PersonOrder = 0;
            listBoxPerson.SelectedIndex = 0;
            triStateTreeViewAtoms.ExpandAll();
            //foreach (TreeNode tn in triStateTreeViewAtoms.Nodes)
            //{
            //    tn.Expand();
            //}
        }
        /*private void DisplayTreeView(JSONClass root, string rootName)
        {
            treeView1.BeginUpdate();
            try
            {
                treeView1.Nodes.Clear();
                var tNode = treeView1.Nodes[treeView1.Nodes.Add(new TreeNode(rootName))];
                tNode.Tag = root;

                AddNode(root, tNode);

                treeView1.ExpandAll();
            }
            finally
            {
                treeView1.EndUpdate();
            }
        }

        private void AddNode(JSONClass token, TreeNode inTreeNode)
        {
            if (token == null)
                return;
            if (token is jso)
            {
                var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(token.ToString()))];
                childNode.Tag = token;
            }
            else if (token is JObject)
            {
                var obj = (JObject)token;
                foreach (var property in obj.Properties())
                {
                    var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(property.Name))];
                    childNode.Tag = property;
                    AddNode(property.Value, childNode);
                }
            }
            else if (token is JArray)
            {
                var array = (JArray)token;
                for (int i = 0; i < array.Count; i++)
                {
                    var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(i.ToString()))];
                    childNode.Tag = array[i];
                    AddNode(array[i], childNode);
                }
            }
            else
            {
                Debug.WriteLine(string.Format("{0} not implemented", token.Type)); // JConstructor, JRaw
            }
        }*/
        private void AnalsisJson()
        {
            triStateTreeViewAtoms.Nodes.Clear();
            TreeNode treenodeCur = triStateTreeViewAtoms.Nodes.Add(varName + ":/" + entryName);
            string sceneFoldername = Path.Combine(Directory.GetCurrentDirectory(), "Cache",
                        Comm.ValidFileName(varName), Comm.ValidFileName(entryName.Replace('\\', '_').Replace('/', '_')));
            jsonCoreControl = new JSONClass();
            atomnodes = new List<TreeNode>();
            GetAtoms(sceneFoldername, treenodeCur);
            string parentAtomFilename = Path.Combine(sceneFoldername, "parentAtom.txt");
            if (File.Exists(parentAtomFilename))
            {
                using (StreamReader sr = new StreamReader(parentAtomFilename))
                {
                    while (!sr.EndOfStream) { 
                       string strParent= sr.ReadLine();
                       string[] splitParent= strParent.Split('\t');
                        if(splitParent.Length == 2)
                        {
                            List<string> childs= splitParent[1].Split(',').ToList();
                            childs = childs.Select(child => child += ".bin").ToList();
                            parentAtoms.Add(splitParent[0]+".bin",childs);
                        }
                    }
                   
                }
            }
            if (!entryName.ToLower().Contains("/scene/"))
            {
                tabControl1.TabPages.RemoveAt(tabControl1.TabPages.Count - 1);
            }

        }
        private void GetAtoms(string sceneFoldername,TreeNode treenodeCur )
        {
            if (Directory.Exists(Path.Combine(sceneFoldername, "atoms")))
            {
                foreach (string atomfolder in Directory.GetDirectories(Path.Combine(sceneFoldername, "atoms"), "*", SearchOption.TopDirectoryOnly).OrderBy(x=>Path.GetFileName(x)))
                {
                    string atomtype = atomfolder.Substring(atomfolder.LastIndexOf("\\") + 1);
                    TreeNode subtreenode = treenodeCur.Nodes.Add(atomtype);
                    if (atomtype == "SubScene")
                    {
                        GetAtoms(atomfolder, subtreenode);
                    }
                   
                    foreach (string atomjson in Directory.GetFiles(atomfolder, "*.bin", SearchOption.TopDirectoryOnly))
                    {

                        atomnodes.Add(subtreenode.Nodes.Add(Path.GetFileName(atomjson)));
                        if (atomtype == "(base)CoreControl")
                        {
                            jsonCoreControl=(JSONClass)JSONNode.LoadFromFile(atomjson);
                        }
                        if (atomtype == "Person")
                        {
                            JSONClass atomitem = (JSONClass)JSONNode.LoadFromFile(atomjson);
                            listJsonPerson.Add(atomitem);
                            listBoxAtom.Items.Add(Path.GetFileNameWithoutExtension(atomjson));
                        }
                    }
                }
            }
        }

        private static string GetAtomID(JSONNode atomitem)
        {
            string charGender = "unknown";
            JSONArray storablesArray = atomitem["storables"].AsArray;
            foreach (JSONNode storablesitem in storablesArray)
            {
                if (storablesitem["id"].Value == "geometry")
                {
                    charGender = GetCharacterGender(storablesitem["character"].Value);
                    break;
                }
            }
            return string.Format("{0}({1})", atomitem["id"], charGender);
            
        }

        private void listBoxAtom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAtom.SelectedItem != null)
            {
                buttonLoadLook.Enabled = true;
                if (listBoxAtom.SelectedItem.ToString().Contains("(Female)"))
                {
                    checkBoxGlute.Visible  = true;
                    checkBoxBreast.Visible = true;
                    checkBoxIgnoreGender.Enabled = true;
                }
                else
                {
                    checkBoxGlute.Visible = false;
                    checkBoxBreast.Visible = false;
                    //if (listBoxAtom.SelectedItem.ToString().Contains("(Futa)"))
                        checkBoxIgnoreGender.Enabled = true;
                    //else
                    //    checkBoxIgnoreGender.Enabled = false;
                }
                JSONArray storablesArray = listJsonPerson[listBoxAtom.SelectedIndex]["storables"].AsArray;
                buttonLoadPose.Enabled = false;
                if ( entryName.EndsWith(".json"))
                    buttonLoadPose.Enabled = true ;
                buttonLoadPlugin.Enabled = false;
                buttonLoadAnimation.Enabled = false;

                bool foundAnime=false,foundPlugin=false;
                foreach (JSONClass storablesitem in storablesArray)
                {
                    if (foundAnime && foundPlugin) break;
                    if (storablesitem["id"].Value.EndsWith ( "Animation") )
                    {
                        buttonLoadAnimation.Enabled = true;
                        foundAnime = true;
                    }
                    if (storablesitem["id"].Value== "PluginManager")
                    {
                        foundPlugin=true;
                        if (storablesitem.HasKey("plugins"))
                        {
                            if (((JSONClass)storablesitem["plugins"]).Count > 0)
                            {
                                buttonLoadPlugin.Enabled = true;
                            }
                        }
                    }
                }
            }
            else
            {
                buttonLoadLook.Enabled= false;
            }
        }

        private void buttonLoadLook_Click(object sender, EventArgs e)
        {
            if (listBoxAtom.SelectedIndex >= 0 && listBoxAtom.SelectedIndex < listJsonPerson.Count)
            {
                if (checkBoxMorphs.Checked || checkBoxHair.Checked ||
                    checkBoxClothing.Checked || checkBoxSkin.Checked ||
                    checkBoxBreast.Checked || checkBoxGlute.Checked)

                {
                    /*using (StreamReader sr = new StreamReader(listJsonPerson[listBoxAtom.SelectedIndex]))
                    {
                        string json = sr.ReadToEnd();
                        JSONClass atomitem = (JSONClass)JSON.Parse(json);
                        SavePreset(atomitem, checkBoxMorphs.Checked, checkBoxHair.Checked,
                           checkBoxClothing.Checked, checkBoxSkin.Checked,
                           checkBoxBreast.Checked, checkBoxGlute.Checked);
                    }*/
                    //JSONClass atomitem =(JSONClass) JSONNode.LoadFromFile(listJsonPerson[listBoxAtom.SelectedIndex]);
                    GetPersonOrder();
                    saveNames = new List<JSONClass>();
                    SavePreset(listJsonPerson[listBoxAtom.SelectedIndex], checkBoxMorphs.Checked, checkBoxHair.Checked,
                           checkBoxClothing.Checked, checkBoxSkin.Checked,
                           checkBoxBreast.Checked, checkBoxGlute.Checked);

                    
                    labelMessage.Text = "Load Look Preset completed!";
                    labelMessage.Visible = true;
                    timer1.Enabled = true;
                    GenLoadscenetxt();
                }
                else
                {
                    MessageBox.Show("Please select at least one preset");
                    checkBoxMorphs.Select();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select a character");
                listBoxAtom.Select();
                return;
            }
        }

        private void GetPersonOrder()
        {
            PersonOrder = 0;
            if (listBoxPerson.SelectedIndex > 0)
                PersonOrder = listBoxPerson.SelectedIndex;
            ignoreGender = false;
            if (checkBoxIgnoreGender.Enabled && checkBoxIgnoreGender.Checked)
                ignoreGender = true;
        }

        private void SavePluginPreset(JSONClass atomitem)
        {
            JSONClass jsonPlugin = new JSONClass();
            jsonPlugin.Add("setUnlistedParamsToDefault", "true");
            jsonPlugin.Add("storables", new JSONArray());
            List<string> pluginid = new List<string>();
            JSONArray storablesArray = atomitem["storables"].AsArray;
            foreach (JSONClass storablesitem in storablesArray)
            {
                if (storablesitem["id"].Value == "PluginManager")
                {
                    JSONArray jsonPluginStorablesArray = jsonPlugin["storables"].AsArray;
                    jsonPluginStorablesArray.Add(storablesitem);
                    if (storablesitem.HasKey("plugins"))
                    {
                        pluginid.AddRange(((JSONClass)storablesitem["plugins"]).Keys);
                    }
                }
            }
            foreach (JSONClass storablesitem in storablesArray)
            {
                foreach (string tempid in pluginid)
                {
                    if (storablesitem["id"].Value.StartsWith(tempid))
                    {
                        jsonPlugin["storables"].Add(storablesitem);
                    }
                }
            }
            SavePluginPresetFile(jsonPlugin);
        }
        private void SavePosePreset(JSONClass atomitem)
        {
            JSONClass jsonPose = new JSONClass();
            jsonPose.Add("setUnlistedParamsToDefault", "true");
            jsonPose.Add("storables", new JSONArray());
            JSONArray storablesArray = atomitem["storables"].AsArray;
           
            foreach (JSONClass storablesitem in storablesArray)
            {
                if (storablesitem["id"].Value == "geometry")
                {
                    JSONClass jsonMorphsGeometry= new JSONClass();
                    jsonMorphsGeometry.Add("id", "geometry");
                    jsonMorphsGeometry.Add("morphs", storablesitem["morphs"]);
                    jsonPose["storables"].Add(jsonMorphsGeometry);

                }
                if (poseControlIDs.Contains( storablesitem["id"].Value) || poseObjectIDs.Contains(storablesitem["id"].Value))
                {
                    jsonPose["storables"].Add(storablesitem);
                }
            }
            
            SavePosePresetFile(jsonPose);
        }
        private string animationStorableIdToControlId(string id)
        {
            switch (id)
            {
                case "eyeTargetControlAnimation":
                case "lNippleControlAnimation":
                case "rNippleControlAnimation":
                    return id.Replace("Animation", "");

                default:
                    return id.Replace("Animation", "Control");
            }
        }
        private JSONClass FindMotionAnimationMasterData()
        {
            foreach (JSONNode storable in jsonCoreControl["storables"].AsArray)
            {
                if (string.Equals(storable["id"], "MotionAnimationMaster"))
                {
                    return storable.AsObject;
                }
            }
            throw new Exception("Selected mocap file does not contain MotionAnimationMaster data!");
        }

        private void SaveAnimationPreset(JSONClass atomitem)
        {
            JSONClass jsonAnimation = new JSONClass();
            jsonAnimation.Add("setUnlistedParamsToDefault", "true");
            jsonAnimation.Add("storables", new JSONArray());
            JSONArray storablesArray = atomitem["storables"].AsArray;
            List<string> animationControlIds = new List<string>();
            foreach (JSONClass storablesitem in storablesArray)
            {
                if (storablesitem["id"].Value.EndsWith("Animation") )
                {
                    jsonAnimation["storables"].Add(storablesitem);
                    animationControlIds.Add(animationStorableIdToControlId(storablesitem["id"].Value));
                }
            }
            foreach (JSONClass storablesitem in storablesArray)
            {
                if (animationControlIds.Contains(storablesitem["id"].Value))
                {
                    jsonAnimation["storables"].Add(storablesitem);
                }
            }
            jsonAnimation.Add("motionAnimationMaster", FindMotionAnimationMasterData());
            SaveAnimationPresetFile(jsonAnimation);
        }
        private void SavePreset(JSONClass atomitem,
            bool morphs = false,bool hair = false,  
            bool clothing = false, bool skin = false,
            bool breast = false, bool glute = false)
        {
            JSONClass jsonPreset = new JSONClass();
            JSONClass jsonMorphs = new JSONClass();
            JSONClass jsonBreast = new JSONClass();
            JSONClass jsonGlute = new JSONClass();
            JSONClass jsonSkin = new JSONClass();
            JSONClass jsonHair = new JSONClass();
            JSONClass jsonClothing = new JSONClass();
            

            jsonPreset.Add("setUnlistedParamsToDefault", "false");
            jsonMorphs.Add("setUnlistedParamsToDefault", "true");
            jsonBreast.Add("setUnlistedParamsToDefault", "true");
            jsonGlute.Add("setUnlistedParamsToDefault", "true");
            jsonSkin.Add("setUnlistedParamsToDefault", "true");
            jsonHair.Add("setUnlistedParamsToDefault", "true");
            jsonClothing.Add("setUnlistedParamsToDefault", "true");
            

            jsonPreset.Add("storables", new JSONArray());
            jsonMorphs.Add("storables", new JSONArray());
            jsonBreast.Add("storables", new JSONArray());
            jsonGlute.Add("storables", new JSONArray());
            jsonSkin.Add("storables", new JSONArray());
            jsonHair.Add("storables", new JSONArray());
            jsonClothing.Add("storables", new JSONArray());
            

            JSONArray storablesArray = atomitem["storables"].AsArray;
            string[] skinid = { "skin", "textures", "teeth", "tongue", "mouth", "FemaleEyelashes", "MaleEyelashes", "lacrimals" , "sclera" , "irises" };
            string[] breastid = { "BreastControl", "BreastPhysicsMesh" };
            string[] gluteid = { "GluteControl", "LowerPhysicsMesh" };
            List<string> hairid=new List<string>();
            List<string> clothingid=new List<string>();
           

            foreach (JSONClass storablesitem in storablesArray)
            {
                if (storablesitem["id"].Value == "geometry")
                {
                    JSONArray jsonPresetStorablesArray = jsonPreset["storables"].AsArray;
                    JSONArray jsonMorphsStorablesArray = jsonMorphs["storables"].AsArray;
                    JSONArray jsonBreastStorablesArray = jsonBreast["storables"].AsArray;
                    JSONArray jsonGluteStorablesArray = jsonGlute["storables"].AsArray;
                    JSONArray jsonSkinStorablesArray = jsonSkin["storables"].AsArray;
                    JSONArray jsonHairStorablesArray = jsonHair["storables"].AsArray;
                    JSONArray jsonClothingStorablesArray = jsonClothing["storables"].AsArray;

                    //jnsstorablesArray.Add(storablesitem);
                    jsonPresetStorablesArray.Add(new JSONClass());
                    jsonMorphsStorablesArray.Add(new JSONClass());
                    jsonBreastStorablesArray.Add(new JSONClass());
                    jsonGluteStorablesArray.Add(new JSONClass());
                    jsonSkinStorablesArray.Add(new JSONClass());
                    jsonHairStorablesArray.Add(new JSONClass());
                    jsonClothingStorablesArray.Add(new JSONClass());

                    JSONClass jsonPresetGeometry = (JSONClass)jsonPresetStorablesArray[jsonPresetStorablesArray.Count - 1];
                    JSONClass jsonMorphsGeometry = (JSONClass)jsonMorphsStorablesArray[jsonMorphsStorablesArray.Count - 1];
                    JSONClass jsonBreastGeometry = (JSONClass)jsonBreastStorablesArray[jsonBreastStorablesArray.Count - 1];
                    JSONClass jsonGluteGeometry = (JSONClass)jsonGluteStorablesArray[jsonGluteStorablesArray.Count - 1];
                    JSONClass jsonSkinGeometry = (JSONClass)jsonSkinStorablesArray[jsonSkinStorablesArray.Count - 1];
                    JSONClass jsonHairGeometry = (JSONClass)jsonHairStorablesArray[jsonHairStorablesArray.Count - 1];
                    JSONClass jsonClothingGeometry = (JSONClass)jsonClothingStorablesArray[jsonClothingStorablesArray.Count - 1];

                    jsonPresetGeometry.Add("id", "geometry");
                    jsonMorphsGeometry.Add("id", "geometry");
                    jsonBreastGeometry.Add("id", "geometry");
                    jsonSkinGeometry.Add("id", "geometry");
                    jsonHairGeometry.Add("id", "geometry");
                    jsonClothingGeometry.Add("id", "geometry");

                    if (storablesitem.HasKey("useFemaleMorphsOnMale"))
                        jsonMorphsGeometry.Add("useFemaleMorphsOnMale", storablesitem["useFemaleMorphsOnMale"].Value);
                    if (skin) jsonPresetGeometry.Add("character", storablesitem["character"].Value);
                    CharacterGender = GetCharacterGender(storablesitem["character"].Value);
                    jsonSkinGeometry.Add("character", storablesitem["character"]);
                   
                    jsonMorphsGeometry.Add("morphs",storablesitem["morphs"]);
                    if (clothing)
                    {
                        jsonPresetGeometry.Add("clothing", storablesitem["clothing"]);
                        jsonClothingGeometry.Add("clothing", storablesitem["clothing"]);
                    }
                    if (hair)
                    {
                        jsonPresetGeometry.Add("hair", storablesitem["hair"]);
                        jsonHairGeometry.Add("hair", storablesitem["hair"]);
                    }
                    if (storablesitem.HasKey("useAuxBreastColliders"))
                        jsonBreastGeometry.Add("useAuxBreastColliders", storablesitem["useAuxBreastColliders"]);

                    
                    if (jsonPresetGeometry.HasKey("clothing"))
                    {
                        JSONArray temparray = jsonPresetGeometry["clothing"].AsArray;
                        foreach (JSONClass tempid in temparray)
                        {
                            clothingid.Add(tempid["internalId"].Value);
                        }
                    }
                    if (jsonPresetGeometry.HasKey("hair"))
                    {
                        JSONArray temparray = jsonPresetGeometry["hair"].AsArray;
                        foreach(JSONClass tempid in temparray)
                        {
                            hairid.Add(tempid["internalId"].Value); 
                        }
                    }
                    
                    if (jsonClothingGeometry.HasKey("clothing"))
                    {
                        JSONArray temparray = jsonClothingGeometry["clothing"].AsArray;
                        foreach (JSONClass tempid in temparray)
                        {
                            clothingid.Add(tempid["internalId"].Value);
                        }
                    }
                    if (jsonHairGeometry.HasKey("hair"))
                    {
                        JSONArray temparray = jsonHairGeometry["hair"].AsArray;
                        foreach (JSONClass tempid in temparray)
                        {
                            hairid.Add(tempid["internalId"].Value);
                        }
                    }
                    break;
                }
            }

            foreach (JSONClass storablesitem in storablesArray)
            {
                //if(storablesitem["id"].Value == "AutoExpressions")
                //{
                //    jns["storables"].Add(storablesitem);
                //}
                if (clothing)
                {
                    foreach (string tempid in clothingid)
                    {
                        if (storablesitem["id"].Value.StartsWith(tempid))
                        {
                            jsonPreset["storables"].Add(storablesitem);
                        }
                    }
                }
               
                foreach (string tempid in clothingid)
                {
                    if (storablesitem["id"].Value.StartsWith(tempid))
                    {
                        jsonClothing["storables"].Add(storablesitem);
                    }
                }

                if (hair)
                {
                    foreach (string tempid in hairid)
                    {
                        if (storablesitem["id"].Value.StartsWith(tempid))
                        {
                            jsonPreset["storables"].Add(storablesitem);
                        }
                    }
                }

                foreach (string tempid in hairid)
                {
                    if (storablesitem["id"].Value.StartsWith(tempid))
                    {
                        jsonHair["storables"].Add(storablesitem);
                    }
                }

                if (skin)
                {
                    if (skinid.Contains(storablesitem["id"].Value))
                    {
                        jsonPreset["storables"].Add(storablesitem);
                    }
                }
               
                if (skinid.Contains(storablesitem["id"].Value))
                {
                    jsonSkin["storables"].Add(storablesitem);
                }
                
                //if (breast)
                //{
                if (breastid.Contains(storablesitem["id"].Value))
                {
                    //  jsonPreset["storables"].Add(storablesitem);
                    jsonBreast["storables"].Add(storablesitem);
                }
                //}
                // if (glute)
                // {
                if (gluteid.Contains(storablesitem["id"].Value))
                {
                    // jsonPreset["storables"].Add(storablesitem);
                    jsonGlute["storables"].Add(storablesitem);
                }
                // }

            }
            
            if (skin) SaveDefaultEyeColorFile();
            if (clothing) SaveClothNakedFile();
            if (hair) SaveHairBaldFile();
            if (morphs) SaveMorphPresetFile(jsonMorphs);
            if (breast) SaveBreastPresetFile(jsonBreast);
            if (glute) SaveGlutePresetFile(jsonGlute);
            //if (clothing) SaveClothingPresetFile(jsonClothing);
            //if (hair) SaveHairPresetFile(jsonHair);
            //if (skin) SaveSkinPresetFile(jsonSkin);
           
            if (clothing|| hair|| skin) SaveAppearancePresetFile(jsonPreset);


        }
        private void SaveDefaultEyeColorFile()
        {
            string eyecolorDefault = "{ \"setUnlistedParamsToDefault\": \"false\", \"storables\": [{  \"id\": \"irises\",  \"hideMaterial\": \"false\",  \"renderQueue\": \"1999\",  \"Specular Texture Offset\": \"0\",  \"Specular Intensity\": \"1\",  \"Gloss\": \"2\",  \"Specular Fresnel\": \"0\",  \"Gloss Texture Offset\": \"0\",  \"Global Illumination Filter\": \"0\",  \"Diffuse Texture Offset\": \"0\",  \"Diffuse Bumpiness\": \"1\",  \"Specular Bumpiness\": \"1\",  \"customTexture1TileX\": \"1\",  \"customTexture1TileY\": \"1\",  \"customTexture1OffsetX\": \"0\",  \"customTexture1OffsetY\": \"0\",  \"customTexture2TileX\": \"1\",  \"customTexture2TileY\": \"1\",  \"customTexture2OffsetX\": \"0\",  \"customTexture2OffsetY\": \"0\",  \"customTexture3TileX\": \"1\",  \"customTexture3TileY\": \"1\",  \"customTexture3OffsetX\": \"0\",  \"customTexture3OffsetY\": \"0\",  \"customTexture4TileX\": \"1\",  \"customTexture4TileY\": \"1\",  \"customTexture4OffsetX\": \"0\",  \"customTexture4OffsetY\": \"0\",  \"Irises\": \"Color 1\",  \"customTexture_MainTex\": \"\",  \"customTexture_SpecTex\": \"\",  \"customTexture_GlossTex\": \"\",  \"customTexture_BumpMap\": \"\",  \"Diffuse Color\": {  \"h\": \"0\",  \"s\": \"0\",  \"v\": \"1\"  },  \"Specular Color\": {  \"h\": \"0\",  \"s\": \"0\",  \"v\": \"1\"  },  \"Subsurface Color\": {  \"h\": \"0\",  \"s\": \"0\",  \"v\": \"1\"  }  }, {  \"id\": \"sclera\",  \"hideMaterial\": \"false\",  \"renderQueue\": \"1999\",  \"Specular Texture Offset\": \"0\",  \"Specular Intensity\": \"0.5\",  \"Gloss\": \"5\",  \"Specular Fresnel\": \"0\",  \"Gloss Texture Offset\": \"0.2\",  \"Global Illumination Filter\": \"0\",  \"Diffuse Texture Offset\": \"0\",  \"Diffuse Bumpiness\": \"0.3\",  \"Specular Bumpiness\": \"0.05\",  \"customTexture1TileX\": \"1\",  \"customTexture1TileY\": \"1\",  \"customTexture1OffsetX\": \"0\",  \"customTexture1OffsetY\": \"0\",  \"customTexture2TileX\": \"1\",  \"customTexture2TileY\": \"1\",  \"customTexture2OffsetX\": \"0\",  \"customTexture2OffsetY\": \"0\",  \"customTexture3TileX\": \"1\",  \"customTexture3TileY\": \"1\",  \"customTexture3OffsetX\": \"0\",  \"customTexture3OffsetY\": \"0\",  \"customTexture4TileX\": \"1\",  \"customTexture4TileY\": \"1\",  \"customTexture4OffsetX\": \"0\",  \"customTexture4OffsetY\": \"0\",  \"Sclera\": \"Sclera 1\",  \"customTexture_MainTex\": \"\",  \"customTexture_SpecTex\": \"\",  \"customTexture_GlossTex\": \"\",  \"customTexture_BumpMap\": \"\",  \"Diffuse Color\": {  \"h\": \"0\",  \"s\": \"0\",  \"v\": \"1\"  },  \"Specular Color\": {  \"h\": \"0.5584416\",  \"s\": \"0.3019608\",  \"v\": \"1\"  },  \"Subsurface Color\": {  \"h\": \"0\",  \"s\": \"0\",  \"v\": \"1\"  }  }, {  \"id\": \"lacrimals\",  \"hideMaterial\": \"false\",  \"renderQueue\": \"1999\",  \"Specular Texture Offset\": \"0\",  \"Specular Intensity\": \"1\",  \"Gloss\": \"6.5\",  \"Specular Fresnel\": \"0.5\",  \"Global Illumination Filter\": \"0\",  \"Diffuse Texture Offset\": \"0\",  \"customTexture1TileX\": \"1\",  \"customTexture1TileY\": \"1\",  \"customTexture1OffsetX\": \"0\",  \"customTexture1OffsetY\": \"0\",  \"customTexture2TileX\": \"1\",  \"customTexture2TileY\": \"1\",  \"customTexture2OffsetX\": \"0\",  \"customTexture2OffsetY\": \"0\",  \"customTexture_MainTex\": \"\",  \"customTexture_SpecTex\": \"\",  \"Diffuse Color\": {  \"h\": \"0\",  \"s\": \"0\",  \"v\": \"1\"  },  \"Specular Color\": {  \"h\": \"0\",  \"s\": \"0\",  \"v\": \"1\"  },  \"Subsurface Color\": {  \"h\": \"0\",  \"s\": \"0\",  \"v\": \"1\"  }  }  ]  }  ";
            string saveName = "Custom\\Atom\\Person\\Appearance\\Preset_eyeDefault.vap";
            string aFileName = Path.Combine(Settings.Default.vampath, saveName);
            Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
            using (FileStream fileStream = File.OpenWrite(aFileName))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(eyecolorDefault);
                sw.Close();
            }
            string type = "looks";
            AddPresetResouce(type, saveName);
        }

        private void AddPresetResouce(string type,string saveName )
        {
            JSONClass jc = new JSONClass();
            jc["type"] = type;
            jc["saveName"] = saveName.Replace('\\', '/');
            if (string.IsNullOrEmpty(CharacterGender))
                CharacterGender = "unknown";
            jc["characterGender"] = CharacterGender.ToLower();
            jc["ignoreGender"] = IgnoreGender.ToString().ToLower();
            jc["personOrder"] = (PersonOrder + 1).ToString();
            saveNames.Add(jc);
        }

        private void SaveClothNakedFile()
        {
            string colthNakedDefault = "{ \"setUnlistedParamsToDefault\" : \"true\", \"storables\" : [ { \"id\" : \"geometry\", \"clothing\" : [ ] } ] }";
            string saveName = "Custom\\Atom\\Person\\Clothing\\Preset_ClothNaked.vap";
            string aFileName = Path.Combine(Settings.Default.vampath, saveName);
            Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
            using (FileStream fileStream = File.OpenWrite(aFileName))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(colthNakedDefault);
                sw.Close();
            }
            AddPresetResouce("clothing", saveName.Replace('\\', '/'));
        }
        private void SaveHairBaldFile()
        {
            string hairBaldDefault = "{ \"setUnlistedParamsToDefault\" : \"true\", \"storables\" : [ { \"id\" : \"geometry\", \"hair\" : [ ] } ] }";
            string saveName = "Custom\\Atom\\Person\\Hair\\Preset_HairBald.vap";
            string aFileName = Path.Combine(Settings.Default.vampath, saveName);
            Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
            using (FileStream fileStream = File.OpenWrite(aFileName))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(hairBaldDefault);
                sw.Close();
            }
            AddPresetResouce("hairstyle", saveName.Replace('\\', '/'));
        }
        private void SaveAppearancePresetFile(JSONClass jsonPreset)
        {
            string strJns = jsonPreset.ToString();
            strJns = strJns.Replace("\"SELF:/", "\"" + varName + ":/");
            string saveName = "Custom\\Atom\\Person\\Appearance\\Preset_temp.vap";
            string aFileName = Path.Combine(Settings.Default.vampath, saveName);
            Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
            using (FileStream fileStream = File.OpenWrite(aFileName))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(strJns);
                sw.Close();
            }
            AddPresetResouce("looks", saveName.Replace('\\', '/'));
        }
        private void SaveMorphPresetFile(JSONClass jsonPreset)
        {
            string strJns = jsonPreset.ToString();
            strJns = strJns.Replace("\"SELF:/", "\"" + varName + ":/");
            string saveName = "Custom\\Atom\\Person\\Morphs\\Preset_temp.vap";
            string aFileName = Path.Combine(Settings.Default.vampath, saveName);
            Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
            using (FileStream fileStream = File.OpenWrite(aFileName))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(strJns);
                sw.Close();
            }
            AddPresetResouce("morphs", saveName.Replace('\\', '/'));
        }
        private void SaveClothingPresetFile(JSONClass jsonPreset)
        {
            string strJns = jsonPreset.ToString();
            strJns = strJns.Replace("\"SELF:/", "\"" + varName + ":/");
            string saveName = "Custom\\Atom\\Person\\Clothing\\Preset_temp.vap";
            string aFileName = Path.Combine(Settings.Default.vampath, saveName);
            Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
            using (FileStream fileStream = File.OpenWrite(aFileName))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(strJns);
                sw.Close();
            }
            AddPresetResouce("clothing", saveName.Replace('\\', '/'));
        }
        private void SaveHairPresetFile(JSONClass jsonPreset)
        {
            string strJns = jsonPreset.ToString();
            strJns = strJns.Replace("\"SELF:/", "\"" + varName + ":/");
            string saveName = "Custom\\Atom\\Person\\Hair\\Preset_temp.vap";
            string aFileName = Path.Combine(Settings.Default.vampath, saveName);
            Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
            using (FileStream fileStream = File.OpenWrite(aFileName))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(strJns);
                sw.Close();
            }
            AddPresetResouce("hairstyle", saveName.Replace('\\', '/'));
        }
        private void SaveSkinPresetFile(JSONClass jsonPreset)
        {
            string strJns = jsonPreset.ToString();
            strJns = strJns.Replace("\"SELF:/", "\"" + varName + ":/");
            string saveName = "Custom\\Atom\\Person\\Skin\\Preset_temp.vap";
            string aFileName = Path.Combine(Settings.Default.vampath, saveName);
            Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
            using (FileStream fileStream = File.OpenWrite(aFileName))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(strJns);
                sw.Close();
            }
            AddPresetResouce("skin", saveName.Replace('\\', '/'));
        }

        private void SaveBreastPresetFile(JSONClass jsonPreset)
        {
            string strJns = jsonPreset.ToString();
            strJns = strJns.Replace("\"SELF:/", "\"" + varName + ":/");
            string saveName = "Custom\\Atom\\Person\\BreastPhysics\\Preset_temp.vap";
            string aFileName = Path.Combine(Settings.Default.vampath, saveName);
            Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
            using (FileStream fileStream = File.OpenWrite(aFileName))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(strJns);
                sw.Close();
            }
            AddPresetResouce("breast", saveName.Replace('\\', '/'));
        }

        private void SaveGlutePresetFile(JSONClass jsonPreset)
        {
            string strJns = jsonPreset.ToString();
            strJns = strJns.Replace("\"SELF:/", "\"" + varName + ":/");
            string saveName = "Custom\\Atom\\Person\\GlutePhysics\\Preset_temp.vap";
            string aFileName = Path.Combine(Settings.Default.vampath, saveName);
            Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
            using (FileStream fileStream = File.OpenWrite(aFileName))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(strJns);
                sw.Close();
            }
            
            AddPresetResouce("glute", saveName.Replace('\\', '/'));
        }
        private void SavePluginPresetFile(JSONClass jsonPreset)
        {
            string strJns = jsonPreset.ToString();
            strJns = strJns.Replace("\"SELF:/", "\"" + varName + ":/");
            string saveName = "Custom\\Atom\\Person\\Plugins\\Preset_temp.vap";
            string aFileName = Path.Combine(Settings.Default.vampath, saveName);
            Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
            using (FileStream fileStream = File.OpenWrite(aFileName))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(strJns);
                sw.Close();
            }

            AddPresetResouce("plugin", saveName.Replace('\\', '/'));
        }
        private void SavePosePresetFile(JSONClass jsonPreset)
        {
            string strJns = jsonPreset.ToString();
            strJns = strJns.Replace("\"SELF:/", "\"" + varName + ":/");
            string saveName = "Custom\\Atom\\Person\\Pose\\Preset_temp.vap";
            string aFileName = Path.Combine(Settings.Default.vampath, saveName);
            Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
            using (FileStream fileStream = File.OpenWrite(aFileName))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(strJns);
                sw.Close();
            }
            AddPresetResouce("pose", saveName.Replace('\\', '/'));
        }
        private void SaveAnimationPresetFile(JSONClass jsonPreset)
        {
            string saveName = "Custom\\Atom\\Person\\AnimationPresets\\Preset_temp.bin";
            string aFileName = Path.Combine(Settings.Default.vampath, saveName);
            Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
            jsonPreset.SaveToFile(aFileName);
            AddPresetResouce("animation", saveName.Replace('\\', '/'));
        }
        private void FormAnalysis_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }

        private void buttonLoadScene_Click(object sender, EventArgs e)
        {
            /*
            string sceneFoldername = Path.Combine(Directory.GetCurrentDirectory(), "Cache",
                           Comm.ValidFileName(varName), Comm.ValidFileName(entryName.Replace('\\', '_').Replace('/', '_')));
           
            JSONClass jsonScene = (JSONClass)JSONNode.LoadFromFile(Path.Combine(sceneFoldername, "posinfo.bin"));
            JSONArray jsonAtoms=new JSONArray();

            foreach (TreeNode tn in atomnodes)
            {
                string atomtype = tn.Parent.Text;
                if (atomtype.StartsWith("(base)")) atomtype = atomtype.Substring(6);
                if (sceneBaseAtoms.Contains(atomtype))
                {
                    if (tn.Nodes.Count == 0)
                    {
                        string path = tn.FullPath;
                        path = path.Substring(path.IndexOf('\\') + 1);
                        path = Path.Combine(sceneFoldername, "atoms", path);
                        jsonAtoms.Add(JSONNode.LoadFromFile(path));
                    }

                    tn.Checked = true;
                }
                   
            }
            //CheckedTreeViewNodes(triStateTreeViewAtoms.Nodes, jsonAtoms, sceneFoldername);

            jsonScene.Add("atoms", jsonAtoms);
            string saveName = "Saves\\scene\\loadscene_temp.json";
            string aFileName = Path.Combine(Settings.Default.vampath, saveName);
            //jsonScene.SaveToFile(aFileName);
            using(StreamWriter sw = new StreamWriter(aFileName, false))
            {
                sw.Write(jsonScene.ToString("\t"));
            }
            */
            foreach (TreeNode tn in atomnodes)
            {
                string atomtype = tn.Parent.Text;
                if (atomtype.StartsWith("(base)")) atomtype = atomtype.Substring(6);
                if (sceneBaseAtoms.Contains(atomtype))
                {
                    tn.Checked = true;
                }
            }
            saveNames = new List<JSONClass>();
            AddPresetResouce("emptyscene", "");
            //AddPresetResouce("scenes", saveName.Replace('\\', '/'));
            //GenLoadscenetxt();
            //Thread.Sleep(2000);
            //saveNames = new List<JSONClass>();
            AddToScene();
            labelMessage.Text = "Load Scene completed!";
            labelMessage.Visible = true;
            timer1.Enabled = true;
        }

        public void GenLoadscenetxt()
        {
            string sceneFoldername = Path.Combine(Directory.GetCurrentDirectory(), "Cache",
                           Comm.ValidFileName(varName), Comm.ValidFileName(entryName.Replace('\\', '_').Replace('/', '_')));
            string dependfFilename = Path.Combine(sceneFoldername, "depend.txt");
            List<string> depends = new List<string>();
            using (StreamReader sr = new StreamReader(dependfFilename))
            {
                string depend;
                while ((depend = sr.ReadLine()) != null)
                {
                    depends.Add(depend);
                }
            }

            JSONClass jsonls =new JSONClass();
            jsonls["resources"] = new JSONArray();
            foreach (JSONClass jc in saveNames)
            {
                jsonls["resources"].Add(jc);
            }
            
            form1.GenLoadscenetxt(jsonls, false, depends);
        }

        public void CheckedTreeViewNodes(TreeNodeCollection node, JSONArray jsonAtoms,string sceneFoldername)
        {
            foreach (TreeNode n in node)
            {
                if (n.Checked)
                {
                    if (n.Nodes.Count==0)
                    {
                        string path = n.FullPath;
                        path=path.Substring(path.IndexOf('\\') + 1);
                        path=Path.Combine(sceneFoldername,"atoms", path);
                        jsonAtoms.Add(JSONNode.LoadFromFile(path));
                    }
                }
                CheckedTreeViewNodes(n.Nodes,jsonAtoms, sceneFoldername);
            }
        }
        public void CheckedTreeViewNodes(TreeNodeCollection node, string sceneFoldername,bool atomSubscene=false)
        {
            string pathPlugindata = Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar");
            
            Directory.CreateDirectory(pathPlugindata);
            foreach (TreeNode n in node)
            {
                if (n.Checked)
                {
                    if (n.Nodes.Count == 0)
                    {
                        string path = n.FullPath;
                        path = path.Substring(path.IndexOf('\\') + 1);
                        path = Path.Combine(sceneFoldername, "atoms", path);
                        
                        string pathPlugindataAtom = Path.Combine(pathPlugindata, n.Text);
                        File.Copy(path, pathPlugindataAtom, true);
                        string type = atomSubscene ? "atomSubscene" : "atom";
                        string saveName = pathPlugindataAtom.Replace('\\', '/');
                        AddPresetResouce(type, saveName);
                    }
                }
                CheckedTreeViewNodes(n.Nodes, sceneFoldername, atomSubscene);
            }
        }
        private void buttonClearCache_Click(object sender, EventArgs e)
        {
        }

        private void buttonLoadPlugin_Click(object sender, EventArgs e)
        {
            if (listBoxAtom.SelectedIndex >= 0 && listBoxAtom.SelectedIndex < listJsonPerson.Count)
            {
                GetPersonOrder();
                saveNames = new List<JSONClass>();
                SavePluginPreset(listJsonPerson[listBoxAtom.SelectedIndex]);
                labelMessage.Text = "Load Plugin completed!";
                labelMessage.Visible = true;
                timer1.Enabled = true;
                GenLoadscenetxt();
            }
            else
            {
                MessageBox.Show("Please select a character");
                listBoxAtom.Select();
                return;
            }
        } 
        private void buttonLoadPose_Click(object sender, EventArgs e)
        {
            if (listBoxAtom.SelectedIndex >= 0 && listBoxAtom.SelectedIndex < listJsonPerson.Count)
            {
                GetPersonOrder();
                saveNames = new List<JSONClass>();
                SavePosePreset(listJsonPerson[listBoxAtom.SelectedIndex]);
                labelMessage.Text = "Load Pose completed!";
                labelMessage.Visible = true;
                timer1.Enabled = true;
                GenLoadscenetxt();
            }
            else
            {
                MessageBox.Show("Please select a character");
                listBoxAtom.Select();
                return;
            }
        }
        private void buttonLoadAnimation_Click(object sender, EventArgs e)
        {
            if (listBoxAtom.SelectedIndex >= 0 && listBoxAtom.SelectedIndex < listJsonPerson.Count)
            {
                GetPersonOrder();
                saveNames = new List<JSONClass>();
                SavePosePreset(listJsonPerson[listBoxAtom.SelectedIndex]);
                SaveAnimationPreset(listJsonPerson[listBoxAtom.SelectedIndex]);

                labelMessage.Text = "Load Animation completed!";
                labelMessage.Visible = true;
                timer1.Enabled = true;
                GenLoadscenetxt();
            }
            else
            {
                MessageBox.Show("Please select a character");
                listBoxAtom.Select();
                return;
            }

        }

        private void triStateTreeViewAtoms_AfterSelect(object sender, TreeViewEventArgs e)
        {
           
        }

        private void triStateTreeViewAtoms_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                if (parentAtoms.ContainsKey(e.Node.Text))
                {
                    foreach (string child in parentAtoms[e.Node.Text])
                    {
                        var tn = atomnodes.Where(x => x.Text == child).First();
                        if (!tn.Checked)
                            tn.Checked = true;
                    }

                }
            }

        }

        private void buttonAddToScene_Click(object sender, EventArgs e)
        {
            saveNames = new List<JSONClass>();
            AddToScene();
        }

        private void AddToScene(bool asSubScene=false)
        {
            string sceneFoldername = Path.Combine(Directory.GetCurrentDirectory(), "Cache",
                           Comm.ValidFileName(varName), Comm.ValidFileName(entryName.Replace('\\', '_').Replace('/', '_')));
            
            string pathPlugindata = Path.Combine(Settings.Default.vampath, "Custom\\PluginData\\feelfar");
            Directory.Delete(pathPlugindata, true);
            CheckedTreeViewNodes(triStateTreeViewAtoms.Nodes, sceneFoldername, asSubScene);
            labelMessage.Text = "Add Selected Atoms to Scene completed!";
            labelMessage.Visible = true;
            timer1.Enabled = true;
            GenLoadscenetxt();
        }

        private void buttonAddAsSubscene_Click(object sender, EventArgs e)
        {
            saveNames = new List<JSONClass>();
            AddToScene(true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelMessage.Visible = false;
            timer1.Enabled = false;
        }
    }
    
}
