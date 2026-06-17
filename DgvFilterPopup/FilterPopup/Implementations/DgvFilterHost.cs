using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DgvFilterPopup;

/// <summary>
///     Is the standard implementation of DgvBaseFilterHost
/// </summary>
[ToolboxItem(false)]
public partial class DgvFilterHost : DgvBaseFilterHost
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DgvFilterHost" /> class.
    /// </summary>
    public DgvFilterHost()
    {
        InitializeComponent();
        CurrentColumnFilterChanged += DgvFilterHost_CurrentColumnFilterChanged;
    }

    /// <summary>
    ///     Return the effective area to which the <i>column filters</i> will be added.
    /// </summary>
    /// <value></value>
    public override Control FilterClientArea => panelFilterArea;

    private void DgvFilterHost_CurrentColumnFilterChanged(object sender, EventArgs e)
    {
        lblColumnName.Text = CurrentColumnFilter.OriginalDataGridViewColumnHeaderText;
    }

    private void tsOK_Click(object sender, EventArgs e)
    {
        FilterManager.ActivateFilter(true);
        Popup.Close();
    }

    private void tsRemove_Click(object sender, EventArgs e)
    {
        FilterManager.ActivateFilter(false);
        Popup.Close();
    }

    private void tsRemoveAll_Click(object sender, EventArgs e)
    {
        FilterManager.ActivateAllFilters(false);
        Popup.Close();
    }
}