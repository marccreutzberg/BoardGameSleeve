using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BoardGameSleeveWebsite
{
    public class VMGames
    {
        public class GamesTable
        {
            public List<string> Sizes;
            public List<Game2> Games;
            public void Sort()
            {
                this.Sizes.Sort();
                this.Games.Sort((Game2 g1, Game2 g2) =>
                {
                    return string.Compare(g1.Name, g2.Name);
                });
            }
        }
        public class Game2
        {
            public string Name;
            public List<string> Sizes;
            public Game2(string name, List<string> sizes)
            {
                this.Name = name;
                this.Sizes = sizes;
                this.Sizes.Sort();
            }
        }

        public GamesTable gamesTable;
        public string GamesTableJson;
        public VMGames(List<Game> games, List<Size> sizes)
        {
            this.gamesTable = new GamesTable();

            List<string> textSizes= new List<string>();
            foreach (Size size in sizes)
                textSizes.Add(size.Name);

            List<Game2> games2 = new List<Game2>();
            foreach (Game game in games)
            {
                List<string> gameSizes = new List<string>();
                foreach (Size size in game.Sizes)
                    gameSizes.Add(size.Name);
                games2.Add(new Game2(game.Name, gameSizes));
            }

            this.gamesTable.Games = games2;
            this.gamesTable.Sizes = textSizes;

            this.gamesTable.Sort();

            this.GamesTableJson = JsonConvert.SerializeObject(gamesTable); ;
        }
    }

    public class VMGames2
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

        public VMGames2(List<Game> games, List<Size> sizes)
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