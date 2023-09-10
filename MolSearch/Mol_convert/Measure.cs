using System;
using System.Collections;

namespace MolMesure;

public class SimilarityCalculator
{
    public decimal Tanimoto(BitArray fingerprint1, BitArray fingerprint2)
    {
        if (fingerprint1.Length != fingerprint2.Length)
        {
            throw new ArgumentException("Fingerprints must be the same length");
        }

        int intersection = 0;
        int union = 0;

        for (int i = 0; i < fingerprint1.Length; i++)
        {
            if (fingerprint1.Get(i) && fingerprint2.Get(i))
            {
                intersection++;
            }
            if (fingerprint1.Get(i) || fingerprint2.Get(i))
            {
                union++;
            }
        }

        if (union == 0)
        {
            return 0;
        }
        else
        {
            return (decimal)intersection / union;
        }
    }
}