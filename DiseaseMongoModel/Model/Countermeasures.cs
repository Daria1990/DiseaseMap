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
        /// <summary>
        /// Закрыть детские сады
        /// </summary>
        public bool CloseKinderGartens { get; set; }

        /// <summary>
        /// Закрыть школы
        /// </summary>
        public bool CloseSchools { get; set; }

        /// <summary>
        /// Перевести всех на удаленную работу
        /// </summary>
        public bool RemoteWork { get; set; }

        /// <summary>
        /// Остановить работу транспорта
        /// </summary>
        public bool StopTransport { get; set; }

        /// <summary>
        /// Строгость мер в процентах
        /// </summary>
        public double Severity { get; set; }
    }
}
