using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace Ascentium.Research.Windows.Forms.Components
{
    /// <summary>
    /// ThreeStateTreeNode inherits from <see cref="http://msdn2.microsoft.com/en-us/library/sc9ba94b(vs.80).aspx">TreeView</see>
    /// and adds the ability to cascade state changes to related nodes, i.e. child nodes and or parent nodes, as well as to optionally
    /// use the three state functionality.
    /// </summary>
    public class ThreeStateTreeView : TreeView
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the ThreeStateTreeView class in addition to intializing
        /// the base class (<see cref="http://msdn2.microsoft.com/en-us/library/system.windows.forms.treeview.treeview(VS.80).aspx">TreeView Constructor</see>). 
        /// </summary>
        public ThreeStateTreeView() : base()
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Flag. If true, use three state checkboxes, otherwise, use the default behavior of the TreeView and associated TreeNodes.
        /// </summary>
        private bool mUseThreeStateCheckBoxes = true;
        [Category("Three State TreeView"), 
        Description("Flag. If true, use three state checkboxes, otherwise, use the default behavior of the TreeView and associated TreeNodes."), 
        DefaultValue(true), 
        TypeConverter(typeof(bool)), 
        Editor("System.Boolean", typeof(bool))]
        public bool UseThreeStateCheckBoxes
        {
            get { return this.mUseThreeStateCheckBoxes; }
            set { this.mUseThreeStateCheckBoxes = value; }
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Raises the AfterCheck event.
        /// </summary>
        /// <param name="e">A <see cref="http://msdn2.microsoft.com/en-us/library/system.windows.forms.treevieweventargs.aspx">TreeViewEventArgs</see> containing the event data.</param>
        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            base.OnAfterCheck(e);

            if (this.UseThreeStateCheckBoxes)
            {
                switch (e.Action)
                {
                    case TreeViewAction.ByKeyboard:
                    case TreeViewAction.ByMouse:
                        {
                            if (e.Node is ThreeStateTreeNode)
                            {
                                // Toggle to the next state.
                                ThreeStateTreeNode tn = e.Node as ThreeStateTreeNode;
                                tn.Toggle();
                            }

                            break;
                        }
                    case TreeViewAction.Collapse:
                    case TreeViewAction.Expand:
                    case TreeViewAction.Unknown:
                    default:
                        {
                            // Do nothing.
                            break;
                        }
                }
            }
        }
        #endregion
    }
}
