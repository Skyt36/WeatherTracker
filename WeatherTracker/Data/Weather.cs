namespace WeatherTracker.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Weather")]
    public partial class Weather
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_weather { get; set; }

        public int id_city { get; set; }

        public DateTime last_update { get; set; }

        public double? temp_c { get; set; }

        public int? humidity { get; set; }

        public double? pressure_in { get; set; }

        public double? wind_mph { get; set; }

        public virtual City City { get; set; }
    }
}
