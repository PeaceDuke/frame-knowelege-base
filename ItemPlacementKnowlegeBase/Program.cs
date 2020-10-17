using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemPlacementKnowlegeBase.Models;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;
using ItemPlacementKnowlegeBase.Services;
using ItemPlacementKnowlegeBase.Test;

namespace ItemPlacementKnowlegeBase
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Run(new Main_form());

            //Test.Test.Do();
        }
    }
}
