using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{	
	bool _stepped = false;
	public bool _paused = true;
	int _stepIndex = 0;
	int _startSceneNumber = 1;

    readonly Canvas _lineContainer = null;
    readonly List<Ball> _movers;
    readonly List<LineSegment> _lines;
    readonly Spawner _spawner;

	public EasyDraw gameOver;
	public EasyDraw HUD;

    public MyGame() : base(800, 600, false, false)
    {
        _lineContainer = new Canvas(width, height);
        AddChild(_lineContainer);

        targetFps = 60;

        _movers = new List<Ball>();
        _lines = new List<LineSegment>();
        
        _spawner = new Spawner();
        AddChild(_spawner);

        gameOver = new EasyDraw(game.width, game.height);
        AddChild(gameOver);

        HUD = new EasyDraw(game.width, game.height);
        AddChild(HUD);

        LoadScene(_startSceneNumber);

        PrintInfo();
    }

    public int GetNumberOfLines() {
		return _lines.Count;
	}

	public LineSegment GetLine(int index) {
		if (index >= 0 && index < _lines.Count) {
			return _lines [index];
		}
		return null;	
	}

	public int GetNumberOfMovers() {
		return _movers.Count;
	}

	public Ball GetMover(int index) {
		if (index >= 0 && index < _movers.Count) {
			return _movers [index];
		}
		return null;
	}

    public void RemoveMover(int index)
    {
        if (index >= 0 && index < _movers.Count)
        {
			RemoveChild(_movers[index]);
			_movers.Remove(_movers[index]);
        }
    }

    public void RemoveMover(Ball toRemove)
    {
        RemoveChild(toRemove);
        _movers.Remove(toRemove);
    }

    public void AddMover(int radius, Vec2 position, Vec2 velocity= new Vec2(), bool moving=true, float bounciness=0.6f, byte greenness=200)
    {
		Ball newBall = new Ball(radius, position, velocity, moving, bounciness, greenness);
        _movers.Add(newBall);
		AddChild(newBall);
    }

    public void AddEnemy(int radius, Vec2 position, Vec2 velocity = new Vec2(), bool pDestroyedByWalls=false)
    {
        Ball newBall = new Enemy(radius, position, velocity, pDestroyedByWalls:pDestroyedByWalls);
        _movers.Add(newBall);
        AddChild(newBall);
    }

    public void AddBomb(Vec2 position, Vec2 velocity = new Vec2(), bool moving=true)
    {
        Ball newBall = new Bomb(position, velocity, moving);
        _movers.Add(newBall);
        AddChild(newBall);
    }

    public Player GetPlayer()
    {
        foreach (Ball mover in _movers)
        {
            if (mover is Player)
            {
				return (Player)mover;
            }
        }
		return null;
    }

	public void RemovePlayer()
	{
        foreach (Ball mover in _movers)
        {
            if (mover is Player)
            {
				_movers.Remove(mover);
                mover.Destroy();
				break;
            }
        }
    }

    public void DrawLine(Vec2 start, Vec2 end) {
		_lineContainer.graphics.DrawLine(Pens.White, start.x, start.y, end.x, end.y);
	}

	
	public void AddLine (Vec2 start, Vec2 end) {
		LineSegment line = new LineSegment (start, end, 0xff00ff00, 4);
        AddChild(line);
        _lines.Add(line);

        LineSegment lineBack = new LineSegment(end, start, 0xff00ff00, 4);
        AddChild(lineBack);
        _lines.Add (lineBack);
	}

    public void RemoveLine(Vec2 start, Vec2 end)
    {
        // Find and remove the forward line segment
        LineSegment lineToRemove = _lines.Find(line => line.start == start && line.end == end);
        if (lineToRemove != null)
        {
            RemoveChild(lineToRemove);
            _lines.Remove(lineToRemove);
        }

        // Find and remove the backward line segment
        LineSegment lineBackToRemove = _lines.Find(line => line.start == end && line.end == start);
        if (lineBackToRemove != null)
        {
            RemoveChild(lineBackToRemove);
            _lines.Remove(lineBackToRemove);
        }
    }


    public void Pause()
	{
		_paused = true;
	}

    public void UnPause()
    {
        _paused = false;
    }

	void LoadScene(int sceneNumber) {
		_startSceneNumber = sceneNumber;
        // remove previous scene:
        
        foreach (Ball mover in _movers) {
			mover.Destroy();
		}
		_movers.Clear();
		foreach (LineSegment line in _lines) {
			line.Destroy();
		}
		_lines.Clear();

		gameOver.ClearTransparent();
		Pause();
		
		// boundary:
		AddLine (new Vec2 (width, height), new Vec2 (0, height));
		AddLine (new Vec2 (0, height), new Vec2 (0, 0));
		AddLine (new Vec2 (0, 0), new Vec2 (width, 0));
		AddLine (new Vec2 (width, 0), new Vec2 (width, height));

		switch (sceneNumber) {
			case 1: // pretty much sandbox
                _movers.Add(new Player(30, new Vec2(200, 300)));
                _movers.Add(new Finish(new Vec2(770, 570)));

				int[] itemUses = new int[] { 5, 5, 5, 5, 5, 5 }; // this being declared here might break something but it shouldn't
                _spawner.SetRemainingUses(itemUses);
                break;
            case 2: // test level
                itemUses = new int[] { 5, 5, 5, 5, 5, 5 };
                _spawner.SetRemainingUses(itemUses);

                _movers.Add(new Player(30, new Vec2(700, 100)));
                _movers.Add(new Enemy(20, new Vec2(650, 500)));
                _movers.Add(new Enemy(20, new Vec2(700, 500)));
                _movers.Add(new Enemy(20, new Vec2(750, 500)));
                _movers.Add(new Enemy(20, new Vec2(250, 400)));
                _movers.Add(new Enemy(20, new Vec2(275, 450)));
                _movers.Add(new Enemy(20, new Vec2(300, 500)));
                AddLine(new Vec2(200, 400), new Vec2(200, 600));
                _movers.Add(new Finish(new Vec2(100, 500)));
                break;
            /*case 2: // one shooter
                _movers.Add(new Player(30, new Vec2(200, 150)));
                _movers.Add(new ShootingEnemy(new Vec2(400, 50), new Vec2(0, 1)));
                AddLine(new Vec2(370, height), new Vec2(370, 300));
                _movers.Add(new Finish(new Vec2(700, 500)));
                break;
            case 3: // shooters hall
                _movers.Add(new Player(30, new Vec2(200, 400)));
                _movers.Add(new ShootingEnemy(new Vec2(350, 550), new Vec2(0, -1)));
                _movers.Add(new ShootingEnemy(new Vec2(400, 550), new Vec2(0, -1)));
                _movers.Add(new ShootingEnemy(new Vec2(450, 550), new Vec2(0, -1)));
                _movers.Add(new ShootingEnemy(new Vec2(500, 550), new Vec2(0, -1)));
                _movers.Add(new ShootingEnemy(new Vec2(550, 550), new Vec2(0, -1)));
                
                _movers.Add(new Finish(new Vec2(700, 400)));
                AddLine(new Vec2(300, 0), new Vec2(300, 300));
                AddLine(new Vec2(600, 300), new Vec2(300, 300));
                break;
            case 4: // clear the path
                _movers.Add(new Player(30, new Vec2(200, 300)));
                _movers.Add(new Ball(40, new Vec2(500, 100)));
                _movers.Add(new Ball(40, new Vec2(500, 200)));
                _movers.Add(new Ball(40, new Vec2(500, 300)));
                _movers.Add(new Ball(40, new Vec2(500, 400)));
                _movers.Add(new Ball(40, new Vec2(500, 500)));
                _movers.Add(new Ball(40, new Vec2(400, 50)));
                _movers.Add(new Ball(40, new Vec2(400, 150)));
                _movers.Add(new Ball(40, new Vec2(400, 250)));
                _movers.Add(new Ball(40, new Vec2(400, 350)));
                _movers.Add(new Ball(40, new Vec2(400, 450)));
                _movers.Add(new Ball(40, new Vec2(400, 550)));
                _movers.Add(new Finish(new Vec2(700, 400)));
                break;
			
			case 6: // twisted path
                _movers.Add(new Player(30, new Vec2(200, 100)));
                AddLine(new Vec2(400, height), new Vec2(400, 200));
                AddLine(new Vec2(600, 0), new Vec2(600, 400));
                _movers.Add(new Finish(new Vec2(700, 100)));
                _movers.Add(new Enemy(20, new Vec2(450, 570)));
                _movers.Add(new Enemy(20, new Vec2(500, 570)));
                _movers.Add(new Enemy(20, new Vec2(550, 570)));
                _movers.Add(new Enemy(20, new Vec2(570, 30)));
                _movers.Add(new Enemy(20, new Vec2(570, 80)));
                _movers.Add(new Enemy(20, new Vec2(570, 130)));
                itemUses = new int[] { 1, 0, 0, 2, 0, 0 };
                _spawner.SetRemainingUses(itemUses);
                break;	*/
            default: // same as case 1
                _movers.Add(new Player(30, new Vec2(200, 300)));
                _movers.Add(new Ball(30, new Vec2(400, 340)));
                _movers.Add(new Finish(new Vec2(700, 500)));
                break;
        }
		_stepIndex = -1;
		foreach (Ball b in _movers) {
			AddChild(b);
		}
    }

	/****************************************************************************************/

	void PrintInfo() {
        /*Console.WriteLine("Hold spacebar to slow down the frame rate.");
		Console.WriteLine("Use arrow keys and backspace to set the gravity.");
		Console.WriteLine("Press S to toggle stepped mode.");
		Console.WriteLine("Press P to toggle pause.");
		Console.WriteLine("Press D to draw debug lines.");
		Console.WriteLine("Press C to clear all debug lines.");
		Console.WriteLine("Press R to reset scene, and numbers to load different scenes.");
		Console.WriteLine("Press B to toggle high/low bounciness.");
		Console.WriteLine("Press W to toggle extra output text.");*/

        Console.WriteLine("Press A/S/D to select ability controlled by mouse");
        Console.WriteLine("Press left mouse button to activate the ability");
        Console.WriteLine("A - draw a line segment (first click selects the start, second click selects the end)");
        Console.WriteLine("S - spawns a small ball");
        Console.WriteLine("D - spawns a bomb");
        Console.WriteLine("Yellow balls are regular ones, orange will kill you and red also shoot small orange balls");
        Console.WriteLine("Big green ball is finish");
        Console.WriteLine("Press a number from 1 to 6 to select level");
        Console.WriteLine("Press R to reset level");
    }

	void HandleInput() {
		// targetFps = Input.GetKey(Key.SPACE) ? 5 : 60;
		/*if (Input.GetKeyDown (Key.UP)) {
			Ball.acceleration.SetXY (0, -1);
		}
		if (Input.GetKeyDown (Key.DOWN)) {
			Ball.acceleration.SetXY (0, 1);
		}
		if (Input.GetKeyDown (Key.LEFT)) {
			Ball.acceleration.SetXY (-1, 0);
		}
		if (Input.GetKeyDown (Key.RIGHT)) {
			Ball.acceleration.SetXY (1, 0);
		}
		if (Input.GetKeyDown (Key.BACKSPACE)) {
			Ball.acceleration.SetXY (0, 0);
		}
		if (Input.GetKeyDown (Key.S)) {
			_stepped ^= true;
		}
		if (Input.GetKeyDown (Key.D)) {
			Ball.drawDebugLine ^= true;
		}
		if (Input.GetKeyDown(Key.P))
		{
			_paused ^= true;
		}
		if (Input.GetKeyDown(Key.B))
		{
			Ball.bounciness = 1.5f - Ball.bounciness;
		}
		if (Input.GetKeyDown(Key.W)) {
			Ball.wordy ^= true;
		}
		if (Input.GetKeyDown (Key.C)) {
			_lineContainer.graphics.Clear (Color.Black);
		}*/
		if (Input.GetKeyDown (Key.R)) {
			LoadScene (_startSceneNumber);
		}
		for (int i = 0; i < 10; i++)
		{
			if (Input.GetKeyDown(48 + i))
			{
				LoadScene(i);
			}
		}

        if (Input.GetKeyDown(Key.SPACE))
        {
            UnPause();
        }
    }

	void StepThroughMovers() {
		if (_stepped) { // move everything step-by-step: in one frame, only one mover moves
			_stepIndex++;
			if (_stepIndex >= _movers.Count) {
				_stepIndex = 0;
			}
			if (_movers [_stepIndex].moving) {
				_movers [_stepIndex].Step ();
			}
		} else { // move all movers every frame
			for (int i=GetNumberOfMovers(); i >= 0; i--) {
				Ball mover = GetMover(i);
				if (mover != null && mover.moving) {
					mover.Step ();
				}
			}
		}
	}

    void Update () {
		HandleInput();
		if (!_paused) {
			StepThroughMovers ();
		}
		HUD.ClearTransparent();
		if (GetPlayer() != null) 
		{
            int[] uitext = _spawner.GetRemainingUses();
            HUD.Text(uitext[0].ToString() + " " + uitext[1].ToString() + " " + uitext[2].ToString() + "\n" + uitext[3].ToString() + " " + uitext[4].ToString() + " " + uitext[5].ToString(), 20, 80);
        }
    }

	static void Main() {
		new MyGame().Start();
	}
}