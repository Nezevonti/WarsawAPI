using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WarsawAPI;


class Program
{
    static async Task Main()
    {
        WawAPI api = new WawAPI("d9b205e9-1575-452e-9e4e-fddda8dbef1a");

        //Console.Write(api.GetLines(7009, "01"));
        //api.GetLines(7009, "01");
        //api.GetstopIDbyName("Marszałkowska");
        //Console.Write(api.GetstopIDbyName("Przy Agorze"));
        //api.GetLines("6013","03");

        //api.GetLineSchedule("6013", "03", "6");

        api.GetSchedule("6013", "03");
    }
}


