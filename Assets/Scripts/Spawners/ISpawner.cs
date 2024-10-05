using UnityEngine;

public interface ISpawner
{
    public void Spawn();
    public void Spawn(int count);

    public void Kill();
    public void Kill(int count);
    public void KillAll();
}
