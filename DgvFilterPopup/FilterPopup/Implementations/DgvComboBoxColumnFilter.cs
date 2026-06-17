using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DgvFilterPopup;

/// <summary>
///     A standard <i>column filter</i> implementation for ComboBox columns.
/// </summary>
/// <remarks>
///     If the <b>DataGridView</b> column to which this <i>column filter</i> is applied
///     is not a ComboBox column, it automatically creates a distinct list of values from the bound <b>DataView</b> column.
///     If the DataView changes, you should do an explicit call to <see cref="DgvComboBoxColumnFilter.RefreshValues" />
///     method.
/// </remarks>
public partial class DgvComboBoxColumnFilter : DgvBaseColumnFilter
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DgvComboBoxColumnFilter" /> class.
    /// </summary>
    public DgvComboBoxColumnFilter()
    {
        InitializeComponent();
        ComboBoxOperator.SelectedValueChanged += onFilterChanged;
        ComboBoxValue.SelectedValueChanged += onFilterChanged;
    }

    /// <summary>
    ///     Gets the ComboBox control containing the available operators.
    /// </summary>
    public ComboBox ComboBoxOperator { get; private set; }


    /// <summary>
    ///     Gets the ComboBox control containing the available values.
    /// </summary>
    public ComboBox ComboBoxValue { get; private set; }


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
        ComboBoxOperator.Items.AddRange(new object[] { "=", "<>", "= �", "<> �" });
        ComboBoxOperator.SelectedIndex = 0;
        if (DataGridViewColumn is DataGridViewComboBoxColumn)
        {
            ComboBoxValue.ValueMember = ((DataGridViewComboBoxColumn)DataGridViewColumn).ValueMember;
            ComboBoxValue.DisplayMember = ((DataGridViewComboBoxColumn)DataGridViewColumn).DisplayMember;
            ComboBoxValue.DataSource = ((DataGridViewComboBoxColumn)DataGridViewColumn).DataSource;
        }
        else
        {
            ComboBoxValue.ValueMember = DataGridViewColumn.DataPropertyName;
            ComboBoxValue.DisplayMember = DataGridViewColumn.DataPropertyName;
            RefreshValues();
        }

        FilterHost.RegisterComboBox(ComboBoxOperator);
        FilterHost.RegisterComboBox(ComboBoxValue);
    }

    /// <summary>
    ///     For non-combobox columns, refreshes the list of the <b>DataView</b> values in the column.
    /// </summary>
    public void RefreshValues()
    {
        if (!(DataGridViewColumn is DataGridViewComboBoxColumn))
        {
            var DistinctDataTable = BoundDataView.ToTable(true, DataGridViewColumn.DataPropertyName);
            DistinctDataTable.DefaultView.Sort = DataGridViewColumn.DataPropertyName;
            ComboBoxValue.DataSource = DistinctDataTable;
        }
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

        var ResultFilterExpression = "";
        var ResultFilterCaption = OriginalDataGridViewColumnHeaderText;

        // Managing the NULL and NOT NULL cases which are type-independent
        if (ComboBoxOperator.Text == "= �")
            ResultFilterExpression = GetNullCondition(DataGridViewColumn.DataPropertyName);
        if (ComboBoxOperator.Text == "<> �")
            ResultFilterExpression = GetNotNullCondition(DataGridViewColumn.DataPropertyName);

        if (ResultFilterExpression != "")
        {
            FilterExpression = ResultFilterExpression;
            FilterCaption = ResultFilterCaption + "\n " + ComboBoxOperator.Text;
            FilterManager.RebuildFilter();
            return;
        }

        var FilterValue = ComboBoxValue.SelectedValue;
        var FormattedValue = "";

        if (ColumnDataType == typeof(string))
        {
            // Managing the string-column case
            var EscapedFilterValue = StringEscape(FilterValue.ToString());
            ResultFilterExpression = DataGridViewColumn.DataPropertyName + " " + ComboBoxOperator.Text + "'" +
                                     EscapedFilterValue + "'";
            ResultFilterCaption += "\n" + ComboBoxOperator.Text + " " + ComboBoxValue.Text;
        }
        else
        {
            // Managing the other cases
            FormattedValue = FormatValue(FilterValue, ColumnDataType);
            if (FormattedValue != "")
            {
                ResultFilterExpression =
                    DataGridViewColumn.DataPropertyName + " " + ComboBoxOperator.Text + FormattedValue;
                ResultFilterCaption += "\n" + ComboBoxOperator.Text + " " + ComboBoxValue.Text;
            }
        }

        if (ResultFilterExpression != "")
        {
            FilterExpression = ResultFilterExpression;
            FilterCaption = ResultFilterCaption;
            FilterManager.RebuildFilter();
        }
    }

    private void onFilterChanged(object sender, EventArgs e)
    {
        if (!FilterApplySoon || !Visible) return;
        Active = true;
        FilterExpressionBuild();
    }
}