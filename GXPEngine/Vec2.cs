using System;
using GXPEngine;

public struct Vec2 
{
	public float x;
	public float y;

	public Vec2 (float pX = 0, float pY = 0) 
	{
		x = pX;
		y = pY;
	}

	public Vec2 (GXPEngine.Core.Vector2 vec) 
	{
		x=vec.x;
		y=vec.y;
	}

	public static Vec2 operator+ (Vec2 left, Vec2 right) {
		return new Vec2(left.x+right.x, left.y+right.y);
	}

	public override string ToString() 
	{
		return String.Format ("({0},{1})", x, y);
	}

	public static Vec2 operator -(Vec2 left, Vec2 right) {
		return new Vec2 (left.x - right.x, left.y - right.y);
	}

	public static Vec2 operator *(Vec2 vec, float num) {
		return new Vec2 (vec.x * num, vec.y * num);
	}

	public static Vec2 operator *(float num, Vec2 vec) {
		return new Vec2 (vec.x * num, vec.y * num);
	}

    public static Vec2 operator /(Vec2 vec, float num)
    {
		if(num == 0)
		{
			return new Vec2();
		}
        return new Vec2(vec.x / num, vec.y / num);
    }

    public static bool operator ==(Vec2 vec1, Vec2 vec2)
    {
        if (vec1.x == vec2.x && vec1.y == vec2.y)
        {
			return true;
        }
		return false;
    }

    public static bool operator !=(Vec2 vec1, Vec2 vec2)
    {
		return !(vec1 == vec2);
    }

    public void Scale(int num)
    {
        this *= num;
    }

    public float Length() {
		return Mathf.Pow(x * x + y * y, 0.5f);
	}

	public void Normalize() {
		float length = this.Length();
		if (length != 0) {
			x /= length;
			y /= length;
		}
	}

	public Vec2 Normalized() {
		return new Vec2(x / this.Length(), y / this.Length());
	}

	public void SetXY(float pX, float pY)
	{
		x = pX;
		y = pY;
	}

	public static float Deg2Rad(float deg) {
		return deg * Mathf.PI / 180;
	}

	public static float Rad2Deg(float rad) {
		return rad / Mathf.PI * 180;
	}

	public static Vec2 GetUnitVectorRad(float rad) {
		return new Vec2(Mathf.Cos(rad), Mathf.Sin(rad));
	}

	public static Vec2 GetUnitVectorDeg(float deg) {
		return GetUnitVectorRad(Deg2Rad(deg));
	}

	public static Vec2 RandomUnitVector() {
		Random rnd = new Random();
		float rad = ((float)rnd.NextDouble()) * 2 * Mathf.PI;
		return new Vec2(Mathf.Cos(rad), Mathf.Sin(rad));
	}

	public void SetAngleRadians(float rad) {
		float length = this.Length();
		this.SetXY(Mathf.Cos(rad) * length, Mathf.Sin(rad) * length);
	}

	public void SetAngleDegrees(float deg) {
		SetAngleRadians(Deg2Rad(deg));
	}

	public float GetAngleRadians() {
		return Mathf.Atan2(this.y, this.x);
	}

	public float GetAngleDegrees() {
		return Rad2Deg(GetAngleRadians());
	}

	public void RotateRadians(float angle) {
		float length = this.Length();
		float oldAngle = this.GetAngleRadians();
		float newAngle = oldAngle + angle;
		Vec2 newVec = GetUnitVectorRad(newAngle) * length;
		this.SetXY(newVec.x, newVec.y);
	}

	public void RotateDegrees(float angle) {
		float radAngle = Deg2Rad(angle);
		RotateRadians(radAngle);
	}

	public void RotateAroundRadians(float angle, Vec2 point) {
		this -= point;
		RotateRadians(angle);
		this += point;
	}

	public void RotateAroundDegrees(float angle, Vec2 point) {
		RotateAroundRadians(Deg2Rad(angle), point);
	}

	public Vec2 Normal() {
		return new Vec2(-y, x).Normalized();
	}

	public float Dot(Vec2 other) {
		return this.x * other.x + this.y * other.y;
	}

    public float Cross(Vec2 other)
    {
        return this.x * other.y - other.x * this.y;
    }

    public void Reflect(float cor, Vec2 pNormal) {
		this -= (1 + cor) * (this.Dot(pNormal)) *  pNormal;
	}

    // unit tests ___________________________________________________________________

