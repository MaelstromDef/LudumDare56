using UnityEngine;

/// <summary>
/// Interface that represents a generator of an item over a constant time.
/// </summary>
public interface IGenerator
{
    void Generate();                                // Yield an amount of the generated item.
    float GetGenerationTime();                      // Get the amount of time it takes to generate.
    void SetGenerationTime(float generationTime);   // Set the amount of time it takes to generate.
    void ToggleGeneration();                        // Toggle the generation on and off.
    void SetGeneration(bool shouldGenerate);        // Set the generation on or off.

    int ClaimYield(int amount);                     // Claims a certain amount of generated item.
    int ClaimFullYield();                           // Claims all of the generated item.
    int GetYield();                                 // Retrieves yield count without claiming it.
}
