using ItemPlacementKnowlegeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Loader
{
    class ONTKnowlegeBaseWriter
    {
        static void Write(KnowlegeBase knowlegeBase)
        {
            Frame[] frames = knowlegeBase.Frames.ToArray<Frame>();
            Domain[] domains = knowlegeBase.Domains.ToArray<Domain>();
        }
    }
}
