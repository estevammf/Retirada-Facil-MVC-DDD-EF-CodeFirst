using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace APP.Store.Mvc.Helper
{
    public static class TimeZoneHelper
    {
        private static DateTime _currentDate;
        private static ReadOnlyCollection<TimeZoneInfo> timezones = TimeZoneInfo.GetSystemTimeZones();
        private static List<SelectListItem> returnList = timezones.Select(x => new SelectListItem
        {
            Text = x.DisplayName,
            Value = x.Id
        }).ToList();

        /// <summary>
        /// Retorna a Data atual Brasil
        /// </summary>
        /// <returns></returns>
        public static DateTime DataAtualBrasil()
        {
            _currentDate = DateTime.UtcNow;
            return _currentDate = TimeZoneInfo.ConvertTimeFromUtc(_currentDate, TimeZoneInfo.FindSystemTimeZoneById(returnList[23].Value));
        }

        public static DateTime DataAtualBrasil(DateTime data)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            _currentDate = TimeZoneInfo.ConvertTimeFromUtc(data.ToUniversalTime(), tz);
            return _currentDate;
        }
    }
}
