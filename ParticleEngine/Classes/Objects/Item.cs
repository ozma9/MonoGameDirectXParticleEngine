using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Classes.Objects
{
    class Product
    {
        private int id;
        private string name;
        private string description;
        private int category;
        private int section;
        private decimal price;
        private byte stock;

        public int ID => id;
        public string Name => name;
        public string Description => description;
        public int Category => category;
        public int Section => section;
        public decimal Price => price;
        public byte Stock => stock;

        public Product()
        {

        }
    }

    class Rubbish
    {

    }

    class Recycling
    {

    }
}
