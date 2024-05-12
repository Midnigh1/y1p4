using System;
using GXPEngine.Core;
using GXPEngine.OpenGL;

namespace GXPEngine
{
	/// <summary>
	/// Implements an OpenGL line
	/// </summary>
	public class LineSegment : GameObject
	{
		public Vec2 start;
		public Vec2 end;

		public uint color = 0xffffffff;
		public uint lineWidth = 1;

		public bool magnet = false;

		public LineSegment (float pStartX, float pStartY, float pEndX, float pEndY, uint pColor = 0xffffffff, uint pLineWidth = 1)
			: this (new Vec2 (pStartX, pStartY), new Vec2 (pEndX, pEndY), pColor, pLineWidth)
		{
		}

		public LineSegment (Vec2 pStart, Vec2 pEnd, uint pColor = 0xffffffff, uint pLineWidth = 1)
		{
			start = pStart;
			end = pEnd;
			color = pColor;
			lineWidth = pLineWidth;
		}
		
		void Update()
		{
			// Check if the mouse is within the bounding box of the line segment
			if (Input.mouseX > Mathf.Min(start.x, end.x) &&
				Input.mouseX < Mathf.Max(start.x, end.x) &&
				Input.mouseY > Mathf.Min(start.y, end.y) &&
				Input.mouseY < Mathf.Max(start.y, end.y))
			{
				// Mouse is hovering over the line segment, change color to red
				color = 0xffff0000;
			}


			if (color == 0xffffff00)
            {
				magnet = true;
			} else
			{
				magnet = false;
			}
		}		
		//------------------------------------------------------------------------------------------------------------------------
		//														RenderSelf()
		//------------------------------------------------------------------------------------------------------------------------
		override protected void RenderSelf(GLContext glContext) {
			if (game != null) {
				Gizmos.RenderLine(start.x, start.y, end.x, end.y, color, lineWidth);
			}
		}
	}
}

