using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Underwater.Think
{
    public sealed class Need
    {
        public static IEnumerable<Need> Enumerate()
        {
            // TODO: Load from database, after implement database
            List<Need> needs = new List<Need>()
            {
                new Need() {
                    DisplayName="Hunger",
                },

                new Need() {
                    DisplayName="Sleep",
                },

                new Need() {
                    DisplayName ="Fun",
                },

                new Need() {
                    DisplayName ="Social",
                },

                new Need() {
                    DisplayName ="Learn",
                },
            };

            foreach (Need need in needs)
            {
                yield return need;
            }
        }

        private Guid ID;
        private Need()
        {
            ID = Guid.NewGuid();
        }

        public string DisplayName;
    }
}
