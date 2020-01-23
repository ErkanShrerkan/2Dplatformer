using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Dplatform
{
    class GameObject
    {
        protected bool collision;
        protected int layer;
        protected string sprite;
        public string name;

        public string GetSprite()
        {
            return sprite;
        }

        public bool GetCollision()
        {
            return collision;
        }
    }
}
