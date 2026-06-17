using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DgvFilterPopup.Properties;

namespace DgvFilterPopup;

/// <summary>
///     The main class involved in adding filtering capabilities to a DataGridView.
/// </summary>
/// <remarks>
///     This is the class that you use to add filtering capabilities to a <b>DataGridView</b>. The
///     <b>DataGridView</b> must be data bound to a <b>DataTable</b>, a <b>DataView</b> or a <b>BindingSource</b> which in
///     turn is
///     bound to one of these two.
///     When you assign a <b>DataGridView</b> to a <b>DgvFilterManager</b>, it attaches some handlers to respond
///     to right click on column headers and to perform some custom painting on the grid.
///     When the user right clicks a column header, the <b>DgvFilterManager</b> shows a popup near the column.
///     This popup is a control that serves as host for other controls, one for each column. Only one of
///     these child controls is visibile, based on clicked column.
///     We have one <i>filter host</i> control and many <i>column filter</i> child controls.
///     <para>
///         The <i>filter host</i> control must be a derivation of the <see cref="DgvBaseFilterHost" /> class, while filter
///         controls must be
///         derived by the <see cref="DgvBaseColumnFilter" /> class. These two classes don't provide any user interface.
///         As a default, <b>DgvFilterManager</b> uses the standard derivation <b>DgvFilterHost</b> and, depending on
///         column type and data type,
///         one of the standard derivations: <see cref="DgvTextBoxColumnFilter" />
///         , <see cref="DgvCheckBoxColumnFilter" />, <see cref="DgvComboBoxColumnFilter" />
///         and <see cref="DgvDateColumnFilter" />.
///     </para>
///     <para>
///         When a <b>DataGridView</b> is attached, the manager perform the following actions:
///         <ul>
///             <li>
///                 it creates a <i>filter host</i>, that is an instance of the <b>DgvFilterHost</b> class. If you
///                 previously provided a
///                 <i>filter host</i>, this step is skipped.
///             </li>
///             <li>
///                 it creates an array of <b>DgvBaseColumnFilter</b>, one per column, and initializes each element to a
///                 specialization
///                 of <b>DgvBaseColumnFilter</b>. If <see cref="DgvFilterManager.AutoCreateFilters" /> is false, this step
///                 is skipped.
///             </li>
///         </ul>
///     </para>
///     <para>
///         You can force a specific <i>column filter</i> for a certain column, intervening in this process through the
///         events
///         <see cref="DgvFilterManager.ColumnFilterAdding" /> and <see cref="DgvFilterManager.ColumnFilterAdded" />. You
///         can also intervene, after the entire process, replacing
///         a <i>column filter</i> instance in the array with another instance you created.
///     </para>
/// </remarks>
public class DgvFilterManager
{
    #region PRIVATE FIELDS

    private DgvBaseFilterHost mFilterHost; // The host UserControl to popup
    private DataGridView mDataGridView; // The DataGridView to which apply filtering
    private DataView mBoundDataView; // The DataView to which the DataGridView is bound
    private BindingSource mBindingSource; // The BindingSource, if any, to which the DataGridView is bound


    private string mBaseFilter = ""; // Developer provided filter expression
    private int mCurrentColumnIndex = -1; // Column Index of currently visibile filter

    private List<DgvBaseColumnFilter> mColumnFilterList; // List of ColumnFilter objects
    private bool mFilterIsActive;

    #endregion


    #region EVENTS

    /// <summary>
    ///     Occurs when a <i>column filter</i> instance for a column is about to be automatically created.
    /// </summary>
    /// <remarks>
    ///     Using this event you can set the <see cref="ColumnFilterEventArgs.ColumnFilter" />
    ///     property to force the <see cref="DgvBaseColumnFilter" /> specialization to use for the
    ///     column.
    ///     This event is raised only if <see cref="DgvFilterManager.AutoCreateFilters" /> is true.
    /// </remarks>
    public event ColumnFilterEventHandler ColumnFilterAdding;


    /// <summary>
    ///     Occurs when a <i>column filter</i> instance is created.
    ///     This event is raised only if <see cref="DgvFilterManager.AutoCreateFilters" /> is true.
    /// </summary>
    public event ColumnFilterEventHandler ColumnFilterAdded;


    /// <summary>
    ///     Occurs when the popup is about to be shown
    /// </summary>
    /// <remarks>
    ///     Use this event to customize the popup position. Set the Handled property of the event argument to <c>true</c>.
    /// </remarks>
    public event ColumnFilterEventHandler PopupShowing;

    #endregion


    #region CONSTRUCTORS

