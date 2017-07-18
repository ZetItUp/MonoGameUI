using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AtriLib3;
using AtriLib3.UI;
using AtriLib3.Utility;

namespace MonoGameUIWindows
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        UIManager ui;
        Monitor monitor;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            UIManager.KeyboardDispatcher = new KeyboardDispatcher(Window);
            Components.Add(new Input(this));
            Components.Add(new AMouse(this));
            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            monitor = new Monitor(graphics);
            monitor.SetResolution(Resolutions.R1024x768);
            ui = new UIManager(monitor);
            ui.LoadContent(Content);


            SpriteFont font = Content.Load<SpriteFont>("GameFont");

            ControlGraphicsData controlData = new ControlGraphicsData(8);
            controlData.SetDataByDimension();
            ui.SetWindowData(controlData);

            Window wnd;
            wnd = new Window("wnd1", new Rectangle(100, 100, 234, 229));
            Label lblWnd1 = new Label("lblWin1", wnd, font, new Rectangle(10, 10, 10, 35));
            wnd.ZOrder = 0.2f;
            lblWnd1.AutoSize = false;
            lblWnd1.Text = "Window 1";
            lblWnd1.CastShadow = true;

            Window anotherWindow = new Window("wnd2", new Rectangle(150, 150, 220, 200));
            Label lblWnd2 = new Label("lblWin2", anotherWindow, font, new Rectangle(10, 20, 100, 15));
            lblWnd2.Text = "Window 2 Is Clipping the Text";
            lblWnd2.CastShadow = true;
            anotherWindow.LockToScreen = true;

            Button btnTest = new Button("btnTest", anotherWindow, font, new Rectangle(10, 100, 100, 40));
            btnTest.Text = "Test";

            CheckBox chkTest = new CheckBox("chkTest", anotherWindow, font, new Rectangle(10, 50, 100, 15));
            chkTest.Text = "A CheckBox :>";
            chkTest.CastShadow = true;

            TextBox txtTest = new TextBox("txtTest", anotherWindow, font, new Rectangle(10, 150, 100, 20));
            txtTest.Text = "NOOB";

            anotherWindow.ZOrder = 0.5f;
            ui.AddControl(wnd);
            ui.AddControl(anotherWindow);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ui.Update(gameTime);

            var win1 = UIManager.ActiveInstance.GetControl<Window>("wnd1");
            var win2 = UIManager.ActiveInstance.GetControl<Window>("wnd2");
            var lbl1 = win1.GetControl<Label>("lblWin1");
            var lbl = win1.GetControl("lblWin1");
            var lbl2 = win2.GetControl<Label>("lblWin2");

            lbl1.Text = $"ZOrder: { lbl1.ZOrder.ToString() }  Movable: { win1.CanMove.ToString() }";
            lbl2.Text = $"ZOrder: { lbl2.ZOrder.ToString() }  Movable: { win2.CanMove.ToString() }";

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            ui.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
