using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleJSON;
using varManager.Properties;

namespace varManager
{
    public partial class FormAnalysis : Form
    {
        private string varName;
        private string sceneName;
        private string jsonscene;
        private List<JSONNode> listJsonAtom;
        
        private int personOrder = 0;
        private bool futaAsFemale=false;
        private string characterGender;
        public string VarName { get => varName; set => varName = value; }
        public string SceneName { get => sceneName; set => sceneName = value; }
        public string Jsonscene { get => jsonscene; set => jsonscene = value; }
        public bool FutaAsFemale { get => futaAsFemale; set => futaAsFemale = value; }
        public int PersonOrder { get => personOrder; set => personOrder = value; }
        public string CharacterGender { get => characterGender; set => characterGender = value; }

        public List<JSONClass> saveNames;

        public FormAnalysis()
        {
            InitializeComponent();
            listJsonAtom = new List<JSONNode>();
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
            labelSceneName.Text = SceneName;
            AnalsisJson();
            if(listBoxAtom.Items.Count > 0)
            {
                listBoxAtom.SelectedIndex = 0;
            }
            PersonOrder = 0;
            listBoxPerson.SelectedIndex = 0;
        }

        private void AnalsisJson()
        {
            JSONNode jsonnode = JSON.Parse(jsonscene);
            JSONArray atomArray = jsonnode["atoms"].AsArray;
            if (atomArray.Count > 0)
            {
                foreach (JSONNode atomitem in atomArray)
                {
                    string atomtype = atomitem["type"];
                    if (atomtype == "Person")
                    {
                        listJsonAtom.Add(atomitem);
                    }
                }
            }
            else
            {
                listJsonAtom.Add(jsonnode);
            }
            foreach(JSONNode atomitem in listJsonAtom)
            {
                listBoxAtom.Items.Add(GetAtomID(atomitem));
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
                    checkBoxFutaAsFemale.Enabled = true;
                }
                else
                {
                    checkBoxGlute.Visible = false;
                    checkBoxBreast.Visible = false;
                    if (listBoxAtom.SelectedItem.ToString().Contains("(Futa)"))
                        checkBoxFutaAsFemale.Enabled = true;
                    else
                        checkBoxFutaAsFemale.Enabled = false;
                }
            }
            else
            {
                buttonLoadLook.Enabled= false;
            }
        }

        private void buttonLoadLook_Click(object sender, EventArgs e)
        {
            if (listBoxAtom.SelectedIndex >= 0 && listBoxAtom.SelectedIndex < listJsonAtom.Count)
            {
                if (checkBoxMorphs.Checked || checkBoxHair.Checked ||
                    checkBoxClothing.Checked || checkBoxSkin.Checked ||
                    checkBoxBreast.Checked || checkBoxGlute.Checked)
                {
                    JSONClass atomitem = (JSONClass)listJsonAtom[listBoxAtom.SelectedIndex];
                    SavePreset(atomitem, checkBoxMorphs.Checked, checkBoxHair.Checked,
                        checkBoxClothing.Checked, checkBoxSkin.Checked,
                        checkBoxBreast.Checked, checkBoxGlute.Checked);
                    PersonOrder = 0;
                    if (listBoxPerson.SelectedIndex > 0)
                        PersonOrder = listBoxPerson.SelectedIndex;
                    FutaAsFemale = false;
                    if (checkBoxFutaAsFemale.Enabled && checkBoxFutaAsFemale.Checked)
                        FutaAsFemale = true;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
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
                    if (clothing) jsonPresetGeometry.Add("clothing",storablesitem["clothing"]);
                    jsonClothingGeometry.Add("clothing", storablesitem["clothing"]);
                    if (hair) jsonPresetGeometry.Add("hair",storablesitem["hair"]);
                    jsonHairGeometry.Add("hair", storablesitem["hair"]);
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
            saveNames = new List<JSONClass>();
            if (skin) SaveDefaultEyeColorFile();

            if (morphs) SaveMorphPresetFile(jsonMorphs);
            if (breast) SaveBreastPresetFile(jsonBreast);
            if (glute) SaveGlutePresetFile(jsonGlute);
            //if (clothing) SaveClothingPresetFile(jsonClothing);
            //if (hair) SaveHairPresetFile(jsonHair);
            //if (skin) SaveSkinPresetFile(jsonSkin);
            SaveAppearancePresetFile(jsonPreset);


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
            JSONClass jc = new JSONClass();
            jc["type"] = "looks";
            jc["saveName"] = saveName.Replace('\\', '/'); ;
            saveNames.Add(jc);
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
            JSONClass jc = new JSONClass();
            jc["type"] = "looks";
            jc["saveName"] = saveName.Replace('\\', '/'); ;
            saveNames.Add(jc);
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
            JSONClass jc = new JSONClass();
            jc["type"] = "morphs";
            jc["saveName"] = saveName.Replace('\\', '/');
            saveNames.Add(jc);
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
            JSONClass jc = new JSONClass();
            jc["type"] = "clothing";
            jc["saveName"] = saveName.Replace('\\', '/');
            saveNames.Add(jc);
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
            JSONClass jc = new JSONClass();
            jc["type"] = "hairstyle";
            jc["saveName"] = saveName.Replace('\\', '/');
            saveNames.Add(jc);
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
            JSONClass jc = new JSONClass();
            jc["type"] = "skin";
            jc["saveName"] = saveName.Replace('\\', '/');
            saveNames.Add(jc);
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
            JSONClass jc = new JSONClass();
            jc["type"] = "breast";
            jc["saveName"] = saveName.Replace('\\', '/');
            saveNames.Add(jc);
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
            JSONClass jc = new JSONClass();
            jc["type"] = "glute";
            jc["saveName"] = saveName.Replace('\\', '/');
            saveNames.Add(jc);
        }
        private void FormAnalysis_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