    /// <summary>
    ///     Initializes a new instance of the <see cref="DgvFilterManager" /> class.
    /// </summary>
    public DgvFilterManager()
    {
    }


    /// <summary>
    ///     Initializes a new instance of the <see cref="DgvFilterManager" /> class.
    /// </summary>
    /// <param name="dataGridView">The <b>DataGridView</b> to which attach filtering capabilities</param>
    /// <param name="autoCreateFilters">if set to <c>true</c> automatically creates a <i>column filter</i> for each column</param>
    public DgvFilterManager(DataGridView dataGridView, bool autoCreateFilters)
    {
        AutoCreateFilters = autoCreateFilters;
        DataGridView = dataGridView;
    }


    /// <summary>
    ///     Initializes a new instance of the <see cref="DgvFilterManager" /> class.
    /// </summary>
    /// <param name="dataGridView">The <b>DataGridView</b> to which attach filtering capabilities.</param>
    public DgvFilterManager(DataGridView dataGridView) : this(dataGridView, true)
    {
    }

    #endregion


    #region PROPERTIES

    /// <summary>
    ///     Gets or sets a value indicating whether the manager must create <i>column filters</i>.
    /// </summary>
    /// <value><c>true</c> by default.</value>
    public bool AutoCreateFilters { get; set; } = true;


    /// <summary>
    ///     Gets and sets the <i>filter host</i> to use.
    /// </summary>
    /// <remarks>
    ///     The default <i>filter host</i> is an instance of <see cref="DgvFilterHost" />
    /// </remarks>

    public DgvBaseFilterHost FilterHost
    {
        get
        {
            if (mFilterHost == null)
                // If not provided, use the default FilterHost
                FilterHost = new DgvFilterHost();
            return mFilterHost;
        }
        set
        {
            mFilterHost = value;
            // initialize FilterManager to this object
            mFilterHost.FilterManager = this;
            mFilterHost.Popup.Closed += Popup_Closed;
        }
    }


    /// <summary>
    ///     Gets and sets the DataGridView to which apply filtering capabilities.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         When a <b>DataGridView</b> is attached, the manager perform the following actions:
    ///         <ul>
    ///             <li>
    ///                 it creates a <i>filter host</i>, that is an instance of the <b>DgvFilterHost</b> class. If you
    ///                 previously provided a
    ///                 <i>filter host</i>, this step is skipped.
    ///             </li>
    ///             <li>
    ///                 it creates an array of <b>DgvBaseColumnFilter</b>, one per column, and initializes each element to a
    ///                 specialization
    ///                 of <b>DgvBaseColumnFilter</b>. If <see cref="DgvFilterManager.AutoCreateFilters" /> is false, this step
    ///                 is skipped.
    ///             </li>
    ///         </ul>
    ///     </para>
    ///     <para>
    ///         You can force a specific <i>column filter</i> for a certain column, intervening in this process through the
    ///         events
    ///         <see cref="DgvFilterManager.ColumnFilterAdding" /> and <see cref="DgvFilterManager.ColumnFilterAdded" />. You
    ///         can also intervene, after the entire process, replacing
    ///         a <i>column filter</i> instance in the array with another instance you created.
    ///     </para>
    /// </remarks>
    public DataGridView DataGridView
    {
        get => mDataGridView;
        set
        {
            mDataGridView = value;
            mColumnFilterList = new List<DgvBaseColumnFilter>(mDataGridView.Columns.Count);
            FindDataView();
            mDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            mDataGridView.CellMouseClick += mDataGridView_CellMouseClick;
            mDataGridView.CellPainting += mDataGridView_CellPainting;
            mDataGridView.ColumnAdded += mDataGridView_ColumnAdded;
            mDataGridView.ColumnRemoved += mDataGridView_ColumnRemoved;
            if (mDataGridView == null) return;
            foreach (DataGridViewColumn c in mDataGridView.Columns)
            {
                mColumnFilterList.Add(null);
                CreateColumnFilter(c);
            }
        }
    }

    /// <summary>
    ///     Gets and sets developer provided filter expression. This expression
    ///     will be "merged" with end-user created filters.
    /// </summary>
    /// <value>The base filter.</value>

    public string BaseFilter
    {
        get => mBaseFilter;
        set
        {
            mBaseFilter = value;
            RebuildFilter();
        }
    }


