using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace RatingControls
{
	public class StarRatingControl : System.Windows.Forms.Control
	{
		public StarRatingControl()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
	
			Width = 120;
			Height = 18;

			m_starAreas = new Rectangle[StarCount];
		}

		#region Properties

		public int LeftMargin
		{
			get
			{
				return m_leftMargin;
			}
			set
			{
				if ( m_leftMargin != value )
				{
					m_leftMargin = value;
					Invalidate();
				}
			}
		} 

		public int RightMargin
		{
			get
			{
				return m_rightMargin;
			}
			set
			{
				if ( m_rightMargin != value )
				{
					m_rightMargin = value;
					Invalidate();
				}
			}
		}

		public int TopMargin
		{
			get
			{
				return m_topMargin;
			}
			set
			{
				if ( m_topMargin != value )
				{
					m_topMargin = value;
					Invalidate();
				}
			}
		}

		public int BottomMargin
		{
			get
			{
				return m_bottomMargin;
			}
			set
			{
				if ( m_bottomMargin != value )
				{
					m_bottomMargin = value;
					Invalidate();
				}
			}
		}

		public int StarSpacing
		{
			get
			{
				return m_starSpacing;
			}
			set
			{
				if ( m_starSpacing != value )
				{
					m_starSpacing = value;
					Invalidate();
				}
			}
		}

		public int StarCount
		{
			get
			{
				return m_starCount;
			}
			set
			{
				if ( m_starCount != value )
				{
					m_starCount = value;
					m_starAreas = new Rectangle[m_starCount];
					Invalidate();
				}
			}
		}

		public bool IsHovering
		{
			get
			{
				return m_hovering;
			}
		}

		public Color OutlineColor
		{
			get
			{
				return m_outlineColor;
			}
			set
			{
				if ( m_outlineColor != value )
				{
					m_outlineColor = value;
					Invalidate();
				}
			}
		}

		public Color HoverColor
		{
			get
			{
				return m_hoverColor;
			}
			set
			{
				if ( m_hoverColor != value )
				{
					m_hoverColor = value;
					Invalidate();
				}
			}
		}

		public Color SelectedColor
		{
			get
			{
				return m_selectedColor;
			}
			set
			{
				if ( m_selectedColor != value )
				{
					m_selectedColor = value;
					Invalidate();
				}
			}
		}

		public int OutlineThickness
		{
			get
			{
				return m_outlineThickness;
			}
			set
			{
				if ( m_outlineThickness != value )
				{
					m_outlineThickness = value;
					Invalidate();
				}
			}
		}

		public double HoverStar
		{
			get
			{
				return m_hoverStar;
			}
		}

		public int SelectedStar
		{
			get
			{
				return m_selectedStar;
			}
		}

		#endregion

		protected override void OnPaint(PaintEventArgs pe)
		{	
			pe.Graphics.Clear(BackColor);

			int starWidth = (Width - (LeftMargin + RightMargin + (StarSpacing * (StarCount - 1)))) / StarCount;
			int starHeight = (Height - (TopMargin + BottomMargin));

			Rectangle drawArea = new Rectangle(LeftMargin, TopMargin, starWidth, starHeight);

			for ( int i = 0 ; i < StarCount; ++i )
			{
				m_starAreas[i].X = drawArea.X - StarSpacing / 2;
				m_starAreas[i].Y = drawArea.Y;
				m_starAreas[i].Width = drawArea.Width + StarSpacing / 2;
				m_starAreas[i].Height = drawArea.Height;

				DrawStar ( pe.Graphics, drawArea, i );
				
				drawArea.X += drawArea.Width + StarSpacing;
			}

			base.OnPaint ( pe );
		}

		protected void DrawStar ( Graphics g, Rectangle rect, int starAreaIndex )
		{	
			Brush fillBrush;
			Pen outlinePen = new Pen ( OutlineColor, OutlineThickness );
			
			if ( m_hovering && m_hoverStar > starAreaIndex )
			{
				fillBrush = new LinearGradientBrush(rect, HoverColor, BackColor, LinearGradientMode.ForwardDiagonal); 
			}
			else if ( (!m_hovering) && m_selectedStar > starAreaIndex )
			{
				fillBrush = new LinearGradientBrush(rect, SelectedColor, BackColor, LinearGradientMode.ForwardDiagonal);
			}
			else
			{
				fillBrush = new SolidBrush ( BackColor );
			}
			
			PointF[] p = new PointF[10];
			p[0].X = rect.X + (rect.Width / 2);
			p[0].Y = rect.Y;
			p[1].X = rect.X + (42 * rect.Width / 64);
			p[1].Y = rect.Y + (19 * rect.Height / 64);
			p[2].X = rect.X + rect.Width;
			p[2].Y = rect.Y + (22 * rect.Height / 64);
			p[3].X = rect.X + (48 * rect.Width / 64);
			p[3].Y = rect.Y + (38 * rect.Height / 64);
			p[4].X = rect.X + (52 * rect.Width / 64);
			p[4].Y = rect.Y + rect.Height;
			p[5].X = rect.X + (rect.Width / 2);
			p[5].Y = rect.Y + (52 * rect.Height / 64);
			p[6].X = rect.X + (12 * rect.Width / 64);
			p[6].Y = rect.Y + rect.Height;
			p[7].X = rect.X + rect.Width / 4;
			p[7].Y = rect.Y + (38 * rect.Height / 64);
			p[8].X = rect.X;
			p[8].Y = rect.Y + (22 * rect.Height / 64);
			p[9].X = rect.X + (22 * rect.Width / 64);
			p[9].Y = rect.Y + (19 * rect.Height / 64);
			p.GetValue()
			g.FillPolygon ( fillBrush, p );
			g.DrawPolygon ( outlinePen, p );
		}

		protected override void OnMouseEnter ( System.EventArgs ea )
		{
			m_hovering = true;
			Invalidate();
			base.OnMouseEnter ( ea );
		}

		protected override void OnMouseLeave ( System.EventArgs ea )
		{
			m_hovering = false;
			Invalidate();
			base.OnMouseLeave ( ea );
		}

		protected override void OnMouseMove ( MouseEventArgs args )
		{
			for ( int i = 0 ; i < StarCount ; ++i )
			{
				if ( m_starAreas[i].Contains(args.X, args.Y) )
				{
					m_hoverStar = i + 1;
					Invalidate();
					break;
				}
			}

			base.OnMouseMove ( args );
		}

		protected override void OnClick ( System.EventArgs args )
		{
			Point p = PointToClient ( MousePosition );

			for ( int i = 0 ; i < StarCount ; ++i )
			{
				if ( m_starAreas[i].Contains(p) )
				{
					m_hoverStar = i + 1;
					m_selectedStar = i + 1;
					Invalidate();
					break;
				}
			}

			base.OnClick ( args );
		}

		#region Protected Data
		
		protected int m_leftMargin = 2;
		protected int m_rightMargin = 2;
		protected int m_topMargin = 2;
		protected int m_bottomMargin = 2;
		protected int m_starSpacing = 8;
		protected int m_starCount = 5;
		protected Rectangle[] m_starAreas;
		protected bool m_hovering = false;
		
		protected double m_hoverStar = 0;
		protected int m_selectedStar = 0;

		protected Color m_outlineColor = Color.DarkGray;
		protected Color m_hoverColor = Color.Yellow;
		protected Color m_selectedColor = Color.RoyalBlue;

		protected int m_outlineThickness = 1;

		#endregion
	}
}
