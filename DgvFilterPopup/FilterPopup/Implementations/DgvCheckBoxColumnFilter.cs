using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DgvFilterPopup;

/// <summary>
///     A standard <i>column filter</i> implementation for checkbox columns.
/// </summary>
public partial class DgvCheckBoxColumnFilter : DgvBaseColumnFilter
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DgvCheckBoxColumnFilter" /> class.
    /// </summary>
    public DgvCheckBoxColumnFilter()
    {
        InitializeComponent();
        ComboBoxOperator.SelectedValueChanged += onFilterChanged;
        CheckBoxValue.CheckStateChanged += onFilterChanged;
    }

    /// <summary>
    ///     Gets the ComboBox control containing the available operators.
    /// </summary>
    public ComboBox ComboBoxOperator { get; private set; }


    /// <summary>
    ///     Gets the CheckBox control containing the checked value.
    /// </summary>
    public CheckBox CheckBoxValue { get; private set; }


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
        ComboBoxOperator.Items.AddRange(new object[] { "=", "= �", "<> �" });
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

        if (CheckBoxValue.Checked)
        {
            ResultFilterExpression = DataGridViewColumn.DataPropertyName + " = 1";
            ResultFilterCaption += "\n = yes";
        }
        else
        {
            ResultFilterExpression = DataGridViewColumn.DataPropertyName + " = 0";
            ResultFilterCaption += "\n = no";
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