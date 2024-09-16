using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public static class SalesRange
    {
        public static string GetSalesCategory(float sales)
        {
            if (sales < 1000) return "LessThan1k";
            if (sales >= 1000 && sales < 2000) return "1kTo2k";
            if (sales >= 2000 && sales < 3000) return "2kTo3k";
            if (sales >= 3000 && sales < 4000) return "3kTo4k";
            if (sales >= 4000 && sales < 5000) return "4kTo5k";
            if (sales >= 5000 && sales < 6000) return "5kTo6k";
            if (sales >= 6000 && sales < 7000) return "6kTo7k";
            if (sales >= 7000 && sales < 8000) return "7kTo8k";
            if (sales >= 8000 && sales < 9000) return "8kTo9k";
            if (sales >= 9000 && sales < 10000) return "9kTo10k";
            if (sales >= 10000 && sales < 11000) return "10kTo11k";
            if (sales >= 11000 && sales < 12000) return "11kTo12k";
            if (sales >= 12000 && sales < 13000) return "12kTo13k";
            if (sales >= 13000 && sales < 14000) return "13kTo14k";
            if (sales >= 14000 && sales < 15000) return "14kTo15k";
            if (sales >= 15000 && sales < 16000) return "15kTo16k";
            if (sales >= 16000 && sales < 17000) return "16kTo17k";
            if (sales >= 17000 && sales < 18000) return "17kTo18k";
            if (sales >= 18000 && sales < 19000) return "18kTo19k";
            if (sales >= 19000 && sales < 20000) return "19kTo20k";
            if (sales >= 20000 && sales < 21000) return "20kTo21k";
            if (sales >= 21000 && sales < 22000) return "21kTo22k";
            if (sales >= 22000 && sales < 23000) return "22kTo23k";
            if (sales >= 23000 && sales < 24000) return "23kTo24k";
            if (sales >= 24000 && sales < 25000) return "24kTo25k";
            if (sales >= 25000 && sales < 26000) return "25kTo26k";
            if (sales >= 26000 && sales < 27000) return "26kTo27k";
            if (sales >= 27000 && sales < 28000) return "27kTo28k";
            if (sales >= 28000 && sales < 29000) return "28kTo29k";
            if (sales >= 29000 && sales < 30000) return "29kTo30k";
            return "GreaterThan30k";
        }
    }
}
