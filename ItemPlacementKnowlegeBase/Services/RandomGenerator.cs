using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Services
{
    static class RandomGenerator
    {
        private static Random random = new Random();

        public static int getRandomNum()
        {
            return random.Next();
        }
    }
}
