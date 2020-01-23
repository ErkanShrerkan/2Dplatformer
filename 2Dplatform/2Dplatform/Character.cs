using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Dplatform
{
    class Character : GameObject
    {
        protected int hp;
        protected float speed = 5.0f;
        public float[] position = new float[2];

        public Character()
        {
            collision = true;
            sprite = "X";
            layer = 0;
        }
    }
}
