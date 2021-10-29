using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiseaseMapMVC.Models.Extensions;

namespace DiseaseMapMVC.Models.Disease
{
    using DiseaseMongoModel;

    /// <summary>
    /// Распространение заболевания в городе
    /// </summary>
    public abstract class DiseaseCitySpread
    {
        /// <summary>
        /// Заболевание, для которого высчитывается статистика распространения
        /// </summary>
        protected Disease Disease;

        /// <summary>
        /// Статистика распространения с динамикой по дням
        /// </summary>
        protected List<DiseaseStatistic> SpreadStatistics;
        
        /// <summary>
        /// Количество людей в городе
        /// </summary>
        protected int CityPopulation;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="disease">Заболевание</param>
        /// <param name="diseaseStatistics">Статистика заболевания</param>
        /// <param name="cityPopulation">количество людей в городе</param>
        public DiseaseCitySpread(Disease disease, List<DiseaseStatistic> diseaseStatistics, int cityPopulation)
        {
            Disease = disease ?? throw new ArgumentNullException(nameof(disease));
            SpreadStatistics = diseaseStatistics ?? throw new ArgumentNullException(nameof(diseaseStatistics));
            CityPopulation = cityPopulation;
        }

        /// <summary>
        /// Метод рассчитывает количество умерших от болезни за последний день
        /// </summary>
        /// <returns>Количество умерших от болезни</returns>
        public int CalculateDead()
        {
            var deadToday = GetDeadToday();

            var deadBefore = GetDeadBeforeToday();

            return deadToday + deadBefore;
        }

        /// <summary>
        /// Метод рассчитывает количество умерших за сегодняшний день
        /// </summary>
        /// <returns>число умерших</returns>
        protected virtual int GetDeadToday()
        {
            var durationStartSick = GetSickDurationDayEarlier();

            return (int)(durationStartSick * Disease.DeathIndex);
        }

        /// <summary>
        /// Метод рассчитывает количество умерший до сегодняшнего дня
        /// </summary>
        /// <returns>число умерших</returns>
        protected virtual int GetDeadBeforeToday()
        {
            var yesterdayStatistics = SpreadStatistics.LastOrDefault();

            return yesterdayStatistics != null ? yesterdayStatistics.Dead : 0;
        }

        /// <summary>
        /// Метод рассчитывает коэффициент распространения заболевания
        /// </summary>
        /// <returns>коэффициент распространения</returns>
        protected virtual double CalculateRe()
        {
            var random = new Random();
            var R0 = random.NextDouble(Disease.R0Min, Disease.R0Max);

            var yesterdayStatistics = SpreadStatistics.LastOrDefault();

            if (yesterdayStatistics == null)
            {
                return R0;
            }
            else
            {
                var notBecomeSick = yesterdayStatistics.Injured;

                if (Disease.AcquireImmunity)
                {
                    notBecomeSick += yesterdayStatistics.Recovered;
                }

                return (1 - (double)notBecomeSick / CityPopulation) * R0;
            }
        }

        /// <summary>
        /// Метод рассчитывает количество заболевших за последний день
        /// </summary>
        /// <param name="peopleInRiskProcent">процент людей, которые находятся в зоне риска заболеть инфекцией</param>
        /// <returns>Количество заболевших</returns>
        public int CalculateSick(double peopleInRiskProcent)
        {
            var sickBeforeToday = GetSickBeforeToday();

            var sickToday = GetSickToday(sickBeforeToday, peopleInRiskProcent);
            
            return sickBeforeToday + sickToday;
        }

        /// <summary>
        /// Метод рассчитывает количество заболевших за сегодняшний день
        /// </summary>
        /// <param name="sickBeforeToday">заболели до сегодняшнего дня</param>
        /// <param name="peopleInRiskProcent">люди в зоне риска</param>
        /// <returns>число заболевших</returns>
        protected virtual int GetSickToday(int sickBeforeToday, double peopleInRiskProcent)
        {
            var getSickToday = (int)(sickBeforeToday * CalculateRe() * peopleInRiskProcent);

            var canBeSick = GetCanBecomeSick();

            return getSickToday > canBeSick || getSickToday < 0 ? canBeSick : getSickToday;
        }

        /// <summary>
        /// Метод рассчитывает количество заболевших до сегодняшнего дня
        /// </summary>
        /// <returns>число заболевших</returns>
        protected virtual int GetSickBeforeToday()
        {
            var yesterdayStatistics = SpreadStatistics.LastOrDefault();

            if (yesterdayStatistics == null)
            {
                return 0;
            }

            var sickBeforeToday = yesterdayStatistics.Sick - GetSickDurationDayEarlier();

            return sickBeforeToday > 0 ? sickBeforeToday : 0;
        }

