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
        private IDataService iDataService;

        public Service(IDataService idataService)
        {
            this.iDataService = idataService;
        }

        public Home HomeModel()
        {
            Home home = new Home();
            

            return home;
        }

    }
}