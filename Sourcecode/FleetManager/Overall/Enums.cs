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
        Passengercar,
        LightFreight
    }
    public enum ExtraServices
    {
        [Description("Discount on fuel.")]
        DiscountFuel,
        [Description("Discount on carwash.")]
        DiscountCarwash

    }
    public enum AuthenticationTypes
    {
        PIN,
        [Description("PIN + KM.")]
        PINKM
    }
}
