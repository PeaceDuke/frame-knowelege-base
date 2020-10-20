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

        public static TestKnowlegeBaseProvider get()
        {
            ONTKnowlegeBaseLoader.Parce("D:\\Projects\\frame-knowelege-base\\Ontolis\\ontolis-meta\\ontolis\\examples\\Онтология курсовой.ont");
            if (instance == null)
            {
                instance = new TestKnowlegeBaseProvider();
            }

            return instance;
        }
    }
}
