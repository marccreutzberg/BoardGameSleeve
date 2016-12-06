using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BoardGameSleeveWebsite.ViewModels
{
    public class VMGames
    {
        public class GameItemFirst
        {
            public string Name;
            public string SizeText;
            public GameItemFirst(Game game)
            {
                this.Name = game.Name;
                List<string> sizesText = new List<string>();

                foreach (var item in game.Sizes)
                    sizesText.Add(item.Name);
                sizesText.Sort();

                StringBuilder sText = new StringBuilder();
                foreach (var item in sizesText)
                    sText.Append(item + ", ");
                sText.Remove(sText.Length - 2, 2);
                this.SizeText = sText.ToString();
            }
        }
        public class SizeItemFirst
        {
            public string Name;
            public List<string> Games;
            public SizeItemFirst(Size size)
            {
                this.Name = size.Name;
                this.Games = new List<string>();
                foreach (var item in size.Games)
                    this.Games.Add(item.Name);
                this.Games.Sort();
            }
        }

        public List<GameItemFirst> gameItems;
        public List<SizeItemFirst> sizeItems;

        public VMGames(List<Game> games, List<Size> sizes)
        {
            this.gameItems = new List<GameItemFirst>();
            this.sizeItems = new List<SizeItemFirst>();
            foreach (Game game in games)
                this.gameItems.Add(new GameItemFirst(game));
            foreach (Size size in sizes)
                this.sizeItems.Add(new SizeItemFirst(size));

            this.gameItems.Sort((GameItemFirst g1, GameItemFirst g2) =>
            {
                return string.Compare(g1.Name, g2.Name);
            });

            this.sizeItems.Sort((SizeItemFirst s1, SizeItemFirst s2) =>
            {
                return string.Compare(s1.Name, s2.Name);
            });
        }
    }
}