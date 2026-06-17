using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DgvFilterPopup;

/// <summary>
///     The base class from which to derive effective <i>filter host</i> controls
/// </summary>
/// <remarks>
///     The purpose of the <i>filter host</i> control is to show a popup near a right-clicked column and to
///     host child <i>column filter</i> controls.
///     When the popup is shown, only the <i>column filter</i> control related to right-clicked column
///     is visibile.
///     <b>DgvBaseFilterHost</b> is a derivation of <b>UserControl</b> and provide functionalities to
///     cooperate with <see cref="DgvFilterManager" />.
///     <para>
///         NOTE:
///         This class must be intended as an abstract class. However, declaring it as abstract,
///         would generate errors whitin the designer when designing derived classes.
///     </para>
///     <para>
///         In your derivation, you have to provide a host area (such as a panel) and ovverride the
///         <see cref="DgvBaseFilterHost.FilterClientArea" /> to return it. Also, create visual elements
///         for <i>remove filter</i>, <i>remove all filters</i>, <i>apply filter</i> and use the
///         <b>DgvFilterManager</b> methods <see cref="DgvFilterManager.ActivateFilter(bool)" /> and
///         <see cref="DgvFilterManager.ActivateAllFilters " />.
///     </para>
/// </remarks>
/// <example>
///     <code>
/// public partial class DgvFilterHost : DgvBaseFilterHost {
/// 
///     public DgvFilterHost() {
///         InitializeComponent();
///         this.CurrentColumnFilterChanged += new EventHandler(DgvFilterHost_CurrentColumnFilterChanged);
///     }
///  
///     void DgvFilterHost_CurrentColumnFilterChanged(object sender, EventArgs e) {
///         lblColumnName.Text = CurrentColumnFilter.OriginalDataGridViewColumnHeaderText;
///     }
///  
///     public override Control FilterClientArea {
///         get {
///             return this.panelFilterArea;
///         }
///     }
///  
///     private void tsOK_Click(object sender, EventArgs e) {
///         FilterManager.ActivateFilter(true);
///         this.Popup.Close();
///     }
///  
///     private void tsRemove_Click(object sender, EventArgs e) {
///         FilterManager.ActivateFilter(false);
///         this.Popup.Close();
///     }
///  
///     private void tsRemoveAll_Click(object sender, EventArgs e) {
///         FilterManager.ActivateAllFilters(false);
///         this.Popup.Close();
///     }
///  
/// }
///  </code>
/// </example>
public class DgvBaseFilterHost : UserControl
{
    #region EVENTS

    /// <summary>
    ///     Occurs when the current visible <i>column filter</i> is changed.
    /// </summary>
    public event EventHandler CurrentColumnFilterChanged;

    #endregion


    #region PRIVATE FIELDS

    private ToolStripDropDown mPopup;
    private DgvBaseColumnFilter mCurrentColumnFilter;

    #endregion


    #region PROPERTIES

    /// <summary>
    ///     Return the effective area to which <i>column filters</i> will be added.
    /// </summary>
    public virtual Control FilterClientArea => this;


    /// <summary>
    ///     Gets the <b>ToolStripDropDown</b> object used to popup the <i>filter host</i>
    /// </summary>
    public ToolStripDropDown Popup
    {
        get
        {
            if (mPopup == null)
            {
                mPopup = new ToolStripDropDown();
                var ControlHost = new ToolStripControlHost(this);
                ControlHost.Padding = Padding.Empty;
                ControlHost.Margin = Padding.Empty;
                ControlHost.AutoSize = false;
                mPopup.Padding = Padding.Empty;
                mPopup.Items.Add(ControlHost);
                mPopup.Region = Region;
            }

            return mPopup;
        }
    }


    /// <summary>
    ///     Gets or sets the <i>filter manger</i>
    /// </summary>
    public DgvFilterManager FilterManager { set; get; }


    /// <summary>
    ///     Gets or sets the currently visibile <i>column filter</i> control
    /// </summary>
    public DgvBaseColumnFilter CurrentColumnFilter
    {
        get => mCurrentColumnFilter;
        set
        {
            // Called once: store the original size difference of the filterhost and the filterClientArea
            if (SizeDifference == Size.Empty)
            {
                SizeDifference = Size.Subtract(Size, FilterClientArea.Size);
                MinimumSize = Size;
            }

            if (mCurrentColumnFilter != null) mCurrentColumnFilter.Visible = false;
            mCurrentColumnFilter = value;
            DoAutoFit();
            if (CurrentColumnFilterChanged != null)
            {
                var e = new EventArgs();
                CurrentColumnFilterChanged(this, e);
            }

            mCurrentColumnFilter.Visible = true;
        }
    }


