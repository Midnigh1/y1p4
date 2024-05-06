using GXPEngine;
using System;
using System.Collections.Generic;

class Barrel : Sprite 
{
	private Vec2 toMouse;
	private float targetRotation;
	private float deltaRotation;
	private int activeItem = 0;

    private const int rotationSpeed = 4;
    private const int boostPower = 3;
	private const int bombSpeed = 5;
    private const int circleSpeed = 5;
    private const int length = 70;

	private int[] remainingUses = {3, 4, 2, 2, 4, 2};

	Vec2 lineStart = new Vec2(-1, -1); // i use a point outside the screen as a way to know when we don't have a point selected
    public Barrel() : base("colors.png") 
	{
		SetScaleXY(1, 0.25f);
		SetOrigin(0, height / 2);
	}

    public int[] GetRemainingUses()
    {
        return remainingUses;
    }

    public void SetRemainingUses(int[] array)
    {
        if(array.Length == 6)
        {
            remainingUses = array;
        }
        else
        {
            Console.WriteLine("couldnt set remaining uses as array length is " + array.Length.ToString() + ", needs to be 6");
        }
    }

    public float GetRotation() {
		return rotation;
	}

	private void Rotate() {
		toMouse = new Vec2(Input.mouseX, Input.mouseY) - ((Player)parent).position;
		// targetRotation = (int)(Mathf.Round(toMouse.GetAngleDegrees() - parent.GetRotation()));
		targetRotation = toMouse.GetAngleDegrees();
		deltaRotation = targetRotation - rotation;
		if(deltaRotation < -180) {
			deltaRotation += 360;
		}
		if(deltaRotation > 180) {
			deltaRotation -= 360;
		}
		if(rotation < -180) {
			rotation += 360;
		}
		if(rotation > 180) {
			rotation -= 360;
		}
        if(deltaRotation > rotationSpeed || deltaRotation < -rotationSpeed) { rotation += Mathf.Sign(deltaRotation) * rotationSpeed; }
        else { rotation += deltaRotation;  } // so that we don't have weird jiggling when it reaches around target rotation
		
	}

	void Controls() 
	{
		if (Input.GetKeyDown (Key.Q)) 
		{
			activeItem = 0;
			lineStart.SetXY(-1, -1);
		}
		if (Input.GetKeyDown (Key.W)) 
		{
			activeItem = 1;
            lineStart.SetXY(-1, -1);
        }
		if (Input.GetKeyDown (Key.E)) 
		{
			activeItem = 2;
            lineStart.SetXY(-1, -1);
        }
		if (Input.GetKeyDown(Key.A))
		{
			activeItem = 3;
		}
        if (Input.GetKeyDown(Key.S))
        {
            activeItem = 4;
            lineStart.SetXY(-1, -1);
        }
        if (Input.GetKeyDown(Key.D))
        {
            activeItem = 5;
            lineStart.SetXY(-1, -1);
        }
        if (Input.GetMouseButtonDown(0)) // activate the item
		{
			if (remainingUses[activeItem] > 0) 
			{
                switch (activeItem)
                {
                    case 0:
                        ((Player)parent).velocity -= Vec2.GetUnitVectorDeg(rotation) * boostPower;
                        break;
                    case 1:
                        ((MyGame)parent.parent).AddMover(10, ((Ball)parent).position + Vec2.GetUnitVectorDeg(rotation) * length, velocity: Vec2.GetUnitVectorDeg(rotation) * circleSpeed + ((Ball)parent).velocity, bounciness: 0.98f);
                        break;
                    case 2:
                        ((MyGame)parent.parent).AddBomb(((Ball)parent).position + Vec2.GetUnitVectorDeg(rotation) * length, Vec2.GetUnitVectorDeg(rotation) * bombSpeed + ((Ball)parent).velocity);
                        break;
                    case 3:
                        if (lineStart == new Vec2(-1, -1)) 
                        {
                            lineStart.SetXY(Input.mouseX, Input.mouseY);
                        }
                        else
                        {
                            ((MyGame)parent.parent).AddLine(lineStart, new Vec2(Input.mouseX, Input.mouseY));
                            lineStart.SetXY(-1, -1);
                            remainingUses[activeItem] -= 1;
                        }
                        break;
                    case 4:
                        ((MyGame)parent.parent).AddMover(10, new Vec2(Input.mouseX, Input.mouseY), moving: false, bounciness: 0.98f);
                        break;
                    case 5:
                        ((MyGame)parent.parent).AddBomb(new Vec2(Input.mouseX, Input.mouseY), moving: false);
                        break;
                }
				if(activeItem != 3) { remainingUses[activeItem] -= 1; } // line is lte only item that is not automatically used after click
            }
		}
	}

	public void Update() 
	{
		if(parent!= null) // avoiding scene deloading problems like this
		{
            Rotate();
            Controls();
        }
	}
}
