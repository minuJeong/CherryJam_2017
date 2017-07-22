using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Underwater
{
    public enum NeedType
    {
        Hunger,
        Sleep,
        Fun,
    }

    public class Needs
    {
        private Random m_Fuzzy = new Random();
        private readonly Dictionary<NeedType, double> NeedSatisfaction = new Dictionary<NeedType, double>();

        public Needs()
        {
            // Initialize satisfaction
            foreach (NeedType need in Enum.GetValues(typeof(NeedType)))
            {
                NeedSatisfaction.Add(need, 0.5);
            }
        }

        public void SatisfyNeed(NeedType need, double amount)
        {
            if (NeedSatisfaction.ContainsKey(need))
            {
                NeedSatisfaction[need] += amount;
            }
        }

        public void ConsumeNeed(NeedType need, double amount)
        {
            if (NeedSatisfaction.ContainsKey(need))
            {
                NeedSatisfaction[need] -= amount;
            }
        }

        public NeedType GetNextNeedFuzzy()
        {
            double needsPriorityCap = NeedSatisfaction
                .Values
                .ToList()
                .ConvertAll((satisfaction) => 1.0 - satisfaction)
                .ConvertAll((needy) => needy * needy)
                .Sum();

            double selectedFuz = m_Fuzzy.NextDouble() * needsPriorityCap;

            var e = NeedSatisfaction.GetEnumerator();
            while (e.MoveNext())
            {
                selectedFuz -= e.Current.Value;
                if (selectedFuz <= 0)
                {
                    return e.Current.Key;
                }
            }

            // should not happen
            throw new Exception("Fuzziness should be handled winthin priorty map capacity");
        }
    }
}
