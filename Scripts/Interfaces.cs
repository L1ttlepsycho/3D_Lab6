using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IPlayerAction 
    {
        void gameStart();
        void shoot(GameObject camera);
        int getScore();
    }
    public interface ISceneController 
    {
        void loadResources();
    }
}
