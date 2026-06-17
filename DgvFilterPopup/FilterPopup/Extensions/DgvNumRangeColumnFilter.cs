using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DgvFilterPopup;

/// <summary>
///     An extended <i>column filter</i> implementation allowing filters on numeric ranges.
/// </summary>
public partial class DgvNumRangeColumnFilter : DgvBaseColumnFilter
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DgvNumRangeColumnFilter" /> class.
    /// </summary>
    public DgvNumRangeColumnFilter()
    {
        InitializeComponent();
        ComboBoxOperator.SelectedValueChanged += onFilterChanged;
        TextBoxValue.TextChanged += onFilterChanged;
        TextBoxValue2.TextChanged += onFilterChanged;
    }

    /// <summary>
    ///     Gets the ComboBox control containing the available operators.
    /// </summary>
    public ComboBox ComboBoxOperator { get; private set; }

    /// <summary>
    ///     Gets the TextBox control containing the first value.
    /// </summary>
    public TextBox TextBoxValue { get; private set; }


    /// <summary>
    ///     Gets the TextBox control containing the second value.
    /// </summary>
    public TextBox TextBoxValue2 { get; private set; }


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
        ComboBoxOperator.Items.AddRange(new object[] { "[...]", "=", "<>", ">", "<", "<=", ">=", "= �", "<> �" });
        ComboBoxOperator.SelectedIndex = 0;
        FilterHost.RegisterComboBox(ComboBoxOperator);
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

        // Managing the NULL and NOT NULL cases which are type-independent
        if (ComboBoxOperator.Text == "= �")
            ResultFilterExpression = GetNullCondition(DataGridViewColumn.DataPropertyName);
        if (ComboBoxOperator.Text == "<> �")
            ResultFilterExpression = GetNotNullCondition(DataGridViewColumn.DataPropertyName);

        if (ResultFilterExpression != "")
        {
            FilterExpression = ResultFilterExpression;
            FilterCaption = OriginalDataGridViewColumnHeaderText + "\n " + ComboBoxOperator.Text;
            FilterManager.RebuildFilter();
            return;
        }

        var FormattedValue = FormatValue(TextBoxValue.Text, ColumnDataType);
        var FormattedValue2 = FormatValue(TextBoxValue2.Text, ColumnDataType);

        if (ComboBoxOperator.Text == "[...]")
        {
            if (FormattedValue != "" && FormattedValue2 != "")
            {
                FilterExpression = DataGridViewColumn.DataPropertyName + ">=" + FormattedValue
                                   + " AND " + DataGridViewColumn.DataPropertyName + "<=" + FormattedValue2;
                FilterCaption = OriginalDataGridViewColumnHeaderText + "\n = [" + TextBoxValue.Text + " , " +
                                TextBoxValue2.Text + "]";
                FilterManager.RebuildFilter();
            }
        }
        else
        {
            if (FormattedValue != "")
            {
                FilterExpression = DataGridViewColumn.DataPropertyName + " " + ComboBoxOperator.Text + FormattedValue;
                FilterCaption = OriginalDataGridViewColumnHeaderText + "\n" + ComboBoxOperator.Text + " " +
                                TextBoxValue.Text;
                FilterManager.RebuildFilter();
            }
        }
    }

    private void onFilterChanged(object sender, EventArgs e)
    {
        if (sender == ComboBoxOperator) TextBoxValue2.Visible = ComboBoxOperator.Text == "[...]";
        if (!FilterApplySoon || !Visible) return;
        Active = true;
        FilterExpressionBuild();
    }
}