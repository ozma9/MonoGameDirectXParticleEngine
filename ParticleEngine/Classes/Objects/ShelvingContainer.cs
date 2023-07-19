using ParticleEngine.Classes.Objects;
using ParticleEngine.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Classes.Containers
{
    class Fixture
    {
        private Fixturing Type;
        private List<ProductHolder> ProductShelves;
        private List<ShelvingContainer> Shelving;
        private readonly int MaxCapacity;

        public Fixture(Fixturing _type)
        {
            Type = _type;
            ProductShelves = new List<ProductHolder>();
            Shelving = new List<ShelvingContainer>();

            switch (Type)
            {
                case Fixturing.ShopFloorDisplay:

                    MaxCapacity = 4;

                    break;
                case Fixturing.WarehouseRacking:

                    MaxCapacity = 5;

                    break;
            }

            for (int x = 0; x <= MaxCapacity; x++)
            {
                switch (Type)
                {
                    case Fixturing.ShopFloorDisplay:
                        ProductShelves.Add(new ProductHolder(ProductContainers.DisplayShelf));

                        break;
                    case Fixturing.WarehouseRacking:
                        Shelving.Add(new ShelvingContainer(ShelvingContainers.WarehouseRack));

                        break;
                }
            }

        }
    }

    class StockTransport
    {
        private Transport Type;
        private List<ProductHolder> ProductShelves;
        private List<ShelvingContainer> Shelving;
        private readonly int MaxCapacity;

        public StockTransport(Transport _type)
        {
            Type = _type;
            ProductShelves = new List<ProductHolder>();
            Shelving = new List<ShelvingContainer>();

            switch (Type)
            {
                case Transport.Cage:
                    MaxCapacity = 3;
                    break;
                case Transport.WowTrolley:
                    MaxCapacity = 4;
                    break;
            }

            for (int x = 0; x <= MaxCapacity; x++)
            {
                switch (Type)
                {
                    case Transport.Cage:
                        Shelving.Add(new ShelvingContainer(ShelvingContainers.CageShelf));

                        break;
                    case Transport.WowTrolley:
                        ProductShelves.Add(new ProductHolder(ProductContainers.WoWShelf));

                        break;
                }
            }

        }
    }

    class ShelvingContainer
    {
        private ShelvingContainers Type;
        private List<ProductHolder> ProductHolders;
        private readonly int MaxCapacity;

        public ShelvingContainer(ShelvingContainers _type)
        {
            Type = _type;
            ProductHolders = new List<ProductHolder>();

            switch (Type)
            {
                case ShelvingContainers.CageShelf:
                    MaxCapacity = 6;
                    break;
                case ShelvingContainers.WarehouseRack:
                    MaxCapacity = 8;
                    break;
            }

        }
    }

    class ProductHolder
    {
        private ProductContainers Type;
        private List<Product> Products;
        private readonly int MaxCapacity;
        public ProductHolder(ProductContainers _type)
        {
            Type = _type;
            Products = new List<Product>();

            switch (Type)
            {
                case ProductContainers.DisplayShelf:
                    MaxCapacity = 10;
                    break;
                case ProductContainers.CardboardBox:
                    MaxCapacity = 20;
                    break;
                case ProductContainers.Tote:
                    MaxCapacity = 30;
                    break;
                case ProductContainers.CustomerBasket:
                    MaxCapacity = 25;
                    break;
                case ProductContainers.CustomerTrolley:
                    MaxCapacity = 100;
                    break;
                case ProductContainers.WoWShelf:
                    MaxCapacity = 20;
                    break;
            }
        }

    }

}

