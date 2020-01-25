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
        protected float[] position = new float[2];

        public Character()
        {
            collision = true;
            sprite = "X";
            layer = 0;
        }

        public float GetSpeed()
        {
            return speed;
        }

        public float[] Pos  // property
        {
            get { return position; }   // get method
            set { position = value; }   // set method
        }
    }
}
