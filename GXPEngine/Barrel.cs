using GXPEngine;
using System;
using System.Collections.Generic;

class Spawner : GameObject 
{
	private int activeItem = 0;

	private int[] remainingUses = {3, 4, 2, 2, 4, 2};

	Vec2 lineStart = new Vec2(-1, -1); // i use a point outside the screen as a way to know when we don't have a point selected

    Vec2 oldMouse = new Vec2(-1, -1);

    public Spawner() : base() 
	{

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

	public void Controls() 
	{
		if (Input.GetKeyDown(Key.A))
		{
			activeItem = 0;
		}
        if (Input.GetKeyDown(Key.S))
        {
            activeItem = 1;
            lineStart.SetXY(-1, -1);
        }
        if (Input.GetKeyDown(Key.D))
        {
            activeItem = 2;
            lineStart.SetXY(-1, -1);
        }
        // level making tools
        if (Input.GetKeyDown(Key.Z)) // spike
        {
            activeItem = 3;
            lineStart.SetXY(-1, -1);
        }
        if (Input.GetKeyDown(Key.P)) // player
        {
            activeItem = 4;
            lineStart.SetXY(-1, -1);
        }
        if (Input.GetKeyDown(Key.F)) // finish
        {
            activeItem = 5;
            lineStart.SetXY(-1, -1);
        }

        if (Input.GetMouseButtonDown(0) && ((MyGame)parent)._paused) // activate the item
        {
			if (remainingUses[activeItem] > 0) 
			{
                switch (activeItem)
                {
                    case 0:
                        if (lineStart == new Vec2(-1, -1)) 
                        {
                            lineStart.SetXY(Input.mouseX, Input.mouseY);
                        }
                        else
                        {
                            ((MyGame)parent).AddLine(lineStart, new Vec2(Input.mouseX, Input.mouseY));
                            Console.WriteLine("AddLine(new Vec2" + lineStart.ToString() + ", new Vec2(" + Input.mouseX.ToString() + ", " + Input.mouseY.ToString() + "));");
                            lineStart.SetXY(-1, -1);
                            remainingUses[activeItem] -= 1;
                        }
                        break;
                    case 1:
                        ((MyGame)parent).AddMover(10, new Vec2(Input.mouseX, Input.mouseY), moving: false, bounciness: 0.98f);
                        break;
                    case 2:
                        ((MyGame)parent).AddBomb(new Vec2(Input.mouseX, Input.mouseY), moving: false);
                        Console.WriteLine("_movers.Add(new Bomb(new Vec2(" + Input.mouseX.ToString() + ", " + Input.mouseY.ToString() + ")));");
                        break;
                    case 3:
                        ((MyGame)parent).AddEnemy(20, new Vec2(Input.mouseX, Input.mouseY));
                        Console.WriteLine("_movers.Add(new Enemy(20, new Vec2(" + Input.mouseX.ToString() + ", " + Input.mouseY.ToString() + ")));");
                        break;
                    case 4:
                        ((MyGame)parent).AddPlayer(new Vec2(Input.mouseX, Input.mouseY));
                        Console.WriteLine("_movers.Add(new Player(30, new Vec2(" + Input.mouseX.ToString() + ", " + Input.mouseY.ToString() + ")));");
                        break;
                    case 5:
                        ((MyGame)parent).AddFinish(new Vec2(Input.mouseX, Input.mouseY));
                        Console.WriteLine("_movers.Add(new Finish(new Vec2(" + Input.mouseX.ToString() + ", " + Input.mouseY.ToString() + ")));");
                        break;
                }
				if(activeItem != 0) { remainingUses[activeItem] -= 1; } // line is the only item that is not automatically used after one click
            }
		}
        if (lineStart != new Vec2(-1, -1) && ((MyGame)parent)._paused)
        {

            ((MyGame)parent).RemoveLine(lineStart, oldMouse);
            ((MyGame)parent).AddLine(lineStart, new Vec2(Input.mouseX, Input.mouseY));
            oldMouse = new Vec2(Input.mouseX, Input.mouseY);
        }
    }

	public void Update() 
	{
		
	}
}
