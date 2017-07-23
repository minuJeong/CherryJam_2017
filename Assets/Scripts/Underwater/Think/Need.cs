using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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

        public override string ToString()
        {
            return string.Format(
                "Need: {0}, Sat: {1}",
                DisplayName,
                Satisfaction
            );
        }

        private Need()
        {
            Satisfaction = 0.5;
        }

        public string DisplayName;

        private double _satisfaction;
        public double Satisfaction
        {
            get { return _satisfaction; }
            set
            {
                _satisfaction = value;
                _satisfaction = Mathf.Min((float)_satisfaction, 1.0F);
                _satisfaction = Mathf.Max((float)_satisfaction, 0.0F);
            }
        }

        public int Scale = 100;

        public void Satisfy(double amount)
        {
            Satisfaction += amount;
        }

        public void Satisfy(int score)
        {
            Satisfy((double)(score / Scale));
        }

        public void Consume(double amount)
        {
            Satisfaction -= amount;
        }

        public void Consume(int score)
        {
            Consume((double)(score / Scale));
        }
    }
}
