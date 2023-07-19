using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Enums
{
//      Fixturing -> 	ShopfloorDisplay -> Shelf -> Product.
//		                WarehouseRacking -> Rack -> CardboardBox/Tote.
//      Transport -> 	Cage -> Shelf -> CardboardBox/Tote.
//		                WOWTrolley -> Shelf -> Product.
//		                CustomerBasket -> Product.
//                      CustomerTrolley -> Product.
//      Rubbish ->	    RecyclingBin -> CardboardBox(empty).
//                      RubbishBin -> Rubbish/Plastic.

    enum Fixturing
    {
        ShopFloorDisplay, WarehouseRacking
    }
    enum Transport
    {
        Cage, WowTrolley
    }

    enum ShelvingContainers
    {
        CageShelf, WarehouseRack
    }

    enum ProductContainers
    {
        DisplayShelf, CardboardBox, Tote, CustomerBasket, CustomerTrolley, WoWShelf
    }

    enum Rubbish
    {
        RecyclingBin, RubbishBin
    }

}
