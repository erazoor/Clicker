using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBonus
{
    void SaveBonus();
    void ResetBonus();
    void LoadBonus();
    void UpdateBonus(float deltaTime);
}