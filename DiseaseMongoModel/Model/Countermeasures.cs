using System;
using System.Collections.Generic;
using System.Text;

namespace DiseaseMongoModel
{
    /// <summary>
    /// Класс принимаемых мер по сдержанию вируса
    /// </summary>
    public class Countermeasures
    {
        public bool CloseKinderGartens { get; set; }

        public bool CloseSchools { get; set; }

        public bool RemoteWork { get; set; }

        public bool StopTransport { get; set; }

        /// <summary>
        /// Строгость мер в процентах
        /// </summary>
        public double Severity { get; set; }
    }
}
