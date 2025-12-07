using System;
using System.Collections.Generic;
using System.Text;

public class ParkingInterval
{
    private ParkingSpot parkingSpot;
    private string registrationPlate;
    private int hoursParked;

    public ParkingSpot ParkingSpot
    {
        get { return parkingSpot; }
        set { parkingSpot = value; }
    }
    public string RegistrationPlate
    {
        get { return registrationPlate; }
        set 
        {
            if (!string.IsNullOrEmpty(value))
            { registrationPlate = value; }
            else
            { throw new ArgumentException("Registration plate can’t be null or empty!"); }
        }
    }
    public int HoursParked  
    {
        get { return hoursParked; }
        set 
        {
            if (value <= 0)
            { throw new ArgumentException("Hours parked can’t be zero or negative!"); }
            hoursParked = value;
        }
    }
    public double Revenue
    {
        get 
        {
            if (parkingSpot.Type == "subscription")
            { return 0; }
            else
            { return parkingSpot.Price * hoursParked; }
        }
    }

    public ParkingInterval(ParkingSpot parkingSpot, string registrationPlate, int hoursParked)
    {
        this.ParkingSpot = parkingSpot;
        this.RegistrationPlate = registrationPlate;
        this.HoursParked = hoursParked;
    }

    public override string ToString()
    {
        return $"> Parking Spot #{ParkingSpot.Id}\n> RegistrationPlate: {RegistrationPlate}\n> HoursParked: {HoursParked}\n> Revenue: {Revenue:f2} BGN\n";
    }
}