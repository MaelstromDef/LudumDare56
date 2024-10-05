using UnityEngine;
using UnityEngine.Events;

public class NectarGenerator : MonoBehaviour, IGenerator
{
    // Events
    public UnityEvent<int> nectarGenerated;

    // Generation
    [SerializeField] float nectarGenerationTime = 1.0f;
    [SerializeField] int nectarGenerationYield = 1;
    float stopwatch = 0;
    bool generate = true;

    // Yield
    int nectar = 0;

    // Debugging
    [SerializeField] bool debugging = false;

    #region Unity

    private void Update()
    {
        if (generate)
        {
            stopwatch += Time.deltaTime;

            if (stopwatch >= nectarGenerationTime) Generate();
        }
    }

    #endregion

    #region IGenerator

    public int ClaimFullYield()
    {
        if (debugging) Debug.Log("NectarGenerator::ClaimFullYield\nYield:\t" + nectar);

        int yield = nectar;
        nectar = 0;

        return yield;
    }

    public int ClaimYield(int amount)
    {
        if (amount > nectar) amount = nectar;     // Input validation

        nectar -= amount;
        return amount;
    }

    public void Generate()
    {
        stopwatch -= nectarGenerationTime;
        nectar += nectarGenerationYield;

        nectarGenerated.Invoke(nectar);

        if (debugging) Debug.Log("NectarGenerator::Generate\nnectar:\t" + nectar);
    }

    public float GetGenerationTime()
    {
        return nectarGenerationTime;
    }

    public void SetGenerationTime(float generationTime)
    {
        nectarGenerationTime = generationTime;
    }

    public void ToggleGeneration()
    {
        generate = !generate;
    }

    public void SetGeneration(bool shouldGenerate)
    {
        generate = shouldGenerate;
    }

    public int GetYield()
    {
        return nectar;
    }

    #endregion
}
