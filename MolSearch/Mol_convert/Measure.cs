using System.Collections;

namespace MolMesure;

// Provides functionality to compute similarity between molecular fingerprints.
public class SimilarityCalculator
{
    // Calculates the Tanimoto coefficient (Jaccard similarity coefficient) between two fingerprints.
    // Returns The computed Tanimoto coefficient.
    public decimal Tanimoto(BitArray fingerprint1, BitArray fingerprint2)
    {
        // Ensure fingerprints are of the same length
        if (fingerprint1.Length != fingerprint2.Length)
        {
            throw new ArgumentException("Fingerprints must be the same length");
        }

        int intersection = 0; // Count of bits set in both fingerprints
        int union = 0; // Count of bits set in either fingerprint

        // Loop through all bits in the fingerprints
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

        // Avoid division by zero if no bits are set in either fingerprint
        if (union == 0)
        {
            return 0;
        }
        else
        {
            // Compute the Tanimoto coefficient
            return (decimal)intersection / union;
        }
    }
}