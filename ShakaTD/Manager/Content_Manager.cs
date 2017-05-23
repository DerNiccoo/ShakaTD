﻿using System;
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
