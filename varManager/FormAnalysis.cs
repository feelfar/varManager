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
        private static string varName;
        private string sceneName;
        private string jsonscene;
        private List<JSONNode> listJsonAtom;
        public FormAnalysis()
        {
            InitializeComponent();
            listJsonAtom = new List<JSONNode>();
        }
        public string VarName { get => varName; set => varName = value; }
        public string SceneName { get => sceneName; set => sceneName = value; }
        public string Jsonscene { get => jsonscene; set => jsonscene = value; }
        public int personOrder = 0;
        public bool futaAsFemale=false;
        public static string CharacterGender(string character)
        {
            string isMale = "Female";
            character= character.ToLower();
            // If the peson atom is not "On", then we cant determine their gender it seems as GetComponentInChildren<DAZCharacter> just returns null
            if (character.StartsWith("male") ||
                    character.StartsWith("jarlee") ||
                    character.StartsWith("lee") ||
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
            personOrder = 0;
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
            string characterGender = "unknown";
            JSONArray storablesArray = atomitem["storables"].AsArray;
            foreach (JSONNode storablesitem in storablesArray)
            {
                if (storablesitem["id"].Value == "geometry")
                {
                    characterGender = CharacterGender(storablesitem["character"].Value);
                    break;
                }
            }
            return string.Format("{0}({1})", atomitem["id"], characterGender);
            
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
                JSONClass atomitem = (JSONClass)listJsonAtom[listBoxAtom.SelectedIndex];
                SavePreset(atomitem, checkBoxMorphs.Checked, checkBoxHair.Checked,
                    checkBoxClothing.Checked, checkBoxSkin.Checked, 
                    checkBoxBreast.Checked, checkBoxGlute.Checked);
                personOrder = 0;
                if (listBoxPerson.SelectedIndex > 0)
                    personOrder = listBoxPerson.SelectedIndex;
                futaAsFemale = false;
                if (checkBoxFutaAsFemale.Enabled && checkBoxFutaAsFemale.Checked)
                    futaAsFemale = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private static void SavePreset(JSONClass atomitem,
            bool morphs = false,bool hair = false,  
            bool clothing = false, bool skin = false,
            bool breast = false, bool glute = false)
        {
            JSONClass jns = new JSONClass();

            jns.Add("setUnlistedParamsToDefault", "false");
           // jns.Add("storables", atomitem["storables"]);
            
            jns.Add("storables", new JSONArray());
            JSONArray storablesArray = atomitem["storables"].AsArray;
            string[] skinid = { "skin", "textures", "teeth", "tongue", "mouth" };
            string[] breastid = { "BreastControl", "BreastPhysicsMesh" };
            string[] gluteid = { "GluteControl", "LowerPhysicsMesh" };
            List<string> hairid=new List<string>();
            List<string> clothingid=new List<string>();
            foreach (JSONClass storablesitem in storablesArray)
            {
                if (storablesitem["id"].Value == "geometry")
                {
                    JSONArray jnsstorablesArray = jns["storables"].AsArray;
                    //jnsstorablesArray.Add(storablesitem);
                    jnsstorablesArray.Add(new JSONClass());
                    JSONClass jsongeometry = (JSONClass)jnsstorablesArray[jnsstorablesArray.Count - 1];
                    jsongeometry.Add("id", "geometry");
                    if( storablesitem.HasKey("useFemaleMorphsOnMale"))
                        jsongeometry.Add("useFemaleMorphsOnMale", storablesitem["useFemaleMorphsOnMale"].Value);
                    if (skin) jsongeometry.Add("character", storablesitem["character"].Value);
                    if (clothing) jsongeometry.Add("clothing",storablesitem["clothing"]);
                    if (hair) jsongeometry.Add("hair",storablesitem["hair"]);
                    if (morphs) jsongeometry.Add("morphs",storablesitem["morphs"]);
                    //if (!clothing) jsongeometry.Remove("clothing");
                    //if (!hair) jsongeometry.Remove("hair");
                    //if (!morphs) jsongeometry.Remove("morphs");
                    if (jsongeometry.HasKey("clothing"))
                    {
                        JSONArray temparray = jsongeometry["clothing"].AsArray;
                        foreach (JSONClass tempid in temparray)
                        {
                            clothingid.Add(tempid["internalId"].Value);
                        }
                    }
                    if (jsongeometry.HasKey("hair"))
                    {
                        JSONArray temparray = jsongeometry["hair"].AsArray;
                        foreach(JSONClass tempid in temparray)
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
                    foreach(string tempid in clothingid)
                    {
                        if (storablesitem["id"].Value.StartsWith(tempid))
                        {
                            jns["storables"].Add(storablesitem);
                        }
                    }
                }
                if (hair)
                {
                    foreach (string tempid in hairid)
                    {
                        if (storablesitem["id"].Value.StartsWith(tempid))
                        {
                            jns["storables"].Add(storablesitem);
                        }
                    }
                }
               
                if (skin)
                {
                    if (skinid.Contains(storablesitem["id"].Value))
                    {
                        jns["storables"].Add(storablesitem);
                    }
                }
                if (breast)
                {
                    if (breastid.Contains(storablesitem["id"].Value))
                    {
                        jns["storables"].Add(storablesitem);
                    }
                }
                if (glute)
                {
                    if (gluteid.Contains(storablesitem["id"].Value))
                    {
                        jns["storables"].Add(storablesitem);
                    }
                }
            }


            SavePresetFile(jns);
        }

        private static void SavePresetFile(JSONClass jns)
        {
            string strJns = jns.ToString();
            strJns = strJns.Replace("\"SELF:/", "\"" + varName + ":/");
            string aFileName = Path.Combine(Settings.Default.vampath, "Custom\\Atom\\Person\\Appearance\\Preset_temp.vap");
            Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
            using (FileStream fileStream = File.OpenWrite(aFileName))
            {
                fileStream.SetLength(0);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(strJns);
                sw.Close();
            }
        }

        private void FormAnalysis_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
