using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoboMines
{
     class StateMachine
    {
        IState currentState;
        

        public void ChangeState(IState newState)
        {
            if (currentState != null)
                currentState.Exit();

            currentState = newState;
            currentState.Enter();
        }

        public void Update()
        {
           
            if (currentState != null) currentState.Execute();
        }
    }
}
