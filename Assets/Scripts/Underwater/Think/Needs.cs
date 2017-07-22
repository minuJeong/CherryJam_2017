using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Underwater.Think
{
    public class Needs
    {
        private Random m_Fuzzy = new Random();
        public readonly List<Need> NeedsContainer = new List<Need>();

        public Needs()
        {
            // Initialize satisfaction
            foreach (Need need in Need.Enumerate())
            {
                NeedsContainer.Add(need);
            }
        }

        public Need GetNextNeedFuzzy()
        {
            double needsPriorityCap = NeedsContainer
                .ConvertAll((need) => 1.0 - need.Satisfaction)
                .ConvertAll((urgency) => urgency * urgency)
                .Sum();

            double selectedFuz = m_Fuzzy.NextDouble() * needsPriorityCap;

            var e = NeedsContainer.GetEnumerator();
            while (e.MoveNext())
            {
                selectedFuz -= e.Current.Satisfaction;
                if (selectedFuz <= 0)
                {
                    return e.Current;
                }
            }

            // should not happen
            throw new Exception("Fuzziness should be handled winthin priorty map capacity");
        }
    }
}