    /// <summary>
    ///     Gets the original size difference of the <i>filter host</i> and the
    ///     <see cref="DgvBaseFilterHost.FilterClientArea" />.
    /// </summary>
    public Size SizeDifference { get; private set; }

    #endregion


    #region HELPERS

    /// <summary>
    ///     Performs growing / shrinking of the <i>filter host</i> to best fit the current visibile <i>column filter</i>.
    /// </summary>
    /// <remarks>
    ///     Ovverride this method to provide your own resize logic.
    /// </remarks>
    protected virtual void DoAutoFit()
    {
        var NewHostSize = Size.Add(SizeDifference, mCurrentColumnFilter.Size);
        NewHostSize.Width = Math.Max(NewHostSize.Width, MinimumSize.Width);
        NewHostSize.Height = Math.Max(NewHostSize.Height, MinimumSize.Height);
        Size = NewHostSize;

        FilterClientArea.Size = Size.Subtract(NewHostSize, SizeDifference);
        AlignFilter();
    }

    /// <summary>
    ///     Aligns the <i>column filter</i> into the filter client area.
    /// </summary>
    /// <remarks>
    ///     Ovverride this method to provide your own alignment logic.
    /// </remarks>
    protected void AlignFilter()
    {
        var x = 0; // VFilterAlignmentType.Left:
        var y = 0; // HFilterAlignmentType.Top:
        switch (mCurrentColumnFilter.VFilterAlignment)
        {
            case VFilterAlignment.Right:
                x = FilterClientArea.Width - mCurrentColumnFilter.Width;
                break;
            case VFilterAlignment.Center:
                x = (FilterClientArea.Width - mCurrentColumnFilter.Width) / 2;
                break;
        }

        switch (mCurrentColumnFilter.HFilterAlignment)
        {
            case HFilterAlignment.Bottom:
                y = FilterClientArea.Height - mCurrentColumnFilter.Height;
                break;
            case HFilterAlignment.Middle:
                y = (FilterClientArea.Height - mCurrentColumnFilter.Height) / 2;
                break;
        }

        mCurrentColumnFilter.Location = new Point(x, y);
    }

    /// <summary>
    ///     Returns a region based on the transparency color of a bitmap.
    /// </summary>
    /// <param name="bitmap">The bitmap.</param>
    /// <param name="transparencyColor">The transparency color.</param>
    /// <returns>A region</returns>
    public static Region BitmapToRegion(Bitmap bitmap, Color transparencyColor)
    {
        if (bitmap == null)
            throw new ArgumentNullException("Bitmap", "Bitmap cannot be null!");

        var height = bitmap.Height;
        var width = bitmap.Width;

        var path = new GraphicsPath();

        for (var j = 0; j < height; j++)
        for (var i = 0; i < width; i++)
        {
            if (bitmap.GetPixel(i, j) == transparencyColor)
                continue;

            var x0 = i;

            while (i < width && bitmap.GetPixel(i, j) != transparencyColor)
                i++;

            path.AddRectangle(new Rectangle(x0, j, i - x0, 1));
        }

        var region = new Region(path);
        path.Dispose();
        return region;
    }

    /// <summary>
    ///     Registers the a combo box.
    /// </summary>
    /// <param name="comboBox">The combo box.</param>
    /// <remarks>
    ///     When the user clicks on an <b>ComboBox</b> item that is outside of the
    ///     host area, this cause an unwanted closing of the <i>filter host</i>.
    ///     If you use a <b>ComboBox</b> in a customized <i>column filter</i>,
    ///     be sure to call this method in your filter intitialitazion code.
    /// </remarks>
    public void RegisterComboBox(ComboBox comboBox)
    {
        comboBox.DropDown += onDropDown;
        comboBox.DropDownClosed += onDropDownClosed;
    }

    private void onDropDown(object sender, EventArgs e)
    {
        Popup.AutoClose = false;
    }

    private void onDropDownClosed(object sender, EventArgs e)
    {
        Popup.AutoClose = true;
    }

    #endregion
}