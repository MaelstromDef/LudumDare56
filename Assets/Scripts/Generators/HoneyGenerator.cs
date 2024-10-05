using UnityEngine;

public class HoneyGenerator : MonoBehaviour, IGenerator
{
    // Generation
    [Header("Generation Parameters")]
    [SerializeField] float honeyGenerationTime = 1.0f;
    [SerializeField] int honeyGenerationYield = 1;
    [SerializeField] int requiredNectar = 1;
    float stopwatch = 0;

    bool generate = false;

    Hive hive;

    // Yield
    int honey = 0;

    // Debugging
    [SerializeField] bool debugging = false;

    #region Unity

    private void Update()
    {
        if (generate)
        {
            stopwatch += Time.deltaTime;

            if (stopwatch >= honeyGenerationTime) Generate();
        }
    }

    #endregion

    #region HoneyGenerator

    public Hive GetHive()
    {
        return hive;
    }

    public void SetHive(Hive hive)
    {
        this.hive = hive;
    }

    #endregion

    #region IGenerator

    public int ClaimFullYield()
    {
        if (debugging) Debug.Log("HoneyGenerator::ClaimFullYield\nYield:\t" + honey);

        int yield = honey;
        honey = 0;

        return yield;
    }

    public int ClaimYield(int amount)
    {
        if (amount > honey) amount = honey;     // Input validation

        honey -= amount;
        return amount;
    }

    public int GetYield()
    {
        return honey;
    }

    public void Generate()
    {
        // Yield does not match requirement
        if(hive.GetNectar() < requiredNectar)
        {
            Debug.Log("HoneyGenerator::Generate\nHive was out of nectar. Honey not generated, turning off generation.");
            SetGeneration(false);
            return;
        }

        // Generate honey
        hive.ClaimNectar(requiredNectar);
        stopwatch -= honeyGenerationTime;
        honey += honeyGenerationYield;

        // Check if generation should continue.
        if (hive.GetNectar() < requiredNectar) SetGeneration(false);

        if (debugging) Debug.Log("HoneyGenerator::Generate\nHoney:\t" + honey);
    }

    public float GetGenerationTime()
    {
        return honeyGenerationTime;
    }

    public void SetGenerationTime(float generationTime)
    {
        honeyGenerationTime = generationTime;
    }

    public void ToggleGeneration()
    {
        stopwatch = 0;
        generate = !generate;
    }

    public void SetGeneration(bool shouldGenerate)
    {
        stopwatch = 0;
        generate = shouldGenerate;
    }

    #endregion
}
