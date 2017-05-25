using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ShakaTD.Screens;

namespace ShakaTD.Manager
{
    enum AllScreens
    {
        Start,
        Menü,
        Pause,
        Gameplay,
        Victory,
        Gameover
    }
    class Screen_Manager
    {
        Dictionary<AllScreens, Screen> Screens = new Dictionary<AllScreens, Screen>();
        Screen CurrentScreen;

        public Screen_Manager(Game game)
        {
            createScreenList();
            CurrentScreen = Screens[AllScreens.Gameplay];
        }

        public void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime); //Updatet den Aktuellen Screen
            if (CurrentScreen.NeedChange()) //Wenn der Screen geändert werden soll
            {
                Screens.Clear(); //Alle löschen damit keine alten Daten vorhanden sind
                createScreenList(); //Alle neu laden
                CurrentScreen = Screens[CurrentScreen.ChangeScreen()]; //Aktuellen Screen auf den neu ausgewählten setzen
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(spriteBatch);
        }

        private void createScreenList()
        {
            Screens.Add(AllScreens.Gameplay, new GamePlayScreen());
            Screens.Add(AllScreens.Victory, new VictoryScreen());
        }
    }
}
