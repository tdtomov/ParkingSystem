using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

internal class ParkingController
{
    private List<ParkingSpot> parkingSpots;

    public ParkingController()
    {
        parkingSpots = new List<ParkingSpot>();
    }

    public string CreateParkingSpot(List<string> args)
    {
        int id = int.Parse(args[0]);
        bool occupied = bool.Parse(args[1]);
        string type = args[2];
        double price = double.Parse(args[3]);
        if (parkingSpots.Any(ps => ps.Id == id))
        { return $"Parking spot {id} is already registered!"; }
        ParkingSpot parkingSpot;
        if (type == "car")
        { parkingSpot = new CarParkingSpot(id, occupied, price); }
        else if (type == "bus")
        { parkingSpot = new BusParkingSpot(id, occupied, price); }
        else if (type == "subscription")
        {
            string registrationPlate = args[4];
            parkingSpot = new SubscriptionParkingSpot(id, occupied, price, registrationPlate);
        }
        else
        { throw new ArgumentException("Unable to create parking spot!"); }
        parkingSpots.Add(parkingSpot);
        return $"Parking spot {parkingSpot.Id} was successfully registered in the system!";
    }

    public string ParkVehicle(List<string> args)
    {
        int parkingSpotId = int.Parse(args[0]);
        string registrationPlate = args[1];
        int hoursParked = int.Parse(args[2]);
        string type = args[3];
        ParkingSpot spot = parkingSpots.FirstOrDefault(s => s.Id == parkingSpotId);
        if (spot == null)
        { return $"Parking spot {parkingSpotId} not found!"; }
        bool parked = spot.ParkVehicle(registrationPlate, hoursParked, type);
        if (parked)
        { return $"Vehicle {registrationPlate} parked at {parkingSpotId} for {hoursParked} hours."; }
        else
        { return $"Vehicle {registrationPlate} can't park at {parkingSpotId}."; }
    }

    public string FreeParkingSpot(List<string> args)
    {
        int parkingSpotId = int.Parse(args[0]);
        ParkingSpot spot = parkingSpots.FirstOrDefault(s => s.Id == parkingSpotId);
        if (spot == null)
        { return $"Parking spot {parkingSpotId} not found!"; }
        if (!spot.Occupied)
        { return $"Parking spot {parkingSpotId} is not occupied."; }
        spot.Occupied = false;
        return $"Parking spot {parkingSpotId} is now free!";
    }

    public string GetParkingSpotById(List<string> args)
    {
        int parkingSpotId = int.Parse(args[0]);
        ParkingSpot spot = parkingSpots.FirstOrDefault(s => s.Id == parkingSpotId);
        if (spot == null)
        { return $"Parking spot {parkingSpotId} not found!"; }
        return spot.ToString();
    }

    public string GetParkingIntervalsByParkingSpotIdAndRegistrationPlate(List<string> args)
    {
        int parkingSpotId = int.Parse(args[0]);
        string registrationPlate = args[1];
        ParkingSpot spot = parkingSpots.FirstOrDefault(s => s.Id == parkingSpotId);
        if (spot == null)
        { return $"Parking spot {parkingSpotId} not found!"; }
        List<ParkingInterval> intervals = spot.GetAllParkingIntervalsByRegistrationPlate(registrationPlate);
        string result = $"Parking intervals for parking spot {parkingSpotId}:\n";
        foreach (ParkingInterval interval in intervals.Where(i => i.ParkingSpot.Id == parkingSpotId))
        { result += interval.ToString(); }
        return result;
    }

    public string CalculateTotal(List<string> args)
    {
        double total = parkingSpots.Sum(s => s.CalculateTotal());
        return $"Total revenue from the parking: {total:F2} BGN";
    }

}

