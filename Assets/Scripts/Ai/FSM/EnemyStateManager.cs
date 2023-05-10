using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : Enemy
{
    EnemyBaseState currentEnemyState;
    EnemyStateFactory _factory;

    public EnemyBaseState CurrentEnemyState { get { return currentEnemyState; } set { currentEnemyState = value; } }
    public EnemyStateFactory Factory { get { return _factory; } }

}
