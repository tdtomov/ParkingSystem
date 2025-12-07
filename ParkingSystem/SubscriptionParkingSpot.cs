using System;
using System.Collections.Generic;
using System.Text;

internal class SubscriptionParkingSpot : ParkingSpot
{
    private string registrationPlate;
    public string RegistrationPlate
    {
        get { return registrationPlate; }
        
        set
        {
            if (string.IsNullOrEmpty(registrationPlate) == false)
            { registrationPlate = value; }
            else
            { throw new ArgumentException("Registration plate can’t be null or empty!"); }
        }
    }

    public SubscriptionParkingSpot(int id, bool occupied, double price, string registrationPlate) : base(id, occupied, "subscription", price)
    {
        this.Id = id;
        this.Occupied = occupied;
        this.Price = price;
        this.RegistrationPlate = registrationPlate;
    }
}

