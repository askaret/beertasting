using System;
using System.ComponentModel.DataAnnotations;

namespace BouvetØlsmaking.Models
{
    public class Vote
    {
        private double _taste = 0;
        private double _appearance = 0;
        private double _overall = 0;

        public int VoteId { get; set; }
        public int BeerId { get; set; }
        public int TastingId { get; set; }
        public int TasterId { get; set; }
        [Range(0, 10)]
        public double Taste
        {
            get => _taste;
            set
            {
                if (value > 10)
                    _taste = 10;
                else if (value < 0)
                    _taste = 0;
                else
                    _taste = Math.Round(value, 2);
            }
        }
        [Range(0, 10)]
        public double Appearance
        {
            get => _appearance;
            set
            {
                if (value > 10)
                    _appearance = 10;
                else if (value < 0)
                    _appearance = 0;
                else
                    _appearance = Math.Round(value, 2);
            }
        }
        [Range(0, 10)]
        public double Overall
        {
            get => _overall;
            set
            {
                if (value > 10)
                    _overall = 10;
                else if (value < 0)
                    _overall = 0;
                else
                    _overall = Math.Round(value, 2);
            }
        }
        [StringLength(1000)]
        public string Note { get; set; }

        internal bool IsBlank()
        {
            return (Appearance == 0 && Taste == 0 && Overall == 0 && String.IsNullOrEmpty(Note));
        }
    }
}