    /// <summary>
    ///     Gets or sets the <i>column filter</i> control related to the ColumnIndex
    /// </summary>
    /// <param name="ColumnIndex">The index of the <b>DataGridView</b> column</param>
    /// <returns>the <b>DgvBaseColumnFilter</b> related to the <b>DataGridView</b> column</returns>
    /// <remarks>
    ///     This indexer allow you to get and set the <i>column filter</i> instance for the column.
    ///     You can set one of the standard <i>column filter</i> implementation or an instance
    ///     of your own <b>DgvBaseFilterColumn</b> specialization.
    /// </remarks>
    public DgvBaseColumnFilter this[int ColumnIndex]
    {
        get => mColumnFilterList[ColumnIndex];
        set
        {
            mColumnFilterList[ColumnIndex] = value;
            value.Init(this, FilterHost, mDataGridView.Columns[ColumnIndex], mBoundDataView);
        }
    }


    /// <summary>
    ///     Gets or sets the <i>column filter</i> control related to the ColumnName
    /// </summary>
    /// <param name="ColumnName">The name of the <b>DataGridView</b> column</param>
    /// <returns>the DgvBaseColumnFilter related to the <b>DataGridView</b> column</returns>
    /// <remarks>
    ///     This indexer allow you to get and set the <i>column filter</i> instance for the column.
    ///     You can set one of the standard <i>column filter</i> implementation or an instance
    ///     of your own <b>DgvBaseFilterColumn</b> specialization.
    /// </remarks>
    public DgvBaseColumnFilter this[string ColumnName]
    {
        get => mColumnFilterList[mDataGridView.Columns[ColumnName].Index];
        set => this[mDataGridView.Columns[ColumnName].Index] = value;
    }

    #endregion


    #region DATAGRIDVIEW EVENT HANDLERS

    private void mDataGridView_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
    {
        mColumnFilterList.RemoveAt(e.Column.Index);
    }

    private void mDataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
    {
        FindDataView();
        mColumnFilterList.Insert(e.Column.Index, null);
        CreateColumnFilter(e.Column);
    }


