using System;

namespace DeMo.Models
{
    public class TinhDiem
    {
        public double Tinh(int? i1, int? i2, int? i3, int? i4, int? i5, int? i6)
        {
            int? t = i1 + i2 + i3 + i4 + (i5 * 2) + (i6 * 3);
            double y = Convert.ToDouble(t) / 9;
            double z = Math.Round(y, 1);
            return z;
        }
    }
}