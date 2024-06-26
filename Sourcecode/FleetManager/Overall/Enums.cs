﻿using System;
using System.ComponentModel;

namespace Overall
{
    public enum License
    {
        AM,
        A,
        B,
        C,
        D,
        G
    }
    public enum FuelTypes
    {
        Gasoline,
        Diesel,
        LPG,
        Electric
    }
    public enum RequestType
    {
        Fuelcard,
        Maintenance,
        Repairment
    }
    public enum ResponseType
    {
        [Description("200")]
        OK,
        [Description("400")]
        BadRequest,
        [Description("404")]
        NotFound
    }
    public enum CarTypes
    {
        [Description("Passenger car.")]
        Passengercar,
        [Description("Lightfreight car")]
        LightFreight
    }
    public enum ExtraServices
    {
        [Description("Discount on fuel.")]
        DiscountFuel,
        [Description("Discount on carwash.")]
        DiscountCarwash, 
        [Description("Discount on tires.")]
        DiscountTires

    }
    public enum AuthenticationTypes
    {
        [Description("PIN.")]
        PIN,
        [Description("PIN + KM.")]
        PINKM
    }
}
