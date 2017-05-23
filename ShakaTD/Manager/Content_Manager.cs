using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ShakaTD.Manager
{
    /// <summary>  
    ///  Stellt alle Grafiken bereit, da ich hier ein relativ kleines Spiel habe werde einfach immer alle Grafiken bereit gehalten.
    ///  Noch eine "unschönere" Lösung aber eine dennoch akzeptable.
    /// </summary>  
    class Content_Manager
    {
        private static Content_Manager instance;
        ContentManager CM;

        public Dictionary<String, Texture2D> Textures;

        private Content_Manager()
        {
            Textures = new Dictionary<string, Texture2D>();
        }

        public static Content_Manager getInstance()
        {
            if (instance == null)
                instance = new Content_Manager();
            return instance;
        }

        public void LoadTextures(ContentManager Content)
        {
            CM = Content;

            AddTexture("Grafiken/Tiles/Gras", "gras");
            AddTexture("Grafiken/Tiles/Sand", "sand");

            AddTexture("Grafiken/Enemys/enemy1", "enemy1");
            AddTexture("Grafiken/Enemys/enemy2", "enemy2");
            AddTexture("Grafiken/Enemys/enemy3", "enemy3");
            AddTexture("Grafiken/Enemys/enemy4", "enemy4");

            AddTexture("Grafiken/Tower/gun1", "gun1");
            AddTexture("Grafiken/Tower/gun2", "gun2");
            AddTexture("Grafiken/Tower/gunfire1", "gunfire1");
            AddTexture("Grafiken/Tower/missle1", "missle1");
            AddTexture("Grafiken/Tower/missle2", "missle2");
            AddTexture("Grafiken/Tower/plattform1", "plattform1");
            AddTexture("Grafiken/Tower/plattform2", "plattform2");
            AddTexture("Grafiken/Tower/plattform3", "plattform3");
            AddTexture("Grafiken/Tower/plattform4", "plattform4");
            AddTexture("Grafiken/Tower/rocket1", "rocket1");
            AddTexture("Grafiken/Tower/rocket2", "rocket2");
            AddTexture("Grafiken/Tower/rocket3", "rocket3");
            AddTexture("Grafiken/Tower/rocket4", "rocket4");

            AddTexture("Grafiken/UI/BuyMenu", "buyMenu");
        }

        private void AddTexture(String file, String name = "")
        {
            Texture2D newTexture = CM.Load<Texture2D>(file);
            if (name == "")
                Textures.Add(file, newTexture);
            else
                Textures.Add(name, newTexture);
        }
    }
}
