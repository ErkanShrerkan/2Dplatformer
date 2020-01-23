using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Dplatform
{
    class Ground : GameObject
    {
        public Ground()
        {
            collision = true;
            sprite = "----------";
            layer = 0;
            name = "Ground";
        }
    }
}
