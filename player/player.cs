using Godot;
using System;

public class player : KinematicBody2D
{
    int Acceleration= 20;
    int Decceleration = 5;
    float StopDecceleration = 10.1f;

    float MaxSpeed = 100.0f;

    int InputX = 0;
    int InputY = 0;

    Vector2 Velocite = new Vector2();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    /// <summary>
    /// S'execute 60 fois par secondes en tout temps.
    /// </summary>
    /// <param name="delta">Delais entre chaque frame</param>
    public override void _PhysicsProcess(float delta) {
        Controls();
        Movement();
        MoveAndSlide(Velocite, new Vector2());
    }


    ///<summary>
    /// Definie les Inputs
    ///</summary>
    public void Controls()
    {
        
        var Left = Input.IsActionPressed("ui_left");
        var Right = Input.IsActionPressed("ui_right");
        var Up = Input.IsActionPressed("ui_up");
        var Down = Input.IsActionPressed("ui_down");
        
        if(Left)
            InputX = -1;
        else if (Right)
            InputX = 1;
        else
            InputX= 0;

        if(Down)
            InputY = 1;
        else if (Up)
            InputY = -1;
        else
            InputY = 0;
    }

    /// <summary>
    /// Gere l'acceleration et la deceleration du joueur
    /// </summary>
    public void Movement()
    {
        //Note: Mathf.Sign() sert a avoir le signe
        //      Utile pour avoir la direction actuellement regarde par le joueur
        //      Ex: Mathf.Sign(-333) = -1

        //Gere la direction et sa vitesse
        
        Velocite.x += InputX * Acceleration;
        Velocite.y += InputY * Acceleration;

        if (InputX != 0 && InputY != 0)
            MaxSpeed = 75 / 1.25f;
        else
            MaxSpeed = 75.0f;

        //Attribue la vitesse maximum en tout temps une fois atteinte
        if (Mathf.Abs(Velocite.x) > MaxSpeed)
            Velocite.x = Mathf.Sign(Velocite.x) * MaxSpeed;
        if(Mathf.Abs(Velocite.y) > MaxSpeed)
            Velocite.y = Mathf.Sign(Velocite.y) * MaxSpeed;

        //Diminue graduellement la vitesse de mouvement
        if(InputX == 0)
            Velocite.x -= Mathf.Sign(Velocite.x) * Decceleration;
        if(InputY == 0)
            Velocite.y -= Mathf.Sign(Velocite.y) * Decceleration;

        //Cancel la decceleration de depart
        if (Mathf.Abs(Velocite.x) < StopDecceleration && InputX == 0)
            Velocite.x = 0;
        if (Mathf.Abs(Velocite.y) < StopDecceleration && InputY == 0)
            Velocite.y = 0;
        GD.Print(Velocite.Normalized().Length());
    }
}
