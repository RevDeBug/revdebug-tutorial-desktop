namespace Starter.Examples.CarsEconomy
{
    public static class FuelUsageConverter
    {
        private const double MpgFactor = 282.5;
        
        public static double Mpg2Liters(double mpg)
        {
            if (mpg == 0.0)
            {
                return 0;
            }
            return MpgFactor / mpg;
        }

        public static double Liters2Mpg(double fuelEconomy)
        {
            if (fuelEconomy == 0.0) return 0;
            return MpgFactor / fuelEconomy;
        }


    }
}
