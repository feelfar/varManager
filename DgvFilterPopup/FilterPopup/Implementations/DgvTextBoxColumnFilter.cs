using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DgvFilterPopup;

/// <summary>
///     A standard <i>column filter</i> implementation for textbox columns.
/// </summary>
public partial class DgvTextBoxColumnFilter : DgvBaseColumnFilter
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DgvTextBoxColumnFilter" /> class.
    /// </summary>
    public DgvTextBoxColumnFilter()
    {
        InitializeComponent();
        ComboBoxOperator.SelectedValueChanged += onFilterChanged;
        TextBoxValue.TextChanged += onFilterChanged;
    }

    /// <summary>
    ///     Gets the ComboBox control containing the available operators.
    /// </summary>
    public ComboBox ComboBoxOperator { get; private set; }

    /// <summary>
    ///     Gets the TextBox control containing the value.
    /// </summary>
    public TextBox TextBoxValue { get; private set; }


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

        if (ColumnDataType == typeof(string))
            ComboBoxOperator.Items.AddRange(new object[] { "..xxx..", "xxx..", "..xxx", "=", "<>", "= �", "<> �" });
        else
            ComboBoxOperator.Items.AddRange(new object[] { "=", "<>", ">", "<", "<=", ">=", "= �", "<> �" });

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
        var ResultFilterCaption = OriginalDataGridViewColumnHeaderText;

        var FilterValue = TextBoxValue.Text;
        var FormattedValue = "";

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

        if (ColumnDataType == typeof(string))
        {
            // Managing the string-column case
            FilterValue = StringEscape(FilterValue);
            switch (ComboBoxOperator.Text)
            {
                case "..xxx..":
                    ResultFilterExpression = DataGridViewColumn.DataPropertyName + " LIKE '%" + FilterValue + "%'";
                    ResultFilterCaption += "\n = '.." + FilterValue + "..'";
                    break;
                case "xxx..":
                    ResultFilterExpression = DataGridViewColumn.DataPropertyName + " LIKE '" + FilterValue + "%'";
                    ResultFilterCaption += "\n = '" + FilterValue + "..'";
                    break;
                case "..xxx":
                    ResultFilterExpression = DataGridViewColumn.DataPropertyName + " LIKE '%" + FilterValue + "'";
                    ResultFilterCaption += "\n = '.." + FilterValue + "'";
                    break;
                default:
                    ResultFilterExpression = DataGridViewColumn.DataPropertyName + " " + ComboBoxOperator.Text + "'" +
                                             FilterValue + "'";
                    ResultFilterCaption += "\n" + ComboBoxOperator.Text + "'" + FilterValue + "'";
                    break;
            }
        }
        else
        {
            // Managing the numeric-column case
            FormattedValue = FormatValue(FilterValue, ColumnDataType);
            if (FormattedValue != "")
            {
                ResultFilterExpression =
                    DataGridViewColumn.DataPropertyName + " " + ComboBoxOperator.Text + FormattedValue;
                ResultFilterCaption += "\n" + ComboBoxOperator.Text + "'" + FilterValue + "'";
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