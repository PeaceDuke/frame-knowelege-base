using ItemPlacementKnowlegeBase.Loader;
using ItemPlacementKnowlegeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Services
{
    class KnowlegeBaseManager
    {
        private static TestKnowlegeBaseProvider instance;

        public static void Initialise(KnowlegeBase knowlegeBase)
        {
            instance = new TestKnowlegeBaseProvider(knowlegeBase);
        }
        public static TestKnowlegeBaseProvider get()
        {
            return instance;
        }
    }
}
