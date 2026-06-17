using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DgvFilterPopup;

public partial class DgvMonthYearColumnFilter : DgvBaseColumnFilter
{
    private int mYearMax;
    private int mYearMin;


    /// <summary>
    ///     Initializes a new instance of the <see cref="DgvMonthYearColumnFilter" /> class.
    /// </summary>
    public DgvMonthYearColumnFilter()
    {
        InitializeComponent();
        mYearMin = DateTime.Today.Year - 10;
        mYearMax = DateTime.Today.Year;
        //Month combo initializing
        ComboBoxMonth.Items.Add("---");
        ComboBoxMonth.Items.AddRange(MonthCsvList.Split(','));
        ComboBoxMonth.SelectedIndex = 0;

        //Year combo initializing
        mYearMin = DateTime.Today.Year - 10;
        mYearMax = DateTime.Today.Year;
        SetYearList();
    }

    /// <summary>
    ///     Gets the ComboBox control containing the months list.
    /// </summary>
    public ComboBox ComboBoxMonth { get; private set; }


    /// <summary>
    ///     Gets the ComboBox control containing the years list.
    /// </summary>
    public ComboBox ComboBoxYear { get; private set; }

    /// <summary>
    ///     Perform filter initialitazion and raises the FilterInitializing event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
    /// <remarks>
    ///     When this <i>column filter</i> control is added to the <i>column filters</i> array of the <i>filter manager</i>,
    ///     the latter calls the <see cref="DgvBaseColumnFilter.Init" /> method which, in turn, calls this method.
    ///     You can ovverride this method to provide initialization code or you can create an event handler and
    ///     set the <i>Cancel</i> property of event argument to true, to skip standard initialization.
    /// </remarks>
    protected override void OnFilterInitializing(object sender, CancelEventArgs e)
    {
        base.OnFilterInitializing(sender, e);
        if (e.Cancel) return;
        ComboBoxMonth.SelectedValueChanged += onFilterChanged;
        ComboBoxYear.SelectedValueChanged += onFilterChanged;
        ComboBoxYear.SelectedIndex = ComboBoxYear.Items.Count - 1;
        FilterHost.RegisterComboBox(ComboBoxMonth);
        FilterHost.RegisterComboBox(ComboBoxYear);
    }

    /// <summary>
    ///     Builds the filter expression and raises the FilterExpressionBuilding event
    /// </summary>
    /// <param name="sender">The event source.</param>
    /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
    /// <remarks>
    ///     Override <b>OnFilterExpressionBuilding</b> to provide a filter expression construction
    ///     logic and to set the values of the <see cref="DgvBaseColumnFilter.FilterExpression" /> and
    ///     <see cref="DgvBaseColumnFilter.FilterCaption" /> properties.
    ///     The <see cref="DgvFilterManager" /> will use these properties in constructing the whole filter expression and to
    ///     change the header text of the filtered column.
    ///     Otherwise, you can create an event handler and set the <i>Cancel</i> property of event argument to true, to skip
    ///     standard filter expression building logic.
    /// </remarks>
    protected override void OnFilterExpressionBuilding(object sender, CancelEventArgs e)
    {
        base.OnFilterExpressionBuilding(sender, e);
        if (e.Cancel)
        {
            FilterManager.RebuildFilter();
            return;
        }

        DateTime MinDate, MaxDate;

        if (ComboBoxMonth.SelectedIndex == 0)
        {
            MinDate = new DateTime(mYearMin + ComboBoxYear.SelectedIndex, 1, 1, 0, 0, 0);
            MaxDate = MinDate.AddYears(1);
            FilterCaption = OriginalDataGridViewColumnHeaderText + "\n = " + ComboBoxYear.Text;
        }
        else
        {
            MinDate = new DateTime(mYearMin + ComboBoxYear.SelectedIndex, ComboBoxMonth.SelectedIndex, 1, 0, 0, 0);
            MaxDate = MinDate.AddMonths(1);
            FilterCaption = OriginalDataGridViewColumnHeaderText + "\n = " + ComboBoxMonth.Text + " " +
                            ComboBoxYear.Text;
        }

        FilterExpression = DataGridViewColumn.DataPropertyName + " >= " + FormatValue(MinDate, ColumnDataType) +
                           " AND " + DataGridViewColumn.DataPropertyName + " < " + FormatValue(MaxDate, ColumnDataType);
        FilterManager.RebuildFilter();
    }


    private void onFilterChanged(object sender, EventArgs e)
    {
        if (!FilterApplySoon || !Visible) return;
        Active = true;
        FilterExpressionBuild();
    }


    private void SetYearList()
    {
        ComboBoxYear.Items.Clear();
        for (var y = mYearMin; y <= mYearMax; y++) ComboBoxYear.Items.Add(y.ToString());
        ComboBoxYear.SelectedIndex = ComboBoxYear.Items.Count - 1;
    }


    #region PROPERTIES

    /// <summary>
    ///     Gets or sets the month comma separated list.
    /// </summary>
    /// <value>The month CSV list.</value>
    /// <remarks>
    ///     Allows you to set once your culture-specific comma separated list of months.
    /// </remarks>
    public static string MonthCsvList { get; set; } =
        "January,February,March,April,May,June,July,August,September,October,November,December";

    /// <summary>
    ///     Gets or sets the minimum year shown in the years combo.
    /// </summary>
    /// <value>The year min.</value>
    public int YearMin
    {
        get => mYearMin;
        set
        {
            mYearMin = value;
            SetYearList();
        }
    }

    /// <summary>
    ///     Gets or sets the maximum year shown in the years combo.
    /// </summary>
    /// <value>The year min.</value>
    public int YearMax
    {
        get => mYearMax;
        set
        {
            mYearMax = value;
            SetYearList();
        }
    }

    #endregion
}