        /// <summary>
        /// Метод рассчитывает количество выздоровевших за последний день
        /// </summary>
        /// <returns>Количество выздоровевших</returns>
        public int CalculateRecovered()
        {
            var recoveredToday = GetRecoveredToday();

            var recoveredBeforeToday = GetRecoveredBeforeToday();

            var recovered = recoveredToday + recoveredBeforeToday;
            
            return recovered > CityPopulation ? CityPopulation : recovered;
        }

        /// <summary>
        /// Метод рассчитывает количество выздоровевших за сегодняшний день
        /// </summary>
        /// <returns>число выздоровевших</returns>
        protected virtual int GetRecoveredToday()
        {
            var durationStartSick = GetSickDurationDayEarlier();

            return (int)(durationStartSick * (1 - Disease.DeathIndex));
        }

        /// <summary>
        /// Метод рассчитывает число выздоровевших до сегодняшнего дня
        /// </summary>
        /// <returns>число выздоровевших</returns>
        protected virtual int GetRecoveredBeforeToday()
        {
            var yesterdayStatistics = SpreadStatistics.LastOrDefault();

            return yesterdayStatistics != null ? yesterdayStatistics.Recovered : 0;
        }

        /// <summary>
        /// Метод рассчитывает количество вакцинированных людей
        /// </summary>
        /// <param name="vaccinationCompany">компания по вакцинации</param>
        /// <param name="vaccinesDoneDayily">число вакцинированных за день</param>
        /// <returns>Количество вакцинированных</returns>
        public int CalculateInjured(IVaccinationCompany vaccinationCompany, int vaccinesDoneDayily)
        {
            var vaccinatedToday = GetInjuredToday(vaccinationCompany, vaccinesDoneDayily);

            var vaccinatedBeforeToday = GetInjuredBeforeToday();

            return vaccinatedBeforeToday + vaccinatedToday;
        }

        /// <summary>
        /// Метод рассчитывает число вакцинированных за сегодня
        /// </summary>
        /// <param name="vaccinationCompany">компания по вакцинации</param>
        /// <param name="vaccinesDoneDayily">число вакцинированных за день</param>
        /// <returns>число вакцинированных</returns>
        protected virtual int GetInjuredToday(IVaccinationCompany vaccinationCompany, int vaccinesDoneDayily)
        {
            var peopleNeedVaccination = GetCanBecomeSick();

            return vaccinationCompany.GetTodaysVaccianted(vaccinesDoneDayily, peopleNeedVaccination);
        }

        /// <summary>
        /// Метод рассчитывает количество вакцинированных до сегодняшнего дня
        /// </summary>
        /// <returns>число вакцинированных</returns>
        protected virtual int GetInjuredBeforeToday()
        {
            var yesterdayStatistics = SpreadStatistics.LastOrDefault();

            return yesterdayStatistics != null ? yesterdayStatistics.Injured : 0;
        }

        /// <summary>
        /// Метод рассчитывает количество людей, которые заболели на N дней ранее. 
        /// Где N - это продолжительность протекания болезни у одного человека
        /// </summary>
        /// <returns>количество заболевших</returns>
        protected int GetSickDurationDayEarlier()
        {
            if (Disease.Duration == 0 || SpreadStatistics.Count() < Disease.Duration)
            {
                return 0;
            }

            var durationStartDay = SpreadStatistics[SpreadStatistics.Count() - Disease.Duration];

            if (SpreadStatistics.Count() < Disease.Duration + 1)
            {
                return durationStartDay.Sick;
            }
            else
            {
                var durationBeforeStartDay = SpreadStatistics[SpreadStatistics.Count() - Disease.Duration - 1];

                var sickInDurationDay = durationStartDay.Sick - durationBeforeStartDay.Sick;

                return sickInDurationDay > 0 ? sickInDurationDay : 0;
            }
        }

        /// <summary>
        /// Метод рассчитывает количество людей, которые рискуют заразиться в ближайшее время
        /// </summary>
        /// <returns>количество людей в зоне риска</returns>
        protected int GetCanBecomeSick()
        {
            var yesterdayStatistic = SpreadStatistics.LastOrDefault();

            if (yesterdayStatistic == null)
            {
                return CityPopulation;
            }

            var canBeSick = CityPopulation - yesterdayStatistic.Dead - yesterdayStatistic.Injured - yesterdayStatistic.Sick;

            if (Disease.AcquireImmunity)
            {
                canBeSick -= yesterdayStatistic.Recovered;
            }

            return canBeSick > 0 ? canBeSick : 0;
        }
    }
}
