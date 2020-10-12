using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Services
{
    class KnowlegeBaseManager
    {
        private static DummyKnowlegeBaseProvider instance;

        public static IKnowlegeBaseProvider get()
        {
            if (instance == null)
                instance = new DummyKnowlegeBaseProvider();

            return instance;
        }
    }
}
