using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class ParkingSpot
{
    private int id;
    private bool occupied;
    private string type;
    private double price;
    protected List<ParkingInterval> parkingIntervals;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public bool Occupied
    {
        get { return occupied; }
        set { occupied = value; }
    }

    public string Type
    {
        get { return type; }
        set { type = value; }
        
    }

    public double Price
    {
        get { return price; }
        set
        {
            if (value > 0)
            { price = value; }
            else
            { throw new ArgumentException("Parking price cannot be less or equal to 0!"); }
        }
    }

    public ParkingSpot(int id, bool occupied, string type, double price)
    {
        this.Id = id;
        this.Occupied = occupied;
        this.Type = type;
        this.Price = price;
        this.parkingIntervals = new List<ParkingInterval>();
    }

    public virtual bool ParkVehicle(string registrationPlate, int hoursParked, string type)
    {
        if (!Occupied && Type == type)
        {
            ParkingInterval intervals = new ParkingInterval(this, registrationPlate, hoursParked);
            parkingIntervals.Add(intervals);
            Occupied = true;
            return true;
        }
        return false;
    }

    public List<ParkingInterval> GetAllParkingIntervalsByRegistrationPlate(string registrationPlate)
    { return parkingIntervals.Where(p => p.RegistrationPlate == registrationPlate).ToList(); }

    public virtual double CalculateTotal()
    {
        return parkingIntervals.Sum(pi => pi.Revenue);
    }

    public override string ToString()
    {
        return $"> Parking Spot #{Id}\n> Occupied: {Occupied}\n> Type: {Type}\n> Price per hour: {Price:f2} BGN";
    }

}
