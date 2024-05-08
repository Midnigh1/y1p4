using System;
using GXPEngine;

public class LineEscalator : LineSegment
{
	Vec2 start;
	Vec2 end;
	public int force;
	private bool reverse;
	public LineEscalator (Vec2 lineStart, Vec2 lineEnd, bool pReverse) : base (lineStart, lineEnd, 0xffff0000, 4)
	{
        start = lineStart;
		end = lineEnd;
		force = 3;
		reverse = pReverse;
    }

	public Vec2 collateralVec() {
		if(reverse){ return start - end;}
		return end - start;
	}

	public void Update()
	{
		
	}
}