    public static void UnitTest()
    {
        //UNIT TEST DOT
        float expecteddot = (3 * 4) + (5 * 4);
        Vec2 dotvec1 = new Vec2(3, 4);
        Vec2 dotvec2 = new Vec2(4, 5);
        float actualdot = dotvec1.Dot(dotvec2);

        bool dotProductCorrect = Math.Abs(actualdot - expecteddot) < 0.0001f;
        Console.WriteLine("is dotproduct correct?:" + dotProductCorrect);


        // Unit test for Length method
        Vec2 myVec = new Vec2(3, 4);
        float length = myVec.Length();
        Console.WriteLine("is Length method correct?: " + (length == 5));


        // Unit test for SetXY method
        myVec.SetXY(6, 8);
        Console.WriteLine("is SetXY method correct?: " + (myVec.x == 6 && myVec.y == 8));


        // Unit test for Scale method
        myVec.Scale(2);
        Console.WriteLine("is Scale method correct?: " + (myVec.x == 12 && myVec.y == 16));


        // Unit test for Deg2Rad method
        float radians = Vec2.Deg2Rad(90);
        Console.WriteLine("is Deg2Rad method correct?: " + (radians == Mathf.PI / 2));


        // Unit test for Rad2Deg method
        float degrees = Vec2.Rad2Deg(Mathf.PI / 2);
        Console.WriteLine("is Rad2Deg method correct?: " + (degrees == 90));

        // Unit test for GetUnitVectorDeg method
        Vec2 unitVectorDeg = Vec2.GetUnitVectorDeg(45);
        Console.WriteLine("is GetUnitVectorDeg method correct?: " + (unitVectorDeg.x == Mathf.Sqrt(2) / 2 && unitVectorDeg.y == Mathf.Sqrt(2) / 2));


        // Unit test for GetUnitVectorRad method
        Vec2 unitVectorRad = Vec2.GetUnitVectorRad(Mathf.PI / 4);
        Console.WriteLine("is GetUnitVectorRad method correct?: " + (unitVectorRad.x == Mathf.Sqrt(2) / 2 && unitVectorRad.y == Mathf.Sqrt(2) / 2));

        // Unit test for RandomUnitVector
        Vec2 result = Vec2.RandomUnitVector();

        // Check if the result is within the valid range for x and y components
        bool withinRange = result.x >= -1f && result.x <= 1f && result.y >= -1f && result.y <= 1f;

        Console.WriteLine("is RandomUnitVector method correct?:" + withinRange);


        // Unit test for SetAngleDegrees method
        myVec.SetAngleDegrees(45);
        float expectedAngle = 45; // The expected angle in degrees
        float actualAngle = myVec.GetAngleDegrees(); // Get the angle of the vector in degrees
        Console.WriteLine("is SetAngleDegrees method correct?: " + (expectedAngle == actualAngle));

        // Unit test for SetAngleRadians method
        myVec.SetAngleRadians(Mathf.PI / 4);
        float expectedAngleRadianstest = 45; // The expected angle in degrees (since Mathf.PI / 4 is equivalent to 45 degrees)
        float actualAngleRadianstest = myVec.GetAngleDegrees(); // Get the angle of the vector in degrees
        Console.WriteLine("is SetAngleRadians method correct?: " + (expectedAngleRadianstest == actualAngleRadianstest));

        // Unit test for GetAngleRadians method
        float angleRadians = myVec.GetAngleRadians();
        double expectedRadians = Math.Round((Mathf.Atan2(4, 3) + Mathf.Atan2(3, 4)) / 2, 7);
        Console.WriteLine("is GetAngleRadians method correct?: " + ((Math.Round(angleRadians), 5) == (Math.Round(expectedRadians), 5)));


        // Unit test for GetAngleDegrees method
        float angleDegrees = myVec.GetAngleDegrees();
        double num3 = Math.Round((Math.Atan2(3, 4) * 180 / Math.PI), 5);
        double num4 = Math.Round((Math.Atan2(4, 3) * 180 / Math.PI), 5);
        Console.WriteLine("is GetAngleDegrees method correct?: " + (angleDegrees == ((num3 + num4) / 2)));

        // Unit test for RotateDegrees method
        Vec2 test = new Vec2(1, 1); // Create a vector (1, 0)
        test.RotateDegrees(90); // Rotate the vector by 90 degrees
        Console.WriteLine("is RotateDegrees method correct?: " + (Math.Round(test.x, 5) == -1 && Math.Round(test.y, 5) == 1));

        // Unit test for RotateRadians method
        Vec2 testRR = new Vec2(3, 7);
        Vec2 otherTest = new Vec2(-7, 3);
        testRR.RotateRadians((float)Mathf.PI / 2);
        Console.WriteLine("is RotateRadians method correct?: " + (Math.Round(testRR.x, 5) == otherTest.x && Math.Round(testRR.y, 5) == otherTest.y));


        // Unit test for RotateAroundDegrees method
        Vec2 RADtest = new Vec2(2, 2);
        Vec2 pivot = new Vec2(0, 0);
        RADtest.RotateAroundDegrees(45, pivot);
        Console.WriteLine("is RotateAroundDegrees method correct?: " + (Math.Round(RADtest.x, 5) == 0 && Math.Round((RADtest.y), 5) == Math.Round(Math.Sqrt(2) * 2, 5)));

        // Unit test for RotateAroundRadians method
        Vec2 RARtest = new Vec2(2, 2);
        Vec2 pivot2 = new Vec2(0, 0);
        RARtest.RotateAroundRadians(Mathf.PI / 4, pivot2);
        Console.WriteLine("is RotateAroundRadians method correct?: " + (Math.Round(RARtest.x, 5) == 0 && Math.Round(RARtest.y, 5) == Math.Round(Math.Sqrt(2) * 2, 5)));
    }
}