using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace DgvFilterPopup;

/// <summary>
///     Specifies how the <i>column filter</i> control is horizontally aligned inside the <i>filter host</i>.
/// </summary>
public enum HFilterAlignment
{
    Top,
    Bottom,
    Middle
}

/// <summary>
///     Specifies how the <i>column filter</i> control is vertically aligned inside the <i>filter host</i>.
/// </summary>
public enum VFilterAlignment
{
    Left,
    Right,
    Center
}

/// <summary>
///     The base class from which to derive effective <i>column filter</i> classes
/// </summary>
/// <remarks>
///     The purpose of a <i>column filter</i> control is to contain visual elements allowing the end user to construct a
///     filter.
///     When inheriting from it, you can work just like creating any other user control.
///     This class is a derivation of <b>UserControl</b> and provide functionalities to
///     cooperate with DgvFilterManager.
///     <para>
///         NOTE:
///         This class must be intended as an abstract class. However, declaring it as abstract,
///         would generate errors whitin the designer when designing derived classes.
///     </para>
///     <para>
///         You should override <see cref="DgvBaseColumnFilter.OnFilterExpressionBuilding" /> to provide a filter
///         expression construction
///         logic and to set the values of the <see cref="DgvBaseColumnFilter.FilterExpression" /> and
///         <see cref="DgvBaseColumnFilter.FilterCaption" /> properties.
///     </para>
/// </remarks>
public class DgvBaseColumnFilter : UserControl
{
    #region EVENTS

    /// <summary>
    ///     Occurs before the filter expression is about to be built.
    /// </summary>
    public event CancelEventHandler FilterExpressionBuilding;


    /// <summary>
    ///     Occurs when the filter column is about to be initialized.
    /// </summary>
    public event CancelEventHandler FilterInitializing;

    #endregion


    #region PRIVATE FIELDS

    private string mFilterCaption = "";

    #endregion


    #region PROPERTIES

    /// <summary>
    ///     Gets or sets a value indicating whether filter apply soon after a user performs some changes.
    /// </summary>
    /// <value><c>true</c> (default) if to apply soon; otherwise, <c>false</c>.</value>
    public bool FilterApplySoon { get; set; } = true;

    /// <summary>
    ///     Gets and sets the filter expression.
    /// </summary>
    /// <remarks>
    ///     It's the filter expression on the column. Its value is used by the <see cref="DgvFilterManager" /> to build the
    ///     whole filter expression.
    ///     In inherited class, set its value in the override of <see cref="DgvBaseColumnFilter.OnFilterExpressionBuilding" />.
    ///     The filter expression must follow the rules of the DataView <see cref="System.Data.DataView.RowFilter" /> property.
    /// </remarks>
    public string FilterExpression { get; set; } = "";

    /// <summary>
    ///     Gets and sets the caption to show in the column header when the filter is active.
    /// </summary>
    /// <remarks>
    ///     Represents the caption to show in the column header when the filter is active.
    ///     In inherited class, set its value in the override of <see cref="DgvBaseColumnFilter.OnFilterExpressionBuilding" />.
    /// </remarks>
    public string FilterCaption
    {
        get => Active && FilterExpression != "" ? mFilterCaption : OriginalDataGridViewColumnHeaderText;
        set => mFilterCaption = value;
    }


    /// <summary>
    ///     Gets or sets a value indicating whether the filter is active.
    /// </summary>
    /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
    public bool Active { get; set; }

    /// <summary>
    ///     Specifies how the <i>column filter</i> control is horizontally aligned inside the <i>filter host</i>.
    /// </summary>
    public HFilterAlignment HFilterAlignment { get; set; } = HFilterAlignment.Middle;


    /// <summary>
    ///     Specifies how the <i>column filter</i> control is vertically aligned inside the <i>filter host</i>.
    /// </summary>
    public VFilterAlignment VFilterAlignment { get; set; } = VFilterAlignment.Center;


    /// <summary>
    ///     Gets the <b>DataView</b> acting as the data source of the <b>DataGridView</b> to which this <i>column filter</i> is
    ///     applied.
    /// </summary>
    public DataView BoundDataView { get; private set; }


    /// <summary>
    ///     Gets the <i>filter host</i> control in which this <i>column filter</i> is shown.
    /// </summary>
    public DgvBaseFilterHost FilterHost { get; private set; }


    /// <summary>
    ///     Gets the <i>filter manager</i>.
    /// </summary>
    public DgvFilterManager FilterManager { get; private set; }


    /// <summary>
    ///     Gets the <b>DataGridView</b> column to which this <i>column filter</i> is applied.
    /// </summary>
    /// <value>The data grid view column.</value>
    public DataGridViewColumn DataGridViewColumn { get; private set; }


    /// <summary>
    ///     Gets the type of the data bound to the <b>DataGridView</b> column.
    /// </summary>
    public Type ColumnDataType { get; private set; }


    /// <summary>
    ///     Gets the original <b>DataGridView</b> column header text.
    /// </summary>
    public string OriginalDataGridViewColumnHeaderText { get; private set; }

    #endregion


    #region FILTER INITIALIZATION, EXPRESSION BUILDING, EVENT MANAGING

