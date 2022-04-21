using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoboMines
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class HoboMines : Game
    {
        GraphicsDeviceManager graphics;   
        Board board;
        GameOverScreen goScreen;
      
        Xturn xturn;
        Oturn oturn;
        EndGame endgame;
       
        StateMachine stateMachine = new StateMachine();
        public HoboMines()
        {
            graphics = new GraphicsDeviceManager(this);
           
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            board = new Board(this);
            goScreen = new GameOverScreen(this);

            board.Players = new System.Collections.Generic.Dictionary<string, Player>();
            board.Players.Add("X",new Player(Board.cellvalue.X));
            board.Players.Add("O",new Player(Board.cellvalue.O));
            xturn = new Xturn(board);
            oturn = new Oturn(board);
            endgame = new EndGame(board);
            graphics.PreferredBackBufferWidth = 700;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 800;   // set this value to the desired height of your window
            graphics.ApplyChanges();

            stateMachine.ChangeState(xturn);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
           
          
         
            board.Load(graphics);
            goScreen.Load(graphics);
           

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            stateMachine.Update();
            if (board.explodemine) 
            {
                stateMachine.ChangeState(endgame);
            }
            else {
                if (board.Players["X"].HasMoved)
                {
                    stateMachine.ChangeState(oturn);
                }
                if (board.Players["O"].HasMoved)
                {
                    stateMachine.ChangeState(xturn);
                }
            }
          

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            board.Draw();
            if (board.explodemine)
            {
                goScreen.Draw(board);
            }
           
            
            base.Draw(gameTime);
        }
       
    }
}
