using MMDLoader.Properties;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MMDLoader.UserCtls
{

    //路由事件关联的状态信息和事件数据
    class JsonResouceMessages : RoutedEventArgs
    {
        public JsonResouceMessages(RoutedEvent routedEvent, object source) : base(routedEvent, source) { }
        public JSONClass? jsonResouce { get; set; }
    }

    /// <summary>
    /// UserCtlPersonVMD.xaml 的交互逻辑
    /// </summary>
    public partial class UserCtlPersonVMD : UserControl
    {
        //Dictionary<string, string> dictPerson1Vmd = new Dictionary<string, string>();
        //Dictionary<string, string> dictPerson1Vmd2 = new Dictionary<string, string>();
        public List<string> Vmds
        {
            get { return (List<string>)GetValue(VmdsProperty); }
            set 
            {
                SetValue(VmdsProperty, value);
                //dictPerson1Vmd.Clear();
                //dictPerson1Vmd2.Clear();

                comboBoxPersonVmd.Items.Clear();
                comboBoxPersonVmd.Items.Add("None");

                comboBoxPersonVmd2.Items.Clear();
                comboBoxPersonVmd2.Items.Add("None");
                string personvmd = "";
                if(value!=null)
                foreach (string vmd in value)
                {
                    //string name = System.IO.Path.GetFileName(vmd);
                    //dictPerson1Vmd[name] = vmd;
                    //dictPerson1Vmd2[name] = vmd;

                    comboBoxPersonVmd.Items.Add(vmd);
                    comboBoxPersonVmd2.Items.Add(vmd);

                    if (string.IsNullOrEmpty(personvmd))
                    {
                        bool foundMatch = false;
                        try
                        {
                            foundMatch = Regex.IsMatch(vmd, @"\bcam\b|\bcamera\b|镜头|カメラ|카메라", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                            if (!foundMatch) personvmd = vmd;
                        }
                        catch (ArgumentException ex)
                        {
                            // Syntax error in the regular expression
                        }
                    }
                }
                if (!string.IsNullOrEmpty(personvmd))
                {
                    comboBoxPersonVmd.SelectedItem = personvmd;
                }
                else
                {
                    comboBoxPersonVmd.SelectedIndex = 0;
                }
                comboBoxPersonVmd2.SelectedIndex = 0;
            }
        }

        // Using a DependencyProperty as the backing store for Vmds.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VmdsProperty =
            DependencyProperty.Register("Vmds", typeof(List<string>), typeof(UserCtlPersonVMD), new FrameworkPropertyMetadata { BindsTwoWayByDefault = true });



        public int Motion1
        {
            get { return (int)GetValue(Motion1Property); }
            set { SetValue(Motion1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Motion1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Motion1Property =
            DependencyProperty.Register("Motion1", typeof(int), typeof(UserCtlPersonVMD), new PropertyMetadata(0));

        public int Motion2
        {
            get { return (int)GetValue(Motion2Property); }
            set { SetValue(Motion2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Motion1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Motion2Property =
            DependencyProperty.Register("Motion2", typeof(int), typeof(UserCtlPersonVMD), new PropertyMetadata(0));


        public int PersonOrder
        {
            get { return (int)GetValue(PersonOrderProperty); }
            set { SetValue(PersonOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PersonOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PersonOrderProperty =
            DependencyProperty.Register("PersonOrder", typeof(int), typeof(UserCtlPersonVMD), new PropertyMetadata(1));



        public double StraightLeg
        {
            get { return (double)GetValue(StraightLegProperty); }
            set { SetValue(StraightLegProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StraightLeg.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StraightLegProperty =
            DependencyProperty.Register("StraightLeg", typeof(double), typeof(UserCtlPersonVMD), new PropertyMetadata((double)0));



        public double StraightLegWorkAngle
        {
            get { return (double)GetValue(StraightLegWorkAngleProperty); }
            set { SetValue(StraightLegWorkAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StraightLegWorkAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StraightLegWorkAngleProperty =
            DependencyProperty.Register("StraightLegWorkAngle", typeof(double), typeof(UserCtlPersonVMD), new PropertyMetadata((double)140));


        public double PosX
        {
            get { return (double)GetValue(PosXProperty); }
            set { SetValue(PosXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PosX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PosXProperty =
            DependencyProperty.Register("PosX", typeof(double), typeof(UserCtlPersonVMD), new PropertyMetadata((double)0));



        public double PosY
        {
            get { return (double)GetValue(PosYProperty); }
            set { SetValue(PosYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PosY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PosYProperty =
            DependencyProperty.Register("PosY", typeof(double), typeof(UserCtlPersonVMD), new PropertyMetadata((double)0));


        public double PosZ
        {
            get { return (double)GetValue(PosZProperty); }
            set { SetValue(PosZProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PosZ.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PosZProperty =
            DependencyProperty.Register("PosZ", typeof(double), typeof(UserCtlPersonVMD), new PropertyMetadata((double)0));



        public bool IgnoreFace
        {
            get { return (bool)GetValue(IgnoreFaceProperty); }
            set { SetValue(IgnoreFaceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IgnoreFace.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IgnoreFaceProperty =
            DependencyProperty.Register("IgnoreFace", typeof(bool), typeof(UserCtlPersonVMD), new PropertyMetadata(false));



        public bool EnableHighHeel
        {
            get { return (bool)GetValue(EnableHighHeelProperty); }
            set { SetValue(EnableHighHeelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnableHighHeel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableHighHeelProperty =
            DependencyProperty.Register("EnableHighHeel", typeof(bool), typeof(UserCtlPersonVMD), new PropertyMetadata(false));




        public double HighHeelFootXangle
        {
            get { return (double)GetValue(HighHeelFootXangleProperty); }
            set { SetValue(HighHeelFootXangleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HighHeelFootXangle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HighHeelFootXangleProperty =
            DependencyProperty.Register("HighHeelFootXangle", typeof(double), typeof(UserCtlPersonVMD), new PropertyMetadata((double)-45));




        public double HighHeelToeXangle
        {
            get { return (double)GetValue(HighHeelToeXangleProperty); }
            set { SetValue(HighHeelToeXangleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HighHeelToeXangle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HighHeelToeXangleProperty =
            DependencyProperty.Register("HighHeelToeXangle", typeof(double), typeof(UserCtlPersonVMD), new PropertyMetadata((double)35));




        public double FootHoldRotMaxForce
        {
            get { return (double)GetValue(FootHoldRotMaxForceProperty); }
            set { SetValue(FootHoldRotMaxForceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FootHoldRotMaxForce.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FootHoldRotMaxForceProperty =
            DependencyProperty.Register("FootHoldRotMaxForce", typeof(double), typeof(UserCtlPersonVMD), new PropertyMetadata((double)0));

        //第一步：声明并注册路由事件，与消息 Messages 关联。
        //EventManager.RegisterRoutedEvent(CLR事件包装器名称,路由事件冒泡策略,事件处理程序的类型,路由事件的所有者类类型)
        public static readonly RoutedEvent JsonGeneratedEvent = EventManager.RegisterRoutedEvent
            ("JsonGenerated", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserCtlPersonVMD));


        //第二步：为路由事件添加CLR事件包装器
        public event RoutedEventHandler JsonGenerated
        {
            add { this.AddHandler(JsonGeneratedEvent, value); }
            remove { this.RemoveHandler(JsonGeneratedEvent, value); }
        }



        public UserCtlPersonVMD()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void sliderFootXAngle_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (rectFoot != null)
            {
                DrawFoot();
            }
        }

        private void DrawFoot()
        {
            if (checkBoxEnableHighHeel.IsChecked??false)
            {
                imageHighheel.Source = new BitmapImage(new Uri(@"../Resources/ImageHighHeel.png", UriKind.Relative));
                RotateTransform rotateTransformFoot =
                        new RotateTransform(sliderFootXAngle.Value, rectFoot.Width * 0.45, 0);
                rectFoot.RenderTransform = rotateTransformFoot;
                RotateTransform rotateTransformToe1 =
                       new RotateTransform(sliderFootXAngle.Value, rectFoot.Width + 9, rectToe.Height / 2);
                RotateTransform rotateTransformToe2 =
                       new RotateTransform(sliderToeXAngle.Value, 20, rectToe.Height / 2);
                TransformGroup rotateGroup = new TransformGroup();

                rotateGroup.Children.Add(rotateTransformToe2);
                rotateGroup.Children.Add(rotateTransformToe1);
                rectToe.RenderTransform = rotateGroup;
                double angle = Math.PI * sliderFootXAngle.Value / 180.0;
                double height = (-Math.Sin(angle) * (rectFoot.Width) - 5) + (Math.Cos(angle) * rectFoot.Height / 2);
                if (height < 0) height = 0;
                rectHeel.Height = height;
            }
            else
            {
                imageHighheel.Source = new BitmapImage(new Uri(@"../Resources/ImageFlats.png", UriKind.Relative));
                rectFoot.RenderTransform = null;
                rectToe.RenderTransform = null;
                rectHeel.Height = 9;
            }
        }

        private void checkBoxEnableHighHeel_Checked(object sender, RoutedEventArgs e)
        {
            if (rectFoot != null)
            {
                DrawFoot();
            }
        }

        private void sliderToeXAngle_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (rectFoot != null)
            {
                DrawFoot();
            }
        }

        private void buttonHighHeelDefault_Click(object sender, RoutedEventArgs e)
        {
            sliderPosX.Value = 0;
            sliderPosY.Value = 0;
            sliderPosZ.Value = 0;
            sliderFootHoldForce.Value = 0;
            sliderFootXAngle.Value = -45;
            sliderToeXAngle.Value = 35;
        }
       
        private void buttonTestHighHeel_Click(object sender, RoutedEventArgs e)
        {
            JSONClass jsonResouce = GetJsonResouce("highheel");
            JsonResouceMessages jsonResouceMessages=new JsonResouceMessages(JsonGeneratedEvent, this);
            jsonResouceMessages.jsonResouce = jsonResouce;
            this.RaiseEvent(jsonResouceMessages);
        }
        private JSONNode JsonResouce(string type,  int personOrder)
        {
            JSONClass jc = new JSONClass();
            jc["type"] = type;
            jc["characterGender"] = "female";
            jc["ignoreGender"] = "true";
            jc["personOrder"] = personOrder.ToString();
            return (JSONNode)jc;
        }

        private JSONClass GetJsonResouce(string type)
        {
            JSONClass jsonResouce = (JSONClass)JsonResouce(type,PersonOrder);
            jsonResouce.Add("ignoreFace", new JSONData(checkBoxIgnoreFace.IsChecked ?? false));
            jsonResouce.Add("enableHeel", new JSONData(checkBoxEnableHighHeel.IsChecked ?? false));
            jsonResouce.Add("footJointDriveXTargetAdjust", new JSONData(sliderFootXAngle.Value));
            jsonResouce.Add("toeJointDriveXTargetAdjust", new JSONData(sliderToeXAngle.Value));
            jsonResouce.Add("holdRotationMaxForceAdjust", new JSONData(sliderFootHoldForce.Value));
            double straightLeg = sliderStraightLeg.Value;
            jsonResouce.Add("straightLeg", new JSONData(straightLeg));
            double straightLegWorkAngle = sliderStraightLegWorkAngle.Value;
            jsonResouce.Add("straightLegWorkAngle", new JSONData(straightLegWorkAngle));
            double posY = sliderPosY.Value;
            if (checkBoxEnableHighHeel.IsChecked ?? false)
            {
                double angle = Math.PI * sliderFootXAngle.Value / 180.0;
                posY += (-Math.Sin(angle)) * 0.07;
            }
            
            jsonResouce.Add("posY", new JSONData(posY));
            jsonResouce.Add("posX", new JSONData(sliderPosX.Value));
            jsonResouce.Add("posZ", new JSONData(sliderPosZ.Value));
            return jsonResouce;
        }

        private void checkBoxEnableHighHeel_Unchecked(object sender, RoutedEventArgs e)
        {

            if (rectFoot != null)
            {
                DrawFoot();
            }
        }

        private void buttonVmdTest_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxPersonVmd.SelectedIndex > 0)
            {
                JSONClass jsonResouce = GetJsonResouce("personvmd");
                jsonResouce.Add("isTest", new JSONData(true));
                JsonResouceMessages jsonResouceMessages = new JsonResouceMessages(JsonGeneratedEvent, this);
                jsonResouceMessages.jsonResouce = jsonResouce;
                this.RaiseEvent(jsonResouceMessages);
            }
        }
        public JSONClass GetPersonvmdJson()
        {
            JSONClass jsonResouce = GetJsonResouce("personvmd");
            jsonResouce.Add("isTest", new JSONData(false));
            return jsonResouce;
        }

        private void comboBoxPersonVmd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            comboBoxPersonVmd2.IsEnabled = (comboBoxPersonVmd.SelectedIndex > 0);
           
        }
    }
}
