using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiseaseMapMVC.Models.Extensions;

namespace DiseaseMapMVC.Models.Disease
{
    using DiseaseMongoModel;

    public abstract class DiseaseCitySpread
    {
        protected Disease Disease;
        protected List<DiseaseStatistic> DiseaseStatistics;
        protected int CityPeopleNumber;

        public DiseaseCitySpread(Disease disease, List<DiseaseStatistic> diseaseStatistics, int cityPeopleNumber)
        {
            Disease = disease;
            DiseaseStatistics = diseaseStatistics;
            CityPeopleNumber = cityPeopleNumber;
        }

        /// <summary>
        /// Метод возвращает количество умерших от болезни за последний день
        /// </summary>
        /// <returns>Количество умерших от болезни</returns>
        public int CalculateDead()
        {
            var deadToday = GetDeadToday();

            var deadBefore = GetDeadBeforeToday();

            return deadToday + deadBefore;
        }

        protected virtual int GetDeadToday()
        {
            var durationStartSick = GetSickInDurationDay();

            return (int)(durationStartSick * Disease.DeathIndex);
        }

        protected virtual int GetDeadBeforeToday()
        {
            var yesterdayStatistics = DiseaseStatistics.LastOrDefault();

            return yesterdayStatistics != null ? yesterdayStatistics.Dead : 0;
        }

        /// <summary>
        /// Метод рассчитывает коэффициент распространения
        /// </summary>
        /// <returns>коэффициент распространения</returns>
        protected virtual double CalculateRe()
        {
            var random = new Random();
            var R0 = random.NextDouble(Disease.R0Min, Disease.R0Max);

            var yesterdayStatistics = DiseaseStatistics.LastOrDefault();

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

                return (1 - (double)notBecomeSick / CityPeopleNumber) * R0;
            }
        }

        /// <summary>
        /// Метод возвращает количество заболевших за последний день
        /// </summary>
        /// <param name="peopleInRiskProcent">процент людей, которые находятся в зоне риска заболеть инфекцией</param>
        /// <returns>Количество заболевших</returns>
        public int CalculateSick(double peopleInRiskProcent)
        {
            var sickBeforeToday = GetSickBeforeToday();

            var sickToday = GetSickToday(sickBeforeToday, peopleInRiskProcent);
            
            return sickBeforeToday + sickToday;
        }

        protected virtual int GetSickToday(int sickBeforeToday, double peopleInRiskProcent)
        {
            var getSickToday = (int)(sickBeforeToday * CalculateRe() * peopleInRiskProcent);

            var canBeSick = GetCanBecomeSick();

            return getSickToday > canBeSick || getSickToday < 0 ? canBeSick : getSickToday;
        }

        protected virtual int GetSickBeforeToday()
        {
            var yesterdayStatistics = DiseaseStatistics.LastOrDefault();

            if (yesterdayStatistics == null)
            {
                return 0;
            }

            var sickBeforeToday = yesterdayStatistics.Sick - GetSickInDurationDay();

            return sickBeforeToday > 0 ? sickBeforeToday : 0;
        }

        /// <summary>
        /// Метод возвращает количество выздоровевших за последний день
        /// </summary>
        /// <returns>Количество выздоровевших</returns>
        public int CalculateRecovered()
        {
            var recoveredToday = GetRecoveredToday();

            var recoveredBeforeToday = GetRecoveredBeforeToday();

            var recovered = recoveredToday + recoveredBeforeToday;
            
            return CheckForOverload(recovered);
        }

        protected virtual int GetRecoveredToday()
        {
            var durationStartSick = GetSickInDurationDay();

            return (int)(durationStartSick * (1 - Disease.DeathIndex));
        }

        protected virtual int GetRecoveredBeforeToday()
        {
            var yesterdayStatistics = DiseaseStatistics.LastOrDefault();

            return yesterdayStatistics != null ? yesterdayStatistics.Recovered : 0;
        }

        protected int CheckForOverload(int checking)
        {
            return checking > CityPeopleNumber ? CityPeopleNumber : checking;
        }

        /// <summary>
        /// Метод возвращает количество вакцинированных людей
        /// </summary>
        /// <param name="vaccinationCompany">компания по вакцинации</param>
        /// <returns>Количество вакцинированных</returns>
        public int CalculateInjured(IVaccinationCompany vaccinationCompany)
        {
            var vaccinatedToday = GetInjuredToday(vaccinationCompany);

            var vaccinatedBeforeToday = GetInjuredBeforeToday();

            return vaccinatedBeforeToday + vaccinatedToday;
        }

        protected virtual int GetInjuredToday(IVaccinationCompany vaccinationCompany)
        {
            var peopleNeedVaccination = GetCanBecomeSick();

            return vaccinationCompany.GetTodaysVacciantedNumber(peopleNeedVaccination);
        }

        protected virtual int GetInjuredBeforeToday()
        {
            var yesterdayStatistics = DiseaseStatistics.LastOrDefault();

            return yesterdayStatistics != null ? yesterdayStatistics.Injured : 0;
        }

        protected int GetSickInDurationDay()
        {
            if (Disease.Duration == 0 || DiseaseStatistics.Count() < Disease.Duration)
            {
                return 0;
            }

            var durationStartDay = DiseaseStatistics[DiseaseStatistics.Count() - Disease.Duration];

            if (DiseaseStatistics.Count() < Disease.Duration + 1)
            {
                return durationStartDay.Sick;
            }
            else
            {
                var durationBeforeStartDay = DiseaseStatistics[DiseaseStatistics.Count() - Disease.Duration - 1];

                var sickInDurationDay = durationStartDay.Sick - durationBeforeStartDay.Sick;

                return sickInDurationDay > 0 ? sickInDurationDay : 0;
            }
        }

        protected int GetCanBecomeSick()
        {
            var yesterdayStatistic = DiseaseStatistics.LastOrDefault();

            if (yesterdayStatistic == null)
            {
                return CityPeopleNumber;
            }

            var canBeSick = CityPeopleNumber - yesterdayStatistic.Dead - yesterdayStatistic.Injured - yesterdayStatistic.Sick;

            if (Disease.AcquireImmunity)
            {
                canBeSick -= yesterdayStatistic.Recovered;
            }

            return canBeSick > 0 ? canBeSick : 0;
        }
    }
}
