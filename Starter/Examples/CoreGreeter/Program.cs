using System;

namespace CoreGreeter
{
    class Program
    {
        //Notice, that variable values are overlayed in code, so you can see them easly
        public static string Greet()
        {
            var time = DateTime.Now;
            var who = System.Environment.GetEnvironmentVariable("USERNAME").Replace(".", " ");

            var when = (time.Hour >= 12 && time.Hour < 18) ? "Afternoon" : 
                       (time.Hour >= 18 && time.Hour < 22) ? "Evening" :
                       (time.Hour >= 22 && time.Hour < 6 ) ? "Night" : 
                       "Morning";

            var greeting = String.Format("Good {0} {1}!", when, who);

            return greeting;
        }
    }
}