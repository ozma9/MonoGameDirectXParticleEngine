using Microsoft.Xna.Framework;
using ParticleEngine.Classes.Objects;
using ParticleEngine.Enums;
using ParticleEngine.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Classes.Containers
{
    class Fixture
    {
        private Fixturing type;
        private List<ProductHolder> productShelves;
        private List<ShelvingContainer> wareHouseRacking;
        private readonly byte maxCapacity;

        private Vector2 position;
        private Point size;

        public Fixturing Type => type;
        public List<ProductHolder> ProductShelves => productShelves;
        public List<ShelvingContainer> WareHouseRacking => wareHouseRacking;
        public byte MaxCapacity => maxCapacity;


        public Fixture(Fixturing _type, Vector2 _pos)
        {
            type = _type;
            productShelves = new List<ProductHolder>();
            wareHouseRacking = new List<ShelvingContainer>();
            position = _pos;

            switch (type)
            {
                case Fixturing.ShopFloorDisplay:

                    maxCapacity = 4;

                    break;
                case Fixturing.WarehouseRacking:

                    maxCapacity = 5;

                    break;
            }

            for (int x = 0; x <= maxCapacity; x++)
            {
                switch (type)
                {
                    case Fixturing.ShopFloorDisplay:
                        productShelves.Add(new ProductHolder(ProductContainers.DisplayShelf));

                        break;
                    case Fixturing.WarehouseRacking:
                        wareHouseRacking.Add(new ShelvingContainer(ShelvingContainers.WarehouseRack));

                        break;
                }
            }

        }

        public void Draw()
        {


            switch (Type)
            {
                case Fixturing.ShopFloorDisplay:

                    Globals.GlobalVars.SpriteBatch.Draw(Globals.Textures.shelfNew, position, Color.White);

                    for (int x = 0; x <= productShelves.Count - 1; x++)
                    {
                        string _products = "";

                        foreach (Product _p in productShelves[x].Products)
                        {
                            _products += "i:"+_p.ID + "(s:" + _p.Stock + ") ";
                        }


                        Globals.GlobalVars.SpriteBatch.DrawString(
                            Fonts.Calibri,
                            "Shelf" + (x + 1) + " P: " + productShelves[x].Products.Count.ToString() + " C:" + _products,
                            new Vector2(position.X, (position.Y + 128) + (x * 15)),
                            Color.Green);
                    }

                    break;
            }

        }

        public bool AddProductsToShelf(Product _product)
        {
            foreach (ProductHolder _shelves in productShelves)
            {
                if (_shelves.AddProduct(_product)) return true;
            }

            return false;
        }

        public bool AddContainersToRacking(ProductHolder _productHolder)
        {
            foreach (ShelvingContainer _rack in wareHouseRacking)
            {
                if (_rack.AddProductHolder(_productHolder)) return true;
            }

            return false;
        }

        public bool RemoveProductsFromShelf(Product _product)
        {
            foreach (ProductHolder _shelves in productShelves)
            {
                if (_shelves.RemoveProduct(_product)) return true;
            }

            return false;
        }

        public bool RemoveContainersFromRacking(ProductHolder _productHolder)
        {
            foreach (ShelvingContainer _rack in wareHouseRacking)
            {
                if (_rack.RemoveProductHolder(_productHolder)) return true;
            }

            return false;
        }


    }

    class StockTransport
    {
        private Transport Type;
        private List<ProductHolder> ProductShelves;
        private List<ShelvingContainer> Shelving;
        private readonly byte MaxCapacity;

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
        private readonly byte MaxCapacity;

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

        public bool AddProductHolder(ProductHolder _holderToAdd)
        {
            if (ProductHolders.Count < MaxCapacity)
            {
                ProductHolders.Add(_holderToAdd);
                return true;
            }

            return false;
        }

        public bool RemoveProductHolder(ProductHolder _holderToRemove)
        {
            foreach (ProductHolder _foundHolder in ProductHolders)
            {
                if (_foundHolder == _holderToRemove)
                {
                    ProductHolders.Remove(_foundHolder);
                    return true;
                }
            }

            return false;
        }

    }

    class ProductHolder
    {
        private ProductContainers type;
        private List<Product> products;
        private readonly byte facings; //Only used for displayshelf
        private readonly byte fillCapacity; //Only used for displayshelf
        private readonly byte overallMaxCapacity;

        public ProductContainers Type => type;
        public List<Product> Products => products;
        public byte MaxCapacity => overallMaxCapacity;
        public ProductHolder(ProductContainers _type)
        {
            type = _type;
            products = new List<Product>();

            switch (type)
            {
                case ProductContainers.DisplayShelf:
                    facings = 10;
                    fillCapacity = 5;
                    overallMaxCapacity = (byte)(facings * fillCapacity);
                    break;
                case ProductContainers.CardboardBox:
                    overallMaxCapacity = 50;
                    break;
                case ProductContainers.Tote:
                    overallMaxCapacity = 35;
                    break;
                case ProductContainers.CustomerBasket:
                    overallMaxCapacity = 25;
                    break;
                case ProductContainers.CustomerTrolley:
                    overallMaxCapacity = 100;
                    break;
                case ProductContainers.WoWShelf:
                    overallMaxCapacity = 100;
                    break;
            }
        }

        public bool AddProduct(Product _productToAdd)
        {
            if (type == ProductContainers.DisplayShelf)
            {
                foreach (Product _foundProduct in products)
                {
                    if (_productToAdd.ID == _foundProduct.ID && _foundProduct.Stock < fillCapacity)// && products.Count < overallMaxCapacity)
                    {
                        _foundProduct.ModifiyStock(1);
                        return true;
                    }
                    // return false;
                }


            }

            if (products.Count < overallMaxCapacity)
            {
                _productToAdd.ModifiyStock(1);
                products.Add(_productToAdd);
                return true;
            }

            return false;
        }

        public bool RemoveProduct(Product _productToRemove)
        {
            foreach (Product _foundProduct in products)
            {
                if (_foundProduct == _productToRemove)
                {
                    products.Remove(_foundProduct);
                    return true;
                }
            }

            return false;
        }

    }

}