    /// <summary>
    ///     Shows the popup when user right-clicks a column header
    /// </summary>
    /// <param name="sender">The event source.</param>
    /// <param name="e">
    ///     The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs" /> instance containing the event
    ///     data.
    /// </param>
    protected virtual void mDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right && e.RowIndex == -1 && e.ColumnIndex > -1) ShowPopup(e.ColumnIndex);
    }

    //Based on filters state, call the appropriate protected paint helpers
    private void mDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
    {
        if (e.RowIndex != -1) return; //skip if it is not the header row

        //Cell Origin
        if (e.RowIndex == -1 && e.ColumnIndex == -1 && mFilterIsActive)
        {
            OnFilteredGridPaint(sender, e);
            return;
        }

        if (FilterHost.Popup.Visible) OnHighlightedColumnPaint(sender, e);

        if (e.ColumnIndex == -1) return;
        if (mColumnFilterList[e.ColumnIndex] != null && mColumnFilterList[e.ColumnIndex].Active)
            OnFilteredColumnPaint(sender, e);
    }


    /// <summary>
    ///     Paints a funnel icon in the cell origin when some column is filtered.
    /// </summary>
    /// <param name="sender">The sender</param>
    /// <param name="e">
    ///     The <see cref="System.Windows.Forms.DataGridViewCellPaintingEventArgs" /> instance containing the event
    ///     data.
    /// </param>
    /// <remarks>
    ///     Override this method to provide your own painting
    /// </remarks>
    protected virtual void OnFilteredGridPaint(object sender, DataGridViewCellPaintingEventArgs e)
    {
        e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
        e.Paint(e.CellBounds, e.PaintParts & ~DataGridViewPaintParts.Background);
        var r = new Rectangle(e.CellBounds.X + 1, e.CellBounds.Y + 1, e.CellBounds.Width - 3, e.CellBounds.Height - 4);
        e.Graphics.DrawImage(FunnelPicture, (e.CellBounds.Width - FunnelPicture.Width) / 2,
            (e.CellBounds.Height - FunnelPicture.Height) / 2, FunnelPicture.Width, FunnelPicture.Height);
        e.Graphics.DrawRectangle(Pens.Black, r);

        e.Handled = true;

        //e.Paint(e.CellBounds, DataGridViewPaintParts.All);
        //e.Graphics.DrawImage(FunnelPicture, (e.CellBounds.Width - FunnelPicture.Width) / 2, (e.CellBounds.Height - FunnelPicture.Height) / 2, FunnelPicture.Width, FunnelPicture.Height);
        //e.Handled = true;
    }

    /// <summary>
    ///     Performs customized column header painting when the popup is visibile.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">
    ///     The <see cref="System.Windows.Forms.DataGridViewCellPaintingEventArgs" /> instance containing the event
    ///     data.
    /// </param>
    /// <remarks>
    ///     Override this method to provide your own painting
    /// </remarks>
    protected virtual void OnHighlightedColumnPaint(object sender, DataGridViewCellPaintingEventArgs e)
    {
        if (e.ColumnIndex != mCurrentColumnIndex || e.RowIndex != -1) return;
        e.Paint(e.CellBounds, DataGridViewPaintParts.All);
        var r = new Rectangle(e.CellBounds.X + 1, e.CellBounds.Y + 1, e.CellBounds.Width - 3, e.CellBounds.Height - 4);
        e.Graphics.DrawRectangle(Pens.Yellow, r);
        e.Handled = true;
    }

    /// <summary>
    ///     Performs customized column header painting when the column is filtered.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">
    ///     The <see cref="System.Windows.Forms.DataGridViewCellPaintingEventArgs" /> instance containing the event
    ///     data.
    /// </param>
    /// <remarks>
    ///     Override this method to provide your own painting
    /// </remarks>
    protected virtual void OnFilteredColumnPaint(object sender, DataGridViewCellPaintingEventArgs e)
    {
        e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
        e.Paint(e.CellBounds, e.PaintParts & ~DataGridViewPaintParts.Background);
        var r = new Rectangle(e.CellBounds.X + 1, e.CellBounds.Y + 1, e.CellBounds.Width - 3, e.CellBounds.Height - 4);
        e.Graphics.DrawRectangle(Pens.Black, r);
        e.Handled = true;
    }

    #endregion


    #region FILTERHOST MANAGING

    //Forces column header repaint when popup is closed, cleaning customized painting performed by OnHighlightedColumnPaint
    private void Popup_Closed(object sender, ToolStripDropDownClosedEventArgs e)
    {
        mDataGridView.InvalidateCell(mCurrentColumnIndex,
            -1); // Force header repaint (to hide the selection yellow frame)
    }


    /// <summary>
    ///     Shows the popup.
    /// </summary>
    /// <param name="ColumnIndex">Index of the column.</param>
    public void ShowPopup(int ColumnIndex)
    {
        if (mColumnFilterList[ColumnIndex] == null) return; // non-data column
        var OldColumnIndex = mCurrentColumnIndex;
        mCurrentColumnIndex = ColumnIndex;
        var r = mDataGridView.GetCellDisplayRectangle(ColumnIndex, -1, false); // get the header size info
        FilterHost.CurrentColumnFilter = mColumnFilterList[ColumnIndex];
        try
        {
            //use "try" because old column could have been removed
            mDataGridView.InvalidateCell(OldColumnIndex, -1);
        }
        catch
        {
        }

        var e = new ColumnFilterEventArgs(mDataGridView.Columns[ColumnIndex], mColumnFilterList[ColumnIndex]);
        if (PopupShowing != null) PopupShowing(this, e);
        if (!e.Handled)
            FilterHost.Popup.Show(mDataGridView, r.X + r.Width - 4,
                r.Y - 10); // show the filterhost popup near the column
        FilterHost.Popup.Focus();

        mDataGridView.InvalidateCell(mCurrentColumnIndex,
            -1); // Force header repaint (to show a selection yellow frame)
    }

    #endregion


    #region COLUMN FILTERS MANAGING

    /// <summary>
    ///     Activates / Deactivates the filter for the column specified by ColumnIndex.
    /// </summary>
    /// <param name="Active">The active state to set</param>
    /// <param name="ColumnIndex">Index of the column.</param>
    public void ActivateFilter(bool Active, int ColumnIndex)
    {
        this[ColumnIndex].Active = Active;
        RebuildFilter();
    }


    /// <summary>
    ///     Activates / Deactivates the filter for the column specified by ColumnName.
    /// </summary>
    /// <param name="Active">The active state to set</param>
    /// <param name="ColumnName">Name of the column.</param>
    public void ActivateFilter(bool Active, string ColumnName)
    {
        this[ColumnName].Active = Active;
        RebuildFilter();
    }


    /// <summary>
    ///     Activates / Deactivates the filter for the current, that is last right-clicked, column.
    /// </summary>
    /// <param name="Active">The active state to set</param>
    public void ActivateFilter(bool Active)
    {
        if (mCurrentColumnIndex == -1) return;
        this[mCurrentColumnIndex].Active = Active;
        if (Active) this[mCurrentColumnIndex].FilterExpressionBuild();
        RebuildFilter();
    }


    /// <summary>
    ///     Activates / Deactivates all filters.
    /// </summary>
    /// <param name="Active">The active state to set</param>
    public void ActivateAllFilters(bool Active)
    {
        foreach (var CF in mColumnFilterList)
        {
            if (CF == null) continue;
            CF.Active = Active;
            if (Active) CF.FilterExpressionBuild();
        }

        RebuildFilter();
    }


    /// <summary>
    ///     Rebuilds the whole filter expression.
    /// </summary>
    /// <remarks>
    ///     The whole filter expression is the conjunction of each <i>column filter</i> and the <see cref="BaseFilter" />.
    ///     Call this method to refresh and apply the whole filter expression.
    /// </remarks>
    public void RebuildFilter()
    {
        mFilterIsActive = false;
        var Filter = "";
        foreach (var CF in mColumnFilterList)
        {
            if (CF == null) continue;
            if (CF.Active && CF.FilterExpression != "")
            {
                Filter += " AND (" + CF.FilterExpression + ")";
                CF.DataGridViewColumn.HeaderText = CF.FilterCaption;
            }
            else
            {
                CF.DataGridViewColumn.HeaderText = CF.OriginalDataGridViewColumnHeaderText;
            }
        }

        if (Filter != "")
        {
            mFilterIsActive = true;
            Filter = mBaseFilter == "" ? "1=1 " + Filter : mBaseFilter + " " + Filter;
        }
        else
        {
            Filter = mBaseFilter;
        }

        // Apply the filter only if any changes occurred
        try
        {
            if (mBindingSource != null)
            {
                if (mBindingSource.Filter != Filter) mBindingSource.Filter = Filter;
            }
            else
            {
                if (mBoundDataView.RowFilter != Filter) mBoundDataView.RowFilter = Filter;
            }
        }
        catch
        {
            Console.WriteLine("Invalid filter: " + Filter);
        }
    }

    #endregion


    #region HELPERS

    // Checks if the DataGridView is data bound and the data source finally resolves to a DataView.
    private void FindDataView()
    {
        mBindingSource = null;
        var DataSource = mDataGridView.DataSource;
        var DataMember = mDataGridView.DataMember;

        var ExceptionMsg =
            "DataGridViewFilter can only work with bound DataGridView. The DataSource must be a DataSet, a DataTable, a DataView or a BindingSource which is bound to a DataSet, a DataTable or a DataView ";

        while (!(DataSource is DataView))
        {
            if (DataSource == null) return;

            if (DataSource is BindingSource)
            {
                mBindingSource = (BindingSource)DataSource;
                DataMember = ((BindingSource)DataSource).DataMember;
                DataSource = ((BindingSource)DataSource).DataSource;
                continue;
            }

            if (DataSource is DataSet)
            {
                DataSource = ((DataSet)DataSource).Tables[DataMember];
                DataMember = "";
                continue;
            }

            if (DataSource is DataTable)
            {
                DataSource = ((DataTable)DataSource).DefaultView;
                break;
            }

            //other types are not allowed
            throw new Exception(ExceptionMsg);
        }

        mBoundDataView = (DataView)DataSource;
    }

    /// <summary>
    ///     Gets a funnel picture.
    /// </summary>
    public static Image FunnelPicture => Resources.Filter_16x;


    private void CreateColumnFilter(DataGridViewColumn c)
    {
        if (!AutoCreateFilters) return;
        //Raise the event about column filter creation
        var e = new ColumnFilterEventArgs(c, null);
        if (ColumnFilterAdding != null) ColumnFilterAdding(this, e);
        //if not provided, by an event handler, proceed with standard filter creation
        if (e.ColumnFilter == null)
        {
            Type DataType = null;
            if (c.DataPropertyName != "")
            {
                DataType = mBoundDataView.Table.Columns[c.DataPropertyName].DataType;

                switch (c.GetType().Name)
                {
                    case "DataGridViewComboBoxColumn":
                        e.ColumnFilter = new DgvComboBoxColumnFilter();
                        break;
                    case "DataGridViewCheckBoxColumn":
                        e.ColumnFilter = new DgvCheckBoxColumnFilter();
                        break;
                    case "DataGridViewTextBoxColumn":
                        if (DataType == typeof(DateTime))
                            e.ColumnFilter = new DgvDateColumnFilter();
                        else
                            e.ColumnFilter = new DgvTextBoxColumnFilter();
                        break;
                }
            }
        }

        mColumnFilterList[c.Index] = e.ColumnFilter;
        if (e.ColumnFilter != null)
        {
            // == null when non-data column
            if (ColumnFilterAdded != null) ColumnFilterAdded(this, e);
            e.ColumnFilter.Init(this, FilterHost, c, mBoundDataView);
        }
    }

    #endregion
}