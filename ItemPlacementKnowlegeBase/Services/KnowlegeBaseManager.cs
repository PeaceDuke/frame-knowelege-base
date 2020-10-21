using ItemPlacementKnowlegeBase.Loader;
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

        public static void Initialise(string filename)
        {
            Test.Test.Filename = filename;
            instance = null;
        }
        public static TestKnowlegeBaseProvider get()
        {
            if (instance == null)
            {
                instance = new TestKnowlegeBaseProvider();
            }

            return instance;
        }
    }
}
