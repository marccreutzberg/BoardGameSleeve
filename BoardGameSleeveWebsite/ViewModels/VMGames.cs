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
        public class GameItemFirst
        {
			public string Name;
			public List<Tuple<int, string>> Sizes; //<id,name>
            public GameItemFirst(Game game)
            {
				this.Name = game.Name;
				this.Sizes = new List<Tuple<int, string>>();
				foreach (Size size in game.Sizes)
					this.Sizes.Add(new Tuple<int, string>(size.ID, size.Name));
				this.Sizes.Sort((Tuple<int, string> t1, Tuple<int, string> t2) =>
				{
					return String.Compare(t1.Item2, t2.Item2);
				});
            }
        }
        public class SizeItemFirst
        {
			public int Id;
            public string Name;
            public List<string> Games;
            public SizeItemFirst(Size size)
            {
				this.Id = size.ID;
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