    /// <summary>
    ///     Called by the <i>filter manager</i>, inits the <i>column filter</i> and raises the FilterInitializing event.
    /// </summary>
    /// <param name="FilterManager">The <i>filter manager</i>.</param>
    /// <param name="FilterHost">The filter host.</param>
    /// <param name="gridColumn">The DataGridView column.</param>
    /// <param name="boundDataView">The bound data view.</param>
    public void Init(DgvFilterManager FilterManager, DgvBaseFilterHost FilterHost, DataGridViewColumn gridColumn,
        DataView boundDataView)
    {
        this.FilterManager = FilterManager;
        this.FilterHost = FilterHost;
        DataGridViewColumn = gridColumn;
        BoundDataView = boundDataView;
        OriginalDataGridViewColumnHeaderText = gridColumn.HeaderText;
        if (gridColumn.DataPropertyName != "")
            ColumnDataType = boundDataView.Table.Columns[gridColumn.DataPropertyName].DataType;
        else
            ColumnDataType = typeof(string);
        FilterHost.FilterClientArea.Controls.Add(this);
        FilterHost.Location = new Point(0, 0);
        Visible = false;
        var e = new CancelEventArgs(false);
        OnFilterInitializing(this, e);
    }

    /// <summary>
    ///     Raises the <see cref="DgvBaseColumnFilter.FilterInitializing" /> event
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
    /// <remarks>
    ///     When this <i>column filter</i> control is added to the <i>column filters</i> array of the <i>filter manager</i>,
    ///     the latter calls the <see cref="DgvBaseColumnFilter.Init" /> method which, in turn, calls this method.
    ///     You can ovverride this method to provide initialization code.
    /// </remarks>
    protected virtual void OnFilterInitializing(object sender, CancelEventArgs e)
    {
        // Ovverride to add custom init code
        if (FilterInitializing != null) FilterInitializing(sender, e);
    }

    /// <summary>
    ///     Forces the rebuilt of filter expression
    /// </summary>
    /// <remarks>
    ///     This method is called by <see cref="DgvFilterManager" /> when popup is closed, to
    ///     force recreation of the filter expression.
    /// </remarks>
    public void FilterExpressionBuild()
    {
        var e = new CancelEventArgs(false);
        OnFilterExpressionBuilding(this, e);
    }

    /// <summary>
    ///     Raises the <see cref="DgvBaseColumnFilter.FilterExpressionBuilding" /> event
    /// </summary>
    /// <param name="sender">The event source.</param>
    /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
    /// <remarks>
    ///     Override <b>OnFilterExpressionBuilding</b> to provide a filter expression construction
    ///     logic and to set the values of the <see cref="DgvBaseColumnFilter.FilterExpression" /> and
    ///     <see cref="DgvBaseColumnFilter.FilterCaption" /> properties.
    ///     The <see cref="DgvFilterManager" /> will use these properties in constructing the whole filter expression and to
    ///     change the header text of the filtered column.
    /// </remarks>
    protected virtual void OnFilterExpressionBuilding(object sender, CancelEventArgs e)
    {
        if (FilterExpressionBuilding != null) FilterExpressionBuilding(sender, e);
    }

    #endregion


    #region HELPERS

    /// <summary>
    ///     Escapes a string to be suitable for filter expression.
    /// </summary>
    /// <param name="s">The string to escape.</param>
    /// <returns>The escaped string</returns>
    public static string StringEscape(string s)
    {
        var sarray = s.ToCharArray();
        var sb = new StringBuilder(s.Length * 2);
        foreach (var c in sarray)
            switch (c)
            {
                case '%':
                case '*':
                case '[':
                case ']':
                    sb.Append("[" + c + "]");
                    break;
                case '\'':
                    sb.Append("''");
                    break;
                default:
                    sb.Append(c);
                    break;
            }

        return sb.ToString();
    }

    /// <summary>
    ///     Returns the string representation of the passed value, based on target type.
    /// </summary>
    /// <param name="value">The value to be formatted.</param>
    /// <param name="targetType">The target type.</param>
    /// <returns>The string representation of the passed value</returns>
    public static string FormatValue(object value, Type targetType)
    {
        if (targetType == typeof(string)) return "'" + value + "'";
        try
        {
            value = Convert.ChangeType(value, targetType);
        }
        catch
        {
            return "";
        }

        if (targetType == typeof(bool)) return (bool)value ? "1" : "0";
        if (targetType == typeof(DateTime)) return "'" + ((DateTime)value).ToString("yyyy'-'MM'-'dd") + "'";
        //Numeric types
        return ((IFormattable)value).ToString(null, NumberFormatInfo.InvariantInfo);
    }

    /// <summary>
    ///     Returns a null condition string to be used in filter expression.
    /// </summary>
    /// <param name="DataColumnName">Name of the data column.</param>
    /// <returns>A string to be used in the filter expression representing a null condition</returns>
    public static string GetNullCondition(string DataColumnName)
    {
        return "ISNULL(CONVERT(" + DataColumnName + ",'System.String'),'NULLVALUE') = 'NULLVALUE'";
    }

    /// <summary>
    ///     Returns a not null condition string to be used in filter expression.
    /// </summary>
    /// <param name="DataColumnName">Name of the data column.</param>
    /// <returns>A string to be used in the filter expression representing a not null condition</returns>
    public static string GetNotNullCondition(string DataColumnName)
    {
        return "ISNULL(CONVERT(" + DataColumnName + ",'System.String'),'NULLVALUE') <> 'NULLVALUE'";
    }

    #endregion
}