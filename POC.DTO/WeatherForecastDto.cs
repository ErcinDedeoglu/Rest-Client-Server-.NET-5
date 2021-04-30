using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace POC.DTO
{
    public class WeatherForecastDto
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        [JsonIgnore]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
