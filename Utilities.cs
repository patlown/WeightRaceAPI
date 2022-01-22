using System;
using WeightRaceAPI.Models;
using System.Linq;

public static class Utilities
{
    public static double? WeightChangeDuringPeriod(IEnumerable<Weight> weights, int daysPassed)
    {
        // Assuming dates are passed in with descending order already established
        var foundWeight = false;
        var first = weights.First();
        var second = new Weight();
        foreach (var weight in weights)
        {
            if ((first.LogDate - weight.LogDate).TotalDays >= daysPassed)
            {
                second = weight;
                foundWeight = true;
                break;
            }
        };
        if (foundWeight)
        {
            return Math.Round((first.Value - second.Value), 2);
        }

        return null;
    }
}