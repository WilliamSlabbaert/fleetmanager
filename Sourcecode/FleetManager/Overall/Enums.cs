using System;
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
