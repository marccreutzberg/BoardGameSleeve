using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBoardGameSleeve;
using BoardGameSleeveWebsite.Models;

namespace BoardGameSleeveWebsite.services
{
    public interface IService
    {
        Home HomeModel();
    }

    public class Service : IService
    {
        DataService dataService = new DataService();

        public Home HomeModel()
        {
            Home home = new Home();


            return home;
        }

    }
}